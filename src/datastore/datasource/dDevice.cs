using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasource
{
    public class dDevice
    {
        private DataSource m_ds = null;
        private List<Session> m_sessions = new List<Session>();
        private SortedDictionary<Int64, List<dCall>> m_calls = new SortedDictionary<Int64, List<dCall>>();
        private SortedDictionary<Int64, List<Session>> m_assSessions = new SortedDictionary<Int64, List<Session>>();
        private SortedDictionary<Int64, List<dContact>> m_contacts = new SortedDictionary<Int64, List<dContact>>();
        private SortedDictionary<Int64, List<dSMS>> m_smsList = new SortedDictionary<Int64, List<dSMS>>();
        private SortedDictionary<Int64, List<String>> m_fsRootsList = new SortedDictionary<Int64, List<String>>();

        internal dDevice(DataSource ds) { m_ds = ds; }

        private mobilespyEntities getDB()
        {
            return m_ds.m_db;
        }
        

        public String UID { get; internal set; }
        public String Type { get; internal set; }
        public String Label { get; internal set; }
        public String Manufacturer { get; internal set; }
        public String Product { get; internal set; }
        public String ESN { get; internal set; }
        public Nullable<Int64> Lac { get; internal set; }
        public Nullable<Int64> Cid { get; internal set; }
        public String HWRevision { get; internal set; }
        public String IMEI { get; internal set; }
        public Nullable<Int64> Phone { get; internal set; }
        public String Platform { get; internal set; }
        public String ReturnedIMEI { get; internal set; }
        public String SWRevision { get; internal set; }
        public String IMSI { get; internal set; }
        public String ICCID { get; internal set; }
        public String LAI { get; internal set; }
        public String Phrase { get;internal set; }

        /**
         * get all sessions in time order
         * return empty array if no session associates to the device
         */
        public Session[] getSessions()
        {
            if (m_sessions.Count > 0) return m_sessions.ToArray();
            
            var result = from s in (getDB().mbs_sessionset) where s.MBS_DeviceUID == UID orderby s.Id ascending select s;
            foreach (mbs_sessionset sesEnt in result)
            {
                Session ses = new Session(this);
                ses.Id = sesEnt.Id;
                ses.ParentSessionId = sesEnt.ParentSessionId;
                ses.Timestamp = sesEnt.Timestamp;
                m_sessions.Add(ses);
            }
            return m_sessions.ToArray();
        }

        /**
         * get all calls of certain session
         * return empty array if no call
         */
        public dCall[] getCalls(Session ses)
        {
            if (ses == null) return new dCall[0];
            List<dCall> calls = null;
            if (m_calls.TryGetValue(ses.Id, out calls)) return calls.ToArray();
            calls = new List<dCall>();
            var result = from c in (getDB().mbs_callset) where c.MBS_SessionId == ses.Id select c;
            foreach (mbs_callset callEnt in result)
            {
                dCall call = new dCall();
                call.ClassId = callEnt.Class;
                call.Duration = callEnt.Duration;
                call.Id = callEnt.Id;
                call.Number = callEnt.Number;
                call.StartTimeStamp = callEnt.StartTimeStamp;
                call.Status = callEnt.Status;
                call.StopTimeStamp = callEnt.StopTimeStamp;
                calls.Add(call); 
            }
            m_calls.Add(ses.Id, calls);
            return calls.ToArray();
        }

        /**
         * get all sub sessions of certain session
         * return empty array if no sub session
         */
        public Session[] getAssociateDeviceSessions(Session ses)
        {
            if (ses == null) return new Session[0];
            List<Session> assSessions = null;
            if (m_assSessions.TryGetValue(ses.Id, out assSessions))
            {
                return assSessions.ToArray();
            }
            assSessions = new List<Session>();

            var result = from s in (this.getDB().mbs_sessionset)
                         where s.ParentSessionId == ses.Id
                         select s;
            mbs_sessionset[] sesEnts = result.ToArray<mbs_sessionset>();
            dDevice[] devices = m_ds.getDevices();
            foreach (mbs_sessionset sesEnt in sesEnts)
            {
                foreach (dDevice dev in devices)
                {
                    if (dev.UID == sesEnt.MBS_DeviceUID && dev != this )
                    {
                        Session[] sessions = dev.getSessions();
                        Session session = new Session(dev) { Id = sesEnt.Id };
                        int idx = Array.BinarySearch<Session>(sessions, session);
                        if (idx >= 0)
                        {
                            assSessions.Add(sessions[idx]);
                            break;
                        }
                    }
                }
            }
            m_assSessions.Add(ses.Id, assSessions);
            return assSessions.ToArray();
        }

        /**
         * get all contacts of certain session
         * return empty array if no contact
         */
        public dContact[] getContacts(Session ses)
        {
            if (ses == null) return new dContact[0];
            List<dContact> contacts = null;
            if (m_contacts.TryGetValue(ses.Id, out contacts)) return contacts.ToArray();
            contacts = new List<dContact>();

            var result = from c in getDB().mbs_contactset
                         where c.MBS_SessionId == ses.Id
                         select c;
            mbs_contactset[] contactEnts = result.ToArray<mbs_contactset>();
            foreach (mbs_contactset contactEnt in contactEnts)
            {
                dContact contact = new dContact();
                contact.ClassId = contactEnt.Class;
                contact.Id = contactEnt.Id;
                contact.Label = contactEnt.Label;
                // get all properties of this contact
                var props = from p in getDB().mbs_contactpropertyset
                            where p.MBS_ContactId == contactEnt.Id
                            select p;
                foreach (mbs_contactpropertyset prop in props)
                {
                    Object obj = Enum.ToObject(typeof(dContact.PropertyType), prop.Property);
                    contact.addProperty((dContact.PropertyType)obj, prop);
                }
                contacts.Add(contact);
            }

            m_contacts.Add(ses.Id, contacts);
            return contacts.ToArray();
        }

        public dContact[] getDialedContacts(Session ses)
        {
            dContact[] contacts = getContacts(ses);
            List<dContact> retContacts = new List<dContact>();
            foreach (dContact contact in contacts)
            {
                if (contact.isDialed())
                {
                    retContacts.Add(contact);
                }
            }
            return retContacts.ToArray();
        }

        public dContact[] getMissedContacts(Session ses)
        {
            dContact[] contacts = getContacts(ses);
            List<dContact> retContacts = new List<dContact>();
            foreach (dContact contact in contacts)
            {
                if (contact.isMissed())
                {
                    retContacts.Add(contact);
                }
            }
            return retContacts.ToArray();
        }

        public dContact[] getReceivedContacts(Session ses)
        {
            dContact[] contacts = getContacts(ses);
            List<dContact> retContacts = new List<dContact>();
            foreach (dContact contact in contacts)
            {
                if (contact.isReceived())
                {
                    retContacts.Add(contact);
                }
            }
            return retContacts.ToArray();
        }

        public dSMS[] getSMSList(Session ses)
        {
            if (ses == null) return new dSMS[0];
            List<dSMS> smsList = null;
            if (m_smsList.TryGetValue(ses.Id, out smsList)) return smsList.ToArray();
            smsList = new List<dSMS>();

            var result = from s in getDB().mbs_smsset
                         where s.MBS_SessionId == ses.Id
                         select s;
            foreach (mbs_smsset smsEnt in result)
            {
                dSMS sms = new dSMS();
                sms.ClassId = smsEnt.Class;
                sms.FromNumber = smsEnt.FromNumber;
                sms.Head = smsEnt.Head;
                sms.Id = smsEnt.Id;
                sms.ReceivedTimeStamp = smsEnt.ReceivedTimeStamp;
                sms.ReceivedTimeZone = smsEnt.ReceivedTimeZone;
                sms.SentTimeStamp = smsEnt.SentTimeStamp;
                sms.ServiceCenter = smsEnt.ServiceCenter;
                sms.State = (!smsEnt.State.HasValue || (smsEnt.State & 0x0F) > (short)dSMS.SMSState.INVALID || smsEnt.State < 0) ?
                    dSMS.SMSState.INVALID : (dSMS.SMSState)smsEnt.State;
                sms.Status = smsEnt.Status;
                sms.Text = smsEnt.Text;
                sms.ToNumber = smsEnt.ToNumber;
                sms.Type = (!smsEnt.Type.HasValue || smsEnt.Type > (short)dSMS.SMSType.INVALID || smsEnt.Type < 0) ? 
                    dSMS.SMSType.INVALID : (dSMS.SMSType)smsEnt.Type;
                smsList.Add(sms);
            }

            m_smsList.Add(ses.Id, smsList);
            return smsList.ToArray();
        }

        public dSMS[] getSentSMSList(Session ses)
        {
            dSMS[] smsList = getSMSList(ses);
            List<dSMS> retSMSList = new List<dSMS>();
            foreach (dSMS sms in smsList)
            {
                if (sms.isSent())
                {
                    retSMSList.Add(sms);
                }
            }
            return retSMSList.ToArray();
        }

        public dSMS[] getDeletedSMSList(Session ses)
        {
            dSMS[] smsList = getSMSList(ses);
            List<dSMS> retSMSList = new List<dSMS>();
            foreach (dSMS sms in smsList)
            {
                if (sms.isDeleted())
                {
                    retSMSList.Add(sms);
                }
            }
            return retSMSList.ToArray();
        }
        public dSMS[] getReceivedSMSList(Session ses)
        {
            dSMS[] smsList = getSMSList(ses);
            List<dSMS> retSMSList = new List<dSMS>();
            foreach (dSMS sms in smsList)
            {
                if (!sms.isCommand() && !sms.isInOutbox() && !sms.isSent() && 
                    (sms.State == dSMS.SMSState.RECEIVED_READ || 
                    sms.State == dSMS.SMSState.RECEIVED_UNREAD))
                {
                    retSMSList.Add(sms);
                }
            }
            return retSMSList.ToArray();
        }
        public String[] getFileSystemRoots(Session ses)
        {
            if (ses == null) return new String[0];
            List<String> fsRoots = null;
            if (m_fsRootsList.TryGetValue(ses.Id, out fsRoots)) return fsRoots.ToArray();
            fsRoots = new List<String>();
            var result = from r in getDB().mbs_filesystemset
                         where r.MBS_SessionId == ses.Id
                         select r.MappingRootPath;
            foreach (String r in result)
            {
                fsRoots.Add(r);
            }
            m_fsRootsList.Add(ses.Id, fsRoots);
            return fsRoots.ToArray();
        }
    }
}
