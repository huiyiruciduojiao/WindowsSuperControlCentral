using CentralControl.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using WinFormium;
using WinFormium.WebResource;

namespace CentralControl {
    public class WindowsSuperConsole : WinFormiumStartup {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
        protected override MainWindowCreationAction? UseMainWindow(MainWindowOptions opts) {
            AllocConsole();
            // 设置应用程序的主窗体
            return opts.UseMainFormium<WindowsSuperForm>();
        }

        protected override void WinFormiumMain(string[] args) {
            // Main函数中的代码应该在这里，该函数只在主进程中运行。这样可以防止子进程运行一些不正确的初始化代码。
            ApplicationConfiguration.Initialize();
        }

        protected override void ConfigurationChromiumEmbedded(ChromiumEnvironmentBuiler cef) {
            // 在此处配置 Chromium Embedded Framwork
        }

        protected override void ConfigureServices(IServiceCollection services) {
            // 在这里配置该应用程序的服务
            //注册嵌入资源
            services.AddEmbeddedFileResource(new EmbeddedFileResourceOptions {
                Scheme = "http",
                DomainName = "windows.super.console",
                ResourceAssembly = typeof(Program).Assembly,
                EmbeddedResourceDirectoryName = "WWWRoot"
            });

        }
    }
}
