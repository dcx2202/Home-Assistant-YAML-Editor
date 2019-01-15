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
        public string filepath;
        public string name;
        public IComponent parent;

        public Component(string aName, string aFilepath, IComponent aParent)
        {
            children = new List<IComponent> { };
            parent = aParent;
            filepath = aFilepath;
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

        public string getName()
        {
            return name;
        }

        public void setName(string aName)
        {
            name = aName;
        }

        public string getFilePath()
        {
            return filepath;
        }
    }
}
