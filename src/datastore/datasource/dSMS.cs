using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasource
{
    public class dSMS
    {
        public enum SMSType
        {
            EMPTY = 0,
            DELIVER,
            SUBMIT,
            STATUS_REPORT,
            COMMAND,
            INVALID
        }
        public enum SMSState
        {
            RECEIVED_UNREAD = 0,
            RECEIVED_READ,
            STORED_UNSENT,
            STORED_SENT,
            INVALID
        }
        internal long Id { get; set; }
        public string FromNumber { get; internal set; }
        public Nullable<System.DateTime> ReceivedTimeStamp { get; internal set; }
        public Nullable<System.DateTime> SentTimeStamp { get; internal set; }
        public string Head { get; internal set; }
        public Nullable<long> ReceivedTimeZone { get; internal set; }
        public string ServiceCenter { get; internal set; }
        public SMSState State { get; internal set; }
        public Nullable<sbyte> Status { get; internal set; }
        public string Text { get; internal set; }
        public string ToNumber { get; internal set; }
        public SMSType Type { get; internal set; }
        internal int ClassId { get; set; }

        public bool isInOutbox()
        {
            return (ClassId & 0xFF000FFF) == 0x02000001;
        }
        public bool isSent()
        {
            return (ClassId & 0xFF000FFF) == 0x02000002;
        }
        public bool isDeleted()
        {
            return (ClassId & 0xFF000FFF) == 0x02000003;
        }
        public bool isCommand()
        {
            return (ClassId & 0xFF000FFF) == 0x02000004;
        }
        public bool isInPhone()
        {
            return (ClassId & 0xFFFFF000) == 0x02008000;
        }
        public bool isInSIM()
        {
            return (ClassId & 0xFFFFF000) == 0x0200C000;
        }
        
    }
}
