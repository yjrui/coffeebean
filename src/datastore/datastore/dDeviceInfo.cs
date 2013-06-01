using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datastore
{
    public class dDeviceInfo
    {
        public String Label { get; set; }
        public String Manufacturer { get; set; }
        public String Product { get; set; }
        public String ESN { get; set; }
        public Int64 Lac { get; set; }
        public Int64 Cid { get; set; }
        public String HWRevision { get; set; }
        public String IMEI { get; set; }
        public Int64 Phone { get; set; }
        public String Platform { get; set; }
        public String ReturnedIMEI { get; set; }
        public String SWRevision { get; set; }
        public String IMSI { get; set; }
        public String ICCID { get; set; }
        public String LAI { get; set; }
        public String Phrase { get; set; }
        public String OwnerName { get; set; }
        public String OwnerID { get; set; }
        public String Description { get; set; }
    }
}
