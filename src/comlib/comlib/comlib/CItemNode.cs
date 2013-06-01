using System;
namespace COMlib
{
	public class CItemNode : CItem
	{
		internal CItemNode Previous;
		internal CItemNode Next;
		internal CItemList List;
		internal CItemNode(CItem parent) : base(parent)
		{
		}
		internal void Remove()
		{
			this.List.Remove(this);
		}
	}
}
