using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using datastore;

namespace snake.Model
{
    public enum SmsSendReceive
    {
        Sent = 1,
        Received = 2
    }

    public class Sms
    {
        public SMSInfo SMSInfo { get; set; }
        public SmsSendReceive SendReceive { get; set; }
    }
}
