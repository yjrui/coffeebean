using MobilEditCom;
using System;
using System.Xml;
namespace COMlib
{
	public class CSource : CItemNode
	{
		public enum EDeviceType
		{
			Archive = 6,
			External = 5,
			Backup = 4,
			Reader = 3,
			SIMCard = 2,
			MobilePhone = 1,
			Unknown = 0
		}
		public class CMobilePhone : CSource
		{
			public class CBattery : CItem
			{
				public enum EStatus
				{
					Bypassed = 1,
					Charging,
					Disconnected = 0,
					Connected = 3
				}
				public readonly int Step;
				public readonly int Capacity;
				public readonly CSource.CMobilePhone.CBattery.EStatus Status;
				internal CBattery(CItem parent, int status, int step, int capacity) : base(parent)
				{
					this.Capacity = capacity;
					this.Step = step;
					this.Status = (CSource.CMobilePhone.CBattery.EStatus)status;
				}
				public override XmlElement XmlDump(XmlDocument xmlDocument)
				{
					XmlElement xmlElement = base.XmlDump(xmlDocument);
					xmlElement.SetAttribute("Capacity", this.Capacity.ToString());
					xmlElement.SetAttribute("Step", this.Step.ToString());
					xmlElement.SetAttribute("Status", this.Status.ToString());
					return xmlElement;
				}
			}
			public class CSignal : CItem
			{
				public readonly int Maximum;
				public readonly int Minimum;
				public readonly int Step;
				public readonly int Strength;
				internal CSignal(CItem parent, int maximum, int minimum, int step, int strength) : base(parent)
				{
					this.Maximum = maximum;
					this.Minimum = minimum;
					this.Step = step;
					this.Strength = strength;
				}
				public override XmlElement XmlDump(XmlDocument xmlDocument)
				{
					XmlElement xmlElement = base.XmlDump(xmlDocument);
					xmlElement.SetAttribute("Maximum", this.Maximum.ToString());
					xmlElement.SetAttribute("Minimum", this.Minimum.ToString());
					xmlElement.SetAttribute("Step", this.Step.ToString());
					xmlElement.SetAttribute("Strength", this.Strength.ToString());
					return xmlElement;
				}
			}
			public class CNetwork : CItem
			{
				public readonly int MCC;
				public readonly int MNC;
				public readonly int LAC;
				public readonly int CID;
				public readonly string Operator;
				internal CNetwork(CItem parent, string operatorName, int mcc, int mnc, int lac, int cid) : base(parent)
				{
					this.Operator = operatorName;
					this.MNC = mnc;
					this.MCC = mcc;
					this.LAC = lac;
					this.CID = cid;
				}
				public override XmlElement XmlDump(XmlDocument xmlDocument)
				{
					XmlElement xmlElement = base.XmlDump(xmlDocument);
					xmlElement.SetAttribute("MNC", this.MNC.ToString());
					xmlElement.SetAttribute("MCC", this.MCC.ToString());
					if (this.LAC != -1)
					{
						xmlElement.SetAttribute("LAC", this.LAC.ToString());
					}
					if (this.CID != -1)
					{
						xmlElement.SetAttribute("CID", this.MCC.ToString());
					}
					xmlElement.SetAttribute("Operator", this.Operator.ToString());
					return xmlElement;
				}
			}
			public enum EPlatform
			{
				Unknown,
				Proprietary = 0,
				Symbian,
				WindowsCE = 4,
				Linux = 2,
				RIM,
				IPhone = 5,
				Android = 7,
				WindowsPhone7
			}
			internal IMobilePhone MobilePhone;
			public bool IsConnectorRequired
			{
				get
				{
					return this.MobilePhone.ConnectorNotPresent > 0;
				}
			}
			public CSource.CMobilePhone.CBattery Battery
			{
				get
				{
					CSource.CMobilePhone.CBattery result;
					try
					{
						int status;
						int step;
						int capacity;
						this.MobilePhone.BatteryStatus(out status, out step, out capacity);
						result = new CSource.CMobilePhone.CBattery(this, status, step, capacity);
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query battery status [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public CSource.CMobilePhone.CSignal Signal
			{
				get
				{
					CSource.CMobilePhone.CSignal result;
					try
					{
						ISignalQuality signalQuality = (ISignalQuality)this.MobilePhone.GetSignalQuality(1);
						result = new CSource.CMobilePhone.CSignal(this, signalQuality.Maximum, signalQuality.Minimum, signalQuality.Step, signalQuality.Strength);
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query ISignalQuality interface [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public CSource.CMobilePhone.CNetwork Network
			{
				get
				{
					CSource.CMobilePhone.CNetwork result;
					try
					{
						int mcc;
						int mnc;
						string networkParameters = this.MobilePhone.GetNetworkParameters(out mcc, out mnc);
						int lac;
						int cid;
						try
						{
							this.MobilePhone.GetLacCid(out lac, out cid);
						}
						catch (Exception ex)
						{
							Console.WriteLine(this.ToString() + " : Unable to query LAC and CID parameters [ " + ex.Message + " ]");
							lac = -1;
							cid = -1;
						}
						result = new CSource.CMobilePhone.CNetwork(this, networkParameters, mcc, mnc, lac, cid);
					}
					catch (Exception ex2)
					{
						Console.WriteLine(this.ToString() + " : Unable to query network parameters [ " + ex2.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public DateTime Time
			{
				get
				{
					DateTime result;
					try
					{
						int num;
						DateTime dateTime = this.MobilePhone.GetDateTime(out num);
						if (num == 0)
						{
							result = DateTime.MinValue;
						}
						else
						{
							result = dateTime;
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query time [ " + ex.Message + " ]");
						result = DateTime.MinValue;
					}
					return result;
				}
			}
			public string HWRevision
			{
				get
				{
					string result;
					try
					{
						result = this.MobilePhone.HWRevision;
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query HW revision [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public string SWRevision
			{
				get
				{
					string result;
					try
					{
						result = this.MobilePhone.SWRevision;
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query SW revision [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public string IMEI
			{
				get
				{
					string result;
					try
					{
						result = this.MobilePhone.IMEI;
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query IMEI [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public string ESN
			{
				get
				{
					string result;
					try
					{
						result = this.MobilePhone.ESN;
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query ESN [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public CSource SIMCard
			{
				get
				{
					CSource result;
					try
					{
						ISIMCard iSIMCard = (ISIMCard)this.MobilePhone.SIMCard;
						result = ((CSources)base.Parent).FindById(iSIMCard.Session);
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query SIMCard property [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public CSource.CMobilePhone.EPlatform Platform
			{
				get
				{
					CSource.CMobilePhone.EPlatform result;
					try
					{
						result = (CSource.CMobilePhone.EPlatform)this.MobilePhone.Platform;
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query platform type [ " + ex.Message + " ]");
						result = CSource.CMobilePhone.EPlatform.Unknown;
					}
					return result;
				}
			}
			public void InstallConnector()
			{
				this.InstallConnector(new CDriver.Progress(CDriver.ProgressClass.Progress));
			}
			public void InstallConnector(CDriver.Progress progress)
			{
				if (this.IsConnectorRequired)
				{
					IOperation operation = this.MobilePhone.InstallConnector() as IOperation;
					if (operation != null && !CDriver.OperationToProgress(operation, progress, 0, 500))
					{
						throw new Exception("Installation of the connector failed");
					}
				}
			}
			internal CMobilePhone(CItem parent, IMedDataSource dataSource) : base(parent, dataSource)
			{
				this.MobilePhone = (IMobilePhone)dataSource;
			}
			public override XmlElement XmlDump(XmlDocument xmlDocument)
			{
				XmlElement xmlElement = base.XmlDump(xmlDocument);
				if (this.HWRevision != null)
				{
					xmlElement.SetAttribute("HWRevision", this.HWRevision);
				}
				if (this.SWRevision != null)
				{
					xmlElement.SetAttribute("SWRevision", this.SWRevision);
				}
				if (this.IMEI != null)
				{
					xmlElement.SetAttribute("IMEI", this.IMEI);
				}
				if (this.ESN != null)
				{
					xmlElement.SetAttribute("ESN", this.ESN);
				}
				xmlElement.SetAttribute("Platform", this.Platform.ToString());
				xmlElement.SetAttribute("Time", this.Time.ToString());
				if (this.Battery != null)
				{
					xmlElement.AppendChild(this.Battery.XmlDump(xmlDocument));
				}
				if (this.Signal != null)
				{
					xmlElement.AppendChild(this.Signal.XmlDump(xmlDocument));
				}
				if (this.Network != null)
				{
					xmlElement.AppendChild(this.Network.XmlDump(xmlDocument));
				}
				if (this.SIMCard != null)
				{
					xmlElement.SetAttribute("SIMCard", this.SIMCard.Label);
				}
				return xmlElement;
			}
		}
		public class CSIMCard : CSource
		{
			public CSource ParentSource
			{
				get
				{
					CSource result;
					try
					{
						IMedDataSource medDataSource = (IMedDataSource)((ISIMCard)this.MedDataSource).ParentSource;
						result = ((CSources)base.Parent).FindById(medDataSource.Session);
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query ParentSource property [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			public string IMSI
			{
				get
				{
					string result;
					try
					{
						result = ((ISIMCard)this.MedDataSource).IMSI;
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : IMSI retrieving failed [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			internal CSIMCard(CItem parent, IMedDataSource dataSource) : base(parent, dataSource)
			{
			}
			public override XmlElement XmlDump(XmlDocument xmlDocument)
			{
				XmlElement xmlElement = base.XmlDump(xmlDocument);
				if (this.IMSI != null)
				{
					xmlElement.SetAttribute("IMSI", this.IMSI);
				}
				if (this.ParentSource != null)
				{
					xmlElement.SetAttribute("ParentSource", this.ParentSource.Label);
				}
				return xmlElement;
			}
		}
		public class CSmartCardReader : CSource
		{
			public enum ESIMState
			{
				Unknown,
				NoSIM,
				NotReady,
				Ready
			}
			public readonly bool Empty;
			public readonly bool Exclusive;
			public readonly bool InUse;
			public readonly bool Mute;
			public readonly bool Present;
			public readonly bool Powered;
			public readonly CSource.CSmartCardReader.ESIMState SIMState;
			internal ISmartCardReader MedSmartCardReader;
			public CSource SIMCard
			{
				get
				{
					CSource result;
					try
					{
						ISIMCard iSIMCard = (ISIMCard)this.MedSmartCardReader.SIMCard;
						result = ((CSources)base.Parent).FindById(iSIMCard.Session);
					}
					catch (Exception ex)
					{
						Console.WriteLine(this.ToString() + " : Unable to query SIMCard property [ " + ex.Message + " ]");
						result = null;
					}
					return result;
				}
			}
			internal CSmartCardReader(CItem parent, IMedDataSource dataSource) : base(parent, dataSource)
			{
				this.MedSmartCardReader = (ISmartCardReader)dataSource;
				IReaderState readerState;
				try
				{
					readerState = (IReaderState)this.MedSmartCardReader.State;
				}
				catch (Exception ex)
				{
					throw new Exception(this.ToString() + " : Cannot query IReaderState interface [ " + ex.Message + " ]");
				}
				this.Empty = (readerState.Empty != 0);
				this.Exclusive = (readerState.Exclusive != 0);
				this.InUse = (readerState.InUse != 0);
				this.Mute = (readerState.Mute != 0);
				this.Present = (readerState.Present != 0);
				this.Powered = (readerState.Unpowered == 0);
				this.SIMState = (CSource.CSmartCardReader.ESIMState)readerState.SIMState;
			}
			public override XmlElement XmlDump(XmlDocument xmlDocument)
			{
				XmlElement xmlElement = base.XmlDump(xmlDocument);
				xmlElement.SetAttribute("Empty", this.Empty.ToString());
				xmlElement.SetAttribute("Exclusive", this.Exclusive.ToString());
				xmlElement.SetAttribute("InUse", this.InUse.ToString());
				xmlElement.SetAttribute("Mute", this.Mute.ToString());
				xmlElement.SetAttribute("Present", this.Present.ToString());
				xmlElement.SetAttribute("Powered", this.Powered.ToString());
				xmlElement.SetAttribute("SIMState", this.SIMState.ToString());
				if (this.SIMCard != null)
				{
					xmlElement.SetAttribute("SIMCard", this.SIMCard.Label);
				}
				return xmlElement;
			}
		}
		internal IMedDataSource MedDataSource;
		internal bool _Online;
		public readonly string Label;
		public readonly string Manufacturer;
		public readonly string Model;
		internal readonly int Id;
		internal readonly int SourceId;
		public readonly string UniqueIdentifier;
		public readonly CSource.EDeviceType DeviceType;
		private CFileSystem _filesystem;
		private bool _filesystemDetected;
		private CPhonebooks _phonebooks;
		private bool _phonebooksDetected;
		private bool _keyboardDetected;
		private CKeyboard _keyboard;
		private bool _callManagementDetected;
		//private CCallManagement _callManagement;
		private bool _organizerDetected;
		private COrganizer _organizer;
		private bool _smsDetected;
		private CSms _sms;
		internal static int counter;
		public bool Online
		{
			get
			{
				return this.MedDataSource.Status == 0;
			}
		}
		public CPort Port
		{
			get
			{
				CPort result;
				try
				{
					result = ((CDriver)base.Parent.Parent).Ports.FindById(this.MedDataSource.Port);
				}
				catch (Exception ex)
				{
					Console.WriteLine(this.ToString() + " : Unable to query Port property [ " + ex.Message + " ]");
					result = null;
				}
				return result;
			}
		}
		public CFileSystem FileSystem
		{
			get
			{
				if (!this._filesystemDetected)
				{
					try
					{
						ICapability capability = (ICapability)this.MedDataSource.GetCapabilityDirect(1792);
						this._filesystem = new CFileSystem(this, capability);
					}
					catch (Exception)
					{
						Console.WriteLine(this.ToString() + " : Unable to query Filesystem Capability");
					}
				}
				this._filesystemDetected = true;
				return this._filesystem;
			}
		}
		public CPhonebooks Phonebooks
		{
			get
			{
				if (!this._phonebooksDetected)
				{
					try
					{
						ICapability medCapability = (ICapability)this.MedDataSource.GetCapabilityDirect(256);
						this._phonebooks = new CPhonebooks(this, medCapability);
					}
					catch (Exception)
					{
						Console.WriteLine(this.ToString() + " : Unable to query Phonebook Capability");
					}
				}
				this._phonebooksDetected = true;
				return this._phonebooks;
			}
		}
		public CKeyboard Keyboard
		{
			get
			{
				if (!this._keyboardDetected)
				{
					try
					{
						ICapability medCapability = (ICapability)this.MedDataSource.GetCapabilityDirect(768);
						this._keyboard = new CKeyboard(this, medCapability);
					}
					catch (Exception)
					{
						Console.WriteLine(this.ToString() + " : Unable to query Keyboard Capability");
					}
				}
				this._keyboardDetected = true;
				return this._keyboard;
			}
		}
        //public CCallManagement CallManagement
        //{
        //    get
        //    {
        //        if (!this._callManagementDetected)
        //        {
        //            try
        //            {
        //                ICapability medCapability = (ICapability)this.MedDataSource.GetCapabilityDirect(1024);
        //                this._callManagement = new CCallManagement(this, medCapability);
        //            }
        //            catch (Exception)
        //            {
        //                Console.WriteLine(this.ToString() + " : Unable to query Call Capability");
        //            }
        //        }
        //        this._callManagementDetected = true;
        //        return this._callManagement;
        //    }
        //}
		public COrganizer Organizer
		{
			get
			{
				if (!this._organizerDetected)
				{
					try
					{
						ICapability medCapability = (ICapability)this.MedDataSource.GetCapabilityDirect(2048);
						this._organizer = new COrganizer(this, medCapability);
					}
					catch (Exception)
					{
						Console.WriteLine(this.ToString() + " : Unable to query organizer Capability");
					}
				}
				this._organizerDetected = true;
				return this._organizer;
			}
		}
		public CSms Sms
		{
			get
			{
				if (!this._smsDetected)
				{
					try
					{
						ICapability medCapability = (ICapability)this.MedDataSource.GetCapabilityDirect(512);
						this._sms = new CSms(this, medCapability);
					}
					catch (Exception)
					{
						Console.WriteLine(this.ToString() + " : Unable to query SMS Capability");
					}
				}
				this._smsDetected = true;
				return this._sms;
			}
		}
		internal CSource(CItem parent, IMedDataSource medDataSource) : base(parent)
		{
			if (!(parent is CSources))
			{
				throw new Exception("Internal error");
			}
			this._Online = true;
			this.MedDataSource = medDataSource;
			this.Label = this.MedDataSource.Label;
			this.Manufacturer = this.MedDataSource.Manufacturer;
			this.Model = this.MedDataSource.Product;
			this.Id = this.MedDataSource.Session;
			this.UniqueIdentifier = this.MedDataSource.UniqueId;
			this.DeviceType = (CSource.EDeviceType)this.MedDataSource.Type;
			this.SourceId = ++CSource.counter;
		}
		public override string ToString()
		{
			if (this.Manufacturer == "")
			{
				return base.ToString() + "[" + this.Model + "]";
			}
			return string.Concat(new string[]
			{
				base.ToString(),
				"[",
				this.Manufacturer,
				" ",
				this.Model,
				"]"
			});
		}
		internal void NotificationHandler(int capability, int classId, int stream, int reason)
		{
			if (capability != 1024)
			{
				return;
			}
            //if (this.CallManagement != null)
            //{
            //    this.CallManagement.NotificationHandler(classId, stream, reason);
            //}
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("Label", this.Label);
			xmlElement.SetAttribute("Manufacturer", this.Manufacturer);
			xmlElement.SetAttribute("Product", this.Model);
			xmlElement.SetAttribute("DeviceType", this.DeviceType.ToString());
			xmlElement.SetAttribute("UniqueIdentifier", this.UniqueIdentifier);
			xmlElement.SetAttribute("Id", this.Id.ToString());
			if (this.FileSystem != null)
			{
				xmlElement.AppendChild(this.FileSystem.XmlDump(xmlDocument));
			}
			if (this.Phonebooks != null)
			{
				xmlElement.AppendChild(this.Phonebooks.XmlDump(xmlDocument));
			}
			if (this.Port != null)
			{
				xmlElement.AppendChild(this.Port.XmlDump(xmlDocument));
			}
			if (this.Keyboard != null)
			{
				xmlElement.AppendChild(this.Keyboard.XmlDump(xmlDocument));
			}
			if (this.Organizer != null)
			{
				xmlElement.AppendChild(this.Organizer.XmlDump(xmlDocument));
			}
            //if (this.CallManagement != null)
            //{
            //    xmlElement.AppendChild(this.CallManagement.XmlDump(xmlDocument));
            //}
			if (this.Sms != null)
			{
				xmlElement.AppendChild(this.Sms.XmlDump(xmlDocument));
			}
			return xmlElement;
		}
	}
}
