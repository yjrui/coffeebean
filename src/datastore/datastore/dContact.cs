using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datastore
{
    public class dContact
    {
        private mobilespyEntities m_db;
        private Int64 m_id;

        internal dContact(mobilespyEntities db, Int64 id)
        {
            m_db = db;
            m_id = id;
        }

        public void addTextProp(int prop, String text)
        {
            mbs_contactpropertyset cntPropEnt = new mbs_contactpropertyset { MBS_ContactId = m_id };
            cntPropEnt.Property = prop;
            cntPropEnt.Text = text;
            m_db.mbs_contactpropertyset.Add(cntPropEnt);
            m_db.SaveChanges();
        }

        public void addDateTimeProp(int prop, DateTime dt)
        {
            mbs_contactpropertyset cntPropEnt = new mbs_contactpropertyset { MBS_ContactId = m_id };
            cntPropEnt.Property = prop;
            cntPropEnt.Datetime = dt;
            m_db.mbs_contactpropertyset.Add(cntPropEnt);
            m_db.SaveChanges();
        }

        public void addBinaryProp(int prop, byte[] data, int offset, int count)
        {
            mbs_contactpropertyset cntPropEnt = new mbs_contactpropertyset { MBS_ContactId = m_id };
            cntPropEnt.Property = prop;
            cntPropEnt.Binary = new byte[count];
            Buffer.BlockCopy(data, offset, cntPropEnt.Binary, 0, count);
            m_db.mbs_contactpropertyset.Add(cntPropEnt);
            m_db.SaveChanges();
        }
    }
}
