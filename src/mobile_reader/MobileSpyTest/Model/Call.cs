using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileSpyTest.Model
{
    public enum CallType
    {
        Dialed = 1,
        Received = 2,
        Missed = 3
    }

    public class Call
    {
        public CallType Type { get; set; }
        public string Number { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public int Class { get; set; }
    }
}
