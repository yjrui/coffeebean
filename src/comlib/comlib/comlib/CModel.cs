using MobilEditCom;
using System;
namespace COMlib
{
	public class CModel : CItemNode
	{
		public readonly string Model;
		public readonly string Manufacturer;
		public bool Enabled;
		private bool WasEnabled;
		internal int Id;
		internal CModel(CItem parent, int id, string manufacturer, string model, bool enabled) : base(parent)
		{
			if (!(parent is CModels))
			{
				throw new Exception("Internal error");
			}
			this.Model = model;
			this.Manufacturer = manufacturer;
			this.Id = id;
			this.Enabled = enabled;
			this.WasEnabled = enabled;
		}
		internal void Commit()
		{
			if (this.WasEnabled == this.Enabled)
			{
				return;
			}
			IMedSettings medSettings = ((CModels)base.Parent).MedSettings;
			this.WasEnabled = this.Enabled;
			if (this.Enabled)
			{
				medSettings.AddPhone(this.Id);
				return;
			}
			int phoneCount = medSettings.PhoneCount;
			for (int i = 1; i <= phoneCount; i++)
			{
				if (medSettings.GetPhone(i) == this.Id)
				{
					medSettings.RemovePhone(i);
					return;
				}
			}
		}
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.ToString(),
				"[",
				this.Manufacturer,
				" ",
				this.Model,
				"]"
			});
		}
	}
}
