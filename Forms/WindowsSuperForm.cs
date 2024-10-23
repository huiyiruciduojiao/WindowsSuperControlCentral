using CentralControl.Auxiliary;
using CentralControl.Tools;
using System.Diagnostics;
using System.Linq;
using System.Management;
using Vanara.Extensions.Reflection;
using Windows.UI.WebUI;
using WinFormium;
using WinFormium.JavaScript;
using WinRT;
namespace CentralControl.Forms {
    public class WindowsSuperForm : Formium {

        public static Queue<JavaScriptObject> queuq = new Queue<JavaScriptObject>();
        private static ExecutorCommand ExecutorCommand = new ExecutorCommand();
        private static Command Command = null;
        private static byte ResultPacketFrequency = 10;

        /// <summary>
        /// 所有可被检测的磁盘盘符名称
        /// </summary>
        private static List<string> diskNames = GetSystemInfoTools.GetAllDiskDrivesName();
        /// <summary>
        /// 前端正在显示的磁盘名称
        /// </summary>
        private static string? showDiskName = null;
        /// <summary>
        /// 所有可以被计数的网络设备
        /// </summary>
        private static List<string> netWorkDriveNames = GetSystemInfoTools.GetAllNetWorkDriveName();
        /// <summary>
        /// 前端正在显示的网络接口名称
        /// </summary>
        private static string? showNetwokInterfaceName = null;
        /// <summary>
        /// 排序列
        /// </summary>
        private static string? sortcolumn = null;
        /// <summary>
        /// 排序方式 desc or asc
        /// </summary>
        private static string? sortorder = null;

        //private 
        public WindowsSuperForm() {
            this.WindowState = FormiumWindowState.FullScreen;
            EnableSplashScreen = false;
            Url = "http://windows.super.console";
            Loaded += WindowsSuperForm_Loaded;
            //test();
        }

        /// <summary>
        /// 窗体加载完成后触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowsSuperForm_Loaded(object? sender, BrowserEventArgs e) {
            Console.WriteLine(GetSystemInfoTools.GetAllProcess()[15].Handle);
            ProcessTools.GetProcessUserName(21740);
            RegisterMessage();

