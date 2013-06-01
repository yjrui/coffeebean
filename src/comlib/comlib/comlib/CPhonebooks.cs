using MobilEditCom;
using System;
namespace COMlib
{
	public class CPhonebooks : CItemList
	{
		internal ICapability MedCapability;
		public CPhonebook this[CPhonebook.EType type]
		{
			get
			{
				foreach (CItemNode cItemNode in this)
				{
					if (cItemNode is CPhonebook && (cItemNode as CPhonebook).Type == type)
					{
						return cItemNode as CPhonebook;
					}
				}
				return null;
			}
		}
		internal CPhonebooks(CItem parent, ICapability medCapability) : base(parent)
		{
			if (!(parent is CSource))
			{
				throw new Exception("Internal error");
			}
			this.MedCapability = medCapability;
			this.Update();
		}
		internal void Update()
		{
			int classCount = this.MedCapability.ClassCount;
			for (int i = 1; i <= classCount; i++)
			{
				base.AddLast(new CPhonebook(this, (IClass)this.MedCapability.GetClass(i)));
			}
		}
		internal override void Invalidate()
		{
			foreach (CPhonebook cPhonebook in this)
			{
				cPhonebook.Invalidate();
			}
			base.Clear();
			base.Invalidate();
		}
	}
}
