using MobilEditCom;
using System;
using System.Xml;
namespace COMlib
{
	public class CSms : CItem
	{
		internal ICapability MedCapability;
		internal ISMSOutbox MedSmsOutbox;
		public readonly CSmsItems Items;
		internal CSms(CItem parent, ICapability medCapability) : base(parent)
		{
			if (!(parent is CSource))
			{
				throw new Exception("CSms : Internal error");
			}
			this.MedCapability = medCapability;
			this.MedSmsOutbox = null;
			ISMSFolder medSmsFolder = null;
			int i = 1;
			while (i <= this.MedCapability.ClassCount)
			{
				IClass @class = this.MedCapability.GetClass(i) as IClass;
				int id = @class.Id;
				switch (id)
				{
				case 33554432:
					goto IL_87;
				case 33554433:
					this.MedSmsOutbox = (@class as ISMSOutbox);
					break;
				case 33554434:
				case 33554435:
				case 33554436:
					break;
				default:
					if (id == 33587200 || id == 33603584)
					{
						goto IL_87;
					}
					break;
				}
				IL_8E:
				i++;
				continue;
				IL_87:
				medSmsFolder = (@class as ISMSFolder);
				goto IL_8E;
			}
			this.Items = new CSmsItems(this, medSmsFolder);
		}
		public CMessage CreateMessage()
		{
			return new CMessage(this);
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.AppendChild(this.Items.XmlDump(xmlDocument));
			return xmlElement;
		}
	}
}
