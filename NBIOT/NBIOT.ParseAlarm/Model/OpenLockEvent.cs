using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.ParseAlarm.Model
{
    public class OpenLockEvent
    {
        public int Header1 { get; set; }
        public int Header2 { get; set; }
        public int Header3 { get; set; }
        public int SyncByte { get; set; }
        public int BodyLen { get; set; }
        public int CmdType { get; set; }
        public int ConfirmCode { get; set; }
        public int Flags { get; set; }
        public int AppId { get; set; }
        public int OrderNo { get; set; }
        public int Battery { get; set; }
        public int CmdVersion { get; set; }
        public int EFlags { get; set; }
        public int EType { get; set; }
        public int KeyType { get; set; }
        public int KeyId { get; set; }
        public string OpenTime { get; set; }
        public string YuLiu { get; set; }
        public int CheckSum { get; set; }
    }
}
