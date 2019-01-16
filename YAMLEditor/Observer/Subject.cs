using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor.Patterns
{
	public class Subject : ISubject
	{
		public event UpdateEventHandler OnUpdate;

		public void Notify(object aData = null)
		{
			OnUpdate?.Invoke(this, aData);
		}
		protected void Notify(ISubject aSubject, object aData = null)
		{
			OnUpdate?.Invoke(aSubject, aData);
		}
	}
}
