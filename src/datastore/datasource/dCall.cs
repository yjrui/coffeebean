using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasource
{
    public class dCall
    {
        internal long Id { get; set; }
        public string Number { get; internal set; }
        public int Duration { get; internal set; }
        public int Status { get; internal set; }
        public DateTime StartTimeStamp { get; internal set; }
        public DateTime StopTimeStamp { get; internal set; }
        internal int ClassId { get; set; }
    }
}
