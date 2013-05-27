using MobilEditCom;
using System;
using System.Xml;
namespace COMlib
{
	public class CFileSystem : CItem
	{
		public enum EMIDP
		{
			None,
			Midp1,
			Midp2
		}
		internal IFileSystem MedFileSystem;
		public readonly CFileSystem.EMIDP MIDP;
		public readonly CFolder RootFolder;
		internal CFileSystem(CItem parent, ICapability capability) : base(parent)
		{
			if (!(parent is CSource))
			{
				throw new Exception("Internal error");
			}
			this.MedFileSystem = (IFileSystem)capability;
			try
			{
				this.MIDP = (CFileSystem.EMIDP)this.MedFileSystem.MIDP;
			}
			catch (Exception ex)
			{
				Console.WriteLine(this.ToString() + " : Unable to query MIDP version [ " + ex.Message + " ]");
			}
			try
			{
				this.RootFolder = new CFolder(this, (IFolder)this.MedFileSystem.GetFolder("\\"));
			}
			catch (Exception ex2)
			{
				this.RootFolder = null;
				Console.WriteLine(this.ToString() + " : Unable to query root folder [ " + ex2.Message + " ]");
			}
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("MIDP", this.MIDP.ToString());
			if (this.RootFolder != null)
			{
				xmlElement.AppendChild(this.RootFolder.XmlDump(xmlDocument));
			}
			return xmlElement;
		}
	}
}
