using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasource
{
    /**
     * the instance of DataSource is a session of data access. All 
     * objects created from it are only available during its lifetime.
     * It caches all of data internally in the sake of performance.
     * The caller needs to create a new instance of DataSource to get
     * the latest data (e.g. refresh the all data)
     */
    public class DataSource : IDisposable
    {
        internal mobilespyEntities m_db = null;
        private List<dDevice> m_devices = new List<dDevice>();
        private void open()
        {
            close();
            m_db = new mobilespyEntities();
        }

        private void close()
        {
            if (m_db != null)
            {
                m_db.Dispose();
                m_db = null;
            }
            m_devices.Clear();
        }

        public DataSource()
        {
            open();
        }

        public dDevice[] getDevices()
        {
            if (m_devices.Count > 0) return m_devices.ToArray();
 
            var result = from d in m_db.mbs_deviceset select d;
            foreach (mbs_deviceset devEnt in result)
            {
                dDevice dev = new dDevice(this);
                dev.Cid = devEnt.Cid;
                dev.ESN = devEnt.ESN;
                dev.HWRevision = devEnt.HWRevision;
                dev.ICCID = devEnt.ICCID;
                dev.IMEI = devEnt.IMEI;
                dev.IMSI = devEnt.IMSI;
                dev.Label = devEnt.Label;
                dev.Lac = devEnt.Lac;
                dev.LAI = devEnt.LAI;
                dev.Manufacturer = devEnt.Manufacturer;
                dev.Phone = devEnt.Phone;
                dev.Phrase = devEnt.Phrase;
                dev.Platform = devEnt.Platform;
                dev.Product = devEnt.Platform;
                dev.ReturnedIMEI = devEnt.ReturnedIMEI;
                dev.SWRevision = devEnt.SWRevision;
                dev.UID = devEnt.UID;
                dev.Type = devEnt.Type;
                m_devices.Add(dev);
            }

            return m_devices.ToArray();
        }

        void IDisposable.Dispose()
        {
            close();
        }
    }
}
