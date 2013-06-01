using System;
using System.Transactions;

namespace datastore
{
    public class DataStore : IDisposable
    {
        private mobilespyEntities m_db = null;
        private TransactionScope m_tran = null;

        public DataStore()
        {
            open();
        }

        public void commit()
        {
            if (m_tran != null)
            {
                m_tran.Complete();
                m_tran.Dispose();
            }
            m_tran = new TransactionScope();
        }

        private void open()
        {
            if (m_db == null)
            {
                m_db = new mobilespyEntities();
            }
            if (m_tran == null)
            {
                m_tran = new TransactionScope();
            }
        }

        private void close()
        {
            if (m_tran != null)
            {
                m_tran.Dispose();
                m_tran = null;
            }
            if (m_db != null)
            {
                m_db.Dispose();
                m_db = null;
            }
        }

        public dDevice createDevice(String uid, String type, dDeviceInfo devInfo = null, dDevice parent = null)
        {
            if (m_db == null) return null;
            // add device entity only if it is a new one
            mbs_deviceset devEnt = m_db.mbs_deviceset.Find(uid);
            if (devEnt == null)
            {
                devEnt = new mbs_deviceset { UID = uid, Type = type };
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
                m_db.mbs_deviceset.Add(devEnt);
            }

            // add session entity
            mbs_sessionset sessionEnt = new mbs_sessionset { 
                MBS_DeviceUID = uid, 
                Timestamp = DateTime.Now
            };
            if (devInfo != null)
            {
                sessionEnt.Description = devInfo.Description;
                sessionEnt.OwnerID = devInfo.OwnerID;
                sessionEnt.OwnerName = devInfo.OwnerName;
            }

            if (parent != null)
            {
                sessionEnt.ParentSessionId = parent.m_session;
            }
            m_db.mbs_sessionset.Add(sessionEnt);

            m_db.SaveChanges();

            return new dDevice(m_db, sessionEnt.Id); 
        }

        public void Dispose()
        {
            close();
        }
    }
}
