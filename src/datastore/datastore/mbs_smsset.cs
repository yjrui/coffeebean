//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datastore
{
    using System;
    using System.Collections.Generic;
    
    public partial class mbs_smsset
    {
        public long Id { get; set; }
        public string FromNumber { get; set; }
        public Nullable<System.DateTime> ReceivedTimeStamp { get; set; }
        public Nullable<System.DateTime> SentTimeStamp { get; set; }
        public string Head { get; set; }
        public Nullable<long> ReceivedTimeZone { get; set; }
        public string ServiceCenter { get; set; }
        public Nullable<short> State { get; set; }
        public Nullable<sbyte> Status { get; set; }
        public string Text { get; set; }
        public string ToNumber { get; set; }
        public Nullable<short> Type { get; set; }
        public long MBS_SessionId { get; set; }
        public int Class { get; set; }
    
        public virtual mbs_sessionset mbs_sessionset { get; set; }
    }
}
