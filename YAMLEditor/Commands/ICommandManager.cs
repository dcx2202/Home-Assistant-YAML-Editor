using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor.Patterns
{
	public interface ICommandManager
	{
		bool HasUndo();
		bool HasRedo();

		void Execute(ICommand aCommand);
		void Undo();
		void Redo();
	}
}
