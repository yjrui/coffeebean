using MobilEditCom;
using System;
namespace COMlib
{
	public class COrganizerEntry : CEntry
	{
		internal IOrganizerItem MedOrganizerItem;
		internal COrganizerEntry(CItem parent, IOrganizerItem medOrganizerItem, CEntryItem.CDescriptor entryDescriptor) : base(parent)
		{
			if (!(parent is COrganizerEntries))
			{
				throw new Exception("Internal error");
			}
			this.EntryDescriptor = entryDescriptor;
			this.Items = new CEntryItem[this.EntryDescriptor.DataTypes.Length];
			this.MedOrganizerItem = medOrganizerItem;
			for (int i = 0; i < this.EntryDescriptor.DataTypes.Length; i++)
			{
				CEntryItem.EDataType eDataType = this.EntryDescriptor.DataTypes[i][0];
				this.Items[i] = new CEntryItem(this, this.EntryDescriptor.DataTypes[i]);
				this.Items[i].DataType = eDataType;
				int num = 1;
				try
				{
					CEntryItem.EDataType eDataType2 = eDataType;
					if (eDataType2 != CEntryItem.EDataType.Number)
					{
						switch (eDataType2)
						{
						case CEntryItem.EDataType.Description:
							(this.Items[i].Data as CEntryItem.CData.CTextData).Text = this.MedOrganizerItem.Description;
							if (this.MedOrganizerItem.Description == "")
							{
								num = 0;
							}
							break;
						case CEntryItem.EDataType.Summary:
							(this.Items[i].Data as CEntryItem.CData.CTextData).Text = this.MedOrganizerItem.Summary;
							if (this.MedOrganizerItem.Summary == "")
							{
								num = 0;
							}
							break;
						case CEntryItem.EDataType.Location:
							(this.Items[i].Data as CEntryItem.CData.CTextData).Text = this.MedOrganizerItem.Location;
							if (this.MedOrganizerItem.Location == "")
							{
								num = 0;
							}
							break;
						default:
							switch (eDataType2)
							{
							case CEntryItem.EDataType.AlarmTime:
							{
								int num2;
								(this.Items[i].Data as CEntryItem.CData.CTimeData).Time = this.MedOrganizerItem.GetAudioAlarmTimestamp(out num2);
								if (num2 == 0)
								{
									num = 0;
								}
								break;
							}
							case CEntryItem.EDataType.CompletedTime:
								(this.Items[i].Data as CEntryItem.CData.CTimeData).Time = this.MedOrganizerItem.GetCompletedTimestamp(out num);
								break;
							case CEntryItem.EDataType.DueToTime:
								(this.Items[i].Data as CEntryItem.CData.CTimeData).Time = this.MedOrganizerItem.GetDueToTimestamp(out num);
								break;
							case CEntryItem.EDataType.EndTime:
								(this.Items[i].Data as CEntryItem.CData.CTimeData).Time = this.MedOrganizerItem.GetEndTimestamp(out num);
								break;
							case CEntryItem.EDataType.StartTime:
								(this.Items[i].Data as CEntryItem.CData.CTimeData).Time = this.MedOrganizerItem.GetStartTimestamp(out num);
								break;
							}
							break;
						}
					}
					else
					{
						(this.Items[i].Data as CEntryItem.CData.CNumberData).Number = this.MedOrganizerItem.PhoneNumber;
						if (this.MedOrganizerItem.PhoneNumber == "")
						{
							num = 0;
						}
					}
					if (num == 0)
					{
						this.Items[i].DataType = CEntryItem.EDataType.Empty;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(this.ToString() + " : [ " + ex.Message + " ]");
					this.Items[i].DataType = CEntryItem.EDataType.Empty;
				}
			}
			this._Changed = false;
		}
		public override void Delete()
		{
			(base.Parent as COrganizerEntries).DeleteEntry(this, (base.Parent as COrganizerEntries).Index);
			this.Invalidate();
		}
		internal override void Invalidate()
		{
			this.MedOrganizerItem = null;
			base.Invalidate();
		}
		internal void Commit()
		{
			if (base.Changed)
			{
				this.MedOrganizerItem.Delete();
				Console.WriteLine(this.Items.Length);
				for (int i = 0; i < this.Items.Length; i++)
				{
					CEntryItem.EDataType dataType = this.Items[i].DataType;
					if (dataType != CEntryItem.EDataType.Number)
					{
						switch (dataType)
						{
						case CEntryItem.EDataType.Description:
							this.MedOrganizerItem.Description = (this.Items[i].Data as CEntryItem.CData.CTextData).Text;
							break;
						case CEntryItem.EDataType.Summary:
							this.MedOrganizerItem.Summary = (this.Items[i].Data as CEntryItem.CData.CTextData).Text;
							break;
						case CEntryItem.EDataType.Location:
							this.MedOrganizerItem.Location = (this.Items[i].Data as CEntryItem.CData.CTextData).Text;
							break;
						default:
							switch (dataType)
							{
							case CEntryItem.EDataType.AlarmTime:
								this.MedOrganizerItem.SetAudioAlarmTimestamp(2, (this.Items[i].Data as CEntryItem.CData.CTimeData).Time);
								break;
							case CEntryItem.EDataType.CompletedTime:
								this.MedOrganizerItem.SetCompletedTimestamp(1, (this.Items[i].Data as CEntryItem.CData.CTimeData).Time);
								break;
							case CEntryItem.EDataType.DueToTime:
								this.MedOrganizerItem.SetDueToTimestamp(1, (this.Items[i].Data as CEntryItem.CData.CTimeData).Time);
								break;
							case CEntryItem.EDataType.EndTime:
								this.MedOrganizerItem.SetEndTimestamp(1, (this.Items[i].Data as CEntryItem.CData.CTimeData).Time);
								break;
							case CEntryItem.EDataType.StartTime:
								this.MedOrganizerItem.SetStartTimestamp(1, (this.Items[i].Data as CEntryItem.CData.CTimeData).Time);
								break;
							}
							break;
						}
					}
					else
					{
						this.MedOrganizerItem.PhoneNumber = (this.Items[i].Data as CEntryItem.CData.CNumberData).Number;
					}
				}
				this.MedOrganizerItem.Type = (int)(base.Parent as COrganizerEntries).Type;
				this.MedOrganizerItem.Save();
			}
		}
	}
}
