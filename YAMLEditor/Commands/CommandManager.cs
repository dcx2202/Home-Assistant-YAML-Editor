using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAMLEditor.Observer;

namespace YAMLEditor.Commands
{
	public class CommandManager : ICommandManager, ISubject
	{
		#region Fields

		protected List<ICommand> Commands { get; } = new List<ICommand>();
		protected int Position { get; set; } = -1;

		#endregion

		/// <summary>
		/// Determines if exists commands to be undoed.
		/// </summary>
		public bool HasUndo()
		{
			return (Position > -1);
		}

		/// <summary>
		/// Determines if exists commands to be redoed.
		/// </summary>
		public bool HasRedo()
		{
			return (Position < Commands.Count - 1);
		}

		/// <summary>
		/// Undo the last executed or redoed command
		/// </summary>
		public void Undo()
		{
			if (!HasUndo()) return;
			Commands[Position].Undo();
			Position = Position - 1;
			Notify();
		}

		/// <summary>
		/// Redo the previous undoed command.
		/// </summary>
		public void Redo()
		{
			if (!HasRedo()) return;
			Position = Position + 1;
			Commands[Position].Redo();
			Notify();
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		public void Execute(ICommand aCommand)
		{
			if (HasRedo())
			{
				Commands.RemoveRange(Position + 1, Commands.Count - Position - 1);
			}
			aCommand.Execute();
			Commands.Add(aCommand);
			Position = Commands.Count - 1;

			Notify();
		}

		public void Notify(object aData = null)
		{
		    throw new NotImplementedException();
        }

        public void AddObserver(IObserver o)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IObserver o)
        {
            throw new NotImplementedException();
        }
    }
}