            ShowDevTools();
            Task.Run(() => {
                while (true) {
                    //发送网络和磁盘数据
                    SendDiskAndNetWorkDrverSelectOptions();
                    //发送任务列表数据
                    SendTaskListData();
                    NetWorkUseInfo netWorkUseInfo = GetSystemInfoTools.GetNetWorkUseInfo(showNetwokInterfaceName);
                    DiskIOInfo? diskIOInfo = GetSystemInfoTools.GetDiskIOInfo(showDiskName);
                    ExecuteJavaScript($"NetDownChart.setvalue({netWorkUseInfo.ReceiveBytes},'{netWorkUseInfo.RUnit}');");
                    ExecuteJavaScript($"NetUpLoadChart.setvalue({netWorkUseInfo.SendBytes},'{netWorkUseInfo.SUnit}');");
                    if (diskIOInfo != null) {
                        ExecuteJavaScript($"DiskInputChart.setvalue({diskIOInfo.WriteBytes},'{diskIOInfo.WUnit}');");
                        ExecuteJavaScript($"DiskOutputChart.setvalue({diskIOInfo.ReadBytes},'{diskIOInfo.RUnit}');");
                    }
                    double cpuUsageRate = GetSystemInfoTools.GetCPUUsageRate();
                    ExecuteJavaScript($"pushData({cpuUsageRate},{60});");
                    Thread.Sleep(1000);

                }
            });

        }
        /// <summary>
        /// 初始化必要内容
        /// </summary>
        private void Initialization() {
            Command = new Command();
            Command.ReStart();
            if (Command != null) {
                Command.Output += Command_Output;
                Command.Error += Command_Error;
                Command.Exited += Command_Exited;
            }
        }
        /// <summary>
        /// 命令执行器退出事件
        /// </summary>
        private void Command_Exited() {
            Command.ReStart();
        }
        /// <summary>
        /// 命令执行错误输出事件
        /// </summary>
        /// <param name="str">错误输出内容</param>
        private void Command_Error(string str) {
            PushCommandResultToQuque(str, false, true);
        }
        /// <summary>
        /// 命令执行输出事件
        /// </summary>
        /// <param name="str">执行结果</param>
        private void Command_Output(string str) {
            PushCommandResultToQuque(str, false, false);
        }
        /// <summary>
        /// 发送磁盘和网络的选项数据和已选择的项目
        /// </summary>
        private void SendDiskAndNetWorkDrverSelectOptions() {

            showDiskName ??= diskNames[0];
            showNetwokInterfaceName ??= netWorkDriveNames[0];

            PostBrowserJavaScriptMessage("setDiskAndNetWorkData", new JavaScriptObject {
                        {"DiskDriveName", ToJavaScriptArray(diskNames)},
                        {"SelectDiskDriveName",showDiskName },
                        {"NetWorkDrivename",ToJavaScriptArray(netWorkDriveNames) },
                        {"SelectNetWorkDriveName",showNetwokInterfaceName }
                    });

        }
        /// <summary>
        /// 发送任务列表数据给前端页面
        /// </summary>
        private void SendTaskListData() {
            sortcolumn ??= "Name";
            sortorder ??= "desc";
            Process[] processes = GetSystemInfoTools.GetAllProcess();

            //List<Process>[] temp = ProcessTools.CheckProcessChanges(processes.ToList());

            ////List =>> JavaScriptArray;
            JavaScriptArray Data = List_Process_ToJavaScriptArray(processes.ToList());
            //JavaScriptArray RemoveTask = List_Process_ToJavaScriptArray(temp[1]);

            JavaScriptObject valuePairs = new JavaScriptObject {
                {"Data",Data },
             
            };
            PostBrowserJavaScriptMessage("setTaskData", valuePairs);

        }
        /// <summary>
        /// 将List<Process>转换为JavaScriptArray对象
        /// </summary>
        /// <returns></returns>
        private static JavaScriptArray List_Process_ToJavaScriptArray(List<Process> processes) {

            JavaScriptArray result = new JavaScriptArray();
            foreach (Process process in processes) {
                string path = ProcessTools.GetProcessPath(process.Id);
                string name = null;
                string filePath = null;
                if (path != null) {
                    name = Path.GetFileName(path);
                    filePath = Path.GetFullPath(path);
                }
                result.Add(new JavaScriptObject {
                    {"Name", process.ProcessName},
                    {"ImageName", name},
                    {"ImageFilePath",filePath },
                    {"UserName",ProcessTools.GetProcessUserName(process.Id)},
                    {"Id", process.Id}
                });
            }
            return result;
        }

