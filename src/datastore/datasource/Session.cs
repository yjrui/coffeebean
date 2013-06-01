using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasource
{
    public class Session : IComparable<Session>
    {
        private dDevice m_device = null;
        internal Session(dDevice device) {m_device = device;}

        public Int64 Id { get; internal set; }
        public DateTime Timestamp { get; internal set; }
        public String Description { get; internal set; }
        public dDevice Device() { return m_device; }
        internal Nullable<Int64> ParentSessionId { get; set; }
        internal String OwnerName{ get; set; }
        internal String OwnerID { get; set; }

        int IComparable<Session>.CompareTo(Session other)
        {
            return (Id > other.Id) ? 1 : ((Id < other.Id) ? -1 : 0);
        }
    }
}
