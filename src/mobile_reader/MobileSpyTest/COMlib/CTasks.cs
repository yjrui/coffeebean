using MobilEditCom;
using System;
namespace COMlib
{
	public class CTasks : COrganizerEntries
	{
		internal CTasks(CItem parent, IOrganizer medOrganizer, int index, COrganizer.EType type) : base(parent, medOrganizer, index, type)
		{
		}
	}
}
