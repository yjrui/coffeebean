using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasource
{
    public class dContact
    {
        private List<Property> m_properties = new List<Property>();

        public enum PropertyType
        {
            // Number
            NUMBER_NUM = 0x00000101,
            HOME_NUM = 0x00000102,
            MOBILE_NUM = 0x00000103,
            FAX_NUM = 0x00000104, 
            PAGER_NUM = 0x00000105,
            WORK_NUM = 0x00000106,
            MODEM_NUM = 0x00000107,
            VIDEO_NUM = 0x00000108,
            DTMF_NUM = 0x00000109,
            HOME_MOBILE_NUM = 0x00000114, 
            HOME_FAX_NUM = 0x00000115,
            HOME_PAGER_NUM = 0x00000116,
            HOME_MODEM_NUM = 0x00000117,
            HOME_VIDEO_NUM = 0x00000119,
            WORK_MOBILE_NUM = 0x0000011E,
            WORK_FAX_NUM = 0x0000011F,
            WORK_PAGER_NUM = 0x00000120,
            WORK_MODEM_NUM = 0x00000121,
            WORK_VIDEO_NUM = 0x00000122,
            CAR_NUMBER_NUM = 0x00000132,
            CAR_MOBILE_NUM = 0x00000133,
            CAR_FAX_NUM = 0x00000134,
            MOBILE_MODEM_NUM = 0x0000013C,
            MOBILE_FAX_NUM = 0x0000013D,
            ASSISTENT_NUM = 0x0000013E,
            OUTLOOK_OTHER_NUM = 0x0000013F,
            // Text
            FIRST_NAME = 0x00000201,
            LAST_NAME = 0x00000202, 
            JOB_TITLE = 0x00000203,
            OCCUPATION = 0x00000204,
            COMPANY = 0x00000205,
            UNIT = 0x00000206,
            POSTAL = 0x0000020A,
            EMAIL = 0x00000207,
            NOTE = 0x00000209,
            URL = 0x00000208,
            CITY = 0x0000020B,
            STATE = 0x0000020C,
            ZIP = 0x0000020D,
            COUNTRY = 0x0000020E,
            STREET = 0x0000020F,
            PERSONAL = 0x00000210,
            WVID = 0x00000211,
            POBOX = 0x00000212,
            ADDREXT = 0x00000213,
            MIDDLE_NAME = 0x00000214,
            NAME_PREFIX = 0x00000215,
            NAME_SUFFIX = 0x00000216,
            NICKNAME = 0x00000217,
            HOME_EMAIL = 0x00000218,
            HOME_URL = 0x00000219,
            WORK_EMAIL = 0x0000021A,
            WORK_URL = 0x0000021B,
            VOIP_PHONE = 0x00000250,
            GROUP_ID = 0x00000227,
            RINGTONE_LINK = 0x00000228,
            OVI = 0x00000229,
            GIZMO = 0x0000022A,
            FACEBOOK = 0x0000022B,
            MSN = 0x0000022C,
            YAHOO = 0x0000022D,
            GOOGLE = 0x0000022E,
            SKYPE = 0x0000022F,
            JABBER = 0x00000230,
            AIM = 0x00000231,
            ICQ = 0x00000232,
            QQ = 0x00000233,
            NETMEETING = 0x00000234,
            VOIP_PHONE_HOME = 0x00000251,
            VOIP_PHONE_WORK = 0x00000252,
            PTT = 0x00000223,
            SHAREVIEW = 0x0000021F,
            SIP = 0x00000220,
            ASSISTENT_NAME = 0x0000021E,
            SPOUSE = 0x00000221,
            CHILDREN = 0x00000222,
            // DateTime
            DATE = 0x00000301,
            // DataTimeEx
            BIRTHDATE = 0x00000401,
            ANNIVERSARY = 0x00000402,
            // Address
            HOME_ADDRESS = 0x00000502, 
            WORK_ADDRESS = 0x00000503,
            PREF_ADDRESS = 0x00000504, 
            // Binary
            ITEMPICTURE = 0x00000601
        }
        public class Property
        {
            public enum ValueFormat
            {
                TEXT, DATETIME, BINARY
            }
            public String name { get; internal set; }
            public Object value { get; internal set; }
            public ValueFormat format { get; internal set; }
        }

        internal long Id { get; set; }
        public string Label { get; internal set; }
        public Property[] getProperties()
        {
            return m_properties.ToArray();
        }

        internal int ClassId { get; set; }

        internal bool addProperty(PropertyType prop, mbs_contactpropertyset propEnt)
        {
            bool added = false;
            String name = Enum.GetName(typeof(PropertyType), prop);
            if (name == null) return added;
            if (isTextProp(prop) || isNumberProp(prop) || isAddressProp(prop))
            {
                m_properties.Add(new Property() 
                { 
                    name=name, 
                    value = propEnt.Text,
                    format = Property.ValueFormat.TEXT
                });
                added = true;
            }
            else if (isDateTimeProp(prop) && propEnt.Datetime.HasValue)
            {
                m_properties.Add(new Property() 
                { 
                    name = name, 
                    value = propEnt.Datetime.Value, 
                    format= Property.ValueFormat.DATETIME
                });
                added = true;
            }
            else if (isBinaryProp(prop))
            {
                m_properties.Add(new Property() 
                { 
                    name = name, 
                    value = propEnt.Binary,
                    format = Property.ValueFormat.BINARY
                });
                added = true;
            }
            else
            {
                added = false;
            }
            return added;
        }
        public bool isDialed()
        {
            return (ClassId & 0xFF000FFF) == 0x01000001;
        }
        public bool isMissed()
        {
            return (ClassId & 0xFF000FFF) == 0x01000003;
        }
        public bool isReceived()
        {
            return (ClassId & 0xFF000FFF) == 0x01000002;
        }
        public bool isFixed()
        {
            return (ClassId & 0xFF000FFF) == 0x01000004;
        }
        public bool isOwn()
        {
            return (ClassId & 0xFF000FFF) == 0x01000005;
        }
        public bool isInPhone()
        {
            return (ClassId & 0xFFFFF000) == 0x01008000;
        }
        public bool isInSIM()
        {
            return (ClassId & 0xFFFFF000) == 0x0100C000;
        }
        private bool isTextProp(PropertyType prop)
        {
            return ((UInt32)prop & 0xFFFFFF00) == 0x00000100;
        }

        private bool isNumberProp(PropertyType prop)
        {
            return ((UInt32)prop & 0xFFFFFF00) == 0x00000200;
        }
        private bool isDateTimeProp(PropertyType prop)
        {
            return ((UInt32)prop & 0xFFFFFF00) == 0x00000300;
        }

        private bool isDateTimeExProp(PropertyType prop)
        {
            return ((UInt32)prop & 0xFFFFFF00) == 0x00000400;
        }
        private bool isAddressProp(PropertyType prop)
        {
            return ((UInt32)prop & 0xFFFFFF00) == 0x00000500;
        }
        private bool isBinaryProp(PropertyType prop)
        {
            return ((UInt32)prop & 0xFFFFFF00) == 0x00000600;
        }
    }
}
