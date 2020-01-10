
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace ConsoleApplication1
{
	public class CoreCount
	{
		public static int CoreCounter()
		{
			int coreCount = 0;
			foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
			{
				coreCount += int.Parse(item["NumberOfCores"].ToString());
			}
			return coreCount;
		}
	}
}