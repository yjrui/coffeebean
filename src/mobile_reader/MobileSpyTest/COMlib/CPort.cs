using MobilEditCom;
using System;
namespace COMlib
{
	public class CPort : CItemNode
	{
		public enum EType
		{
			Unknown,
			Serial,
			LegacyBT,
			USB,
			IrDA,
			Bluetooth,
			WindowsCE = 7,
			iTunesBackup,
			AndroidLive = 10,
			RawApple = 13
		}
		public readonly string PortName;
		public bool Enabled;
		public readonly CPort.EType Type;
		public readonly int Number;
		internal int Id;
		internal bool WasEnabled;
		internal CPort(CItem parent, int id, string portName, int portType, int portNumber, bool enabled) : base(parent)
		{
			if (!(parent is CPorts))
			{
				throw new Exception("Internal error");
			}
			this.PortName = portName;
			this.Id = id;
			this.Enabled = enabled;
			this.WasEnabled = enabled;
			this.Type = (CPort.EType)portType;
		}
		internal void Commit()
		{
			if (this.WasEnabled == this.Enabled)
			{
				return;
			}
			IMedSettings medSettings = ((CPorts)base.Parent).MedSettings;
			this.WasEnabled = this.Enabled;
			if (this.Enabled)
			{
				medSettings.AddPort(this.Id);
				return;
			}
			int portCount = medSettings.PortCount;
			for (int i = 1; i <= portCount; i++)
			{
				if (medSettings.GetPort(i) == this.Id)
				{
					medSettings.RemovePort(i);
					return;
				}
			}
		}
	}
}
