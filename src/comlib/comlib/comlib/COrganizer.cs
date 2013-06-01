using MobilEditCom;
using System;
using System.Collections.Generic;
using System.Xml;
namespace COMlib
{
	public class COrganizer : CItemList
	{
		internal enum EType
		{
			Events = 1,
			Tasks,
			Notes
		}
		public readonly CEvents Events;
		public readonly CTasks Tasks;
		public readonly CNotes Notes;
		internal IOrganizer[] MedOrganizers;
		internal LinkedList<IOrganizerItem>[] EmptyLists;
		internal LinkedList<IOrganizerItem>[] EmptyListsToPush;
		internal COrganizer(CItem parent, ICapability medCapability) : base(parent)
		{
			if (!(parent is CSource))
			{
				throw new Exception("Internal error");
			}
			int num;
			try
			{
				num = medCapability.ClassCount;
			}
			catch (Exception ex)
			{
				Console.WriteLine(this.ToString() + "Unable to query ClassCount property [ " + ex.Message + " ]");
				num = 0;
			}
			this.MedOrganizers = new IOrganizer[num];
			this.EmptyLists = new LinkedList<IOrganizerItem>[num];
			this.EmptyListsToPush = new LinkedList<IOrganizerItem>[num];
			for (int i = 1; i <= num; i++)
			{
				IOrganizer organizer = medCapability.GetClass(i) as IOrganizer;
				this.EmptyLists[i - 1] = new LinkedList<IOrganizerItem>();
				this.EmptyListsToPush[i - 1] = new LinkedList<IOrganizerItem>();
				if (organizer.SupportsEvents != 0)
				{
					if (this.Events == null)
					{
						this.Events = new CEvents(this, organizer, i - 1, COrganizer.EType.Events);
					}
					else
					{
						Console.WriteLine(this.ToString() + " : Multiple streams for Events");
					}
				}
				if (organizer.SupportsNotes != 0)
				{
					if (this.Notes == null)
					{
						this.Notes = new CNotes(this, organizer, i - 1, COrganizer.EType.Notes);
					}
					else
					{
						Console.WriteLine(this.ToString() + " : Multiple streams for Notes");
					}
				}
				if (organizer.SupportsTasks != 0 && this.Tasks == null)
				{
					if (this.Tasks == null)
					{
						this.Tasks = new CTasks(this, organizer, i - 1, COrganizer.EType.Tasks);
					}
					else
					{
						Console.WriteLine(this.ToString() + " : Multiple streams for Tasks");
					}
				}
				this.MedOrganizers[i - 1] = organizer;
			}
		}
		public virtual void Update()
		{
			this.Update(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public bool Update(CDriver.Progress progress)
		{
			if (this.Events != null)
			{
				this.Events.Invalidate();
			}
			if (this.Tasks != null)
			{
				this.Tasks.Invalidate();
			}
			if (this.Notes != null)
			{
				this.Notes.Invalidate();
			}
			for (int i = 0; i < this.EmptyListsToPush.Length; i++)
			{
				this.EmptyListsToPush[i].Clear();
				this.EmptyLists[i].Clear();
			}
			int num = 0;
			int num2 = 1000 / ((this.MedOrganizers.Length > 0) ? this.MedOrganizers.Length : 1);
			for (int j = 0; j < this.MedOrganizers.Length; j++)
			{
				IOrganizer organizer = this.MedOrganizers[j];
				IOperation operation = organizer.Cache() as IOperation;
				if (!CDriver.OperationToProgress(operation, progress, num, num + num2 / 2))
				{
					return false;
				}
				int num3;
				try
				{
					num3 = organizer.StreamCount;
				}
				catch (Exception ex)
				{
					Console.WriteLine(this.ToString() + "Unable to query StreamCount property [ " + ex.Message + " ]");
					num3 = 0;
				}
				for (int k = 1; k <= num3; k++)
				{
					IOrganizerItem organizerItem = (IOrganizerItem)organizer.GetStream(k);
					if (organizerItem.Type == 1)
					{
						this.Events.AddLast(new COrganizerEntry(this.Events, organizerItem, this.Events.EntryDescriptor));
					}
					else
					{
						if (organizerItem.Type == 3)
						{
							this.Notes.AddLast(new COrganizerEntry(this.Notes, organizerItem, this.Notes.EntryDescriptor));
						}
						else
						{
							if (organizerItem.Type == 2)
							{
								this.Tasks.AddLast(new COrganizerEntry(this.Tasks, organizerItem, this.Tasks.EntryDescriptor));
							}
							else
							{
								this.EmptyLists[j].AddLast(organizerItem);
							}
						}
					}
					if (!progress(num + num2 * k / num3 / 2 + num2 / 2))
					{
						return false;
					}
				}
				num += num2;
			}
			return progress(1000);
		}
		public virtual void Push()
		{
			this.Push(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Push(CDriver.Progress progress)
		{
			for (int i = 0; i < this.EmptyListsToPush.Length; i++)
			{
				for (int j = 0; j < this.MedOrganizers[i].StreamCount; j++)
				{
					IOrganizerItem organizerItem = this.MedOrganizers[i].GetStream(j + 1) as IOrganizerItem;
					organizerItem.GetType();
				}
				foreach (IOrganizerItem current in this.EmptyListsToPush[i])
				{
					current.Commit();
				}
			}
			if (this.Events != null)
			{
				foreach (COrganizerEntry cOrganizerEntry in this.Events)
				{
					cOrganizerEntry.Commit();
				}
			}
			if (this.Notes != null)
			{
				foreach (COrganizerEntry cOrganizerEntry2 in this.Notes)
				{
					cOrganizerEntry2.Commit();
				}
			}
			if (this.Tasks != null)
			{
				foreach (COrganizerEntry cOrganizerEntry3 in this.Tasks)
				{
					cOrganizerEntry3.Commit();
				}
			}
			int min = 0;
			int max = 1000 / ((this.MedOrganizers.Length > 0) ? this.MedOrganizers.Length : 1);
			for (int k = 0; k < this.MedOrganizers.Length; k++)
			{
				IOrganizer organizer = this.MedOrganizers[k];
				for (int l = 0; l < organizer.StreamCount; l++)
				{
					IOrganizerItem organizerItem2 = organizer.GetStream(l + 1) as IOrganizerItem;
					organizerItem2.GetType();
				}
				IOperation operation = organizer.Flush() as IOperation;
				if (!CDriver.OperationToProgress(operation, progress, min, max))
				{
					return false;
				}
			}
			return progress(1000);
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			if (this.Events != null)
			{
				xmlElement.AppendChild(this.Events.XmlDump(xmlDocument));
			}
			if (this.Notes != null)
			{
				xmlElement.AppendChild(this.Notes.XmlDump(xmlDocument));
			}
			if (this.Tasks != null)
			{
				xmlElement.AppendChild(this.Tasks.XmlDump(xmlDocument));
			}
			return xmlElement;
		}
	}
}
