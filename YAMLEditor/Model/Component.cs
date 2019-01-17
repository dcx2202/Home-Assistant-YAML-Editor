using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YAMLEditor.Observer;

namespace YAMLEditor
{
    class Component : IComponent, ISubject
    {
        private List<IComponent> children;
        private string filename;
        private string name;
        private IComponent parent;
        private List<IObserver> observers;
        public string NodeType { get; private set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify(this);
            }
        }

        public Component(string aName, string aFileName, IComponent aParent, string aType)
        {
            children = new List<IComponent> { };
            observers = new List<IObserver>();
            parent = aParent;
            filename = aFileName;
            name = aName;
            NodeType = aType;
        }

        public void Add(IComponent child)
        {
            children.Add(child);
        }

        public void Remove(IComponent child)
        {
            children.Remove(child);
        }

        public IComponent GetChild(int i)
        {
            if(children.Count >= i)
                return children[i];
            return null;
        }

        public List<IComponent> GetChildren()
        {
            return children;
        }

        public IComponent GetParent()
        {
            return parent;
        }

        public string GetFileName()
        {
            return filename;
        }

        public void SetFileName(string aFileName)
        {
            if(children.Count > 0)
            {
                foreach(IComponent child in children)
                    child.SetFileName(aFileName);
            }

            filename = aFileName;
        }

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void Notify(object aData)
        {
            YAMLEditorForm.UpdateTree(this, Name, null);
            foreach(IObserver o in observers)
                o.Update(this);
        }
    }
}
