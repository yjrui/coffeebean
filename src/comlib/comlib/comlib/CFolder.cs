using MobilEditCom;
using System;
using System.Xml;
namespace COMlib
{
	public class CFolder : CItemNode
	{
		public enum EStorage
		{
			Uknown,
			Internal,
			External
		}
		internal IFolder MedFolder;
		public readonly CFolders Folders;
		public readonly CFiles Files;
		public readonly CFolder.EStorage Storage;
		public readonly int FreeMemory;
		public readonly int TotalMemory;
		public readonly string Name;
		internal CFolder(CItem parent, IFolder medFolder) : base(parent)
		{
			if (!(parent is CFolders) && !(parent is CFileSystem))
			{
				throw new Exception("Internal error");
			}
			this.MedFolder = medFolder;
			this.Name = medFolder.Name;
			this.Storage = (CFolder.EStorage)this.MedFolder.Storage;
			int arg_51_0 = this.MedFolder.FreeMemory;
			int arg_5D_0 = this.MedFolder.TotalMemory;
			this.Folders = new CFolders(this, this.MedFolder);
			this.Files = new CFiles(this, this.MedFolder);
		}
		public CFile CreateFile(string name)
		{
			IFolderFile medFolderFile = this.MedFolder.CreateFileDirect(name) as IFolderFile;
			CFile cFile;
			if (base.Parent is CFileSystem)
			{
				cFile = new CFile((base.Parent as CFileSystem).RootFolder.Files, medFolderFile, true);
			}
			else
			{
				cFile = new CFile(((base.Parent as CFolders).Parent as CFolder).Files, medFolderFile, true);
			}
			this.Files.AddLast(cFile);
			return cFile;
		}
		public CFolder CreateFolder(string name)
		{
			IFolder medFolder = this.MedFolder.CreateSubfolderDirect(name) as IFolder;
			CFolder cFolder = new CFolder(base.Parent, medFolder);
			this.Folders.AddLast(cFolder);
			return cFolder;
		}
		public void Delete()
		{
			(base.Parent as CFolders).DeleteFolder(this);
			this.Invalidate();
		}
		internal override void Invalidate()
		{
			foreach (CFolder cFolder in this.Folders)
			{
				cFolder.Invalidate();
			}
			foreach (CFile cFile in this.Files)
			{
				cFile.Invalidate();
			}
			base.Invalidate();
			this.MedFolder = null;
		}
		public virtual void Push()
		{
			this.Push(new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Push(CDriver.Progress progress)
		{
			if (!progress(0))
			{
				return false;
			}
			IOperation operation = this.MedFolder.Flush() as IOperation;
			operation.Wait();
			return progress(1000);
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("Storage", this.Storage.ToString());
			xmlElement.SetAttribute("FreeMemory", this.FreeMemory.ToString());
			xmlElement.SetAttribute("TotalMemory", this.TotalMemory.ToString());
			xmlElement.AppendChild(this.Folders.XmlDump(xmlDocument));
			xmlElement.AppendChild(this.Files.XmlDump(xmlDocument));
			return xmlElement;
		}
	}
}
