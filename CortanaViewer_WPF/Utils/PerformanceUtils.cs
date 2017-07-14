using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CortanaViewer_WPF.Utils
{
    public class PerformanceUtils
    {
        public PerformanceUtils()
        {
            //初始化CPU计数器 
            m_PcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            m_PcCpuLoad.MachineName = ".";
            m_PcCpuLoad.NextValue();

            //CPU个数 
            m_ProcessorCount = Environment.ProcessorCount;

            //获得物理内存 
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    m_TotalPhysicalMemory = long.Parse(mo["TotalPhysicalMemory"].ToString());
                }
            }
        }

        private int m_ProcessorCount = 0;//CPU个数 
        public int ProcessorCount { get { return m_ProcessorCount; } }//CPU个数 

        private PerformanceCounter m_PcCpuLoad;//CPU计数器 
        public PerformanceCounter PcCpuLoad { get { return m_PcCpuLoad; } }//CPU计数器 

        /// <summary>
        /// CPU使用率
        /// </summary>
        public double CPUPercent
        {
            get
            {
                double c = m_PcCpuLoad != null ? m_PcCpuLoad.NextValue() : 0;
                return c;
            }
        }

        private long m_TotalPhysicalMemory = 0;//物理内存 
        public long TotalPhysicalMemory { get { return m_TotalPhysicalMemory; } }//物理内存 

        private long m_FreePhysicalMemory//可用内存 
        {
            get
            {
                long availablebytes = 0;
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
                    }
                }
                return availablebytes;
            }
        }
        public long FreePhysicalMemory { get { return m_FreePhysicalMemory; } }//可用内存 

        /// <summary>
        /// 内存使用率
        /// </summary>
        public double MemoryPercent
        {
            get
            {
                if (m_TotalPhysicalMemory == 0 || m_FreePhysicalMemory == 0)
                {
                    return 0;
                }
                else
                {
                    return (1 - (float)m_FreePhysicalMemory / m_TotalPhysicalMemory) * 100;
                }
            }
        }

    }
}