        /// <summary>
        /// 将命令结果封装为一个JavaScriptObject 压入列队中
        /// </summary>
        /// <param name="str">执行结果</param>
        /// <param name="newDate">是否新数据</param>
        /// <param name="status">结果状态</param>
        public static void PushCommandResultToQuque(string str, bool newDate, bool status) {

            JavaScriptObject keyValuePairs = new JavaScriptObject {
                {"data",str},
                {"newData",newDate },
                {"status",status},
            };
            if (queuq != null) {
                lock (queuq) {
                    queuq.Enqueue(keyValuePairs);
                }
            }
        }
        /// <summary>
        /// 一些需要实时更新的内容，请放在该方法中
        /// </summary>
        private void RealTimeUpdates() {
            Task.Run(() => {
                while (true) {
                    lock (queuq) {
                        ExecuteJavaScript($"setVolumeValue({GetSystemInfoTools.GetSpeakerVolume()});");
                        if (queuq.Count > 0) {
                            PostBrowserJavaScriptMessage("commandDataUp", CompressionResults());
                        }
                    }
                    Thread.Sleep(1000 / 60);
                }
            });
        }
        /// <summary>
        /// 压缩发送到前端的数据包，节省前端对dom树的操作
        /// </summary>
        /// <returns></returns>
        private JavaScriptArray CompressionResults() {
            JavaScriptArray javaScriptArray = new JavaScriptArray();
            for (int i = 0; i < queuq.Count; i++) {
                lock (queuq) {
                    javaScriptArray.Add(queuq.Dequeue());
                }
            }
            return javaScriptArray;

        }
        /// <summary>
        /// 将一个字符串列表转化为JavaScriptArray对象
        /// </summary>
        /// <param name="strings">传入一个List<string></param>
        /// <returns>JavaScriptArray 对象</returns>
        private JavaScriptArray ToJavaScriptArray(List<string> strings) {
            JavaScriptArray javaScriptArray = new JavaScriptArray();
            foreach (string s in strings) {
                javaScriptArray.Add(s);
            }
            return javaScriptArray;
        }
        private void RegisterMessage() {

            RegisterJavaScriptMessagDispatcher("changeVolume", args => {
                int a = int.Parse(args.GetString());

                ModifySystemInfo.ModifyingSpeakerVolume(a);
            });
            RegisterJavaScriptMessagDispatcher("executorCommand", args => {
                string? commandStr = args.GetString();
                if (Command != null && commandStr != null) {
                    if (commandStr.Equals("restart")) {
                        Command.ReStart();
                        PushCommandResultToQuque("\r\n", false, false);
                        return;
                    }
                    //执行命令
                    Command.RunCMD(commandStr);
                }
            });
            RegisterJavaScriptMessagDispatcher("changeDiskSelect", args => {
                showDiskName = args.GetString();
            });
            RegisterJavaScriptMessagDispatcher("changeNetWorkSelect", args => {
                showNetwokInterfaceName = args.GetString();
            });
            RegisterJavaScriptMessagDispatcher("killtask", args => {
                //Command.RunCMD("taskkill /f /pid " + args.GetInt());
                int processId = args.GetInt();
                try {
                    Process.GetProcessById(processId).Kill();
                    //push数据到前端控制台
                    Command_Output($"\r\n成功: 已终止 PID 为 {processId} 的进程。\r\n");
                } catch (Exception ex) {
                    Command_Error("\r\n" + ex.Message + "\r\n");
                }
            });
            RegisterJavaScriptMessagDispatcher("attribute", args => {
                //发送进程详细数据
                int processId = args.GetInt();

            });
            RegisterJavaScriptMessagDispatcher("open", args => {
                //打开进程对应文件夹
                int processId = args.GetInt();
                string path = ProcessTools.GetProcessPath(processId);
                try {
                    if (path != null) {
                        Process.Start("explorer.exe", Path.GetDirectoryName(path));
                        Command_Output($"\r\n成功打开PID为 {processId} 的进程镜像\\模块文件所在文件夹");
                    } else {
                        Command_Error($"\r\n尝试打开PID为 {processId} 的进程镜像\\模块文件所在文件夹失败 找不到对应映像\\模块文件");

                    }
                } catch (Exception ex) {
                    Command_Error($"\r\n尝试打开PID为 {processId} 的进程映像\\模块文件所在文件夹失败 {ex.Message}");
                }
            });
            RegisterJavaScriptMessagDispatcher("TaskSort", args => {
                //Console.WriteLine(args.ToString());
                JavaScriptObject keyValuePairs = (JavaScriptObject)args;
                //修改排序相关属性
                sortcolumn = keyValuePairs.GetValue("sortcolumn").GetString();
                sortorder = keyValuePairs.GetValue("sortorder").GetString();
                //重新发送任务数据到页面中
                SendTaskListData();

            });
        }
        protected override void PaintSplashScreen(PaintEventArgs e) {
            base.PaintSplashScreen(e);
        }
        protected override void OnPageLoadEnd(PageLoadEndEventArgs args) {
            RealTimeUpdates();
            //初始化必要数据
            Initialization();
        }

    }

}
