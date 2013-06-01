using System;
using System.Collections;
using System.Xml;
namespace COMlib
{
	public class CItemList : CItemNode, IEnumerable
	{
		public class CItemsEnumerator : IEnumerator
		{
			private CItemList List;
			private CItemNode Node;
			private CItemNode Next;
			public object Current
			{
				get
				{
					return this.Node;
				}
			}
			internal CItemsEnumerator(CItemList list)
			{
				this.List = list;
				this.Reset();
			}
			public bool MoveNext()
			{
				this.Node = this.Next;
				this.Next = this.Node.Next;
				return this.Node != this.List;
			}
			public void Reset()
			{
				this.Node = this.List;
				this.Next = this.Node.Next;
			}
		}
		internal CItemList(CItem parent) : base(parent)
		{
			this.Previous = this;
			this.Next = this;
			this.List = this;
		}
		protected internal void AddFirst(CItemNode node)
		{
			node.Next = this.Next;
			node.Previous = this;
			this.Next = node;
			node.Next.Previous = node;
			node.List = this;
		}
		protected internal void AddLast(CItemNode node)
		{
			node.Next = this;
			node.Previous = this.Previous;
			this.Previous = node;
			node.Previous.Next = node;
			node.List = this;
		}
		protected internal virtual void Remove(CItemNode node)
		{
			if (node == null || node.List != this)
			{
				throw new Exception("Cannot remove node");
			}
			node.Next.Previous = node.Previous;
			node.Previous.Next = node.Next;
			node.Next = node;
			node.Previous = node;
			node.List = null;
		}
		protected internal void Clear()
		{
			this.Next = this;
			this.Previous = this;
		}
		public IEnumerator GetEnumerator()
		{
			return new CItemList.CItemsEnumerator(this);
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			foreach (object current in this)
			{
				if (current is CItem)
				{
					xmlElement.AppendChild(((CItem)current).XmlDump(xmlDocument));
				}
			}
			return xmlElement;
		}
	}
}
