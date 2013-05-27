using System;
using System.Collections.Generic;
using System.Xml;
namespace COMlib
{
	public class CEntryItem : CItem
	{
		public enum EDataType
		{
			Empty,
			Number = 257,
			HomeNumber,
			MobileNumber,
			FaxNumber,
			WorkNumber,
			PagerNumber,
			ModemNumber,
			VideoNumber,
			DtmfNumber,
			HomeMobileNumber = 276,
			HomeFaxNumber,
			HomePagerNumber,
			HomeModemNumber,
			HomeVideoNumber,
			WorkMobileNumber = 286,
			WorkFaxNumber,
			WorkPagerNumber,
			WorkModemNumber,
			WorkVideoNumber,
			PrefNumber = 296,
			PrefMobileNumber,
			PrefFaxNumber,
			CarNumber = 306,
			CarMobileNumber,
			CarFaxNumber,
			MobileModemNumber = 316,
			MobileFaxNumber,
			AssistentNumber,
			ServiceNumber = 384,
			FirstName = 513,
			LastName,
			Job,
			Occupation,
			Company,
			Unit,
			Email,
			Url,
			Note,
			Postal,
			City,
			State,
			Zip,
			Country,
			Street,
			Personal,
			Wvid,
			POBox,
			Addrext,
			MiddleName,
			NamePrefix,
			NameSuffix,
			Nickname,
			HomeEmail,
			HomeUrl,
			WorkEmail,
			WorkUrl,
			PrefEmail,
			VoIPPhone = 592,
			HomeVoIPPhone,
			WorkVoIPPhone,
			PTT = 547,
			Shareview = 543,
			SIP,
			AssistentName = 542,
			Spouse = 545,
			Children,
			ManagerName = 548,
			Label = 640,
			Description,
			Summary,
			Location,
			Text,
			Date = 769,
			Birthday = 1025,
			Anniversary,
			AlarmTime = 1152,
			CompletedTime,
			DueToTime,
			EndTime,
			StartTime,
			Address = 1281,
			HomeAddress,
			WorkAddress,
			PrefAddress,
			Wave = 1793,
			PCM,
			Aiff,
			HintUnknown = 65535,
			HintGroup = 65281,
			HintPicture,
			HintRingtone,
			HintVoicedial,
			SyncUniqueId
		}
		public class CData : CItem
		{
			public class CTextData : CEntryItem.CData
			{
				public readonly int MaxLength;
				internal string _Text;
				public string Text
				{
					get
					{
						return this._Text;
					}
					set
					{
						if (value.Length > this.MaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._Text = value;
						base.HasChanged();
					}
				}
				internal CTextData(CItem parent, CEntryItem.EDataType type, CEntryItem.CDescriptor entryDescriptor) : base(parent)
				{
					this.MaxLength = entryDescriptor.MaxDataLengths[type];
				}
				public override XmlElement XmlDump(XmlDocument xmlDocument)
				{
					XmlElement xmlElement = base.XmlDump(xmlDocument);
					xmlElement.SetAttribute("value", this.Text);
					return xmlElement;
				}
			}
			public class CAddressData : CEntryItem.CData
			{
				public readonly int StreetMaxLength;
				internal string _Street;
				public readonly int AddrextMaxLength;
				internal string _Addrext;
				public readonly int POBoxMaxLength;
				internal string _POBox;
				public readonly int CityMaxLength;
				internal string _City;
				public readonly int StateMaxLength;
				internal string _State;
				public readonly int CountryMaxLength;
				internal string _Country;
				public readonly int ZipMaxLength;
				internal string _Zip;
				public string Street
				{
					get
					{
						return this._Street;
					}
					set
					{
						if (value.Length > this.StreetMaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._Street = value;
						base.HasChanged();
					}
				}
				public string Addrext
				{
					get
					{
						return this._Addrext;
					}
					set
					{
						if (value.Length > this.AddrextMaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._Addrext = value;
						base.HasChanged();
					}
				}
				public string POBox
				{
					get
					{
						return this._POBox;
					}
					set
					{
						if (value.Length > this.POBoxMaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._POBox = value;
						base.HasChanged();
					}
				}
				public string City
				{
					get
					{
						return this._City;
					}
					set
					{
						if (value.Length > this.CityMaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._City = value;
						base.HasChanged();
					}
				}
				public string State
				{
					get
					{
						return this._State;
					}
					set
					{
						if (value.Length > this.StateMaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._State = value;
						base.HasChanged();
					}
				}
				public string Country
				{
					get
					{
						return this._Country;
					}
					set
					{
						if (value.Length > this.CountryMaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._Country = value;
						base.HasChanged();
					}
				}
				public string Zip
				{
					get
					{
						return this._Zip;
					}
					set
					{
						if (value.Length > this.ZipMaxLength)
						{
							throw new Exception(this.ToString() + " : Text to long");
						}
						this._Zip = value;
						base.HasChanged();
					}
				}
				internal CAddressData(CItem parent, CEntryItem.EDataType type, CEntryItem.CDescriptor entryDescriptor) : base(parent)
				{
					this.StreetMaxLength = entryDescriptor.MaxDataLengths[CEntryItem.EDataType.Street];
					this.StateMaxLength = entryDescriptor.MaxDataLengths[CEntryItem.EDataType.State];
					this.CityMaxLength = entryDescriptor.MaxDataLengths[CEntryItem.EDataType.City];
					this.POBoxMaxLength = entryDescriptor.MaxDataLengths[CEntryItem.EDataType.POBox];
					this.ZipMaxLength = entryDescriptor.MaxDataLengths[CEntryItem.EDataType.Zip];
					this.CountryMaxLength = entryDescriptor.MaxDataLengths[CEntryItem.EDataType.Country];
					this.AddrextMaxLength = entryDescriptor.MaxDataLengths[CEntryItem.EDataType.Addrext];
				}
				public override XmlElement XmlDump(XmlDocument xmlDocument)
				{
					XmlElement xmlElement = base.XmlDump(xmlDocument);
					XmlElement xmlElement2 = xmlDocument.CreateElement("Street");
					xmlElement2.SetAttribute("value", this.Street);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("State");
					xmlElement2.SetAttribute("value", this.State);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("City");
					xmlElement2.SetAttribute("value", this.City);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("POBox");
					xmlElement2.SetAttribute("value", this.POBox);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("Zip");
					xmlElement2.SetAttribute("value", this.Zip);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("Country");
					xmlElement2.SetAttribute("value", this.Country);
					xmlElement.AppendChild(xmlElement2);
					xmlElement2 = xmlDocument.CreateElement("Addrext");
					xmlElement2.SetAttribute("value", this.Addrext);
					xmlElement.AppendChild(xmlElement2);
					return xmlElement;
				}
			}
			public class CTimeData : CEntryItem.CData
			{
				internal DateTime _Time;
				public DateTime Time
				{
					get
					{
						return this._Time;
					}
					set
					{
						this._Time = value;
						base.HasChanged();
					}
				}
				internal CTimeData(CItem parent, CEntryItem.EDataType type, CEntryItem.CDescriptor entryDescriptor) : base(parent)
				{
				}
				public override XmlElement XmlDump(XmlDocument xmlDocument)
				{
					XmlElement xmlElement = base.XmlDump(xmlDocument);
					xmlElement.SetAttribute("value", this.Time.ToString());
					return xmlElement;
				}
			}
			public class CNumberData : CEntryItem.CData
			{
				public readonly int MaxLength;
				internal string _Number;
				public string Number
				{
					get
					{
						return this._Number;
					}
					set
					{
						if (value.Length > this.MaxLength)
						{
							throw new Exception(this.ToString() + " : Number too long");
						}
						this._Number = value;
						base.HasChanged();
					}
				}
				internal CNumberData(CItem parent, CEntryItem.EDataType type, CEntryItem.CDescriptor entryDescriptor) : base(parent)
				{
					this.MaxLength = entryDescriptor.MaxDataLengths[type];
				}
				public override XmlElement XmlDump(XmlDocument xmlDocument)
				{
					XmlElement xmlElement = base.XmlDump(xmlDocument);
					xmlElement.SetAttribute("value", this.Number);
					return xmlElement;
				}
			}
			internal CData(CItem parent) : base(parent)
			{
				if (!(parent is CEntryItem))
				{
					throw new Exception("Internal error");
				}
			}
			internal void HasChanged()
			{
				((CEntryItem)base.Parent).HasChanged();
			}
		}
		public class CDescriptor : CItem
		{
			protected internal CEntryItem.EDataType[][] _DataTypes;
			protected internal Dictionary<CEntryItem.EDataType, int> _MaxDataLengths;
			public CEntryItem.EDataType[][] DataTypes
			{
				get
				{
					return this._DataTypes;
				}
			}
			public Dictionary<CEntryItem.EDataType, int> MaxDataLengths
			{
				get
				{
					return this._MaxDataLengths;
				}
			}
			protected CDescriptor(CItem parent) : base(parent)
			{
			}
			public override XmlElement XmlDump(XmlDocument xmlDocument)
			{
				XmlElement xmlElement = base.XmlDump(xmlDocument);
				CEntryItem.EDataType[][] dataTypes = this.DataTypes;
				for (int i = 0; i < dataTypes.Length; i++)
				{
					CEntryItem.EDataType[] array = dataTypes[i];
					XmlElement xmlElement2 = xmlDocument.CreateElement("item");
					CEntryItem.EDataType[] array2 = array;
					for (int j = 0; j < array2.Length; j++)
					{
						CEntryItem.EDataType eDataType = array2[j];
						XmlElement xmlElement3 = xmlDocument.CreateElement("data");
						xmlElement3.SetAttribute("Type", eDataType.ToString());
						xmlElement3.SetAttribute("MaxLength", this.MaxDataLengths[eDataType].ToString());
						xmlElement2.AppendChild(xmlElement3);
					}
					xmlElement.AppendChild(xmlElement2);
				}
				return xmlElement;
			}
		}
		private CEntryItem.EDataType _DataType;
		private CEntryItem.CData _Data;
		public readonly CEntryItem.EDataType[] SupportedTypes;
		public CEntryItem.CData Data
		{
			get
			{
				return this._Data;
			}
		}
		public CEntryItem.EDataType DataType
		{
			get
			{
				return this._DataType;
			}
			set
			{
				if (value == this._DataType)
				{
					return;
				}
				if (value != CEntryItem.EDataType.Empty && Array.IndexOf<CEntryItem.EDataType>(this.SupportedTypes, value) < 0)
				{
					throw new Exception(this.ToString() + " : Unsupported type");
				}
				this.HasChanged();
				this._DataType = value;
				int num = (int)(value & (CEntryItem.EDataType)65280);
				if (num <= 512)
				{
					if (num == 256)
					{
						this._Data = new CEntryItem.CData.CNumberData(this, value, ((CEntry)base.Parent).EntryDescriptor);
						return;
					}
					if (num == 512)
					{
						this._Data = new CEntryItem.CData.CTextData(this, value, ((CEntry)base.Parent).EntryDescriptor);
						return;
					}
				}
				else
				{
					if (num == 768 || num == 1024)
					{
						this._Data = new CEntryItem.CData.CTimeData(this, value, ((CEntry)base.Parent).EntryDescriptor);
						return;
					}
					if (num == 1280)
					{
						this._Data = new CEntryItem.CData.CAddressData(this, value, ((CEntry)base.Parent).EntryDescriptor);
						return;
					}
				}
				this._Data = null;
			}
		}
		public CEntryItem(CItem parent, CEntryItem.EDataType[] supportedTypes) : base(parent)
		{
			if (!(parent is CEntry))
			{
				parent = null;
				throw new Exception("Internal error");
			}
			this.DataType = CEntryItem.EDataType.Empty;
			this.SupportedTypes = supportedTypes;
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("type", this.DataType.ToString());
			xmlElement.AppendChild(this.Data.XmlDump(xmlDocument));
			return xmlElement;
		}
		internal void HasChanged()
		{
			((CEntry)base.Parent).HasChanged();
		}
	}
}
