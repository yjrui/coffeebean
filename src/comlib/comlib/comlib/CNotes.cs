using MobilEditCom;
using System;
namespace COMlib
{
	public class CNotes : COrganizerEntries
	{
		internal CNotes(CItem parent, IOrganizer medOrganizer, int index, COrganizer.EType type) : base(parent, medOrganizer, index, type)
		{
		}
	}
}
