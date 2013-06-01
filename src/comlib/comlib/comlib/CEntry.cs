using System;
using System.Collections.Generic;
using System.Xml;
namespace COMlib
{
	public abstract class CEntry : CItemNode
	{
		public delegate bool Predicate(CEntryItem item);
		public CEntryItem[] Items;
		protected internal CEntryItem.CDescriptor EntryDescriptor;
		internal bool _Changed;
		internal bool Changed
		{
			get
			{
				return this._Changed;
			}
		}
		protected CEntry(CItem parent) : base(parent)
		{
		}
		internal void HasChanged()
		{
			this._Changed = true;
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			for (int i = 0; i < this.Items.Length; i++)
			{
				if (this.Items[i].DataType != CEntryItem.EDataType.Empty)
				{
					xmlElement.AppendChild(this.Items[i].XmlDump(xmlDocument));
				}
			}
			return xmlElement;
		}
		public CEntryItem Find(CEntry.Predicate match)
		{
			for (int i = 0; i < this.Items.Length; i++)
			{
				if (match(this.Items[i]))
				{
					return this.Items[i];
				}
			}
			return null;
		}
		public List<CEntryItem> FindAll(CEntry.Predicate match)
		{
			List<CEntryItem> list = new List<CEntryItem>();
			for (int i = 0; i < this.Items.Length; i++)
			{
				if (match(this.Items[i]))
				{
					list.Add(this.Items[i]);
				}
			}
			return list;
		}
		public List<CEntryItem> FindItemsByType(CEntryItem.EDataType type)
		{
			return this.FindAll((CEntryItem item) => item.DataType == type);
		}
		public CEntryItem FindItemByType(CEntryItem.EDataType type)
		{
			return this.Find((CEntryItem item) => item.DataType == type);
		}
		public CEntryItem FindItemThatSupports(CEntryItem.EDataType type)
		{
			return this.Find((CEntryItem item) => Array.IndexOf<CEntryItem.EDataType>(item.SupportedTypes, type) >= 0);
		}
		public abstract void Delete();
	}
}
