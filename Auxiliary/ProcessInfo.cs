using System.Diagnostics;

namespace CentralControl.Auxiliary {
    public class ProcessInfo {
        /// <summary>
        /// 进程名称
        /// </summary>
        public string? ProcessName = null;
        /// <summary>
        /// 进程Id
        /// </summary>
        public int? ProcessID = -1;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName = null;
        /// <summary>
        /// 模块(映像)名称
        /// </summary>
        public string? ModuleOrImageName = null;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string? FilePath = null;
        /// <summary>
        /// 进程描述
        /// </summary>
        public string? ProcessDescription = null;
        /// <summary>
        /// 启动线程数
        /// </summary>
        public int? NumberOfStartupThreads = -1;
        /// <summary>
        /// CPU占用时间
        /// </summary>
        public TimeSpan? CPUUsageTime = null;
        /// <summary>
        /// 线程优先级
        /// </summary>
        public ProcessPriorityClass? ThreadPriority = null;
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime? StartTime = null;
        /// <summary>
        /// 专用内存
        /// </summary>
        public long? DedicatedMemory = null;
        /// <summary>
        /// 峰值虚拟内存
        /// </summary>
        public long? PeakVirtualMemory = null;
        /// <summary>
        /// 峰值分页内存
        /// </summary>
        public long? PeakPagingMemory = null;
        /// <summary>
        /// 分页系统内存
        /// </summary>
        public long? PagingSystemMemory = null;
        /// <summary>
        /// 分页内存
        /// </summary>
        public long? PagedMemory = null;
        /// <summary>
        /// 未分页系统内存
        /// </summary>
        public long? UnpagedSystemMemory = null;
        /// <summary>
        /// 物理内存
        /// </summary>
        public long? PhysicalMemory = null;
        /// <summary>
        /// 虚拟内存
        /// </summary>
        public long? VirtualMemory = null;
        /// <summary>
        /// 进程访问失败消息
        /// </summary>
        public string? ProcessAccessFailureMessage = null;

    }
}
