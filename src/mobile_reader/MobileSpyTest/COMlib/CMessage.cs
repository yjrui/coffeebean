using MobilEditCom;
using System;
namespace COMlib
{
	public class CMessage : CItem
	{
		public enum EValidity
		{
			OneHour = 11,
			ThreeHours = 35,
			SixHours = 71,
			TwelveHours = 143,
			OneDay = 167,
			OneWeek = 173,
			Maximal = 255
		}
		public string Text;
		public string Number;
		public string ServiceCenter;
		public bool RequestReport;
		public CMessage.EValidity Validity;
		public int Reference;
		internal CMessage(CItem parent) : base(parent)
		{
			if (!(parent is CSms))
			{
				throw new Exception("Cannot new");
			}
			this.Text = "";
			this.Number = "";
			this.RequestReport = false;
			this.Validity = CMessage.EValidity.Maximal;
			this.Reference = 0;
			this.ServiceCenter = "";
		}
		public void Send()
		{
			this.Send(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public bool Send(CDriver.Progress progress)
		{
			if (!progress(0))
			{
				return false;
			}
			byte[] validity = new byte[]
			{
				(byte)this.Validity
			};
			bool flag = false;
			int num = 160;
			if (this.Text.Length > 160)
			{
				num = 152;
				flag = true;
			}
			int i = 0;
			int num2 = 0;
			int num3 = (this.Text.Length + num - 1) / num;
			int num4 = new Random().Next(65535);
			while (i < this.Text.Length)
			{
				string strText;
				if (this.Text.Length - i + 1 < num)
				{
					strText = this.Text.Substring(i);
				}
				else
				{
					strText = this.Text.Substring(i, num);
				}
				bool flag2 = this.RequestReport && (!flag || num2 == num3 - 1);
				byte[] header;
				if (flag)
				{
					header = new byte[]
					{
						8,
						4,
						(byte)(num4 / 256),
						(byte)(num4 % 256),
						(byte)num3,
						(byte)(num2 + 1)
					};
				}
				else
				{
					header = new byte[0];
				}
				IOperation operation = (base.Parent as CSms).MedSmsOutbox.SendSMSEx(0, this.Reference, 16 | (flag2 ? 32 : 0), validity, this.Number, 129, this.ServiceCenter, 129, strText, header) as IOperation;
				operation.Wait();
				if (!progress(999 * (num2 + 1) / num3))
				{
					return false;
				}
				num2++;
				i = num2 * num;
			}
			return progress(1000);
		}
	}
}
