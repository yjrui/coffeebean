using MobilEditCom;
using System;
using System.Threading;
using System.Xml;
namespace COMlib
{
	public class CDriver : CItem
	{
		public delegate bool Progress(int progress);
		internal class ProgressClass
		{
			public static bool Progress(int progress)
			{
				return true;
			}
		}
		public const int ProgressMaxValue = 1000;
		internal readonly IMedApplication MedApplication;
		private CPorts _ports;
		private CModels _models;
		private CSources _sources;
		public readonly CLicense License;
		public CPorts Ports
		{
			get
			{
				if (this._ports == null)
				{
					this._ports = new CPorts(this);
				}
				return this._ports;
			}
		}
		public CModels Models
		{
			get
			{
				if (this._models == null)
				{
					this._models = new CModels(this);
				}
				return this._models;
			}
		}
		public CSources Sources
		{
			get
			{
				if (this._sources == null)
				{
					this._sources = new CSources(this);
				}
				return this._sources;
			}
		}
		internal static bool OperationToProgress(IOperation operation, CDriver.Progress progress, int min, int max)
		{
			while (operation.Status == 0)
			{
				int num;
				if (operation.Max > operation.Min)
				{
					num = (max - min) * (operation.Current - operation.Min) / (operation.Max - operation.Min);
				}
				else
				{
					num = (min + max) / 2;
				}
				if (num == 0)
				{
					num = 1;
				}
				if (!progress(num))
				{
					operation.Cancel();
					operation.Wait();
					return false;
				}
				Thread.Sleep(100);
			}
			if (operation.Status == 1)
			{
				return true;
			}
			throw new Exception("Item number : " + ((operation.Result != null) ? operation.Result.ToString() : "0") + " HRESULT : " + operation.Error.ToString());
		}
		public CDriver(IMedApplication MedApplication) : base(null)
		{
            this.MedApplication = MedApplication;
			this.License = new CLicense(this, (IMedLicense)this.MedApplication.License);
		}
		internal void NotificationHandler(int msg, int eventId)
		{
			if (msg >= 32768 && msg < 33024)
			{
				int num = msg - 32768;
				foreach (CSource cSource in this.Sources)
				{
					if (num == cSource.SourceId)
					{
						INotification notification = cSource.MedDataSource.GetNotification(eventId) as INotification;
						cSource.NotificationHandler(notification.Capability, notification.Class, notification.Stream, notification.Reason);
					}
				}
			}
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.AppendChild(this.License.XmlDump(xmlDocument));
			xmlElement.AppendChild(this.Sources.XmlDump(xmlDocument));
			return xmlElement;
		}
	}
}
