using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CentralControl.Tools {
    public class ProcessTools {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);
        [DllImport("psapi.dll")]
        static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetTokenInformation(IntPtr tokenHandle, TokenInformationClass tokenInformationClass, IntPtr tokenInformation, uint tokenInformationLength, out uint returnLength);
        // 定义枚举，表示Token信息类别
        enum TokenInformationClass {
            TokenUser = 1
        }
        // 定义结构体，表示Token用户信息
        [StructLayout(LayoutKind.Sequential)]
        struct TOKEN_USER {
            public SID_AND_ATTRIBUTES User;
        }

        // 定义结构体，表示SID及其属性
        [StructLayout(LayoutKind.Sequential)]
        struct SID_AND_ATTRIBUTES {
            public IntPtr Sid;
            public uint Attributes;
        }
        private static List<Process> previousProcesses = new List<Process>();
        public static List<Process>[] CheckProcessChanges(List<Process> currentProcesses) {
            // 计算进程的变化
            List<Process> addedProcesses = new List<Process>();
            List<Process> removedProcesses = new List<Process>();


            for (int i = 0; i < currentProcesses.Count; i++) {
                int thisId = currentProcesses[i].Id;
                bool flage = false;
                for (int j = 0; j < previousProcesses.Count; j++) {
                    int oldId = previousProcesses[j].Id;
                    if (thisId == oldId) {
                        flage = true;
                        break;
                    }
                }
                if (!flage) {
                    addedProcesses.Add(currentProcesses[i]);
                }
            }

            for (int i = 0; i < previousProcesses.Count; i++) {
                int oldId = previousProcesses[i].Id;
                bool flage = false;
                for (int j = 0; j < currentProcesses.Count; j++) {
                    int thisId = currentProcesses[j].Id;
                    if (thisId == oldId) {
                        flage = true;
                        break;
                    }
                }
                if (!flage) {
                    removedProcesses.Add(previousProcesses[i]);
                }
            }

            // 更新上一次的进程列表
            previousProcesses = currentProcesses;

            return new List<Process>[] { addedProcesses, removedProcesses };

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetProcessPath(int pid) {
            var processHandle = OpenProcess(0x0400 | 0x0010, false, pid);
            if (processHandle == IntPtr.Zero) {
                return null;
            }
            const int lengthSb = 4000;
            var sb = new StringBuilder(lengthSb);
            string result = null;
            if (GetModuleFileNameEx(processHandle, IntPtr.Zero, sb, lengthSb) > 0) {
                result = sb.ToString();
            }
            CloseHandle(processHandle);
            return result;
        }
        public static string GetProcessUserName(int processId) {
            try {
                // 打开进程
                IntPtr processHandle = OpenProcess(0x0400 | 0x0010, false, processId);
                if (processHandle == IntPtr.Zero) {
                    return null;
                }

                // 打开进程的访问令牌
                if (OpenProcessToken(processHandle, 8 /*TOKEN_QUERY*/, out IntPtr tokenHandle)) {
                    // 获取Token用户信息
                    GetTokenInformation(tokenHandle, TokenInformationClass.TokenUser, IntPtr.Zero, 0, out uint tokenInfoLength);
                    IntPtr tokenInformation = Marshal.AllocHGlobal((int)tokenInfoLength);
                    string username = null;
                    if (GetTokenInformation(tokenHandle, TokenInformationClass.TokenUser, tokenInformation, tokenInfoLength, out tokenInfoLength)) {
                        TOKEN_USER tokenUser = (TOKEN_USER)Marshal.PtrToStructure(tokenInformation, typeof(TOKEN_USER));
                        // 转换SID为用户名
                        username = new SecurityIdentifier(tokenUser.User.Sid).Translate(typeof(NTAccount)).Value;
                    } else {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                    Marshal.FreeHGlobal(tokenInformation);
                    CloseHandle(tokenHandle); // 关闭句柄
                    return username;
                } else {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            } catch (Exception) {
            }
            return null;
        }
        public static void KillProcess(int pid) {
            Process.GetProcessById(pid).Kill();


        }
        public static void DisplaySet(HashSet<Process> set) {
            Console.Write("{");
            foreach (Process i in set) {
                Console.Write(" {0}", i.ProcessName);
            }
            Console.WriteLine(" }");
        }
    }
}
