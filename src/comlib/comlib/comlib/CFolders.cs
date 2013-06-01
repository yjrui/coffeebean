using MobilEditCom;
using System;
namespace COMlib
{
	public class CFolders : CItemList
	{
		internal IFolder MedFolder;
		internal CFolders(CItem parent, IFolder medFolder) : base(parent)
		{
			if (!(parent is CFolder))
			{
				throw new Exception("Internal error");
			}
			this.MedFolder = medFolder;
		}
		internal void InvalidateChildren()
		{
			foreach (CFolder cFolder in this)
			{
				cFolder.Invalidate();
			}
			base.Clear();
		}
		internal override void Invalidate()
		{
			this.InvalidateChildren();
			this.MedFolder = null;
			base.Invalidate();
		}
		public virtual void Update()
		{
			this.Update(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Update(CDriver.Progress progress)
		{
			this.InvalidateChildren();
			int subfolderCount = this.MedFolder.SubfolderCount;
			for (int i = 1; i <= subfolderCount; i++)
			{
				IFolder medFolder = (IFolder)this.MedFolder.GetSubfolder(i);
				base.AddLast(new CFolder(this, medFolder));
				if (!progress(i * 1000 / subfolderCount))
				{
					return false;
				}
			}
			return progress(1000);
		}
		internal void DeleteFolder(CFolder folderToDelete)
		{
			int subfolderCount = this.MedFolder.SubfolderCount;
			for (int i = 1; i <= subfolderCount; i++)
			{
				IFolder folder = this.MedFolder.GetSubfolder(i) as IFolder;
				if (folder.Id == folderToDelete.MedFolder.Id)
				{
					this.MedFolder.RemoveSubfolder(i);
					return;
				}
			}
			this.Remove(folderToDelete);
		}
	}
}
