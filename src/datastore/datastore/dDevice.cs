using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datastore
{
    public class dDevice
    {
        private mobilespyEntities m_db;
        internal Int64 m_session;

        internal dDevice(mobilespyEntities db, Int64 session)
        {
            m_db = db;
            m_session = session;
        }

        public void addCall(String number, int dur, int status, DateTime begin, DateTime end, int Class)
        {
            mbs_callset callEnt = new mbs_callset { MBS_SessionId = m_session };
            callEnt.StartTimeStamp = begin;
            callEnt.StopTimeStamp = end;
            callEnt.Number = number;
            callEnt.Status = status;
            callEnt.Duration = dur;
            callEnt.Class = Class;
            m_db.mbs_callset.Add(callEnt);
            m_db.SaveChanges();
        }

        public dContact createContact(String label, int Class)
        {
            mbs_contactset cnt = new mbs_contactset { MBS_SessionId = m_session };
            cnt.Label = label;
            cnt.Class = Class;
            m_db.mbs_contactset.Add(cnt);
            m_db.SaveChanges();
            return new dContact(m_db, cnt.Id);
        }

        public void addSMS(int Class, SMSInfo smsInfo = null)
        {
            mbs_smsset smsEnt = new mbs_smsset { MBS_SessionId = m_session, Class = Class };
            if (smsInfo != null)
            {
                smsEnt.FromNumber = smsInfo.FromNumber;
                smsEnt.Head = smsInfo.Head;
                smsEnt.ReceivedTimeStamp = smsInfo.ReceivedTimeStamp;
                smsEnt.ReceivedTimeZone = smsInfo.ReceivedTimeZone;
                smsEnt.SentTimeStamp = smsInfo.SentTimeStamp;
                smsEnt.ServiceCenter = smsInfo.ServiceCenter;
                smsEnt.State = smsInfo.State;
                smsEnt.Status = (sbyte)smsInfo.Status;
                smsEnt.Text = smsInfo.Text;
                smsEnt.ToNumber = smsInfo.ToNumber;
                smsEnt.Type = smsInfo.Type;
            }
            m_db.mbs_smsset.Add(smsEnt);
            m_db.SaveChanges();
        }

        public dDevFileSystem createDevFileSystem(String mappingRootPath)
        {
            String path = mappingRootPath + "\\" + m_db.mbs_sessionset.Find(m_session).MBS_DeviceUID + "\\" + m_session;
            mbs_filesystemset fsEnt = new mbs_filesystemset { MBS_SessionId = m_session, MappingRootPath = path };
            m_db.mbs_filesystemset.Add(fsEnt);
            m_db.SaveChanges();
            return new dDevFileSystem(path);
        }
    }
}
