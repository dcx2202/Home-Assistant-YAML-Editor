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
            //YAMLEditor.
            YAMLEditorForm.UpdateComposite(null, component, newvalue);
            YAMLEditorForm.UpdateTree(component, YAMLEditorForm.FileTreeRoot);
        }

        public void Redo()
        {
            Execute();
        }

        public void Undo()
        {
            YAMLEditorForm.UpdateComposite(null, component, oldvalue);
            YAMLEditorForm.UpdateTree(component, YAMLEditorForm.FileTreeRoot);
        }
    }
}
