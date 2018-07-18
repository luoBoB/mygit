using NBIOT.Common;
using NBIOT.ParseAlarm.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.ParseAlarm
{
    public class Command
    {
        /// <summary>
        /// OpenLock
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public object GetOpenLockPara(Dictionary<string, object> data)
        {
            return new OpenLock()
            {
                SyncByte = 0x5A,
                BodyLen = 0x1B,
                CmdType = 0x01,
                Status = 0x04,
                Flags = 0x00,
                AppId = DataHelper.HexToInt32("0384"),
                OrderNo = 0x14,
                SDID1 = DataHelper.HexToInt32("A137"),
                SDID2 = DataHelper.HexToInt32("8101"),
                Pwd = DataHelper.HEXToASCII("313233343536000000000000"),
                DelayLockSeconds1 = DataHelper.HexToInt32("0000"),
                DelayLockSeconds2 = DataHelper.HexToInt32("00FF"),
                CheckSum = 0xD1
            };
        }
    }
}
