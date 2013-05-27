using MobilEditCom;
using System;
using System.Collections.Generic;
namespace COMlib
{
	public class CPhonebookEntries : CEntries
	{
		private LinkedList<IPhonebookItem> EmptyList;
		private LinkedList<IPhonebookItem> EmptyListToPush;
		private IPhonebook MedPhonebook;
		public override int MaxEntries
		{
			get
			{
				int result;
				try
				{
					if (this.MedPhonebook.Growable != 0)
					{
						result = 0;
					}
					else
					{
						result = this.MedPhonebook.StreamCount;
					}
				}
				catch (Exception)
				{
					result = 0;
				}
				return result;
			}
		}
		internal CPhonebookEntries(CItem parent, IPhonebook medPhoneBook) : base(parent)
		{
			if (!(parent is CPhonebook))
			{
				throw new Exception("Internal error");
			}
			this.MedPhonebook = medPhoneBook;
			this.EmptyList = new LinkedList<IPhonebookItem>();
			this.EmptyListToPush = new LinkedList<IPhonebookItem>();
		}
		private void InvalidateChildren()
		{
			foreach (CPhonebookEntry cPhonebookEntry in this)
			{
				cPhonebookEntry.Invalidate();
			}
			base.Clear();
		}
		internal override void Invalidate()
		{
			this.InvalidateChildren();
			base.Invalidate();
		}
		public virtual void Update()
		{
			this.Update(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Update(CDriver.Progress progress)
		{
			this.InvalidateChildren();
			IOperation operation = this.MedPhonebook.Cache() as IOperation;
			if (!CDriver.OperationToProgress(operation, progress, 0, 500))
			{
				return false;
			}
			int num = 0;
			try
			{
				num = this.MedPhonebook.StreamCount;
			}
			catch (Exception ex)
			{
				Console.WriteLine(this.ToString() + "Unable to query StreamCount property [ " + ex.Message + " ]");
				return true;
			}
			for (int i = 1; i <= num; i++)
			{
				IPhonebookItem phonebookItem = (IPhonebookItem)this.MedPhonebook.GetStream(i);
				if (phonebookItem.Empty == 0)
				{
					base.AddLast(new CPhonebookEntry(this, phonebookItem));
				}
				else
				{
					this.EmptyList.AddFirst(phonebookItem);
				}
				if (!progress(1000 * i / num / 2 + 500))
				{
					return false;
				}
			}
			return progress(1000);
		}
		public override CEntry CreateEntry()
		{
			if ((base.Parent as CPhonebook).ReadOnly)
			{
				return null;
			}
			if (this.EmptyList.Count != 0)
			{
				CPhonebookEntry cPhonebookEntry = new CPhonebookEntry(this, this.EmptyList.Last.Value);
				this.EmptyList.RemoveLast();
				base.AddLast(cPhonebookEntry);
				return cPhonebookEntry;
			}
			if (this.EmptyListToPush.Count != 0)
			{
				CPhonebookEntry cPhonebookEntry2 = new CPhonebookEntry(this, this.EmptyListToPush.Last.Value);
				this.EmptyListToPush.RemoveLast();
				base.AddLast(cPhonebookEntry2);
				return cPhonebookEntry2;
			}
			if (this.MedPhonebook.Growable == 0)
			{
				return null;
			}
			IPhonebookItem medPhonebookItem;
			try
			{
				medPhonebookItem = (this.MedPhonebook.AddStream() as IPhonebookItem);
			}
			catch (Exception)
			{
				return null;
			}
			CPhonebookEntry cPhonebookEntry3 = new CPhonebookEntry(this, medPhonebookItem);
			base.AddLast(cPhonebookEntry3);
			return cPhonebookEntry3;
		}
		public virtual void Push()
		{
			this.Push(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Push(CDriver.Progress progress)
		{
			foreach (IPhonebookItem current in this.EmptyListToPush)
			{
				current.Save();
			}
			foreach (CPhonebookEntry cPhonebookEntry in this)
			{
				cPhonebookEntry.Commit();
			}
			IOperation operation = this.MedPhonebook.Flush() as IOperation;
			return CDriver.OperationToProgress(operation, progress, 0, 1000);
		}
		internal void DeleteEntry(CPhonebookEntry entry)
		{
			entry.MedPhonebookItem.Delete();
			this.EmptyListToPush.AddLast(entry.MedPhonebookItem);
			this.Remove(entry);
		}
	}
}
