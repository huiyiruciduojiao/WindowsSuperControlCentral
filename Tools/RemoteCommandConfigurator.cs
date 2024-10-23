using CentralControl.Forms;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vanara;

namespace CentralControl.Tools {

    public class RemoteCommandConfigurator {
        private string host = null;
        private int port = -1;

        public RemoteCommandConfigurator(string host, int port) {
            DomainNameResolution(host);
            if (IsItPort(port)) {
                this.port = port;
            } else {
                this.port = 8888;
            }

        }
        public string Host {
            get => host; set {
                DomainNameResolution(value);
            }
        }
        public int Port {
            get => port; set {
                if (IsItPort(value)) {
                    port = value;
                } else {
                    port = 8888;
                }
            }
        }
        /// <summary>
        /// 如果设置的内容是一个域名，需要将域名解析
        /// </summary>
        /// <returns>解析结果的第一个IP地址</returns>
        public void DomainNameResolution(string domainName) {
            //如果是域名，调用方法解析域名，如果解析失败，提示
            if (IsItIP(domainName)) {
                host = domainName;
                return;
            }
            if (IsDomain(domainName)) {
                Task task = new Task(() => {
                    IPAddress[] ipHostInfo = null;
                    try {
                        ipHostInfo = Dns.GetHostAddresses(domainName);
                        if (ipHostInfo != null && ipHostInfo.Length > 0) {
                            host = ipHostInfo[0].ToString();
                        }
                    } catch (Exception e) {
                        WindowsSuperForm.PushCommandResultToQuque(e.Message, false, true);
                    }

                });
                task.Start();
            }

        }
        /// <summary>
        /// 验证字符串是否是域名
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public bool IsDomain(string str) {
            string pattern = @"^[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$";
            return Regex.IsMatch(pattern, str);
        }
        /// <summary>
        /// 判断传入的address是否是一个IP 重载方法 
        /// </summary>
        /// <param name="address">传入一个需要判断的字符串</param>
        /// <returns></returns>
        public bool IsItIP(String address) {
            return Regex.IsMatch(address, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 判断传入的port是否是一个端口号 重载
        /// </summary>
        /// <param name="port">传入一个需要判断的String值 如果传入的值是一个port 将会将传入的String转换为int,并把转换后的int值添加到当前配置中</param>
        /// <returns>true 是端口;false 不是端口</returns>
        public bool IsItPort(String port) {
            int tempPort = IsNum(port);
            if (tempPort > 0) {
                return IsItPort(tempPort);
            }
            return false;
        }
        /// <summary>
        /// 判断一个字符串是否是整数并且这个数字大于零
        /// </summary>
        /// <param name="num">需要判断的字符串</param>
        /// <returns>字符串转换后的值</returns>
        public int IsNum(String num) {
            int temp = -1;
            if (int.TryParse(num, out temp)) {
                return temp;
            } else {
                return -1;
            }
        }
        /// <summary>
        /// 判断传入的port是否是一个端口号
        /// </summary>
        /// <param name="port">传入一个需要判断的int值</param>
        /// <returns>true 是端口;false 不是端口</returns>
        public bool IsItPort(int port) {
            if (port >= 0 && port <= 65535) {
                return true;
            } else {
                return false;
            }
        }
    }
}
