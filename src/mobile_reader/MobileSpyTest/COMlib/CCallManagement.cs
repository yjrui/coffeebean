using MobilEditCom;
using System;
namespace COMlib
{
	public class CCallManagement : CItem
	{
		public enum EHandsFreeStatus
		{
			Unknown,
			Disabled,
			Enabled
		}
		public enum EReason
		{
			Incoming = 1,
			Terminated
		}
		public delegate void EventHandler(string number, CCallManagement.EReason reason);
		public enum EState
		{
			Idle,
			Dialing,
			Connecting,
			Active,
			Hold,
			Waiting,
			Ringing
		}
		internal ICallCapability MedCallCapability;
		internal ICallClass MedCallClass;
		internal ICallStream MedCallStream;
		public CCallManagement.EventHandler Notification;
		private int id;
		public CCallManagement.EHandsFreeStatus HandsFreeStatus
		{
			get
			{
				CCallManagement.EHandsFreeStatus result;
				try
				{
					result = (CCallManagement.EHandsFreeStatus)this.MedCallCapability.HandsFreeStatus;
				}
				catch (Exception ex)
				{
					Console.WriteLine(this.ToString() + " : Unable to query HandsFreeStatus property [ " + ex.Message + " ]");
					result = CCallManagement.EHandsFreeStatus.Unknown;
				}
				return result;
			}
		}
		public CCallManagement.EState State
		{
			get
			{
				return (CCallManagement.EState)this.MedCallStream.State;
			}
		}
		public int Duration
		{
			get
			{
				return this.MedCallStream.Duration;
			}
		}
		public string Number
		{
			get
			{
				return this.MedCallStream.Number;
			}
		}
		public DateTime? StartTime
		{
			get
			{
				int num;
				DateTime startTimestamp = this.MedCallStream.GetStartTimestamp(out num);
				if (num != 0)
				{
					return new DateTime?(startTimestamp);
				}
				return null;
			}
		}
		public DateTime? EndTime
		{
			get
			{
				int num;
				DateTime stopTimestamp = this.MedCallStream.GetStopTimestamp(out num);
				if (num != 0)
				{
					return new DateTime?(stopTimestamp);
				}
				return null;
			}
		}
		internal CCallManagement(CItem parent, ICapability medCapability) : base(parent)
		{
			if (!(parent is CSource))
			{
				throw new Exception("Internal error");
			}
			this.MedCallCapability = (ICallCapability)medCapability;
			this.MedCallClass = (this.MedCallCapability.GetClassDirect(67108864) as ICallClass);
			this.id = (base.Parent as CSource).MedDataSource.RegisterNotification((int)(base.Parent.Parent.Parent as CDriver).form.Handle, 32768 + (base.Parent as CSource).SourceId, medCapability.Id, 67108864);
			if (this.MedCallClass.StreamCount < 1)
			{
				Console.WriteLine(this.ToString() + " : No streams");
				this.MedCallStream = null;
				return;
			}
			this.MedCallStream = (this.MedCallClass.GetStream(1) as ICallStream);
		}
		~CCallManagement()
		{
			try
			{
				(base.Parent as CSource).MedDataSource.UnregisterNotification(this.id);
			}
			catch (Exception)
			{
			}
		}
		internal void NotificationHandler(int classId, int stream, int reason)
		{
			if (classId == 67108864)
			{
				try
				{
					if (this.MedCallStream.State == 0 && this.Notification != null)
					{
						this.Notification(this.MedCallStream.Number, CCallManagement.EReason.Terminated);
					}
					if (this.MedCallStream.State == 6 && this.Notification != null)
					{
						this.Notification(this.MedCallStream.Number, CCallManagement.EReason.Incoming);
					}
				}
				catch (Exception)
				{
				}
			}
		}
		public void Accept()
		{
			try
			{
				this.MedCallStream.Activate();
			}
			catch (Exception innerException)
			{
				throw new Exception("Call accepting failed", innerException);
			}
		}
		public void Dial(string number)
		{
			try
			{
				this.MedCallStream.Dial(number);
			}
			catch (Exception innerException)
			{
				throw new Exception("Dialing failed", innerException);
			}
		}
		public void Hangup()
		{
			try
			{
				this.MedCallStream.Hangup();
			}
			catch (Exception innerException)
			{
				throw new Exception("Hangup failed", innerException);
			}
		}
		public void Hold()
		{
			try
			{
				this.MedCallStream.Hold();
			}
			catch (Exception innerException)
			{
				throw new Exception("Hold failed", innerException);
			}
		}
	}
}
