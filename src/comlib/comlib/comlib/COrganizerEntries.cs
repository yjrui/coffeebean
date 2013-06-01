using MobilEditCom;
using System;
using System.Collections.Generic;
using System.Xml;
namespace COMlib
{
	public class COrganizerEntries : CEntries
	{
		public class CEntryDescriptor : CEntryItem.CDescriptor
		{
			private static CEntryItem.EDataType[] entryTypes = new CEntryItem.EDataType[]
			{
				CEntryItem.EDataType.Description,
				CEntryItem.EDataType.AlarmTime,
				CEntryItem.EDataType.CompletedTime,
				CEntryItem.EDataType.DueToTime,
				CEntryItem.EDataType.EndTime,
				CEntryItem.EDataType.StartTime,
				CEntryItem.EDataType.Location,
				CEntryItem.EDataType.Number,
				CEntryItem.EDataType.Summary
			};
			public CEntryDescriptor(CItem parent, IOrganizer organizer) : base(parent)
			{
				if (!(parent is COrganizerEntries))
				{
					throw new Exception("Internal error");
				}
				int num = COrganizerEntries.CEntryDescriptor.entryTypes.Length;
				if (organizer.SupportsAudioAlarmTimestamp == 0)
				{
					num--;
				}
				if (organizer.SupportsCompletedTimestamp == 0)
				{
					num--;
				}
				if (organizer.SupportsDueToTimestamp == 0)
				{
					num--;
				}
				if (organizer.SupportsEndTimestamp == 0)
				{
					num--;
				}
				if (organizer.SupportsStartTimestamp == 0)
				{
					num--;
				}
				if (organizer.DescriptionCharacters == 0)
				{
					num--;
				}
				if (organizer.LocationCharacters == 0)
				{
					num--;
				}
				if (organizer.SummaryCharacters == 0)
				{
					num--;
				}
				this._MaxDataLengths = new Dictionary<CEntryItem.EDataType, int>();
				this._DataTypes = new CEntryItem.EDataType[num][];
				int num2 = 0;
				for (int i = 0; i < COrganizerEntries.CEntryDescriptor.entryTypes.Length; i++)
				{
					bool flag = true;
					CEntryItem.EDataType eDataType = COrganizerEntries.CEntryDescriptor.entryTypes[i];
					if (eDataType != CEntryItem.EDataType.Number)
					{
						switch (eDataType)
						{
						case CEntryItem.EDataType.Description:
							if (organizer.DescriptionCharacters == 0)
							{
								flag = false;
							}
							this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = organizer.DescriptionCharacters;
							break;
						case CEntryItem.EDataType.Summary:
							if (organizer.SummaryCharacters == 0)
							{
								flag = false;
							}
							this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = organizer.SummaryCharacters;
							break;
						case CEntryItem.EDataType.Location:
							if (organizer.LocationCharacters == 0)
							{
								flag = false;
							}
							this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = organizer.LocationCharacters;
							break;
						default:
							switch (eDataType)
							{
							case CEntryItem.EDataType.AlarmTime:
								flag = (organizer.SupportsAudioAlarmTimestamp != 0);
								this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = 65536;
								break;
							case CEntryItem.EDataType.CompletedTime:
								flag = (organizer.SupportsCompletedTimestamp != 0);
								this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = 65536;
								break;
							case CEntryItem.EDataType.DueToTime:
								flag = (organizer.SupportsDueToTimestamp != 0);
								this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = 65536;
								break;
							case CEntryItem.EDataType.EndTime:
								flag = (organizer.SupportsEndTimestamp != 0);
								this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = 65536;
								break;
							case CEntryItem.EDataType.StartTime:
								flag = (organizer.SupportsStartTimestamp != 0);
								this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = 65536;
								break;
							}
							break;
						}
					}
					else
					{
						this._MaxDataLengths[COrganizerEntries.CEntryDescriptor.entryTypes[i]] = 15;
					}
					if (flag)
					{
						this._DataTypes[num2] = new CEntryItem.EDataType[1];
						this._MaxDataLengths[CEntryItem.EDataType.Empty] = 0;
						this._DataTypes[num2][0] = COrganizerEntries.CEntryDescriptor.entryTypes[i];
						num2++;
					}
				}
			}
		}
		public readonly bool ReadOnly;
		private readonly bool Growable;
		internal IOrganizer MedOrganizer;
		internal COrganizer.EType Type;
		public COrganizerEntries.CEntryDescriptor EntryDescriptor;
		internal int Index;
		public override int MaxEntries
		{
			get
			{
				int result;
				try
				{
					if (this.Growable)
					{
						result = 0;
					}
					else
					{
						result = this.MedOrganizer.StreamCount;
					}
				}
				catch (Exception)
				{
					result = 0;
				}
				return result;
			}
		}
		internal COrganizerEntries(CItem parent, IOrganizer medOrganizer, int index, COrganizer.EType type) : base(parent)
		{
			if (!(parent is COrganizer))
			{
				throw new Exception("Internal error");
			}
			this.Index = index;
			this.Type = type;
			this.MedOrganizer = medOrganizer;
			this.EntryDescriptor = new COrganizerEntries.CEntryDescriptor(this, this.MedOrganizer);
			try
			{
				this.ReadOnly = (this.MedOrganizer.ReadOnly != 0);
			}
			catch (Exception ex)
			{
				Console.WriteLine(this.ToString() + "Unable to query ReadOnly property [ " + ex.Message + " ]");
				this.ReadOnly = true;
			}
			try
			{
				this.Growable = (this.MedOrganizer.Growable != 0);
			}
			catch (Exception ex2)
			{
				Console.WriteLine(this.ToString() + "Unable to query Growable property [ " + ex2.Message + " ]");
				this.Growable = false;
				this.ReadOnly = true;
			}
		}
		public override CEntry CreateEntry()
		{
			if (this.ReadOnly)
			{
				return null;
			}
			if ((base.Parent as COrganizer).EmptyLists[this.Index].Count != 0)
			{
				COrganizerEntry cOrganizerEntry = new COrganizerEntry(this, (base.Parent as COrganizer).EmptyLists[this.Index].Last.Value, this.EntryDescriptor);
				(base.Parent as COrganizer).EmptyLists[this.Index].RemoveLast();
				base.AddLast(cOrganizerEntry);
				return cOrganizerEntry;
			}
			if ((base.Parent as COrganizer).EmptyListsToPush[this.Index].Count != 0)
			{
				COrganizerEntry cOrganizerEntry2 = new COrganizerEntry(this, (base.Parent as COrganizer).EmptyListsToPush[this.Index].Last.Value, this.EntryDescriptor);
				(base.Parent as COrganizer).EmptyListsToPush[this.Index].RemoveLast();
				base.AddLast(cOrganizerEntry2);
				return cOrganizerEntry2;
			}
			if (!this.Growable)
			{
				return null;
			}
			IOrganizerItem medOrganizerItem;
			try
			{
				medOrganizerItem = (this.MedOrganizer.AddStream() as IOrganizerItem);
			}
			catch (Exception)
			{
				return null;
			}
			COrganizerEntry cOrganizerEntry3 = new COrganizerEntry(this, medOrganizerItem, this.EntryDescriptor);
			base.AddLast(cOrganizerEntry3);
			return cOrganizerEntry3;
		}
		internal void InvalidateChildren()
		{
			foreach (COrganizerEntry cOrganizerEntry in this)
			{
				cOrganizerEntry.Invalidate();
			}
			base.Clear();
		}
		internal override void Invalidate()
		{
			this.InvalidateChildren();
		}
		internal void DeleteEntry(COrganizerEntry entry, int index)
		{
			entry.MedOrganizerItem.Delete();
			(base.Parent as COrganizer).EmptyListsToPush[index].AddLast(entry.MedOrganizerItem);
			for (int i = 0; i < (base.Parent as COrganizer).MedOrganizers[index].StreamCount; i++)
			{
				IOrganizerItem organizerItem = (base.Parent as COrganizer).MedOrganizers[index].GetStream(i + 1) as IOrganizerItem;
				organizerItem.GetType();
			}
			this.Remove(entry);
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("ReadOnly", this.ReadOnly.ToString());
			xmlElement.SetAttribute("Growable", this.Growable.ToString());
			if (xmlElement.ChildNodes.Count >= 1)
			{
				xmlElement.InsertBefore(this.EntryDescriptor.XmlDump(xmlDocument), xmlElement.FirstChild);
			}
			else
			{
				xmlElement.AppendChild(this.EntryDescriptor.XmlDump(xmlDocument));
			}
			return xmlElement;
		}
	}
}
