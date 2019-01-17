using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace YAMLEditor
{
    class Component : IComponent
    {
        public List<IComponent> children;
        public string filename;
        private string name;
        public IComponent parent;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                YAMLEditorForm.updateTree(this, this.name, null);
            }
        }

        public Component(string aName, string aFileName, IComponent aParent)
        {
            children = new List<IComponent> { };
            parent = aParent;
            filename = aFileName;
            name = aName;
        }

        public void add(IComponent child)
        {
            children.Add(child);
        }

        public void remove(IComponent child)
        {
            children.Remove(child);
        }

        public IComponent getChild(int i)
        {
            if(children.Count >= i)
                return children[i];
            return null;
        }

        public List<IComponent> getChildren()
        {
            return children;
        }

        public IComponent getParent()
        {
            return parent;
        }

        public string getFileName()
        {
            return filename;
        }

        public void setFileName(string aFileName)
        {
            if (children.Count > 0)
            {
                foreach (IComponent child in children)
                    child.setFileName(aFileName);
            }

            filename = aFileName;
        }
    }
}
