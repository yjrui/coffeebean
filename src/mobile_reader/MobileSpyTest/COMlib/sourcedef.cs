using System;
namespace COMlib
{
	internal class sourcedef
	{
		public const int CACHE_INVALIDATE = 2;
		public const int WM_APP = 32768;
		public const int WM_CALL = 32768;
		public const int WM_SMS = 33024;
		public const int PLATFORM_PROPRIETARY = 0;
		public const int PLATFORM_SYMBIAN = 1;
		public const int PLATFORM_LINUX = 2;
		public const int PLATFORM_RIM = 3;
		public const int PLATFORM_WINDOWSCE = 4;
		public const int PLATFORM_IPHONE = 5;
		public const int PLATFORM_ANDROID = 7;
		public const int PLATFORM_WP7 = 8;
		public const int STORAGE_UNKNOWN = 0;
		public const int STORAGE_INTERNAL = 1;
		public const int STORAGE_EXTERNAL = 2;
		public const int SRCCAP_INVALID = 0;
		public const int SRCCAP_PHONEBOOK = 256;
		public const int SRCCAP_SMS = 512;
		public const int SRCCAP_KEYBOARD = 768;
		public const int SRCCAP_CALL = 1024;
		public const int SRCCAP_FILESYSTEM = 1792;
		public const int SRCCAP_ORGANIZER = 2048;
		public const int SRCCAP_NETWORKS = 2304;
		public const int SRCCAP_COMMAND = 2560;
		public const int SRCCAP_MMS = 2816;
		public const int SRCCAP_USSD = 3072;
		public const int SRCCAP_GLOBAL = -1;
		public const int SRCCLASS_INVALID = 0;
		public const int SRCCLASS_GLOBAL = -1;
		public const int SRCCLASS_TREEUNKNOWN = 0;
		public const int SRCSTREAM_GLOBAL = -1;
		public const int CLASS_IS_NOT_BOTH = 32768;
		public const int CLASS_IS_SLAVE = 16384;
		public const int SRCPARAM_STREAMCOUNT = -65535;
		public const int SRCPARAM_VOLATILE = -65534;
		public const int SRCPARAM_GROWABLE = -65533;
		public const int SRCPARAM_TREETYPE = -65532;
		public const int SRCPARAM_TREECHILDS = -65531;
		public const int SRCPARAM_TREEPARENT = -65530;
		public const int SRCPARAM_CLASSNAME = -65529;
		public const int SRCPARAM_DISABLEDOPS = -65528;
		public const int SRCPARAM_READONLY = -65528;
		public const int SRCPARAM_IMPORTANTPARAMS = -65527;
		public const int SRCPARAM_SNAPSHOTSCOUNT = -65526;
		public const int SRCPARAM_SNAPSHOTCREATION = -65525;
		public const int SRCPARAM_PERSTREAMFALLBACK = -65524;
		public const int DISOP_WRITE = 1;
		public const int DISOP_DELETE = 2;
		public const int DISOP_ALL = -1;
		public const int SRCPROP_TYPE = -64536;
		public const int SRCPROP_STATUS = -64535;
		public const int SRCPROP_MANUFACTURER = -64534;
		public const int SRCPROP_PRODUCT = -64533;
		public const int SRCPROP_UNIQUEID = -64532;
		public const int SRCPROP_SESSIONID = -64531;
		public const int SRCPROP_FOLDER = -64530;
		public const int SRCPROP_ICON = -64529;
		public const int SRCPROP_LABEL = -64528;
		public const int SRCPROP_PRODUCTPICTURE = -64527;
		public const int SRCPROP_DATASOURCEPICTURE = -64526;
		public const int SRCPROP_PORTNAME = -64525;
		public const int SRCPROP_LASTCONNECTION = -64524;
		public const int SRCPROP_DEVICEFULLNAME = -64523;
		public const int SRCPROP_NETWORKS = -64522;
		public const int SRCPROP_USEDNETWORKS = -64521;
		public const int SRCPROP_DEVICEID = -64520;
		public const int SRCPROP_USERAGENT = -64519;
		public const int SRCPROP_MELASTCONNECTION = -64518;
		public const int SRCPROP_BATTERYSTATUS = -64516;
		public const int SRCPROP_SIGNALQUALITY = -64515;
		public const int SRCPROP_IMEI = -64514;
		public const int SRCPROP_IMSI = -64513;
		public const int SRCPROP_HWREVISION = -64512;
		public const int SRCPROP_SWREVISION = -64511;
		public const int SRCPROP_DATETIME = -64510;
		public const int SRCPROP_NETWORK = -64509;
		public const int SRCPROP_PORTID = -64508;
		public const int SRCPROP_ICCID = -64507;
		public const int SRCPROP_PHASE = -64506;
		public const int SRCPROP_CHVSTATUS = -64505;
		public const int SRCPROP_READERSTATE = -64504;
		public const int SRCPROP_PINCHANGE = -64503;
		public const int SRCPROP_PINUNBLOCK = -64502;
		public const int SRCPROP_PARENTSOURCESESSION = -64501;
		public const int SRCPROP_DISPLAYINFO = -64500;
		public const int SRCPROP_GPRSINFO = -64499;
		public const int SRCPROP_CONNECTIVITY = -64498;
		public const int SRCPROP_CHECKEDIMEI = -64497;
		public const int SRCPROP_DATAFILE = -64496;
		public const int SRCPROP_ICONPATH = -64495;
		public const int SRCPROP_LOCI = -64494;
		public const int SRCPROP_CHILDSOURCESESSION = -64493;
		public const int SRCPROP_CONNECTORINFO = -64491;
		public const int SRCPROP_PLATFORM = -64490;
		public const int SRCPROP_PORTDETAILS = -64489;
		public const int SRCPROP_ESN = -64488;
		public const int SRCPROP_TRANSPORTSERVICES = -64487;
		public const int SRCPROP_APNPOSITIONS = -64486;
		public const int SRCPROP_MEDATETIME = -64485;
		public const int SRCPROP_MULTIMEDIAFORMATS = -64484;
		public const int SRCPROP_FORENSICSLOGID = -64483;
		public const int SRCPROP_GROUPID = -64482;
		public const int SRCPROP_USERNAME = -64481;
		public const int SRCPROP_OWNERNAME = -64480;
		public const int SRCPROP_OWNERNUMBER = -64479;
		public const int SRCPROP_BACKUPSTART = -64478;
		public const int SRCPROP_BACKUPEND = -64477;
		public const int SRCPROP_SOURCECOMMENT = -64476;
		public const int SRCPROP_BACKUPLICENSEINFO = -64475;
		public const int SRCPROP_SIGNALQUALITYEX = -64473;
		public const int SRCPROP_TSSTART = -64236;
		public const int SRCPROP_TSEND = -64137;
		public const int SRCPROP_ORIG_TYPE = -64436;
		public const int SRCPROP_ORIG_UNIQUEID = -64435;
		public const int SRCPARAM_PROTOCOLS = -64336;
		public const int SRCPARAM_CURRENTPROTOCOL = -64335;
		public const int SRCPARAM_PROTOCOLMAP = -64334;
		public const int UNIQUEID_SIZE = 20;
		public const int UID_IMSI = 0;
		public const int UID_IMEI = 1;
		public const int UID_BACKUP = 2;
		public const int UID_EXTERNAL = 3;
		public const int UID_READER = 4;
		public const int UID_TEST = 5;
		public const int UID_ESN = 6;
		public const int UID_SIMFAKE = 7;
		public const int UID_INVALID = 8;
		public const int UID_SYMBIANWITHOUTCONNECTOR = 9;
		public const int UID_BTADDR = 10;
		public const int UID_IRDAADDR = 11;
		public const int UID_WINDOWSCE = 12;
		public const int UID_IPHONE = 13;
		public const int UID_FAKE = 255;
		public const int SRCTYPE_UNKNOWN = 0;
		public const int SRCTYPE_MOBILEPHONE = 1;
		public const int SRCTYPE_SIMCARD = 2;
		public const int SRCTYPE_READER = 3;
		public const int SRCTYPE_BACKUP = 4;
		public const int SRCTYPE_EXTERNAL = 5;
		public const int SRCTYPE_OUTLOOK = 5;
		public const int SRCTYPE_OUTLOOKEXPRESS = 5;
		public const int SRCTYPE_ARCHIVE = 6;
		public const int SRCSTATUS_ONLINE = 0;
		public const int SRCSTATUS_OFFLINE = 1;
		public const int SRCSTATUS_NOTLICENSED = 2;
		public const int SRCSTATUS_CONNECTORNOTPRESENT = 4;
		public const int SRCSTATUS_NOTPAIRED = 8;
		public const int SRCSTATUS_NOTACTIVATED = 16;
		public const int SRCSTATUS_BETA = 32;
		public const int SRCSTATUS_EXPIRED = 64;
		public const int SRCSTATUS_UNSUPPORTED = 128;
		public const int SRCSTATUS_READONLY = 256;
		public const int SRCSTATUS_DISABLED = 82;
		public const int SRCSTATUS_FEEDBACKWANTED = 160;
		public const int SRCPROTOCOL_ATCOMMANDS = 1;
		public const int SRCPROTOCOL_OBEX = 2;
		public const int SRCBATT_BATTERY_NONE = 0;
		public const int SRCBATT_BATTERY_CONNECTED = 1;
		public const int SRCBATT_BATTERY_CHARGING = 2;
		public const int SRCBATT_BATTERY_OK = 3;
		public const int DI_DISPLAY = 1;
		public const int DI_WALLPAPER = 2;
		public const int ME_DATETIME_VERSION_0 = 0;
		public const int ME_DATETIME_TIME = 1;
		public const int ME_DATETIME_DATE = 2;
		public const int ME_DATETIME_DATETIME_MASK = 3;
		public const int ME_DATETIME_RAWDATA = 0;
		public const int ME_DATETIME_DAYLIGHT_APPLIED = 16;
		public const int ME_DATETIME_TIMEZONE_APPLIED = 32;
		public const int ME_DATETIME_TIMEZONE_MASK = 48;
		public const int ME_DATETIME_TIMEZONEBIAS_VALID = 32768;
		public const int ME_DATETIME_DAYLIGHTBIAS_VALID = 16384;
		public const int CONNECTION_BLUETOOTH = 1;
		public const int CONNECTION_IRDA = 2;
		public const int CONNECTION_CABLE = 4;
		public const int GPRS_EDGE = 1;
		public const int SIGNAL_QUALITY_TYPE_EMPTY = 0;
		public const int SIGNAL_QUALITY_TYPE_DBM = 1;
		public const int SIGNAL_QUALITY_TYPE_PERCENT = 2;
		public const int SIGNAL_QUALITY_TYPE_UNKNOWN = 4;
		public const int INVALID_DATETIME = -1;
		public const int INVALID_TIME = -1;
		public const int START_DATETIME = 86400;
		public const int INVALID_DATETIMEEX = -1;
		public const int START_DATETIMEEX = 2026291200;
		public const int NETWORK_TYPE_GSM = 1;
		public const int NETWORK_TYPE_AMPS = 2;
		public const int NETWORK_TYPE_TDMA = 4;
		public const int NETWORK_TYPE_CDMA = 8;
		public const int NETWORK_TYPE_UMTS = 16;
		public const int NETWORK_TYPE_IDEN = 32;
		public const int SIM_PHASE_1 = 0;
		public const int SIM_PHASE_2 = 1;
		public const int SIM_PHASE_2plus = 2;
		public const int SIM_UNKNOWN = -1;
		public const int CHV_PIN1_ACTIVE = 1;
		public const int CHV_PIN2_ACTIVE = 2;
		public const int CHV_PUK1_ACTIVE = 4;
		public const int CHV_PUK2_ACTIVE = 8;
		public const int CHV_PIN1_ENABLED = 16;
		public const int CHV_COUNTER_UNKNOWN = 255;
		public const int READER_STATE_EMPTY = 16;
		public const int READER_STATE_PRESENT = 32;
		public const int READER_STATE_EXCLUSIVE = 128;
		public const int READER_STATE_INUSE = 256;
		public const int READER_STATE_MUTE = 512;
		public const int READER_STATE_UNPOWERED = 1024;
		public const int SIM_STATE_UNKNOWN = 0;
		public const int SIM_STATE_NOT_SIM = 1;
		public const int SIM_STATE_NOT_READY = 2;
		public const int SIM_STATE_READY = 3;
		public const int PORT_DETAILS_TYPE = 1;
		public const int PORT_DETAILS_STATE = 2;
		public const int PORT_DETAILS_NUMBER = 4;
		public const int PORT_DETAILS_SPECIFIC = 8;
		public const int PORT_DETAILS_DRIVERVER = 16;
		public const int PORT_DETAILS_DRIVERDATE = 32;
		public const int PORT_DETAILS_ICON = 64;
		public const int SRCPROP_TSCSD = -64236;
		public const int SRCPROP_TSGPRS = -64235;
		public const int SRCPROP_TSHSCSD = -64234;
		public const int SRCPROP_TSEGPRS = -64233;
		public const int SRCPROP_TSECSD = -64232;
		public const int SRCPROP_TS1XRTT = -64231;
		public const int SRCPROP_TS1XEVDO = -64230;
		public const int SRCPROP_TSWGPRS = -64229;
		public const int SRCPROP_TSHSDPA = -64228;
		public const int SRCPROP_TSHSUPA = -64227;
		public const int ITEM_FORMAT_UNKNOWN = 0;
		public const int ITEM_FORMAT_CONTAINER = 1;
		public const int ITEM_FORMAT_TEXT = 2;
		public const int ITEM_FORMAT_DWORD = 3;
		public const int ITEM_FORMAT_TIMET = 4;
		public const int ITEM_FORMAT_FILETIME = 5;
		public const int ITEM_FORMAT_BLOB = 6;
		public const int ITEM_FORMAT_ANSITEXT = 7;
		public const int TAG_FIRST_NAME = 1;
		public const int TAG_LAST_NAME = 2;
		public const int TAG_JOB_TITLE = 3;
		public const int TAG_OCCUPATION = 4;
		public const int TAG_COMPANY = 5;
		public const int TAG_UNIT = 6;
		public const int TAG_EMAIL = 7;
		public const int TAG_URL = 8;
		public const int TAG_NOTE = 9;
		public const int TAG_POSTAL = 10;
		public const int TAG_CITY = 11;
		public const int TAG_STATE = 12;
		public const int TAG_ZIP = 13;
		public const int TAG_COUNTRY = 14;
		public const int TAG_STREET = 15;
		public const int TAG_PERSONAL = 16;
		public const int TAG_WVID = 17;
		public const int TAG_POBOX = 18;
		public const int TAG_ADDREXT = 19;
		public const int TAG_MIDDLE_NAME = 20;
		public const int TAG_NAME_PREFIX = 21;
		public const int TAG_NAME_SUFFIX = 22;
		public const int TAG_NICKNAME = 23;
		public const int TAG_HOME_EMAIL = 24;
		public const int TAG_HOME_URL = 25;
		public const int TAG_WORK_EMAIL = 26;
		public const int TAG_WORK_URL = 27;
		public const int TAG_PREF_EMAIL = 28;
		public const int TAG_FILE_NAME = 29;
		public const int TAG_ASSISTENT_NAME = 30;
		public const int TAG_SHAREVIEW = 31;
		public const int TAG_SIP = 32;
		public const int TAG_SPOUSE = 33;
		public const int TAG_CHILDREN = 34;
		public const int TAG_PTT = 35;
		public const int TAG_MANAGER_NAME = 36;
		public const int TAG_FILE_LINK = 37;
		public const int TAG_VOIP_PHONE = 80;
		public const int TAG_VOIP_PHONE_HOME = 81;
		public const int TAG_VOIP_PHONE_WORK = 82;
		public const int TAG_MIME_TYPE = 1;
		public const int TAG_UNSPECIFIED_NUMBER = 1;
		public const int TAG_HOME_NUMBER = 2;
		public const int TAG_MOBILE_NUMBER = 3;
		public const int TAG_FAX_NUMBER = 4;
		public const int TAG_WORK_NUMBER = 5;
		public const int TAG_PAGER_NUMBER = 6;
		public const int TAG_MODEM_NUMBER = 7;
		public const int TAG_VIDEO_NUMBER = 8;
		public const int TAG_DTMF = 9;
		public const int TAG_HOME_MOBILE = 20;
		public const int TAG_HOME_FAX = 21;
		public const int TAG_HOME_PAGER = 22;
		public const int TAG_HOME_MODEM = 23;
		public const int TAG_HOME_VIDEO = 24;
		public const int TAG_WORK_MOBILE = 30;
		public const int TAG_WORK_FAX = 31;
		public const int TAG_WORK_PAGER = 32;
		public const int TAG_WORK_MODEM = 33;
		public const int TAG_WORK_VIDEO = 34;
		public const int TAG_PREF_NUMBER = 40;
		public const int TAG_PREF_MOBILE = 41;
		public const int TAG_PREF_FAX = 42;
		public const int TAG_CAR_NUMBER = 50;
		public const int TAG_CAR_MOBILE = 51;
		public const int TAG_CAR_FAX = 52;
		public const int TAG_MOBILE_MODEM = 60;
		public const int TAG_MOBILE_FAX = 61;
		public const int TAG_ASSISTENT_NUMBER = 62;
		public const int TAG_PHONEBOOKCLASS = 1;
		public const int TAG_SYNCFLAGS = 2;
		public const int TAG_SYNCADDED = 3;
		public const int TAG_SYNCCHANGED = 4;
		public const int TAG_SYNCDELETED = 5;
		public const int TAG_LASTSYNC = 1;
		public const int TAG_EVENTSSYNCFOLDERID = 2;
		public const int TAG_TASKSSYNCFOLDERID = 3;
		public const int TAG_NOTESSYNCFOLDERID = 4;
		public const int TAG_SOURCEUNIQUEID = 5;
		public const int TAG_STREAM = 6;
		public const int TAG_PREVIOUSSTATE = 7;
		public const int TAG_DEVICEMAPPING = 8;
		public const int TAG_OUTLOOKMAPPING = 9;
		public const int TAG_PHONEBOOKSYNCFOLDER = 11;
		public const int TAG_PICTURE = 12;
		public const int SRCCLASS_PHONEBOOK = 16777216;
		public const int SRCCLASS_PHONEBOOKDIALED = 16777217;
		public const int SRCCLASS_PHONEBOOKRECEIVED = 16777218;
		public const int SRCCLASS_PHONEBOOKMISSED = 16777219;
		public const int SRCCLASS_PHONEBOOKFIXED = 16777220;
		public const int SRCCLASS_PHONEBOOKOWN = 16777221;
		public const int SRCCLASS_FIRSTMAPPED = 16777316;
		public const int SRCCLASS_PHONEBOOKSIM = 16826368;
		public const int SRCCLASS_PHONEBOOKSIMDIALED = 16826369;
		public const int SRCCLASS_PHONEBOOKSIMFIX = 16826372;
		public const int SRCCLASS_PHONEBOOKSIMOWN = 16826373;
		public const int SRCCLASS_PHONEBOOKME = 16809984;
		public const int SRCCLASS_PHONEBOOKMEDIALED = 16809985;
		public const int SRCCLASS_PHONEBOOKMERECVD = 16809986;
		public const int SRCCLASS_PHONEBOOKMEMISSED = 16809987;
		public const int SRCPARAM_NAMELENGTH = 16777216;
		public const int SRCPARAM_PBNFORMAT = 16777217;
		public const int SRCPARAM_ITEMBASE = 16777218;
		public const int SRCPARAM_FAKEINDEXES = 16777219;
		public const int SRCPARAM_PHBKREADONLY = 16777220;
		public const int SRCPARAM_MERGEDSTREAMNAME = 16777221;
		public const int SRCPARAM_PBNFORMAT2 = 16777222;
		public const int SRCPARAM_EMPTYITEMS = 16777223;
		public const int SRCPARAM_SAMELABELS = 16777224;
		public const int SRCPARAM_DEFAULTNUMBER = 16777225;
		public const int SAMELABELS_FORBIDEN = 0;
		public const int SAMELABELS_ALLOWED = 1;
		public const int SAMELABELS_SAMECONTACT = 2;
		public const int SAMELABELS_MASK = 3;
		public const int PBN_FORMAT_MASK = 65280;
		public const int PBN_TYPE_MASK = 255;
		public const int PBN_FORMAT_SPECIAL = 0;
		public const int PBN_TYPE_UNKNOWN = 0;
		public const int PBN_TYPE_EMPTY = 1;
		public const int PBN_FORMAT_NUMBER = 256;
		public const int PBN_TYPE_NUMBER = 257;
		public const int PBN_TYPE_HOME = 258;
		public const int PBN_TYPE_MOBILE = 259;
		public const int PBN_TYPE_FAX = 260;
		public const int PBN_TYPE_WORK = 261;
		public const int PBN_TYPE_PAGER = 262;
		public const int PBN_TYPE_MODEM = 263;
		public const int PBN_TYPE_VIDEO = 264;
		public const int PBN_TYPE_DTMF = 265;
		public const int PBN_TYPE_HOME_NUMBER = 258;
		public const int PBN_TYPE_HOME_MOBILE = 276;
		public const int PBN_TYPE_HOME_FAX = 277;
		public const int PBN_TYPE_HOME_PAGER = 278;
		public const int PBN_TYPE_HOME_MODEM = 279;
		public const int PBN_TYPE_HOME_VIDEO = 280;
		public const int PBN_TYPE_WORK_NUMBER = 261;
		public const int PBN_TYPE_WORK_MOBILE = 286;
		public const int PBN_TYPE_WORK_FAX = 287;
		public const int PBN_TYPE_WORK_PAGER = 288;
		public const int PBN_TYPE_WORK_MODEM = 289;
		public const int PBN_TYPE_WORK_VIDEO = 290;
		public const int PBN_TYPE_PREF_NUMBER = 296;
		public const int PBN_TYPE_PREF_MOBILE = 297;
		public const int PBN_TYPE_PREF_FAX = 298;
		public const int PBN_TYPE_CAR_NUMBER = 306;
		public const int PBN_TYPE_CAR_MOBILE = 307;
		public const int PBN_TYPE_CAR_FAX = 308;
		public const int PBN_TYPE_MOBILE_MODEM = 316;
		public const int PBN_TYPE_MOBILE_FAX = 317;
		public const int PBN_TYPE_ASSISTENT_NUMBER = 318;
		public const int PBN_FORMAT_TEXT = 512;
		public const int PBN_TYPE_FIRST_NAME = 513;
		public const int PBN_TYPE_LAST_NAME = 514;
		public const int PBN_TYPE_JOB_TITLE = 515;
		public const int PBN_TYPE_OCCUPATION = 516;
		public const int PBN_TYPE_COMPANY = 517;
		public const int PBN_TYPE_UNIT = 518;
		public const int PBN_TYPE_EMAIL = 519;
		public const int PBN_TYPE_URL = 520;
		public const int PBN_TYPE_NOTE = 521;
		public const int PBN_TYPE_POSTAL = 522;
		public const int PBN_TYPE_CITY = 523;
		public const int PBN_TYPE_STATE = 524;
		public const int PBN_TYPE_ZIP = 525;
		public const int PBN_TYPE_COUNTRY = 526;
		public const int PBN_TYPE_STREET = 527;
		public const int PBN_TYPE_PERSONAL = 528;
		public const int PBN_TYPE_WVID = 529;
		public const int PBN_TYPE_POBOX = 530;
		public const int PBN_TYPE_ADDREXT = 531;
		public const int PBN_TYPE_MIDDLE_NAME = 532;
		public const int PBN_TYPE_NAME_PREFIX = 533;
		public const int PBN_TYPE_NAME_SUFFIX = 534;
		public const int PBN_TYPE_NICKNAME = 535;
		public const int PBN_TYPE_HOME_EMAIL = 536;
		public const int PBN_TYPE_HOME_URL = 537;
		public const int PBN_TYPE_WORK_EMAIL = 538;
		public const int PBN_TYPE_WORK_URL = 539;
		public const int PBN_TYPE_PREF_EMAIL = 540;
		public const int PBN_TYPE_VOIP_PHONE = 592;
		public const int PBN_TYPE_VOIP_PHONE_HOME = 593;
		public const int PBN_TYPE_VOIP_PHONE_WORK = 594;
		public const int PBN_TYPE_PTT = 547;
		public const int PBN_TYPE_SHAREVIEW = 543;
		public const int PBN_TYPE_SIP = 544;
		public const int PBN_TYPE_ASSISTENT_NAME = 542;
		public const int PBN_TYPE_SPOUSE = 545;
		public const int PBN_TYPE_CHILDREN = 546;
		public const int PBN_TYPE_MANAGER_NAME = 548;
		public const int PBN_FORMAT_DATETIME = 768;
		public const int PBN_TYPE_DATE = 769;
		public const int PBN_FORMAT_DATETIMEEX = 1024;
		public const int PBN_TYPE_BIRTHDATE = 1025;
		public const int PBN_TYPE_ANNIVERSARY = 1026;
		public const int PBN_FORMAT_ADDRESS = 1280;
		public const int PBN_TYPE_ADDRESS = 1281;
		public const int PBN_TYPE_HOME_ADDRESS = 1282;
		public const int PBN_TYPE_WORK_ADDRESS = 1283;
		public const int PBN_TYPE_PREF_ADDRESS = 1284;
		public const int PBN_FORMAT_PICTURE = 1536;
		public const int PBN_TYPE_ITEMPICTURE = 1537;
		public const int PBN_FORMAT_AUDIO = 1792;
		public const int PBN_TYPE_WAVE_AUDIO = 1793;
		public const int PBN_TYPE_PCM_AUDIO = 1794;
		public const int PBN_TYPE_AIFF_AUDIO = 1795;
		public const int PBN_FORMAT_UNKNOWN = 65280;
		public const int PBN_TYPE_HINT_UNKNOWN = 65535;
		public const int PBN_TYPE_HINT_GROUP = 65281;
		public const int PBN_TYPE_HINT_PICTURE = 65282;
		public const int PBN_TYPE_HINT_RINGTONE = 65283;
		public const int PBN_TYPE_HINT_VOICEDIAL = 65284;
		public const int PBN_TYPE_SYNCUNIQUEID = 65285;
		public const int SRCCLASS_SMSSTORAGE = 33554432;
		public const int SRCCLASS_SMSOUTBOX = 33554433;
		public const int SRCCLASS_SENTITEMS = 33554434;
		public const int SRCCLASS_DELETEDITEMS = 33554435;
		public const int SRCCLASS_SMSCOMMAND = 33554436;
		public const int SRCCLASS_SMSSTORAGEME = 33587200;
		public const int SRCCLASS_SMSOUTBOXME = 33554433;
		public const int SRCCLASS_SMSSTORAGESIM = 33603584;
		public const int SRCCLASS_SMSOUTBOXSIM = 33554433;
		public const int SRCPARAM_SCNUMBERLENGTH = 33554432;
		public const int SRCPARAM_SMSNUMBERLENGTH = 33554433;
		public const int SRCPARAM_MESSAGELENGTH = 33554434;
		public const int SRCPARAM_SPLITSMS = 33554435;
		public const int SRCPARAM_USESTORECOMMAND = 33554436;
		public const int SRCPARAM_SERVICECENTERREQUIRED = 33554437;
		public const int SRCPARAM_SCNUMBER = 33554532;
		public const int SMS_SPLIT_NO_HEADER = 0;
		public const int SMS_SPLIT_WITH_HEADER = 1;
		public const int SMS_SPLIT_ONE_BLOCK = 2;
		public const int SMS_TYPE_EMPTY = 0;
		public const int SMS_TYPE_DELIVER = 1;
		public const int SMS_TYPE_SUBMIT = 2;
		public const int SMS_TYPE_STATUS_REPORT = 3;
		public const int SMS_TYPE_COMMAND = 4;
		public const int SMS_TYPE_INVALID = 5;
		public const int SMS_STATE_MASK = 15;
		public const int SMS_STATE_RECEIVED_UNREAD = 0;
		public const int SMS_STATE_RECEIVED_READ = 1;
		public const int SMS_STATE_STORED_UNSENT = 2;
		public const int SMS_STATE_STORED_SENT = 3;
		public const int SMS_STATE_DELETED = 16;
		public const int SMS_FLAG_MORE_MSG_TO_SEND = 4;
		public const int SMS_FLAG_STATUS_REPORT = 32;
		public const int SMS_FLAG_REPLY_PATH = 128;
		public const int SMS_FLAG_REJECT_DUPLICATES = 4;
		public const int SMS_FLAG_VALIDITY_PERIOD = 24;
		public const int SMS_FLAGS_DELIVER = 164;
		public const int SMS_FLAGS_SUBMIT = 188;
		public const int SMS_FLAGS_STATUS_REPORT = 36;
		public const int SMS_VALIDITY_NONE = 0;
		public const int SMS_VALIDITY_ENHANCED = 8;
		public const int SMS_VALIDITY_RELATIVE = 16;
		public const int SMS_VALIDITY_ABSOLUTE = 24;
		public const int SMS_STATUS_TYPE_MASK = 96;
		public const int SMS_STATUS_VALUE_MASK = -97;
		public const int SMS_STATUS_TRANSACTION_COMPLETED = 0;
		public const int SMS_STATUS_TEMPORARY_ERROR_1 = 32;
		public const int SMS_STATUS_PERMANENT_ERROR = 64;
		public const int SMS_STATUS_TEMPORARY_ERROR_2 = 96;
		public const int SMS_STATUS_RECEIVED = 0;
		public const int SMS_STATUS_FORWARDED = 1;
		public const int SMS_STATUS_REPLACED = 2;
		public const int SMS_STATUS_CONGESTION = 0;
		public const int SMS_STATUS_SME_BUSY = 1;
		public const int SMS_STATUS_NO_SME_RESPONSE = 2;
		public const int SMS_STATUS_SERVICE_REJECTED = 3;
		public const int SMS_STATUS_QOS_UNAVAILABLE = 4;
		public const int SMS_STATUS_SME_ERROR = 5;
		public const int SMS_STATUS_REMOTE_PROC_ERROR = 0;
		public const int SMS_STATUS_INCOMPATIBLE_DESTINATION = 1;
		public const int SMS_STATUS_SME_REJECTED = 2;
		public const int SMS_STATUS_NOT_OBTAINABLE = 3;
		public const int SMS_STATUS_NO_INTERNETWORKING = 5;
		public const int SMS_STATUS_VALIDITY_EXPIRED = 6;
		public const int SMS_STATUS_DELETED_BY_ORIG_SME = 7;
		public const int SMS_STATUS_DELETED_BY_SM_ADMIN = 8;
		public const int SMS_STATUS_NOT_EXIST = 9;
		public const int SMS_COMMAND_SEND = 1;
		public const int SMS_COMMAND_STORE = 2;
		public const int SMS_CODING_NORMAL = 0;
		public const int SMS_ALPHABET_7BIT = 0;
		public const int SMS_ALPHABET_16BIT = 2;
		public const int SMS_CODING_SCHEME_NORMAL_7BIT = 0;
		public const int SMS_CODING_SCHEME_NORMAL_16BIT = 8;
		public const int SMS_EXTENSION_USEDDEVICE = 1;
		public const int SMS_EXTENSION_STORETIME = 2;
		public const int KEY_CODE_0 = 48;
		public const int KEY_CODE_1 = 49;
		public const int KEY_CODE_2 = 50;
		public const int KEY_CODE_3 = 51;
		public const int KEY_CODE_4 = 52;
		public const int KEY_CODE_5 = 53;
		public const int KEY_CODE_6 = 54;
		public const int KEY_CODE_7 = 55;
		public const int KEY_CODE_8 = 56;
		public const int KEY_CODE_9 = 57;
		public const int KEY_CODE_STAR = 42;
		public const int KEY_CODE_HASH = 35;
		public const int KEY_CODE_CONNEND = 69;
		public const int KEY_CODE_CONNSTART = 83;
		public const int KEY_CODE_FUNCTION = 70;
		public const int KEY_CODE_MENU = 77;
		public const int KEY_CODE_CLEAR = 67;
		public const int KEY_CODE_LEFTARROW = 60;
		public const int KEY_CODE_RIGHTARROW = 62;
		public const int KEY_CODE_UPARROW = 94;
		public const int KEY_CODE_DOWNARROW = 118;
		public const int KEY_CODE_VOLUMEUP = 85;
		public const int KEY_CODE_VOLUMEDOWN = 68;
		public const int KEY_CODE_POWER = 80;
		public const int KEY_CODE_JOYSTICK = 74;
		public const int KEY_CODE_RETURN = 82;
		public const int KEY_CODE_OPERATOR = 79;
		public const int KEY_CODE_SOFTKEY1 = 91;
		public const int KEY_CODE_SOFTKEY2 = 93;
		public const int KEY_CODE_CAMERA = 109;
		public const int KEY_CODE_FOCUS = 102;
		public const int KEY_CODE_VOICERECORDER = 114;
		public const int KEY_CODE_PTT = 112;
		public const int KEY_CODE_BROWSER = 98;
		public const int KEY_CODE_SPECIAL_CMEC = 32767;
		public const int KEY_MODE_PRESSRELEASE = 1;
		public const int KEY_MODE_TIME = 2;
		public const int SRCPARAM_SUPPORTEDKEYS = 50331648;
		public const int SRCPARAM_NATIVEKEYMODE = 50331649;
		public const int SRCCLASS_KEYPRESS = 50331648;
		public const int SRCCLASS_KEYNOTIFY = 50331649;
		public const int SRCCLASS_CALLLIST = 67108864;
		public const int SRCCLASS_CALLLISTME = 67108864;
		public const int SRCCLASS_CALLLISTSIM = 67108864;
		public const int SRCPARAM_HANDSFREESTATUS = 67108865;
		public const int SRCPARAM_POSSIBLEOPERATIONS = 67108866;
		public const int HANDSFREE_STATUS_UNKNOWN = 0;
		public const int HANDSFREE_STATUS_DISABLED = 1;
		public const int HANDSFREE_STATUS_ENABLED = 2;
		public const int CALL_POSSIBLE_DIAL = 1;
		public const int CALL_POSSIBLE_HOLD = 2;
		public const int CALL_POSSIBLE_ACTIVATE = 4;
		public const int CALL_POSSIBLE_BUSY = 8;
		public const int CALL_POSSIBLE_HANGUP = 16;
		public const int CALL_POSSIBLE_ALL = -1;
		public const int SRCCLASS_FSCONTROL = 117473280;
		public const int SRCCLASS_FSROOT = 117473281;
		public const int SRCCLASS_FSFIRSTFOLDER = 117473282;
		public const int SRCCLASS_FSLASTFOLDER = 117489663;
		public const int SRCCLASS_FSUSECONTROL = 0;
		public const int SRCPARAM_FSJAVAAPPSROOT = 117440612;
		public const int SRCPARAM_FSJAVAGAMESROOT = 117440613;
		public const int SRCPARAM_FSJAVACAPABILITIES = 117440614;
		public const int SRCPARAM_FSPARENT = -65530;
		public const int SRCPARAM_FSCHILDFOLDERS = -65531;
		public const int SRCPARAM_FSFOLDERNAME = -65529;
		public const int SRCPARAM_FSFOLDERINFO = 117440512;
		public const int SRCPARAM_FSMEMORYINFO = 117440513;
		public const int SRCPARAM_FSPROTOCOL = 117440514;
		public const int SRCPARAM_FSCURRENTPROTOCOL = 117440515;
		public const int SRCPARAM_FSFOLDERINFOEX = 117440516;
		public const int SRCPARAM_FSINTERNALFSMASK = 117440517;
		public const int SRCFSPROTOCOL_OBEX = 0;
		public const int SRCFSPROTOCOL_NOKIANATIVE = 1;
		public const int INVALID_MEMORY_SIZE = -1;
		public const int FSCOMMAND_CREATE_FOLDER = 1;
		public const int FSCOMMAND_RENAME_FOLDER = 2;
		public const int FSCOMMAND_DELETE_FOLDER = 3;
		public const int FSCOMMAND_CREATE_FILE = 4;
		public const int FSCOMMAND_RENAME_FILE = 5;
		public const int FSCOMMAND_DELETE_FILE = 6;
		public const int FSCOMMAND_INSTALL_JAVAAPP = 16;
		public const int FSCOMMAND_INSTALL_JAVAGAME = 17;
		public const int FSOPTION_ATTRIBUTES = 1;
		public const int FSOPTION_CREATION_TIME = 2;
		public const int FSOPTION_MODIFICATION_TIME = 4;
		public const int FSOPTION_ACCESS_TIME = 8;
		public const int FSOPTION_PERMISSIONS = 16;
		public const int FSOPTION_RENAME_SELF = 65536;
		public const int FSOPTION_MOVE_SELF = 131072;
		public const int FSOPTION_RENAME_FILE = 262144;
		public const int FSOPTION_MOVE_FILE = 524288;
		public const int FSOPTION_UNICODE_NAMES = 1048576;
		public const int FSOPTION_CASE_SENSITIVE = 2097152;
		public const int FSOPTION_EXTERNAL = 1073741824;
		public const int FSOPTION_DRIVES = -2147483648;
		public const int FSFILE_NAME = 1;
		public const int FSFILE_ATTRIBUTES = 2;
		public const int FSFILE_CREATION_TIME = 3;
		public const int FSFILE_MODIFICATION_TIME = 4;
		public const int FSFILE_ACCESS_TIME = 5;
		public const int FSFILE_SIZE_HINT = 6;
		public const int FSFILE_PERMISSIONS = 7;
		public const int FSFILE_CREATION_METIME = 8;
		public const int FSFILE_MODIFICATION_METIME = 9;
		public const int FSFILE_ACCESS_METIME = 10;
		public const int FSPERM_READ = 1;
		public const int FSPERM_WRITE = 2;
		public const int FSPERM_DELETE = 4;
		public const int FSPERM_PRESENT_BIT = 128;
		public const int FSPERM_USER_SHIFT = 0;
		public const int FSPERM_USER_READ = 1;
		public const int FSPERM_USER_WRITE = 2;
		public const int FSPERM_USER_DELETE = 4;
		public const int FSPERM_USER_PRESENT = 128;
		public const int FSPERM_GROUP_SHIFT = 8;
		public const int FSPERM_GROUP_READ = 256;
		public const int FSPERM_GROUP_WRITE = 512;
		public const int FSPERM_GROUP_DELETE = 1024;
		public const int FSPERM_GROUP_PRESENT = 32768;
		public const int FSPERM_OTHER_SHIFT = 16;
		public const int FSPERM_OTHER_READ = 65536;
		public const int FSPERM_OTHER_WRITE = 131072;
		public const int FSPERM_OTHER_DELETE = 262144;
		public const int FSPERM_OTHER_PRESENT = 8388608;
		public const int FSPERM_EFFECTIVE_SHIFT = 24;
		public const int FSPERM_EFFECTIVE_READ = 16777216;
		public const int FSPERM_EFFECTIVE_WRITE = 33554432;
		public const int FSPERM_EFFECTIVE_DELETE = 67108864;
		public const int FSMIDP_NONE = 0;
		public const int FSMIDP_1 = 1;
		public const int FSMIDP_2 = 2;
		public const int FSJAVA_MODEL_GENERIC_MIDP1 = 1;
		public const int FSJAVA_MODEL_GENERIC_MIDP2 = 2;
		public const int FSJAVA_MODEL_GENERIC_NOKIA_MIDP1 = 3;
		public const int FSJAVA_MODEL_GENERIC_NOKIA_MIDP2 = 4;
		public const int FSJAVA_MODEL_EX_GENERIC = 1;
		public const int SRCCLASS_EVENTS = 134250496;
		public const int SRCCLASS_TASKS = 134250497;
		public const int SRCCLASS_NOTES = 134250498;
		public const int SRCCLASS_ORGANIZER = 134250499;
		public const int ORGANIZER_UNIQUE_ID = -559086230;
		public const int ORGANIZER_TYPE_CLASS = 1;
		public const int ORGANIZER_TYPE_CATEGORIES = 32;
		public const int ORGANIZER_TYPE_RECURENCE_OLD = 33;
		public const int ORGANIZER_TYPE_PRIORITY = 34;
		public const int ORGANIZER_TYPE_STATUS = 35;
		public const int ORGANIZER_TYPE_RECURENCE = 36;
		public const int ORGANIZER_TYPE_ALARM_TYPE = 37;
		public const int ORGANIZER_TYPE_START_DATETIME = 257;
		public const int ORGANIZER_TYPE_END_DATETIME = 258;
		public const int ORGANIZER_TYPE_ALARM_DATETIME = 259;
		public const int ORGANIZER_TYPE_SUMMARY = 272;
		public const int ORGANIZER_TYPE_LOCATION = 273;
		public const int ORGANIZER_TYPE_DESCRIPTION = 274;
		public const int ORGANIZER_TYPE_PHONENUMBER = 275;
		public const int ORGANIZER_TYPE_BIRTHDAY_YEAR = 513;
		public const int SRCPARAM_ORGANIZER_FORMAT = 134217728;
		public const int SRCCLASS_NETWORKSPREFERRED = 151044096;
		public const int SRCCLASS_NETWORKSFORBIDDEN = 151044097;
		public const int SRCCLASS_COMMAND = 167772160;
		public const int SRCCOMMAND_CLEARALL = 0;
		public const int SRCCOMMAND_POWEROFF = 1;
		public const int SRCCOMMAND_CHECKSIM = 2;
		public const int SRCCOMMAND_REPAIRSIM = 3;
		public const int SRCCOMMAND_INSTALL_CONNECTOR = 4;
		public const int SRCCLASS_MMSSTORAGE = 184582144;
		public const int SRCCLASS_MMSINBOX = 184582145;
		public const int SRCCLASS_MMSOUTBOX = 184582146;
		public const int SRCCLASS_MMSSENTITEMS = 184582147;
		public const int SRCCLASS_MMSPUTOBEX = 184582148;
		public const int SRCCLASS_MMSSENDWAP = 184582149;
		public const int SRCCLASS_MMSDELETEDITEMS = 184582150;
		public const int MMS_FORMAT_MASK = -65536;
		public const int MMS_FORMAT_HEADER = -2147483648;
		public const int MMS_FORMAT_TEXTHEADER = -2147352576;
		public const int MMS_FORMAT_DATETIMEHEADER = -2147287040;
		public const int MMS_FORMAT_FILE = 65536;
		public const int MMS_TYPE_FROM = -2147352575;
		public const int MMS_TYPE_TO = -2147352574;
		public const int MMS_TYPE_CC = -2147352573;
		public const int MMS_TYPE_BCC = -2147352572;
		public const int MMS_TYPE_SUBJECT = -2147352571;
		public const int MMS_TYPE_RECEIVED = -2147287039;
		public const int MMS_TYPE_SENT = -2147287038;
		public const int MMS_TYPE_SMIL = 65537;
		public const int MMS_TYPE_TEXTPLAIN = 65538;
		public const int MMS_TYPE_IMGJPEG = 65539;
		public const int MMS_TYPE_AUDIOMID = 65540;
		public const int MMS_TYPE_IMGGIF = 65541;
		public const int MMS_TYPE_AUDIOAMR = 65542;
		public const int MMS_TYPE_AUDIOWAV = 65543;
		public const int MMS_TYPE_AUDIOMP3 = 65544;
		public const int MMS_TYPE_IMGTIFF = 65545;
		public const int MMS_TYPE_IMGBMP = 65546;
		public const int MMS_TYPE_IMGPNG = 65547;
		public const int MMS_TYPE_AUDIO3GP = 65548;
		public const int MMS_TYPE_VIDEO3GP = 65549;
		public const int MMS_TYPE_VIDEOMPEG = 65550;
		public const int MMS_TYPE_AUDIOMPEG = 65551;
		public const int POSITION_AUTO_DETECT = -1;
		public const int SRCPARAM_MMSSENDOPTS = 184549376;
		public const int SRCCLASS_USSDSEND = 201359360;
		public const int SRCCLASS_USSDINBOX = 201359361;
		public const int SRCPARAM_USSDLISTEN = 201326592;
		public const int OATEmpty = 0;
		public const int OATDisabled = 1;
		public const int OATNormal = 2;
		public const int OPEmpty = 0;
		public const int OPLow = 1;
		public const int OPNormal = 2;
		public const int OPHigh = 3;
		public const int OREmpty = 0;
		public const int OREvent = 1;
		public const int ORTask = 2;
		public const int ORNote = 3;
		public const int OSEmpty = 0;
		public const int OSCompleted = 1;
		public const int OSNeedsAction = 2;
		public const int FILE_BEGIN = 0;
		public const int FILE_CURRENT = 1;
		public const int FILE_END = 2;
		public const int PRODUCT_COMCOMPONENTS = 67;
		public const int PRODUCTTYPE_BLUETOOTH = 66;
		public const int PRODUCTTYPE_IRDA = 68;
		public const int PRODUCTTYPE_ENTERPRISE = 69;
		public const int PRODUCTTYPE_FULL = 70;
		public const int PRODUCTTYPE_CABLE = 75;
		public const int PRODUCTTYPE_LITE = 76;
		public const int PRODUCTTYPE_READONLY = 82;
		public const int PRODUCTTYPE_TRIAL = 84;
		public const int LICENSE_INVALID = 0;
		public const int LICENSE_ACTIVATED = 1;
		public const int LICENSE_ACTIVATED_LIMITED = 2;
	}
}
