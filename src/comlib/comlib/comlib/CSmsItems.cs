using MobilEditCom;
using System;
namespace COMlib
{
	public class CSmsItems : CItemList
	{
		private ISMSFolder MedSmsFolder;
		private bool _Changed;
		internal bool Changed
		{
			get
			{
				return this._Changed;
			}
		}
		public bool ReadOnly
		{
			get
			{
				return this.MedSmsFolder.ReadOnly != 0 || this.MedSmsFolder.WriteMethod != 1;
			}
		}
		internal CSmsItems(CItem parent, ISMSFolder medSmsFolder) : base(parent)
		{
			if (!(parent is CSms))
			{
				throw new Exception("Internal error");
			}
			this.MedSmsFolder = medSmsFolder;
		}
		protected internal override void Remove(CItemNode node)
		{
			base.Remove(node);
			this._Changed = true;
		}
		internal void Commit()
		{
			if (this.Changed)
			{
				this.MedSmsFolder.Flush();
			}
		}
		private void InvalidateChildren()
		{
			foreach (CSmsItem cSmsItem in this)
			{
				cSmsItem.Invalidate();
			}
			base.Clear();
		}
		internal override void Invalidate()
		{
			this.InvalidateChildren();
		}
		public virtual void Update()
		{
			this.Update(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Update(CDriver.Progress progress)
		{
			this.InvalidateChildren();
			this.MedSmsFolder.CacheControl(2);
			if (this.MedSmsFolder != null)
			{
				IOperation operation = this.MedSmsFolder.Cache() as IOperation;
				if (!CDriver.OperationToProgress(operation, progress, 0, 500))
				{
					return false;
				}
				int streamCount = this.MedSmsFolder.StreamCount;
				for (int i = 1; i <= streamCount; i++)
				{
					ISMS iSMS = this.MedSmsFolder.GetStream(i) as ISMS;
					if (iSMS.Empty == 0 && iSMS.Type != 5)
					{
						base.AddLast(new CSmsItem(this, iSMS));
					}
					if (!progress(1000 * i / streamCount / 2 + 500))
					{
						return false;
					}
				}
			}
			return progress(1000);
		}
		public void StoreSMS(CSmsItem.EType type, CSmsItem.EState state, string number, string serviceCenter, string text, DateTime time, int timeZone)
		{
			this.MedSmsFolder.StoreSMS((int)type, (int)state, 0, 0, 0, 0, 0, null, number, 129, serviceCenter, 129, text, time, timeZone, null);
		}
		public virtual void Push()
		{
			this.Push(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Push(CDriver.Progress progress)
		{
			IOperation operation = this.MedSmsFolder.Flush() as IOperation;
			return CDriver.OperationToProgress(operation, progress, 0, 1000);
		}
	}
}
