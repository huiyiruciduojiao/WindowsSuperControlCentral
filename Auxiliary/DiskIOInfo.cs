namespace CentralControl.Auxiliary {
    public class DiskIOInfo {
        /// <summary>
        /// 每秒写出到磁盘数据
        /// </summary>
        private double writeBytes = -1;
        /// <summary>
        /// 每秒从磁盘读取数据
        /// </summary>
        private double readBytes = -1;

        private string wUnit = "MB/s";
        private string rUnit = "MB/s";

        public double WriteBytes {
            get => writeBytes; set {
                if (value / (1024 * 1024) > 1024) {
                    writeBytes = Math.Round(value / (1024 * 1024 * 1024), 2);
                    WUnit = "GB/s";
                } else if (value / 1024 > 1024) {
                    writeBytes = Math.Round(value / (1024 * 1024), 2);
                    WUnit = "MB/s";
                } else if (value / 1024 < 1024) {
                    writeBytes = Math.Round(value / 1024, 2);
                    WUnit = "KB/s";
                }
            }
        }
        public double ReadBytes {
            get => readBytes; set {
                if (value / (1024 * 1024) > 1024) {
                    readBytes = Math.Round(value / (1024 * 1024 * 1024), 2);
                    RUnit = "GB/s";
                } else if (value / 1024 > 1024) {
                    readBytes = Math.Round(value / (1024 * 1024), 2);
                    RUnit = "MB/s";
                } else if (value / 1024 < 1024) {
                    readBytes = Math.Round(value / 1024, 2);
                    RUnit = "KB/s";
                }
            }
        }

        public string WUnit { get => wUnit; set => wUnit = value; }
        public string RUnit { get => rUnit; set => rUnit = value; }

    }
}
