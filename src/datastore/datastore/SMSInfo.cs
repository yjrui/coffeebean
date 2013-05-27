using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datastore
{
    public class SMSInfo
    {
        public String FromNumber { get ; set ;}
        public DateTime ReceivedTimeStamp { get; set; }
        public DateTime SentTimeStamp { get; set; }
        public String Head { get; set; }
        public Int64 ReceivedTimeZone { get; set; }
        public String ServiceCenter { get; set; }
        public Int16 State { get; set; }
        public Byte Status { get; set; }
        public String Text { get; set; }
        public String ToNumber { get; set; }
        public Int16 Type { get; set; }
    }
}
