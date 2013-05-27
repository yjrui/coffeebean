using System;
using System.Xml;
namespace COMlib
{
	public class CItem
	{
		private CItem _Parent;
		internal CItem Parent
		{
			get
			{
				return this._Parent;
			}
		}
		internal string ClassType
		{
			get
			{
				string text = base.GetType().ToString();
				int num = text.Length - 1;
				while (num > 0 && text[num] != '.' && text[num] != '+')
				{
					num--;
				}
				return text.Substring(num + 1);
			}
		}
		internal CItem(CItem parent)
		{
			this._Parent = parent;
		}
		public override string ToString()
		{
			if (this.Parent != null)
			{
				return this.Parent.ToString() + "/" + this.ClassType;
			}
			return this.ClassType;
		}
		internal virtual void Invalidate()
		{
			this._Parent = null;
		}
		public virtual XmlElement XmlDump(XmlDocument xmlDocument)
		{
			return xmlDocument.CreateElement(this.ClassType);
		}
	}
}
