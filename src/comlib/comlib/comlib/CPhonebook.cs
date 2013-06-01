using MobilEditCom;
using System;
using System.Collections.Generic;
using System.Xml;
namespace COMlib
{
	public class CPhonebook : CItemNode
	{
		public enum EType
		{
			Phonebook = 16777216,
			Dialed,
			Fixed = 16777220,
			Missed = 16777219,
			Received = 16777218,
			Own = 16777221
		}
		public enum ESameLabels
		{
			Forbiden,
			Allowed,
			SameContact
		}
		public class CEntryDescriptor : CEntryItem.CDescriptor
		{
			private static CEntryItem.EDataType[] addressTypes = new CEntryItem.EDataType[]
			{
				CEntryItem.EDataType.Address,
				CEntryItem.EDataType.HomeAddress,
				CEntryItem.EDataType.WorkAddress,
				CEntryItem.EDataType.PrefAddress
			};
			private static CEntryItem.EDataType[] addressItems = new CEntryItem.EDataType[]
			{
				CEntryItem.EDataType.Street,
				CEntryItem.EDataType.Addrext,
				CEntryItem.EDataType.POBox,
				CEntryItem.EDataType.City,
				CEntryItem.EDataType.State,
				CEntryItem.EDataType.Country,
				CEntryItem.EDataType.Zip
			};
			internal CEntryDescriptor(CItem parent, IPhonebookDescription medPhonebookDescription) : base(parent)
			{
				if (!(parent is CPhonebook))
				{
					throw new Exception("Internal error");
				}
				this._MaxDataLengths = new Dictionary<CEntryItem.EDataType, int>();
				bool flag = false;
				CEntryItem.EDataType[] array = CPhonebook.CEntryDescriptor.addressTypes;
				for (int i = 0; i < array.Length; i++)
				{
					CEntryItem.EDataType lType = array[i];
					int num;
					medPhonebookDescription.IsTypeSupported((int)lType, out num);
					if (num != 0)
					{
						flag = true;
					}
				}
				if (flag)
				{
					CEntryItem.EDataType[] array2 = CPhonebook.CEntryDescriptor.addressItems;
					for (int j = 0; j < array2.Length; j++)
					{
						CEntryItem.EDataType eDataType = array2[j];
						int value;
						medPhonebookDescription.GetTypeCharacters((int)eDataType, out value);
						if (!base.MaxDataLengths.ContainsKey(eDataType))
						{
							base.MaxDataLengths[eDataType] = value;
						}
					}
				}
				int num2 = 0;
				int groupsCount = medPhonebookDescription.GroupsCount;
				for (int k = 1; k <= groupsCount; k++)
				{
					num2 += medPhonebookDescription.GetGroupPositions(k);
				}
				bool flag2 = ((CPhonebook)parent).MergedLabels.HasValue && ((CPhonebook)parent).MergedLabels.Value;
				if (!flag2)
				{
					num2++;
				}
				CEntryItem.EDataType[][] array3 = new CEntryItem.EDataType[num2][];
				int num3 = 0;
				if (!flag2)
				{
					array3[num3] = new CEntryItem.EDataType[1];
					array3[num3][0] = CEntryItem.EDataType.Label;
					base.MaxDataLengths[CEntryItem.EDataType.Label] = medPhonebookDescription.LabelCharacters;
					num3++;
				}
				for (int l = 1; l <= groupsCount; l++)
				{
					int groupPositions = medPhonebookDescription.GetGroupPositions(l);
					for (int m = 1; m <= groupPositions; m++)
					{
						int groupTypes = medPhonebookDescription.GetGroupTypes(l);
						array3[num3] = new CEntryItem.EDataType[groupTypes];
						for (int n = 1; n <= groupTypes; n++)
						{
							CEntryItem.EDataType groupType = (CEntryItem.EDataType)medPhonebookDescription.GetGroupType(l, n);
							if (!base.MaxDataLengths.ContainsKey(groupType))
							{
								int value2;
								medPhonebookDescription.GetTypeCharacters((int)groupType, out value2);
								base.MaxDataLengths[groupType] = value2;
							}
							array3[num3][n - 1] = groupType;
							if (Array.IndexOf<CEntryItem.EDataType>(CPhonebook.CEntryDescriptor.addressItems, groupType) >= 0)
							{
								num3--;
								break;
							}
						}
						num3++;
					}
				}
				this._DataTypes = new CEntryItem.EDataType[num3][];
				for (int num4 = 0; num4 < num3; num4++)
				{
					this._DataTypes[num4] = array3[num4];
				}
			}
		}
		public readonly CPhonebook.ESameLabels SameLabels;
		public readonly CPhonebook.EType Type;
		internal readonly bool? DefaultNumber;
		internal readonly bool? FakeIndexes;
		internal readonly int? ItemBase;
		internal readonly bool? MergedLabels;
		public readonly bool ReadOnly;
		internal readonly bool? Volatile;
		internal readonly bool? Growable;
		internal readonly bool? EmptyItems;
		public readonly CPhonebookEntries Entries;
		public readonly CPhonebook.CEntryDescriptor EntryDescriptor;
		internal IPhonebook MedPhonebook;
		internal CPhonebook(CItem parent, IClass medClass) : base(parent)
		{
			if (!(parent is CPhonebooks))
			{
				throw new Exception("Internal error");
			}
			this.MedPhonebook = (IPhonebook)medClass;
			int id = this.MedPhonebook.Id;
			switch (id)
			{
			case 16777216:
				break;
			case 16777217:
				goto IL_A9;
			case 16777218:
				goto IL_C3;
			case 16777219:
				goto IL_B6;
			case 16777220:
				goto IL_D0;
			case 16777221:
				goto IL_DD;
			default:
				switch (id)
				{
				case 16809984:
					break;
				case 16809985:
					goto IL_A9;
				case 16809986:
					goto IL_C3;
				case 16809987:
					goto IL_B6;
				default:
					switch (id)
					{
					case 16826368:
						goto IL_9C;
					case 16826369:
						goto IL_A9;
					case 16826372:
						goto IL_D0;
					case 16826373:
						goto IL_DD;
					}
					Console.WriteLine(this.ToString() + " : Unknown phonebook identifier");
					this.Type = (CPhonebook.EType)this.MedPhonebook.Id;
					goto IL_110;
				}
				break;
			}
			IL_9C:
			this.Type = CPhonebook.EType.Phonebook;
			goto IL_110;
			IL_A9:
			this.Type = CPhonebook.EType.Dialed;
			goto IL_110;
			IL_B6:
			this.Type = CPhonebook.EType.Missed;
			goto IL_110;
			IL_C3:
			this.Type = CPhonebook.EType.Received;
			goto IL_110;
			IL_D0:
			this.Type = CPhonebook.EType.Fixed;
			goto IL_110;
			IL_DD:
			this.Type = CPhonebook.EType.Own;
			IL_110:
			this.Entries = new CPhonebookEntries(this, this.MedPhonebook);
			try
			{
				this.DefaultNumber = new bool?(this.MedPhonebook.DefaultNumber != 0);
			}
			catch (Exception ex)
			{
				Console.WriteLine(this.ToString() + " : Unable to query DefaultNumber property [ " + ex.Message + " ]");
				this.DefaultNumber = null;
			}
			try
			{
				this.FakeIndexes = new bool?(this.MedPhonebook.FakeIndexes != 0);
			}
			catch (Exception ex2)
			{
				Console.WriteLine(this.ToString() + " : Unable to query FakeIndexes property [ " + ex2.Message + " ]");
				this.FakeIndexes = null;
			}
			try
			{
				this.ItemBase = new int?(this.MedPhonebook.ItemBase);
			}
			catch (Exception ex3)
			{
				Console.WriteLine(this.ToString() + " : Unable to query ItemBase property [ " + ex3.Message + " ]");
				this.ItemBase = null;
			}
			try
			{
				this.MergedLabels = new bool?(this.MedPhonebook.MergedLabels != 0);
			}
			catch (Exception ex4)
			{
				Console.WriteLine(this.ToString() + " : Unable to query MergedLabels property [ " + ex4.Message + " ]");
				this.MergedLabels = null;
			}
			try
			{
				this.SameLabels = (CPhonebook.ESameLabels)this.MedPhonebook.SameLabels;
			}
			catch (Exception ex5)
			{
				Console.WriteLine(this.ToString() + " : Unable to query SameLabels property [ " + ex5.Message + " ]");
			}
			try
			{
				this.ReadOnly = (this.MedPhonebook.ReadOnly != 0);
			}
			catch (Exception ex6)
			{
				Console.WriteLine(this.ToString() + " : Unable to query Readonly property [ " + ex6.Message + " ]");
				this.ReadOnly = true;
			}
			try
			{
				this.Volatile = new bool?(this.MedPhonebook.Volatile != 0);
			}
			catch (Exception ex7)
			{
				Console.WriteLine(this.ToString() + " : Unable to query Volatile property [ " + ex7.Message + " ]");
				this.Volatile = null;
			}
			try
			{
				this.Growable = new bool?(this.MedPhonebook.Growable != 0);
			}
			catch (Exception ex8)
			{
				Console.WriteLine(this.ToString() + " : Unable to query Growable property [ " + ex8.Message + " ]");
				this.Growable = null;
			}
			try
			{
				this.EmptyItems = new bool?(this.MedPhonebook.EmptyItems != 0);
			}
			catch (Exception ex9)
			{
				Console.WriteLine(this.ToString() + " : Unable to query EmptyItems property [ " + ex9.Message + " ]");
				this.EmptyItems = null;
			}
			IPhonebookDescription phonebookDescription = null;
			try
			{
				phonebookDescription = (IPhonebookDescription)this.MedPhonebook.PhonebookDescription;
			}
			catch (Exception ex10)
			{
				Console.WriteLine(this.ToString() + " : Unable to query PhonebookDescription [ " + ex10.Message + " ]");
			}
			if (phonebookDescription != null)
			{
				this.EntryDescriptor = new CPhonebook.CEntryDescriptor(this, phonebookDescription);
				return;
			}
			this.EntryDescriptor = null;
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("SameLabels", this.SameLabels.ToString());
			xmlElement.SetAttribute("Type", this.Type.ToString());
			if (this.DefaultNumber.HasValue)
			{
				xmlElement.SetAttribute("DefaultNumber", this.DefaultNumber.Value.ToString());
			}
			if (this.FakeIndexes.HasValue)
			{
				xmlElement.SetAttribute("FakeIndexes", this.FakeIndexes.Value.ToString());
			}
			if (this.ItemBase.HasValue)
			{
				xmlElement.SetAttribute("ItemBase", this.ItemBase.Value.ToString());
			}
			if (this.MergedLabels.HasValue)
			{
				xmlElement.SetAttribute("MergedLabels", this.MergedLabels.Value.ToString());
			}
			xmlElement.SetAttribute("ReadOnly", this.ReadOnly.ToString());
			if (this.Volatile.HasValue)
			{
				xmlElement.SetAttribute("Volatile", this.Volatile.Value.ToString());
			}
			if (this.Growable.HasValue)
			{
				xmlElement.SetAttribute("Growable", this.Growable.Value.ToString());
			}
			if (this.EmptyItems.HasValue)
			{
				xmlElement.SetAttribute("EmptyItems", this.EmptyItems.Value.ToString());
			}
			if (this.EntryDescriptor != null)
			{
				xmlElement.AppendChild(this.EntryDescriptor.XmlDump(xmlDocument));
			}
			xmlElement.AppendChild(this.Entries.XmlDump(xmlDocument));
			return xmlElement;
		}
		public override string ToString()
		{
			return base.ToString() + "[" + this.Type.ToString() + "]";
		}
	}
}
