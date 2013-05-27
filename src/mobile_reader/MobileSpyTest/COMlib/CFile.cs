using MobilEditCom;
using System;
using System.IO;
using System.Xml;
namespace COMlib
{
	public class CFile : CItemNode
	{
		internal bool Changed;
		public readonly string Name;
		public readonly bool ReadOnly;
		public readonly int Size;
		internal IFolderFile MedFolderFile;
		internal CFile(CItem parent, IFolderFile medFolderFile, bool justCreated) : base(parent)
		{
			if (!(parent is CFiles))
			{
				throw new Exception("Internal error");
			}
			this.MedFolderFile = medFolderFile;
			this.Name = this.MedFolderFile.Name;
			this.Size = this.MedFolderFile.Size;
			this.ReadOnly = false;
			this.Changed = false;
		}
		internal CFile(CItem parent, IFolderFile medFolderFile) : base(parent)
		{
			if (!(parent is CFiles))
			{
				throw new Exception("Internal error");
			}
			this.MedFolderFile = medFolderFile;
			this.Name = this.MedFolderFile.Name;
			this.Size = this.MedFolderFile.Size;
			this.ReadOnly = (this.MedFolderFile.IsReadOnly != 0);
			this.Changed = false;
		}
		public void Download(FileStream stream)
		{
			this.MedFolderFile.Open();
			int num = 0;
			int num2;
			do
			{
				object obj;
				num2 = this.MedFolderFile.Read(10240, out obj);
				stream.Write((byte[])obj, 0, num2);
				num += num2;
				this.MedFolderFile.SetPointer(num, 0);
			}
			while (num2 > 0);
			this.MedFolderFile.Close();
		}
		internal override void Invalidate()
		{
			base.Invalidate();
			this.MedFolderFile = null;
		}
		public void Delete()
		{
			(base.Parent as CFiles).DeleteFile(this);
			this.Invalidate();
		}
		public void Upload(FileStream stream)
		{
			this.Changed = true;
			if (this.ReadOnly)
			{
				throw new Exception(this.ToString() + " : File is read-only");
			}
			if (this.MedFolderFile.IsOpened != 0)
			{
				this.MedFolderFile.Open();
			}
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, (int)stream.Length);
			this.MedFolderFile.Write(array);
			this.MedFolderFile.Close();
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("Name", this.Name);
			xmlElement.SetAttribute("Size", this.Size.ToString());
			xmlElement.SetAttribute("ReadOnly", this.ReadOnly.ToString());
			return xmlElement;
		}
	}
}
