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
    
    public partial class mbs_filesystemset
    {
        public long Id { get; set; }
        public string MappingRootPath { get; set; }
        public long MBS_SessionId { get; set; }
    
        public virtual mbs_sessionset mbs_sessionset { get; set; }
    }
}
