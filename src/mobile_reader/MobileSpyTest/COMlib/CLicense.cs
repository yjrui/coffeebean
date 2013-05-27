using MobilEditCom;
using System;
using System.Xml;
namespace COMlib
{
	public class CLicense : CItem
	{
		public enum EProduct
		{
			COMComponents = 67
		}
		public enum EProductType
		{
			Bluetooth = 66,
			IrDA = 68,
			Enterprise,
			Full,
			Cable = 75,
			Lite,
			ReadOnly = 82,
			Trial = 84
		}
		public enum EStatus
		{
			Invalid,
			Activated,
			Limited
		}
		private IMedLicense MedLicense;
		public string Company
		{
			get
			{
				return this.MedLicense.LicensedCompany;
			}
		}
		public string User
		{
			get
			{
				return this.MedLicense.LicensedUser;
			}
		}
		public string OEM
		{
			get
			{
				return this.MedLicense.Oem;
			}
		}
		public CLicense.EProduct Product
		{
			get
			{
				return (CLicense.EProduct)this.MedLicense.Product;
			}
		}
		public CLicense.EProductType ProductType
		{
			get
			{
				return (CLicense.EProductType)this.MedLicense.ProductType;
			}
		}
		public CLicense.EStatus Status
		{
			get
			{
				return (CLicense.EStatus)this.MedLicense.Status;
			}
		}
		public DateTime UpdateUntil
		{
			get
			{
				return this.MedLicense.UpdateUntil;
			}
		}
		public DateTime ValidUntil
		{
			get
			{
				return this.MedLicense.ValidUntil;
			}
		}
		public bool DongleValidated
		{
			get
			{
				return this.MedLicense.DongleStatus == 1;
			}
		}
		internal CLicense(CItem parent, IMedLicense medLicense) : base(parent)
		{
			if (!(parent is CDriver))
			{
				throw new Exception("Internal error");
			}
			this.MedLicense = medLicense;
		}
		public virtual void Activate(string key, string user, string company, string oem)
		{
			this.Activate(key, user, company, oem, new CDriver.Progress(CDriver.ProgressClass.Progress));
		}
		public virtual bool Activate(string key, string user, string company, string oem, CDriver.Progress progress)
		{
			IOperation operation;
			try
			{
				operation = (IOperation)this.MedLicense.Activate(key, user, company, oem);
			}
			catch (Exception ex)
			{
				throw new Exception(this.ToString() + " : Activation failed [ " + ex.Message + " ]", ex);
			}
			if (!CDriver.OperationToProgress(operation, progress, 0, 1000))
			{
				return false;
			}
			if (operation.Status != 1)
			{
				throw new Exception(this.ToString() + " : Activation failed with error " + operation.Error.ToString());
			}
			return true;
		}
		public void Deactivate()
		{
			IOperation operation;
			try
			{
				operation = (IOperation)this.MedLicense.Deactivate();
			}
			catch (Exception ex)
			{
				throw new Exception(this.ToString() + " : Deactivation failed [ " + ex.Message + " ]", ex);
			}
			operation.Wait();
			if (operation.Status != 1)
			{
				throw new Exception(this.ToString() + " : Deactivation failed with error " + operation.Error.ToString());
			}
		}
		public override XmlElement XmlDump(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = base.XmlDump(xmlDocument);
			xmlElement.SetAttribute("Company", this.Company);
			xmlElement.SetAttribute("User", this.User);
			xmlElement.SetAttribute("OEM", this.OEM);
			xmlElement.SetAttribute("Product", this.Product.ToString());
			xmlElement.SetAttribute("ProductType", this.ProductType.ToString());
			xmlElement.SetAttribute("UpdateUntil", this.UpdateUntil.ToString());
			xmlElement.SetAttribute("ValidUntil", this.ValidUntil.ToString());
			xmlElement.SetAttribute("DongleValidated", this.DongleValidated.ToString());
			return xmlElement;
		}
	}
}
