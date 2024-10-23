using CentralControl.Auxiliary;
using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace CentralControl.Tools {
    public class GetSystemInfoTools {
        /// <summary>
        /// 操作系统版本
        /// </summary>
        public static string OSDescription { get; } = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        /// <summary>
        /// 操作系统架构（<see cref="Architecture">）
        /// </summary>
        public static string OSArchitecture { get; } = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();
        /// <summary>
        /// 电脑型号
        /// Caption:计算机系统产品
        /// Description:计算机系统产品
        /// ElementName:
        /// IdentifyingNumber:CND0162XZ1
        /// InstanceID:
        /// Name:HP ENVY x360 Convertible 15-ed0xxx
        /// SKUNumber:
        /// UUID:A6C31C0E-997C-EA11-8104-B05CDA905B6C
        /// Vendor:HP
        /// Version:Type1ProductConfigId
        /// WarrantyDuration:
        /// WarrantyStartDate:
        /// </summary>
        /// <returns></returns>
        public static string GetComputerVersion() {
            var version = new StringBuilder();
            var moc = new ManagementClass("Win32_ComputerSystemProduct").GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>()) {
                foreach (var item in mo.Properties) {
                    version.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return version.ToString(); ;
        }
        /// <summary>
        /// 主板信息
        /// Caption:简短说明
        /// ConfigOptions:数组，表示位于在底板上跳线和开关的配置
        /// CreationClassName:表示类的名称(就是Win32_baseboard类)
        /// Depth:对象的描述（底板）
        /// Description:基板
        /// Height:
        /// HostingBoard:如果为TRUE，该卡是一个主板，或在一个机箱中的基板。
        /// HotSwappable:如果为TRUE，就是支持热插拔（判断是否支持热插拔）
        /// InstallDate:
        /// Manufacturer:表示制造商的名称
        /// Model:
        /// Name:对象的名称标签
        /// OtherIdentifyingInfo:
        /// PartNumber:
        /// PoweredOn:如果为真，物理元素处于开机状态
        /// Product:产品的型号
        /// Removable:判断是否可拆卸的
        /// Replaceable:判断是否可更换的
        /// RequirementsDescription:
        /// RequiresDaughterBoard:False
        /// SerialNumber:制造商分配的用于识别所述物理元件数目
        /// SKU:
        /// SlotLayout:
        /// SpecialRequirements:
        /// Status:对象的当前状态
        /// Tag:符系统的基板唯一标识
        /// Version:08.32
        /// Weight:
        /// Width:
        /// </summary>
        /// <returns></returns>
        public static string GetBaseBoardInfo() {
            var baseBoard = new StringBuilder();
            var moc = new ManagementClass("Win32_BaseBoard").GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>()) {
                foreach (var item in mo.Properties) {
                    baseBoard.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return baseBoard.ToString();
        }
        /// <summary>
        /// 处理器信息（<see cref="https://www.cnblogs.com/zhesong/p/wmiid.html">）
        ///  AddressWidth:在32位操作系统，该值是32，在64位操作系统是64
        ///  Architecture:所使用的平台的处理器架构
        ///  AssetTag:代表该处理器的资产标签
        ///  Availability:设备的状态
        ///  Caption:设备的简短描述
        ///  Characteristics:处理器支持定义的功能
        ///  ConfigManagerErrorCode:Windows API的配置管理器错误代码
        ///  ConfigManagerUserConfig:如果为TRUE，该装置是使用用户定义的配置
        ///  CpuStatus:处理器的当前状态
        ///  CreationClassName:出现在用来创建一个实例继承链的第一个具体类的名称
        ///  CurrentClockSpeed:处理器的当前速度，以MHz为单位
        ///  CurrentVoltage:处理器的电压。如果第八位被设置，位0-6包含电压乘以10，如果第八位没有置位，则位在VoltageCaps设定表示的电压值。 CurrentVoltage时SMBIOS指定的电压值只设置
        ///  DataWidth:在32位处理器，该值是32，在64位处理器是64
        ///  Description:描述
        ///  DeviceID:在系统上的处理器的唯一标识符
        ///  ErrorCleared:如果为真，报上一个错误代码的被清除
        ///  ErrorDescription:错误的代码描述
        ///  ExtClock:外部时钟频率，以MHz为单位
        ///  Family:处理器系列类型
        ///  InstallDate:安装日期
        ///  L2CacheSize:二级缓存大小
        ///  L2CacheSpeed:二级缓存处理器的时钟速度
        ///  L3CacheSize:三级缓存大小
        ///  L3CacheSpeed:三级缓存处理器的时钟速度
        ///  LastErrorCode:报告的逻辑设备上一个错误代码
        ///  Level:处理器类型的定义。该值取决于处理器的体系结构
        ///  LoadPercentage:每个处理器的负载能力，平均到最后一秒
        ///  Manufacturer:处理器的制造商
        ///  MaxClockSpeed:处理器的最大速度，以MHz为单位
        ///  Name:处理器的名称
        ///  NumberOfCores:处理器的当前实例的数目。核心是在集成电路上的物理处理器
        ///  NumberOfEnabledCore:每个处理器插槽启用的内核数
        ///  NumberOfLogicalProcessors:用于处理器的当前实例逻辑处理器的数量
        ///  OtherFamilyDescription:处理器系列类型
        ///  PartNumber:这款处理器的产品编号制造商所设置
        ///  PNPDeviceID:即插即用逻辑设备的播放设备标识符
        ///  PowerManagementCapabilities:逻辑设备的特定功率相关的能力阵列
        ///  PowerManagementSupported:如果为TRUE，该装置的功率可以被管理，这意味着它可以被放入挂起模式
        ///  ProcessorId:描述处理器功能的处理器的信息
        ///  ProcessorType:处理器的主要功能
        ///  Revision:系统修订级别取决于体系结构
        ///  Role:所述处理器的作用
        ///  SecondLevelAddressTranslationExtensions:如果为True，该处理器支持用于虚拟地址转换扩展
        ///  SerialNumber:处理器的序列号
        ///  SocketDesignation:芯片插座的线路上使用的类型
        ///  Status:对象的当前状态
        ///  StatusInfo:对象的当前状态信息
        ///  Stepping:在处理器家族处理器的版本
        ///  SystemCreationClassName:创建类名属性的作用域计算机的价值
        ///  SystemName:系统的名称
        ///  ThreadCount:每个处理器插槽的线程数
        ///  UniqueId:全局唯一标识符的处理器
        ///  UpgradeMethod:CPU插槽的信息
        ///  Version:依赖于架构处理器的版本号
        ///  VirtualizationFirmwareEnabled:如果真，固件可以虚拟化扩展
        ///  VMMonitorModeExtensions:如果为True，该处理器支持Intel或AMD虚拟机监控器扩展。
        ///  VoltageCaps:该处理器的电压的能力
        /// </summary>
        /// <returns></returns>
        public static string GetCPUInfo() {
            var cpu = new StringBuilder();
            var moc = new ManagementClass("Win32_Processor").GetInstances();
            foreach (var mo in moc) {
                foreach (var item in mo.Properties) {
                    cpu.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return cpu.ToString();
        }
        /// <summary>
        /// 内存信息（<see cref="https://www.cnblogs.com/zhesong/p/wmiid.html">）
        ///  Attributes:1
        ///  BankLabel:BANK 2
        ///  Capacity:获取内存容量（单位KB）
        ///  Caption:物理内存还虚拟内存
        ///  ConfiguredClockSpeed:配置时钟速度
        ///  ConfiguredVoltage:配置电压
        ///  CreationClassName:创建类名
        ///  DataWidth:获取内存带宽
        ///  Description:描述
        ///  DeviceLocator:获取设备定位器
        ///  FormFactor:构成因素
        ///  HotSwappable:是否支持热插拔
        ///  InstallDate:安装日期
        ///  InterleaveDataDepth:数据交错深度
        ///  InterleavePosition:数据交错的位置
        ///  Manufacturer:生产商
        ///  MaxVoltage:最大电压
        ///  MemoryType:内存类型
        ///  MinVoltage:最小电压
        ///  Model:型号
        ///  Name:名字
        ///  OtherIdentifyingInfo:其他识别信息
        ///  PartNumber:零件编号
        ///  PositionInRow:行位置
        ///  PoweredOn:是否接通电源
        ///  Removable:是否可拆卸
        ///  Replaceable:是否可更换
        ///  SerialNumber:编号
        ///  SKU:SKU号
        ///  SMBIOSMemoryType:SMBIOS内存类型
        ///  Speed:速率
        ///  Status:状态
        ///  Tag:唯一标识符的物理存储器
        ///  TotalWidth:总宽
        ///  TypeDetail:类型详细信息
        ///  Version:版本信息
        ///  AvailableBytes:可利用内存大小（B）
        ///  AvailableKBytes:可利用内存大小（KB）
        ///  AvailableMBytes:可利用内存大小（MB）
        ///  CacheBytes:125460480
        ///  CacheBytesPeak:392294400
        ///  CacheFaultsPersec:70774721
        ///  Caption:
        ///  CommitLimit:31939616768
        ///  CommittedBytes:20280020992
        ///  DemandZeroFaultsPersec:759274721
        ///  Description:
        ///  FreeAndZeroPageListBytes:2097152
        ///  FreeSystemPageTableEntries:12528527
        ///  Frequency_Object:0
        ///  Frequency_PerfTime:10000000
        ///  Frequency_Sys100NS:10000000
        ///  LongTermAverageStandbyCacheLifetimes:14400
        ///  ModifiedPageListBytes:41500672
        ///  Name:
        ///  PageFaultsPersec:1560432075
        ///  PageReadsPersec:19173703
        ///  PagesInputPersec:98834167
        ///  PagesOutputPersec:25921396
        ///  PagesPersec:124755563
        ///  PageWritesPersec:103362
        ///  PercentCommittedBytesInUse:2727084283
        ///  PercentCommittedBytesInUse_Base:4294967295
        ///  PoolNonpagedAllocs:0
        ///  PoolNonpagedBytes:798519296
        ///  PoolPagedAllocs:0
        ///  PoolPagedBytes:709898240
        ///  PoolPagedResidentBytes:496873472
        ///  StandbyCacheCoreBytes:247545856
        ///  StandbyCacheNormalPriorityBytes:847036416
        ///  StandbyCacheReserveBytes:0
        ///  SystemCacheResidentBytes:125460480
        ///  SystemCodeResidentBytes:0
        ///  SystemCodeTotalBytes:0
        ///  SystemDriverResidentBytes:17592179236864
        ///  SystemDriverTotalBytes:16953344
        ///  Timestamp_Object:0
        ///  Timestamp_PerfTime:5838028983825
        ///  Timestamp_Sys100NS:132532052633540000
        ///  TransitionFaultsPersec:792343233
        ///  TransitionPagesRePurposedPersec:78554340
        ///  WriteCopiesPersec:17253788
        /// </summary>
        /// <returns></returns>
        public static string GetRAMInfo() {
            var ram = new StringBuilder();
            var searcher = new ManagementObjectSearcher() {
                Query = new SelectQuery("Win32_PhysicalMemory"),
            }.Get().GetEnumerator();

            while (searcher.MoveNext()) {
                ManagementBaseObject baseObj = searcher.Current;
                foreach (var item in baseObj.Properties) {
                    ram.Append($"{item.Name}:{item.Value}\r\n");
                }
            }

            searcher = new ManagementObjectSearcher() {
                Query = new SelectQuery("Win32_PerfRawData_PerfOS_Memory"),
            }.Get().GetEnumerator();

            while (searcher.MoveNext()) {
                ManagementBaseObject baseObj = searcher.Current;
                foreach (var item in baseObj.Properties) {
                    ram.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return ram.ToString();
        }
        /// <summary>
        /// 显卡信息
        ///  AcceleratorCapabilities:
        ///  AdapterCompatibility:Intel Corporation
        ///  AdapterDACType:Internal
        ///  AdapterRAM:1073741824
        ///  Availability:3
        ///  CapabilityDescriptions:
        ///  Caption:Intel(R) UHD Graphics
        ///  ColorTableEntries:
        ///  ConfigManagerErrorCode:0
        ///  ConfigManagerUserConfig:False
        ///  CreationClassName:Win32_VideoController
        ///  CurrentBitsPerPixel:32
        ///  CurrentHorizontalResolution:1920
        ///  CurrentNumberOfColors:4294967296
        ///  CurrentNumberOfColumns:0
        ///  CurrentNumberOfRows:0
        ///  CurrentRefreshRate:60
        ///  CurrentScanMode:4
        ///  CurrentVerticalResolution:1080
        ///  Description:Intel(R) UHD Graphics
        ///  DeviceID:VideoController1
        ///  DeviceSpecificPens:
        ///  DitherType:0
        ///  DriverDate:20200109000000.000000-000
        ///  DriverVersion:26.20.100.7755
        ///  ErrorCleared:
        ///  ErrorDescription:
        ///  ICMIntent:
        ///  ICMMethod:
        ///  InfFilename:oem41.inf
        ///  InfSection:iCML_w10_DS
        ///  InstallDate:
        ///  InstalledDisplayDrivers:C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igdumdim64.dll,C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igd10iumd64.dll,C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igd10iumd64.dll,C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igd12umd64.dll
        ///  LastErrorCode:
        ///  MaxMemorySupported:
        ///  MaxNumberControlled:
        ///  MaxRefreshRate:75
        ///  MinRefreshRate:50
        ///  Monochrome:False
        ///  Name:Intel(R) UHD Graphics
        ///  NumberOfColorPlanes:
        ///  NumberOfVideoPages:
        ///  PNPDeviceID:PCI\VEN_8086&DEV_9B41&SUBSYS_8757103C&REV_02\3&11583659&2&10
        ///  PowerManagementCapabilities:
        ///  PowerManagementSupported:
        ///  ProtocolSupported:
        ///  ReservedSystemPaletteEntries:
        ///  SpecificationVersion:
        ///  Status:OK
        ///  StatusInfo:
        ///  SystemCreationClassName:Win32_ComputerSystem
        ///  SystemName:DESKTOP-OLA70V5
        ///  SystemPaletteEntries:
        ///  TimeOfLastReset:
        ///  VideoArchitecture:5
        ///  VideoMemoryType:2
        ///  VideoMode:
        ///  VideoModeDescription:屏幕描述
        ///  VideoProcessor:Intel(R) UHD Graphics Family
        ///  AcceleratorCapabilities:
        ///  AdapterCompatibility:NVIDIA
        ///  AdapterDACType:Integrated RAMDAC
        ///  AdapterRAM:4293918720
        ///  Availability:8
        ///  CapabilityDescriptions:
        ///  Caption:显卡描述
        ///  ColorTableEntries:
        ///  ConfigManagerErrorCode:0
        ///  ConfigManagerUserConfig:False
        ///  CreationClassName:Win32_VideoController
        ///  CurrentBitsPerPixel:
        ///  CurrentHorizontalResolution:
        ///  CurrentNumberOfColors:
        ///  CurrentNumberOfColumns:
        ///  CurrentNumberOfRows:
        ///  CurrentRefreshRate:
        ///  CurrentScanMode:
        ///  CurrentVerticalResolution:
        ///  Description:NVIDIA GeForce MX330
        ///  DeviceID:VideoController2
        ///  DeviceSpecificPens:
        ///  DitherType:
        ///  DriverDate:20200923000000.000000-000
        ///  DriverVersion:27.21.14.5241
        ///  ErrorCleared:
        ///  ErrorDescription:
        ///  ICMIntent:
        ///  ICMMethod:
        ///  InfFilename:oem123.inf
        ///  InfSection:Section043
        ///  InstallDate:
        ///  InstalledDisplayDrivers:C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll,C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll,C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll,C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll
        ///  LastErrorCode:
        ///  MaxMemorySupported:
        ///  MaxNumberControlled:
        ///  MaxRefreshRate:
        ///  MinRefreshRate:
        ///  Monochrome:False
        ///  Name:NVIDIA GeForce MX330
        ///  NumberOfColorPlanes:
        ///  NumberOfVideoPages:
        ///  PNPDeviceID:PCI\VEN_10DE&DEV_1D16&SUBSYS_8757103C&REV_A1\4&24375CB2&0&00E0
        ///  PowerManagementCapabilities:
        ///  PowerManagementSupported:
        ///  ProtocolSupported:
        ///  ReservedSystemPaletteEntries:
        ///  SpecificationVersion:
        ///  Status:OK
        ///  StatusInfo:
        ///  SystemCreationClassName:Win32_ComputerSystem
        ///  SystemName:DESKTOP-OLA70V5
        ///  SystemPaletteEntries:
        ///  TimeOfLastReset:
        ///  VideoArchitecture:5
        ///  VideoMemoryType:2
        ///  VideoMode:
        ///  VideoModeDescription:
        ///  VideoProcessor:GeForce MX330
        /// </summary>
        /// <returns></returns>
        public static string GetGPUInfo() {
            var gpu = new StringBuilder();
            var moc = new ManagementObjectSearcher("select * from Win32_VideoController").Get();

            foreach (var mo in moc) {
                foreach (var item in mo.Properties) {
                    gpu.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return gpu.ToString(); ;
        }
        /// <summary>
        /// 硬盘驱动器信息（<see cref="https://www.cnblogs.com/zhesong/p/wmiid.html">）
        ///  Access:0
        ///  Availability:
        ///  BlockSize:
        ///  Caption:硬盘描述，例如“C:”
        ///  Compressed:False
        ///  ConfigManagerErrorCode:Windows配置管理器错误代码
        ///  ConfigManagerUserConfig:如果为True，该设备使用用户定义的配置
        ///  CreationClassName:Win32_LogicalDisk
        ///  Description:本地固定磁盘
        ///  DeviceID:磁盘驱动器与系统中的其他设备的唯一标识符，例如“C:”
        ///  DriveType:3
        ///  ErrorCleared:如果为True，报告LastErrorCode错误现已清除
        ///  ErrorDescription:关于可能采取的纠正措施记录在LastErrorCode错误，和信息的详细信息
        ///  ErrorMethodology:误差检测和校正的类型被此设备支持
        ///  FileSystem:NTFS
        ///  FreeSpace:可使用硬盘大小
        ///  InstallDate:
        ///  LastErrorCode:
        ///  MaximumComponentLength:255
        ///  MediaType:由该设备使用或访问的媒体类型
        ///  Name:硬盘名字
        ///  NumberOfBlocks:
        ///  PNPDeviceID:即插即用逻辑设备的播放设备标识符
        ///  PowerManagementCapabilities:
        ///  PowerManagementSupported:
        ///  ProviderName:
        ///  Purpose:
        ///  QuotasDisabled:True
        ///  QuotasIncomplete:False
        ///  QuotasRebuilding:False
        ///  Size:硬盘总大小
        ///  Status:对象的当前状态
        ///  StatusInfo:逻辑设备的状态
        ///  SupportsDiskQuotas:True
        ///  SupportsFileBasedCompression:True
        ///  SystemCreationClassName:Win32_ComputerSystem
        ///  SystemName:DESKTOP-OLA70V5
        ///  VolumeDirty:False
        ///  VolumeName:Windows
        ///  VolumeSerialNumber:硬盘的序列号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskInfo() {
            var disk = new StringBuilder();
            var moc = new ManagementClass("Win32_LogicalDisk").GetInstances();
            foreach (ManagementObject mo in moc.Cast<ManagementObject>()) {
                foreach (var item in mo.Properties) {
                    disk.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return disk.ToString();
        }
        /// <summary>
        /// 初始化CPUCounters
        /// </summary>
        /// <returns></returns>
        public static bool InitializationCPUCounters() {
            return CPUHelper.Initialization();
        }
        /// <summary>
        /// 获取CPU所有核心的占用率
        /// </summary>
        /// <returns>该数组包含了每一个核心的使用状态</returns>
        public static double[] GetCPUCoreUsageRate() {
            return CPUHelper.GetCPUCoreUsage();
        }
        /// <summary>
        /// 获取指定核心的使用率
        /// </summary>
        /// <returns></returns>
        public static double GetCPUUsageOFCore(ushort core) {
            return CPUHelper.GetCpuUsageOfCore(core);
        }
        /// <summary>
        /// 获取CPU的总体使用率
        /// </summary>
        /// <returns></returns>
        public static double GetCPUUsageRate() {
            return CPUHelper.GetCPUUsage();
        }
        /// <summary>
        /// 释放CPUCounters
        /// </summary>
        /// <returns></returns>
        public static bool DisposerCPUCounters() {
            return CPUHelper.Dispose();
        }
        /// <summary>
        /// 获取内存占用率
        /// </summary>
        /// <returns>一个百分值</returns>
        public static double GetMemoryUsageRate() {
            return MemoryHelper.GetMemoryUsageRate();
        }
        /// <summary>
        /// 获取剩余可用内存
        /// </summary>
        /// <returns>放回一个double值，单位MB</returns>
        public static double GetAvailableMemory() {
            return MemoryHelper.GetAvailableMemory();
        }
        /// <summary>
        /// 释放内存Counters
        /// </summary>
        /// <returns></returns>
        public static bool DisposerMemoryCounters() {
            return MemoryHelper.Dispose();
        }
        /// <summary>
        /// 获取所有的进程，如果进程名称一致，则只取第一次进程。
        /// 因此使用HashSet来去除重复的
        /// </summary>
        /// <returns></returns>
        public static Process[] GetAllProcess() {
            //Process.
            return Process.GetProcesses();

        }
        /// <summary>
        /// 获取进程对应的用户名
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public static string GetProcessUserName(int pID) {
            string userName = string.Empty;
            //添加对System.Management的引用
            try {
                foreach (ManagementObject item in new ManagementObjectSearcher("Select * from Win32_Process WHERE processID=" + pID).Get().Cast<ManagementObject>()) {
                    ManagementBaseObject inPar = null;
                    ManagementBaseObject outPar = null;

                    inPar = item.GetMethodParameters("GetOwner");
                    outPar = item.InvokeMethod("GetOwner", inPar, null);
                    userName = Convert.ToString(outPar["User"]);
                    break;
                }
            } catch {
                userName = "SYSTEM";
            }
            return userName;
        }
        /// <summary>
        /// 打印当前选中的进程信息
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static ProcessInfo GetProcessInfo(Process process) {
            ProcessInfo info = new() {
                ProcessName = process.ProcessName,
                ProcessID = process.Id,
                UserName = GetProcessUserName(process.Id)
            };
            try {
                info.ModuleOrImageName = process.MainModule.ModuleName;
                info.FilePath = process.MainModule.FileName;
                info.ProcessDescription = process.MainModule.FileVersionInfo.FileDescription;
                info.NumberOfStartupThreads = process.Threads.Count;
                info.CPUUsageTime = process.TotalProcessorTime;
                info.ThreadPriority = process.PriorityClass;
                info.StartTime = process.StartTime;
                info.DedicatedMemory = process.PrivateMemorySize64;
                info.PeakVirtualMemory = process.PeakVirtualMemorySize64;
                info.PeakPagingMemory = process.PagedSystemMemorySize64;
                info.PagingSystemMemory = process.PagedMemorySize64;
                info.PagedMemory = process.PagedMemorySize64;
                info.UnpagedSystemMemory = process.NonpagedSystemMemorySize64;
                info.PhysicalMemory = process.WorkingSet64;
                info.VirtualMemory = process.VirtualMemorySize64;
            } catch (Exception ex) {
                info.ProcessAccessFailureMessage = ex.Message;
            }
            return info;
        }
        /// <summary>
        /// 获取扬声器音量
        /// </summary>
        /// <returns>0-100的int值</returns>
        public static int GetSpeakerVolume() {
            var enumerator = new MMDeviceEnumerator();

            MMDevice mMDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);

            return Convert.ToInt16(mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);

        }
        /// <summary>
        /// 获取指定网络设备的网络速率
        /// </summary>
        /// <param name="driveName">网络设备驱动名称</param>
        /// <returns>一个NetWorUseInfo对象，包含了上传，下载速率，并提供相应单位</returns>
        public static NetWorkUseInfo GetNetWorkUseInfo(string driveName) {
            return NetworkHelper.GetNetWorkDriveUsage(driveName);
        }
        /// <summary>
        /// 获取所有可被计数器统计的网络设备名称
        /// </summary>
        /// <returns>网络设备名称列表List<string></returns>
        public static List<string> GetAllNetWorkDriveName() {
            return NetworkHelper.GetAllNetWorkDriveName();
        }
        /// <summary>
        /// 释放相关网络计数器资源
        /// </summary>
        /// <returns>是否释放成功</returns>
        public static bool DisposeNetWrokDriveCounter() {
            return NetworkHelper.Dispose();
        }
        /// <summary>
        /// 获取所有磁盘盘符名
        /// </summary>
        /// <returns>List<string>对象</returns>
        public static List<string> GetAllDiskDrivesName() {
            return DiskHelper.GetAllDiskDrivesName();
        }
        /// <summary>
        /// 获取指定磁盘的IO信息
        /// </summary>
        /// <param name="driveName">盘符</param>
        /// <returns>磁盘IO信息对象</returns>
        public static DiskIOInfo? GetDiskIOInfo(string driveName) {
            return DiskHelper.GetDiskIOInfo(driveName);
        }
        /// <summary>
        /// 获取指定盘符的磁盘信息
        /// </summary>
        /// <param name="driveName">盘符</param>
        /// <returns>DriveInfo 对象</returns>
        public static DriveInfo? GetDriveInfo(string driveName) {
            return DiskHelper.GetDiskInfo(driveName);
        }
        /// <summary>
        /// 释放磁盘相关对象
        /// </summary>
        /// <returns></returns>
        public static bool DisposeDiskCounter() {
            return DiskHelper.Dispose();
        }
    }
    /// <summary>
    /// 获取CPU占用率的辅助类
    /// </summary>
    class CPUHelper {
        /// <summary>
        /// 获取计算机上的逻辑核心数量
        /// </summary>
        private static readonly int coreCount = Environment.ProcessorCount;
        /// <summary>
        /// 创建一个PerformanceCounter对象数组，用于获取每个核心的CPU使用率
        /// </summary>
        private static PerformanceCounter[] cpuCounters = new PerformanceCounter[coreCount];
        /// <summary>
        /// 创建一个PerformanceCounter对象，用于获取CPU使用率
        /// </summary>
        private static PerformanceCounter cpuCounter = new("Processor Information", "% Processor Utility", "_Total");
        /// <summary>
        /// cpuCounters是否被释放
        /// </summary>
        private static bool sign = false;
        /// <summary>
        /// 创建一个double数组来保存每个核心的CPU使用率
        /// </summary>
        private static double[] cpuUsages = new double[coreCount];
        /// <summary>
        /// 初始化CPUCounters
        /// </summary>
        /// <returns></returns>
        public static bool Initialization() {
            try {
                for (int core = 0; core < coreCount; core++) {
                    cpuCounters[core] = new PerformanceCounter("Processor Information", "% Processor Utility", "0," + core.ToString());
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }
        /// <summary>
        /// 返回所有核心的CPU的占用率的值
        /// </summary>
        /// <returns>每个核心的占用情况</returns>
        public static double[] GetCPUCoreUsage() {
            if (sign) {
                cpuCounters = new PerformanceCounter[coreCount];
                Initialization();
                sign = false;
            }
            for (int core = 0; core < coreCount; core++) {

                cpuUsages[core] = cpuCounters[core].NextValue();
            }

            return cpuUsages;
        }
        /// <summary>
        /// 获取指定的cpu核心使用率
        /// </summary>
        /// <returns></returns>
        public static double GetCpuUsageOfCore(ushort core) {
            if (core >= coreCount) {
                throw new ArgumentOutOfRangeException();
            }
            return GetCPUCoreUsage()[core];
        }
        /// <summary>
        /// 获取CPU总体使用率
        /// </summary>
        /// <returns></returns>
        public static double GetCPUUsage() {
            cpuCounter ??= new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            return cpuCounter.NextValue();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns></returns>
        public static bool Dispose() {
            try {
                cpuCounter?.Dispose();
                if (cpuCounters != null) {
                    // 关闭PerformanceCounter对象
                    for (int core = 0; core < coreCount; core++) {
                        cpuCounters[core].Dispose();
                    }
                }
                sign = true;
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
    /// <summary>
    /// 获取内存相关数据的辅助类
    /// </summary>
    class MemoryHelper {
        /// <summary>
        /// 创建一个性能计数器对象，用来监听内存使用状态,计算其使用百分比
        /// </summary>
        private static PerformanceCounter? memoryCounter = new("Memory", "Committed Bytes In Use");
        /// <summary>
        /// 创建一个性能计数器对象，用来监听内存的使用状态，计数其剩余可用内存
        /// </summary>
        private static PerformanceCounter? memoryCounterAvailble = new("Memory", "Available MBytes");
        /// <summary>
        /// 获取内存使用率
        /// </summary>
        /// <returns>占用率单位百分值</returns>
        public static double GetMemoryUsageRate() {
            memoryCounter ??= new PerformanceCounter("Memory", "Committed Bytes In Use");
            return memoryCounter.NextValue();
        }
        /// <summary>
        /// 获取剩余可用内存
        /// </summary>
        /// <returns>一个double数据，单位MB</returns>
        public static double GetAvailableMemory() {
            memoryCounterAvailble ??= new PerformanceCounter("Memory", "Available MBytes");
            return memoryCounterAvailble.NextValue();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns>是否释放成功</returns>
        public static bool Dispose() {
            try {
                if (memoryCounter != null) {
                    memoryCounter?.Dispose();
                    memoryCounterAvailble?.Dispose();
                    memoryCounter = null;
                    memoryCounterAvailble = null;
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
    /// <summary>
    /// 获取显卡相关数据辅助类
    /// </summary>
    class GPUHelper {
        // 初始化NVML
        //NVMLWrapper.Init();

        //// 获取GPU数量
        //int gpuCount = NVMLWrapper.DeviceGetCount();
        //Console.WriteLine("GPU数量: " + gpuCount);
        //public static double GetGPUUsageRat() {

        //    // 遍历每个GPU
        //    for (int gpuIndex = 0; gpuIndex < gpuCount; gpuIndex++) {
        //        // 获取GPU句柄
        //        NVMLDevice device = NVMLWrapper.DeviceGetHandleByIndex(gpuIndex);

        //        // 获取GPU名称
        //        string gpuName = NVMLWrapper.DeviceGetName(device);
        //        Console.WriteLine("GPU " + gpuIndex + " 名称: " + gpuName);

        //        // 获取GPU使用率
        //        NVMLUtilization utilization = NVMLWrapper.DeviceGetUtilizationRates(device);

        //        // 打印GPU使用率
        //        Console.WriteLine("GPU " + gpuIndex + " 使用率: " + utilization.Gpu + "%");
        //        Console.WriteLine("GPU " + gpuIndex + " 内存使用率: " + utilization.Memory + "%");
        //    }

        //    return 1;
        //}
    }
    /// <summary>
    /// 获取磁盘相关数据辅助类
    /// </summary>
    class DiskHelper {
        /// <summary>
        /// 标记相关计数器释放被初始化
        /// </summary>
        private static bool sign = false;
        /// <summary>
        /// 所有可以被计数器计数的磁盘设备
        /// </summary>
        private static Dictionary<string, PerformanceCounter[]>? diskDrives = new();
        /// <summary>
        /// 返回的磁盘使用状态对象
        /// </summary>
        private static DiskIOInfo diskInfo = new();
        /// <summary>
        /// 初始化磁盘计数器等相关对象
        /// </summary>
        /// <returns></returns>
        private static bool Initialization() {
            try {
                if (diskDrives == null) {
                    diskDrives = new Dictionary<string, PerformanceCounter[]>();
                } else {
                    diskDrives.Clear();
                }

                List<string> strings = GetAllDiskDrivesName();
                foreach (string s in strings) {
                    diskDrives.Add(s, new PerformanceCounter[] {
                            new PerformanceCounter("LogicalDisk","Disk Write Bytes/sec",s),
                            new PerformanceCounter("LogicalDisk","Disk Read Bytes/sec",s)
                        });
                }
                diskInfo ??= new DiskIOInfo();
            } catch (Exception) {
                sign = false;
                return false;
            }
            sign = true;
            return true;
        }
        /// <summary>
        ///  获取指定盘符的IO状态
        /// </summary>
        /// <param name="driveLetter">盘符</param>
        /// <returns>DiskInfo对象，包括IO数据和单位</returns>
        public static DiskIOInfo? GetDiskIOInfo(String driveLetter) {
            if (!sign) {
                Initialization();
            }
            if (diskDrives == null) {
                return null;
            }
            PerformanceCounter[]? diskDrivesCounters = null;
            if (diskDrives.ContainsKey(driveLetter)) {
                diskDrivesCounters = diskDrives[driveLetter];
            }
            if (diskDrivesCounters != null && diskDrivesCounters.Length == 2) {
                diskInfo.WriteBytes = diskDrivesCounters[0].NextValue();
                diskInfo.ReadBytes = diskDrivesCounters[1].NextValue();
            }
            return diskInfo;
        }
        /// <summary>
        /// 获取指定盘符的磁盘数据
        /// </summary>
        /// <param name="driveLetter"></param>
        /// <returns>一个磁盘信息对象</returns>
        public static DriveInfo? GetDiskInfo(string driveLetter) {
            DriveInfo[] driveInfos = GetAllDiskInfo();
            foreach (DriveInfo item in driveInfos) {
                if (item.IsReady && RemoveSlash(item.Name).Equals(RemoveSlash(driveLetter))) {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取所有磁盘信息
        /// </summary>
        /// <returns>磁盘信息对象数组</returns>
        private static DriveInfo[] GetAllDiskInfo() {
            return DriveInfo.GetDrives();
        }
        /// <summary>
        /// 获取所有可用磁盘盘符，以便后面绑定计数器
        /// </summary>
        /// <returns>盘符列表</returns>
        public static List<string> GetAllDiskDrivesName() {
            DriveInfo[] allDrives = GetAllDiskInfo();
            List<string> diskDrivesNames = new();
            foreach (DriveInfo drive in allDrives) {
                if (drive.IsReady) {
                    diskDrivesNames.Add(RemoveSlash(drive.Name));
                }
            }
            return diskDrivesNames;
        }
        /// <summary>
        /// 由于上面获取的盘符含有 "\"，不便于绑定计数器，所以通过该方法去除"\"
        /// </summary>
        /// <param name="str">包含"\"的字符串</param>
        /// <returns>不包含"\"的字符串</returns>
        private static string RemoveSlash(string str) {
            return str.Replace("\\", "");
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns>是否释放成功</returns>
        public static bool Dispose() {
            sign = false;
            if (diskInfo != null) {
                diskDrives = null;
            }
            if (diskDrives != null && diskDrives.Count > 0) {
                try {
                    foreach (string key in diskDrives.Keys) {
                        diskDrives[key][0].Dispose();
                        diskDrives[key][0].Dispose();
                    }
                    diskDrives.Clear();
                    diskDrives = null;
                } catch (Exception) {
                    return false;
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 获取网络相关数据辅助类
    /// </summary>
    class NetworkHelper {
        /// <summary>
        /// 标记相关计数器是否被初始化
        /// </summary>
        private static bool sign = false;
        /// <summary>
        ///  所有可以被计数器计数的设备
        /// </summary>
        private static Dictionary<string, PerformanceCounter[]>? drives = new();
        /// <summary>
        /// 返回的网络设备使用状态对象
        /// </summary>
        private static NetWorkUseInfo? NetWorkUseInfo = new();
        /// <summary>
        /// 初始化网络设备计数器
        /// </summary>
        /// <returns>是否初始化成功</returns>
        private static bool Initialization() {
            try {
                if (drives == null) {
                    drives = new Dictionary<string, PerformanceCounter[]>();
                } else {
                    drives.Clear();
                }
                ManagementObjectCollection moc = GetAllNetWorkDrive();
                foreach (ManagementObject m in moc.Cast<ManagementObject>()) {
                    Console.WriteLine(RPWB(m["DriverDescription"]?.ToString()));

                    drives.Add(RPWB(m["DriverDescription"]?.ToString()),
                        new PerformanceCounter[] { new PerformanceCounter("Network Adapter", "Bytes Sent/sec", RPWB(m["DriverDescription"]?.ToString())),
                            new PerformanceCounter("Network Adapter", "Bytes Received/sec", RPWB(m["DriverDescription"]?.ToString())) });
                }

                NetWorkUseInfo ??= new NetWorkUseInfo();
                sign = true;
                return true;
            } catch (Exception e) {
                Console.WriteLine("初始化发生错误" + e.Message);
                sign = false;
                return false;
            }
        }
        /// <summary>
        /// 获取一个网络设备的使用状态
        /// </summary>
        /// <returns>NetWorkUseInfod对象</returns>
        public static NetWorkUseInfo GetNetWorkDriveUsage(string driveName) {
            driveName = RPWB(driveName);
            if (!sign) {
                Initialization();
            }

            PerformanceCounter[]? performanceCounters = null;
            if (drives.ContainsKey(driveName)) {
                performanceCounters = drives[driveName];
            }
            if (performanceCounters != null && performanceCounters.Length == 2) {

                NetWorkUseInfo.SendBytes = performanceCounters[0].NextValue();
                NetWorkUseInfo.ReceiveBytes = performanceCounters[1].NextValue();
            }
            return NetWorkUseInfo;
        }
        /// <summary>
        /// 获取所有可以被计数器计数的网络设备对象
        /// </summary>
        /// <returns>设备对象合集</returns>
        private static ManagementObjectCollection GetAllNetWorkDrive() {
            string qry = "SELECT * FROM MSFT_NetAdapter";
            ManagementScope scope = new(@"\\.\ROOT\StandardCimv2");
            ObjectQuery query = new(qry);
            ManagementObjectSearcher mos = new(scope, query);
            ManagementObjectCollection moc = mos.Get();
            return moc;
        }
        /// <summary>
        /// 获取所有可以被计数器统计的网络设备名称
        /// </summary>
        /// <returns>名称List<string> 对象</returns>
        public static List<string> GetAllNetWorkDriveName() {
            List<string> names = new List<string>();
            if (!sign) {
                Initialization();
            }
            foreach (string key in drives.Keys) {
                try {
                    //尝试读取使用状态
                    GetNetWorkDriveUsage(key);
                    //未发生错误，表示该设备可被计数器统计
                    names.Add(key);
                } catch { }
            }
            return names;
        }
        /// <summary>
        /// 处理部分driveName中出现小括号的情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string RPWB(string input) {
            // 使用 Replace 方法替换小括号为中括号
            string result = input.Replace("(", "[").Replace(")", "]");
            return result;
        }
        ///// <summary>
        ///// 释放资源
        ///// </summary>
        ///// <returns>是否释放成功</returns>
        public static bool Dispose() {
            if (NetWorkUseInfo != null) {
                NetWorkUseInfo = null;
            }
            if (drives != null && drives.Count != 0) {
                try {
                    foreach (string key in drives.Keys) {
                        drives[key][0].Dispose();
                        drives[key][1].Dispose();
                    }
                    drives.Clear();
                    drives = null;
                } catch (Exception) {

                    return false;
                }
            }
            GC.Collect();
            sign = false;
            return true;
        }
    }
}