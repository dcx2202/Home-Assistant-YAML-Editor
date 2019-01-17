using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YAMLEditor.Patterns;
using YAMLEditor;

namespace YAMLEditor.Commands
{
    class Command : ICommand
    {
        public IComponent component;
        public string oldvalue;
        public string newvalue;
        
        public Command(IComponent aComponent, string aOldvalue, string aNewvalue)
        {
            component = aComponent;
            oldvalue = aOldvalue;
            newvalue = aNewvalue;
        }

        public void Execute()
        {
            YAMLEditorForm.updateComposite(null, component, newvalue);
            YAMLEditorForm.updateTree(component, YAMLEditorForm.FileTreeRoot);
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            YAMLEditorForm.updateComposite(null, component, oldvalue);
            YAMLEditorForm.updateTree(component, YAMLEditorForm.FileTreeRoot);
        }
    }
}
