using MobilEditCom;
using System;
namespace COMlib
{
	public class CSources : CItemList
	{
		private IMedApplication MedApplication;
		internal CSources(CItem parent) : base(parent)
		{
			if (!(parent is CDriver))
			{
				throw new Exception("Internal error");
			}
			this.MedApplication = ((CDriver)parent).MedApplication;
		}
		internal CSource FindById(int id)
		{
			foreach (CSource cSource in this)
			{
				if (cSource.Id == id)
				{
					return cSource;
				}
			}
			return null;
		}
		public virtual void Update()
		{
			this.Update(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Update(CDriver.Progress progress)
		{
			if (!progress(0))
			{
				return false;
			}
			base.Clear();
			foreach (CSource cSource in this)
			{
				cSource._Online = false;
			}
			int sourceCount = this.MedApplication.SourceCount;
			for (int i = 1; i <= sourceCount; i++)
			{
				IMedDataSource medDataSource = (IMedDataSource)this.MedApplication.GetSource(i);
				bool flag = true;
				foreach (CSource cSource2 in this)
				{
					if (cSource2.UniqueIdentifier == medDataSource.UniqueId)
					{
						cSource2._Online = true;
						flag = false;
					}
				}
				if (flag)
				{
					switch (medDataSource.Type)
					{
					case 0:
					case 3:
					case 4:
					case 5:
					case 6:
						IL_FD:
						base.AddLast(new CSource(this, medDataSource));
						goto IL_128;
					case 1:
						base.AddLast(new CSource.CMobilePhone(this, medDataSource));
						goto IL_128;
					case 2:
						base.AddLast(new CSource.CSIMCard(this, medDataSource));
						goto IL_128;
					}
					//goto IL_FD;
				}
				IL_128:
				if (!progress(i * 999 / sourceCount))
				{
					return false;
				}
			}
			return progress(1000);
		}
	}
}
