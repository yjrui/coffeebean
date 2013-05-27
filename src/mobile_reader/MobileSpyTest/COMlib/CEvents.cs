using MobilEditCom;
using System;
namespace COMlib
{
	public class CEvents : COrganizerEntries
	{
		internal CEvents(CItem parent, IOrganizer medOrganizer, int index, COrganizer.EType type) : base(parent, medOrganizer, index, type)
		{
		}
	}
}
