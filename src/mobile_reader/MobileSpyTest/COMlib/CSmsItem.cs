using MobilEditCom;
using System;
using System.Xml;
namespace COMlib
{
	public class CSmsItem : CItemNode
	{
		public enum EType
		{
			Deliver = 1,
			Submit,
			StatusReport,
			Command
		}
		public enum EState
		{
			Unread,
			Read,
			Sent = 3,
			Unsent = 2
		}
		public readonly string Text;
		public readonly string Number;
		public readonly string ServiceCenter;
		public readonly CSmsItem.EType Type;
		public readonly CSmsItem.EState State;
		public readonly int Status;
		public readonly int ReceivedTimezone;
		public readonly DateTime ReceivedTime;
		public readonly DateTime SentTime;
		private ISMS MedSms;
		internal CSmsItem(CItem parent, ISMS medSms) : base(parent)
		{
			if (!(parent is CSmsItems))
			{
				throw new Exception("Internal error");
			}
			this.MedSms = medSms;
			this.Text = medSms.Text;
			this.Type = (CSmsItem.EType)medSms.Type;
			this.State = (CSmsItem.EState)medSms.State;
			this.Number = "";
			if (this.Type == CSmsItem.EType.StatusReport || this.Type == CSmsItem.EType.Submit)
			{
				this.Number = medSms.ToNumber;
			}
			if (this.Type == CSmsItem.EType.Deliver)
			{
				this.Number = medSms.FromNumber;
			}
			this.ServiceCenter = medSms.ServiceCenter;
			if (this.Type == CSmsItem.EType.StatusReport)
			{
				this.Status = medSms.Status;
			}
			this.ReceivedTimezone = medSms.ReceivedTimezone;
			this.ReceivedTime = DateTime.MinValue;
			try
			{
				int num;
				this.ReceivedTime = medSms.GetReceivedTimestamp(out num);
				if (num == 0)
				{
					this.ReceivedTime = DateTime.MinValue;
				}
			}
			catch
			{
			}
			this.SentTime = DateTime.MinValue;
			try
			{
				int num;
				this.SentTime = medSms.GetSentTimestamp(out num);
				if (num == 0)
				{
					this.SentTime = DateTime.MinValue;
				}
			}
			catch
			{
			}
		}
		internal override void Invalidate()
		{
			this.MedSms = null;
			base.Invalidate();
		}
		public void Delete()
		{
			this.MedSms.Delete();
			(base.Parent as CSmsItems).Remove(this);
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("Text", this.Text);
			xmlElement.SetAttribute("Number", this.Number.ToString());
			xmlElement.SetAttribute("SentTime", this.SentTime.ToString());
			xmlElement.SetAttribute("ReceivedTime", this.ReceivedTime.ToString());
			xmlElement.SetAttribute("Status", this.Status.ToString());
			xmlElement.SetAttribute("State", this.State.ToString());
			xmlElement.SetAttribute("Type", this.Type.ToString());
			xmlElement.SetAttribute("ServiceCenter", this.ServiceCenter.ToString());
			return xmlElement;
		}
	}
}
