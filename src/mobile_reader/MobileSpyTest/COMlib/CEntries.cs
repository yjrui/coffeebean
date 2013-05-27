using System;
namespace COMlib
{
	public abstract class CEntries : CItemList
	{
		public abstract int MaxEntries
		{
			get;
		}
		public CEntries(CItem parent) : base(parent)
		{
		}
		public abstract CEntry CreateEntry();
	}
}
