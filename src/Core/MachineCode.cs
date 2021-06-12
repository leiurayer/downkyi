using System;
using System.Management;

namespace Core
{
    public class MachineCode
    {
        private static MachineCode machineCode;

        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <returns></returns>
        public static string GetMachineCodeString()
        {
            if (machineCode == null)
            {
                machineCode = new MachineCode();
            }

            string machineCodeString = "PC." +
                machineCode.GetMainBordId() + "." +
                machineCode.GetCpuInfo() + "." +
                machineCode.GetDiskID();// + "." +
                                        //machineCode.GetMoAddress();
            return machineCodeString.Replace(" ", "");
        }

        /// <summary>
        /// 获取主板ID
        /// </summary>
        /// <returns></returns>
        public string GetMainBordId()
        {
            string strId = "";
            try
            {
                using (ManagementClass mc = new ManagementClass("Win32_BaseBoard"))
                {
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        strId = mo.Properties["SerialNumber"].Value.ToString();
                        mo.Dispose();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                return "unknown";
                //throw;
            }
            return strId;
        }

        /// <summary> 
        /// 获取cpu序列号
        /// </summary>
        /// <returns></returns>
        public string GetCpuInfo()
        {
            string cpuInfo = "";
            try
            {
                using (ManagementClass cimobject = new ManagementClass("Win32_Processor"))
                {
                    ManagementObjectCollection moc = cimobject.GetInstances();

                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                return "unknown";
                //throw;
            }
            return cpuInfo;
        }

        /// <summary> 
        /// 获取硬盘ID
        /// </summary> 
        /// <returns></returns> 
        public string GetDiskID()
        {
            string diskName = "";
            string diskID = "";
            try
            {
                using (ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive"))
                {
                    ManagementObjectCollection moc1 = cimobject1.GetInstances();
                    foreach (ManagementObject mo in moc1)
                    {
                        diskName = mo.Properties["Model"].Value.ToString();
                        diskID = mo.Properties["SerialNumber"].Value.ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                return "unknown";
                //throw;
            }
            return diskName + diskID;
        }

        /// <summary> 
        /// 获取网卡硬件地址
        /// </summary> 
        /// <returns></returns> 
        public string GetMoAddress()
        {
            string MoAddress = "";
            try
            {
                using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
                {
                    ManagementObjectCollection moc2 = mc.GetInstances();
                    foreach (ManagementObject mo in moc2)
                    {
                        if ((bool)mo["IPEnabled"] == true)
                            MoAddress = mo["MacAddress"].ToString();
                        mo.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                return "unknown";
                //throw;
            }
            return MoAddress;
        }
    }
}
