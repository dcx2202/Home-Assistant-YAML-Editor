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
            YAMLEditorForm.UpdateTree(component, YAMLEditorForm.FileTreeRoot, newvalue);
        }

        public void Redo()
        {
            YAMLEditorForm.UpdateTree(component, YAMLEditorForm.FileTreeRoot, newvalue);
            YAMLEditorForm.UpdateComposite(null, component, newvalue);
            var parents = new List<IComponent>();
            parents = YAMLEditorForm.GetParents(parents, component);
            parents.Remove(parents.Last());
            component.SetName(newvalue);
            YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { oldvalue, parents } }, component);
        }

        public void Undo()
        {
            YAMLEditorForm.UpdateTree(component, YAMLEditorForm.FileTreeRoot, oldvalue);
            YAMLEditorForm.UpdateComposite(null, component, oldvalue);
            var parents = new List<IComponent>();
            parents = YAMLEditorForm.GetParents(parents, component);
            parents.Remove(parents.Last());
            component.SetName(oldvalue);
            YAMLEditorForm.changedComponents.Add(new Dictionary<string, List<IComponent>>() { { newvalue, parents } }, component);
        }
    }
}
