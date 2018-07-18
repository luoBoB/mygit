using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.ParseAlarm.Model
{
    public class OpenLock
    {
        public int SyncByte { get; set; }
        public int BodyLen { get; set; }
        public int CmdType { get; set; }
        public int Status { get; set; }
        public int Flags { get; set; }
        public int AppId { get; set; }
        public int OrderNo { get; set; }
        public int SDID1 { get; set; }
        public int SDID2 { get; set; }
        public string Pwd { get; set; }
        public int DelayLockSeconds1{ get; set; }
        public int DelayLockSeconds2 { get; set; }
        public int CheckSum { get; set; }
    }
}
