namespace CentralControl.Auxiliary {
    public class NetWorkUseInfo {
        /// <summary>
        /// 每秒发送数据
        /// </summary>
        private double sendBytes = -1;
        /// <summary>
        /// 每秒接收数据
        /// </summary>
        private double receiveBytes = -1;

        private string sUnit = "MB/s";
        private string rUnit = "MB/s";

        public double SendBytes {
            get => sendBytes; set {
                if (value / 1024 > 1024) {
                    sendBytes = Math.Round(value / (1024 * 1024), 2);
                    SUnit = "MB/s";
                } else if (value / 1024 < 1024) {
                    sendBytes = Math.Round(value / 1024, 2);
                    SUnit = "KB/s";
                }
            }
        }
        public double ReceiveBytes {
            get => receiveBytes; set {
                if (value / 1024 > 1024) {
                    receiveBytes = Math.Round(value / (1024 * 1024), 2);
                    RUnit = "MB/s";
                } else if (value / 1024 < 1024) {
                    receiveBytes = Math.Round(value / 1024, 2);
                    RUnit = "KB/s";
                }
            }
        }

        public string SUnit { get => sUnit; set => sUnit = value; }
        public string RUnit { get => rUnit; set => rUnit = value; }
    }
}
