using MobilEditCom;
using System;
using System.Collections;
namespace COMlib
{
	public class CPorts : CItemList
	{
		internal IMedSettings MedSettings;
		internal IMedApplication MedApplication;
		internal CPorts(CItem parent) : base(parent)
		{
			if (!(parent is CDriver))
			{
				throw new Exception("Cannot new");
			}
			this.MedApplication = ((CDriver)parent).MedApplication;
			this.MedSettings = (IMedSettings)this.MedApplication.Settings;

            //int portCount = this.MedSettings.PortCount;
            //for (int i = 1; i <= portCount; i++)
            //{
            //    var port = MedSettings.GetPort(i);
            //    if (port != 0)
            //    this.MedSettings.RemovePort(port);
            //}
            //this.MedSettings.Save();
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
			int portCount = this.MedSettings.PortCount;
			for (int i = 1; i <= portCount; i++)
			{
				if (hashtable.ContainsKey(this.MedSettings.GetPort(i)))
				{
					Console.WriteLine("Error: port already enabled");
				}
				else
				{
					hashtable.Add(this.MedSettings.GetPort(i), null);
				}
			}
			portCount = this.MedApplication.PortCount;
			for (int j = 1; j <= portCount; j++)
			{
				int num;
				string portName;
				this.MedApplication.GetPort(j, out num, out portName);
				int portType;
				int portNumber;
				this.MedApplication.GetPortCharacteristics(num, out portType, out portNumber);
				base.AddLast(new CPort(this, num, portName, portType, portNumber, hashtable.ContainsKey(num)));
			}
			return progress(1000);
		}
		internal void Commit()
		{
			foreach (CPort cPort in this)
			{
				cPort.Commit();
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
		internal CPort FindById(int id)
		{
			foreach (CPort cPort in this)
			{
				if (cPort.Id == id)
				{
					return cPort;
				}
			}
			return null;
		}
	}
}
