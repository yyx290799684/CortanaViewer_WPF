using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaViewer_WPF.Utils
{
    public class NetworkUtils
    {
        private List<PerformanceCounter> dataSentCounter = new List<PerformanceCounter>();
        private List<PerformanceCounter> dataReceivedCounter = new List<PerformanceCounter>();
        private const int numberOfIterations = 10;

        /// <summary>
        /// 初始化
        /// </summary>
        public NetworkUtils()
        {
            foreach (var item in GetNetworkCards())
            {
                dataSentCounter.Add(new PerformanceCounter("Network Interface", "Bytes Sent/sec", item));
                dataReceivedCounter.Add(new PerformanceCounter("Network Interface", "Bytes Received/sec", item));
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="networkCard">网卡名字</param>
        public NetworkUtils(string networkCard)
        {
            dataSentCounter.Add(new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard));
            dataReceivedCounter.Add(new PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard));
        }

        /// <summary>
        /// 获取上传速度
        /// </summary>
        /// <returns></returns>
        public string GetUploadSpeed()
        {
            float sendSum = 0;

            for (int index = 0; index < numberOfIterations; index++)
            {
                foreach (var item in dataSentCounter)
                {
                    sendSum += item.NextValue();
                }
            }
            float dataSent = sendSum;

            double upload = dataSent / numberOfIterations;
            return FormatSpeed(upload);
        }

        /// <summary>
        /// 获取下载速度
        /// </summary>
        /// <returns></returns>
        public string GetDownloadSpeed()
        {
            float receiveSum = 0;

            for (int index = 0; index < numberOfIterations; index++)
            {
                foreach (var item in dataReceivedCounter)
                {
                    receiveSum += item.NextValue();
                }
            }
            float dataReceived = receiveSum;

            double download = dataReceived / numberOfIterations;
            return FormatSpeed(download);
        }

        public string FormatSpeed(double speed)
        {
            if (speed < 1024)
            {
                return string.Format("{0:F}", speed) + " b/s";
            }
            else if (speed < 1024000)
            {
                return string.Format("{0:F}", speed / 1024) + " KB/s";
            }
            else if (speed < 1024000000)
            {
                return string.Format("{0:F}", speed / 1024000) + " MB/s";
            }
            else
            {
                return string.Format("{0:F}", speed / 1024000) + " MB/s";
            }
        }

        public List<string> GetNetworkCards()
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            List<string> instancename = category.GetInstanceNames().ToList();
            return instancename;
            //foreach (string name in instancename)
            //{
            //    Debug.WriteLine(name);
            //}
        }
    }
}
