using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mysql_connection_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new mobilespyEntities();
            //store.mbs_callset.AddObject(new mbs_callset()
            //{
            //    Number = "123",
            //    Duration = 50,
            //    Status = 1,
            //    StartTimeStamp = DateTime.Now,
            //    StopTimeStamp = DateTime.Now,
            //    MBS_SessionId = 0,
            //    Class = 0
            //});

            var devEnt = new mbs_deviceset { UID = Guid.NewGuid().ToString(), Type = "Phone" };
            DeviceInfo devInfo = new DeviceInfo();
            devInfo.Label = "IPHONE 5";
            if (devInfo != null)
            {
                devEnt.Label = devInfo.Label;
                devEnt.Manufacturer = devInfo.Manufacturer;
                devEnt.Product = devInfo.Product;
                devEnt.ESN = devInfo.ESN;
                devEnt.Lac = devInfo.Lac;
                devEnt.Cid = devInfo.Cid;
                devEnt.HWRevision = devInfo.HWRevision;
                devEnt.IMEI = devInfo.IMEI;
                devEnt.Phone = devInfo.Phone;
                devEnt.Platform = devInfo.Platform;
                devEnt.ReturnedIMEI = devInfo.ReturnedIMEI;
                devEnt.SWRevision = devInfo.SWRevision;
                devEnt.IMSI = devInfo.IMSI;
                devEnt.ICCID = devInfo.ICCID;
                devEnt.LAI = devInfo.LAI;
                devEnt.Phrase = devInfo.Phrase;
            }
            //store.mbs_deviceset.AddObject(devEnt);
            store.AddTombs_deviceset(devEnt);

            // add session entity
            //mbs_sessionset sessionEnt = new mbs_sessionset { MBS_DeviceUID = Guid.NewGuid().ToString(), Timestamp = DateTime.Now };
            //store.AddTombs_sessionset(sessionEnt);

            //store.mbs_sessionset.AddObject(sessionEnt);
            

            store.SaveChanges();
        }
    }

    public class DeviceInfo
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
    }
}
