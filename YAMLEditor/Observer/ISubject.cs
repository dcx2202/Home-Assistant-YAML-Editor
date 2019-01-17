using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor.Patterns
{
	public delegate void UpdateEventHandler(ISubject aSubject, object aData);
	public interface ISubject
	{
		event UpdateEventHandler OnUpdate;
		void Notify(object aData);
	}
}
