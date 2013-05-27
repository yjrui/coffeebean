using MobilEditCom;
using System;
namespace COMlib
{
	public class CFiles : CItemList
	{
		private IFolder MedFolder;
		internal CFiles(CItem parent, IFolder medFolder) : base(parent)
		{
			if (!(parent is CFolder))
			{
				throw new Exception("Internal error");
			}
			this.MedFolder = medFolder;
		}
		internal void InvalidateChildren()
		{
			foreach (CFile cFile in this)
			{
				cFile.Invalidate();
			}
			base.Clear();
		}
		internal override void Invalidate()
		{
			this.InvalidateChildren();
			this.MedFolder = null;
			base.Invalidate();
		}
		internal void DeleteFile(CFile fileToDelete)
		{
			int fileCount = this.MedFolder.FileCount;
			for (int i = 1; i <= fileCount; i++)
			{
				IFolderFile folderFile = this.MedFolder.GetFile(i) as IFolderFile;
				if (folderFile.Name == fileToDelete.Name)
				{
					this.MedFolder.DeleteFolderFile(i);
					break;
				}
			}
			this.Remove(fileToDelete);
		}
		public virtual void Update()
		{
			this.Update(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Update(CDriver.Progress progress)
		{
			this.InvalidateChildren();
			int fileCount = this.MedFolder.FileCount;
			for (int i = 1; i <= fileCount; i++)
			{
				IFolderFile medFolderFile = (IFolderFile)this.MedFolder.GetFile(i);
				base.AddLast(new CFile(this, medFolderFile));
				if (!progress(i * 1000 / fileCount + 1000))
				{
					return false;
				}
			}
			return progress(1000);
		}
	}
}
