using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NBIOT.Common
{
    public class FunHelper
    {
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", "1498974232", DateTime.Now.ToString("HHmmss"), ran.Next(1000, 9999));
        }

        public static DateTime? UTCToDateTime(string timeStr)
        {
            if (string.IsNullOrEmpty(timeStr))
            {
                return null;
            }
            return DateTime.ParseExact(timeStr.Replace("T", "").Replace("Z", ""), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).AddHours(8);
        }

        public static string GetStatus(string status)
        {
            var rst = string.Empty;
            switch (status)
            {
                case "PENDING":
                    rst = "超期";
                    break;
                case "SENT":
                    rst = "已发送";
                    break;
                case "DELIVERED":
                    rst = "已送达";
                    break;
                case "SUCCESSFUL":
                    rst = "执行成功";
                    break;

            }

            return rst;
        }
    }
}
