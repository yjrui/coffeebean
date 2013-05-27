using MobilEditCom;
using System;
namespace COMlib
{
	public class CPhonebookEntry : CEntry
	{
		internal IPhonebookItem MedPhonebookItem;
		internal CPhonebookEntry(CItem parent, IPhonebookItem medPhonebookItem) : base(parent)
		{
			if (!(parent is CPhonebookEntries))
			{
				throw new Exception("Internal error");
			}
			this.EntryDescriptor = ((CPhonebook)base.Parent.Parent).EntryDescriptor;
			this.MedPhonebookItem = medPhonebookItem;
			this.Items = new CEntryItem[this.EntryDescriptor.DataTypes.Length];
			for (int i = 0; i < this.Items.Length; i++)
			{
				this.Items[i] = new CEntryItem(this, this.EntryDescriptor.DataTypes[i]);
			}
			if (!((CPhonebook)base.Parent.Parent).MergedLabels.HasValue || !((CPhonebook)base.Parent.Parent).MergedLabels.Value)
			{
				for (int j = 0; j < this.EntryDescriptor.DataTypes.Length; j++)
				{
					if (this.Items[j].DataType == CEntryItem.EDataType.Empty && Array.IndexOf<CEntryItem.EDataType>(this.EntryDescriptor.DataTypes[j], CEntryItem.EDataType.Label) >= 0)
					{
						this.Items[j].DataType = CEntryItem.EDataType.Label;
						if (this.Items[j].Data is CEntryItem.CData.CTextData)
						{
							CEntryItem.CData.CTextData cTextData = (CEntryItem.CData.CTextData)this.Items[j].Data;
							cTextData._Text = this.MedPhonebookItem.Label;
						}
					}
				}
			}
			int itemCount = this.MedPhonebookItem.ItemCount;
			for (int k = 1; k <= itemCount; k++)
			{
				CEntryItem.EDataType itemType = (CEntryItem.EDataType)this.MedPhonebookItem.GetItemType(k);
				int l = 0;
				while (l < this.EntryDescriptor.DataTypes.Length)
				{
					if (this.Items[l].DataType == CEntryItem.EDataType.Empty && Array.IndexOf<CEntryItem.EDataType>(this.EntryDescriptor.DataTypes[l], itemType) >= 0)
					{
						this.Items[l].DataType = itemType;
						if (this.Items[l].Data is CEntryItem.CData.CAddressData)
						{
							CEntryItem.CData.CAddressData cAddressData = (CEntryItem.CData.CAddressData)this.Items[l].Data;
							IPhonebookAddress phonebookAddress = (IPhonebookAddress)this.MedPhonebookItem.GetItemAddress(k);
							cAddressData._Addrext = phonebookAddress.GetText(531);
							cAddressData._Street = phonebookAddress.GetText(527);
							cAddressData._City = phonebookAddress.GetText(523);
							cAddressData._Country = phonebookAddress.GetText(530);
							cAddressData._POBox = phonebookAddress.GetText(527);
							cAddressData._State = phonebookAddress.GetText(524);
							cAddressData._Zip = phonebookAddress.GetText(525);
						}
						if (this.Items[l].Data is CEntryItem.CData.CTextData)
						{
							CEntryItem.CData.CTextData cTextData2 = (CEntryItem.CData.CTextData)this.Items[l].Data;
							cTextData2._Text = this.MedPhonebookItem.GetItemText(k);
						}
						if (this.Items[l].Data is CEntryItem.CData.CNumberData)
						{
							CEntryItem.CData.CNumberData cNumberData = (CEntryItem.CData.CNumberData)this.Items[l].Data;
							cNumberData._Number = this.MedPhonebookItem.GetItemText(k);
						}
						if (!(this.Items[l].Data is CEntryItem.CData.CTimeData))
						{
							break;
						}
						CEntryItem.CData.CTimeData cTimeData = (CEntryItem.CData.CTimeData)this.Items[l].Data;
						int num;
						cTimeData._Time = this.MedPhonebookItem.GetItemTimestamp(k, out num);
						if (num == 0)
						{
							this.Items[l].DataType = CEntryItem.EDataType.Empty;
							break;
						}
						break;
					}
					else
					{
						l++;
					}
				}
			}
			this._Changed = false;
		}
		public override void Delete()
		{
			(base.Parent as CPhonebookEntries).DeleteEntry(this);
			this.Invalidate();
		}
		internal override void Invalidate()
		{
			this.MedPhonebookItem = null;
			base.Invalidate();
		}
		internal void Commit()
		{
			if (base.Changed)
			{
				this.MedPhonebookItem.Delete();
				for (int i = 0; i < this.Items.Length; i++)
				{
					if (this.Items[i].DataType == CEntryItem.EDataType.Label)
					{
						this.MedPhonebookItem.Label = (this.Items[i].Data as CEntryItem.CData.CTextData).Text;
					}
					else
					{
						if (this.Items[i].Data is CEntryItem.CData.CAddressData)
						{
							CEntryItem.CData.CAddressData cAddressData = this.Items[i].Data as CEntryItem.CData.CAddressData;
							IPhonebookAddress phonebookAddress = this.MedPhonebookItem.AddAddress((int)this.Items[i].DataType) as IPhonebookAddress;
							phonebookAddress.SetText(523, cAddressData.City);
							phonebookAddress.SetText(531, cAddressData.Addrext);
							phonebookAddress.SetText(527, cAddressData.Street);
							phonebookAddress.SetText(530, cAddressData.POBox);
							phonebookAddress.SetText(526, cAddressData.Country);
							phonebookAddress.SetText(525, cAddressData.Zip);
						}
						else
						{
							if (this.Items[i].Data is CEntryItem.CData.CNumberData)
							{
								this.MedPhonebookItem.AddNumber((int)this.Items[i].DataType, (this.Items[i].Data as CEntryItem.CData.CNumberData).Number, 129);
							}
							else
							{
								if (this.Items[i].Data is CEntryItem.CData.CTextData)
								{
									this.MedPhonebookItem.AddText((int)this.Items[i].DataType, (this.Items[i].Data as CEntryItem.CData.CTextData).Text);
								}
								else
								{
									if (this.Items[i].Data is CEntryItem.CData.CTimeData)
									{
										this.MedPhonebookItem.AddTimestamp((int)this.Items[i].DataType, (this.Items[i].Data as CEntryItem.CData.CTimeData).Time);
									}
								}
							}
						}
					}
				}
				this.MedPhonebookItem.Save();
			}
		}
	}
}
