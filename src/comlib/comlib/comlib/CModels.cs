using MobilEditCom;
using System;
using System.Collections;
namespace COMlib
{
	public class CModels : CItemList
	{
		internal IMedSettings MedSettings;
		internal IMedApplication MedApplication;
		internal CModels(CItem parent) : base(parent)
		{
			if (!(parent is CDriver))
			{
				throw new Exception("Internal error");
			}
			this.MedApplication = ((CDriver)parent).MedApplication;
			this.MedSettings = (IMedSettings)this.MedApplication.Settings;
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
			Hashtable hashtable = new Hashtable();
			int phoneCount = this.MedSettings.PhoneCount;
			for (int i = 1; i <= phoneCount; i++)
			{
				hashtable.Add(this.MedSettings.GetPhone(i), null);
			}
			phoneCount = this.MedApplication.PhoneCount;
			for (int j = 1; j <= phoneCount; j++)
			{
				int num;
				string manufacturer;
				string model;
				this.MedApplication.GetPhone(j, out num, out manufacturer, out model);
				base.AddLast(new CModel(this, num, manufacturer, model, hashtable.ContainsKey(num)));
			}
			return progress(1000);
		}
		internal void Commit()
		{
			foreach (CModel cModel in this)
			{
				cModel.Commit();
			}
		}
		public virtual void Push()
		{
			this.Push(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Push(CDriver.Progress progress)
		{
			if (!progress(0))
			{
				return false;
			}
			this.Commit();
			this.MedSettings.Save();
			return progress(1000);
		}
	}
}
