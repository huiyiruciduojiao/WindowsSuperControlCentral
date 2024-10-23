using NAudio.CoreAudioApi;

namespace CentralControl.Tools {
    /// <summary>
    /// 该类将提供修改系统数据状态的接口
    /// </summary>
    public class ModifySystemInfo {
        /// <summary>
        /// 修改系统扬声器音量
        /// </summary>
        /// <param name="value">音量值 （0-100）如果大于100则为100 如果小于0 则为0</param>
        /// <returns>返回修改后的值</returns>
        public static int ModifyingSpeakerVolume(int value) {
            if (value < 0) {
                value = 0;
            }
            if (value > 100) {
                value = 100;
            }
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice mMDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            return (int)((mMDevice.AudioEndpointVolume.MasterVolumeLevelScalar = value / 100.0f) * 100.0f);

        }

    }
}
