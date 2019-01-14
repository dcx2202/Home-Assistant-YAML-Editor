using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace YAMLEditor
{
    class YamlComponent : IComponent
    {
        public List<IComponent> children { get; }
        public Object value { get; set; }
        public string filepath { get; }

        public YamlComponent(string aFilepath, List<IComponent> aChildren = null)
        {
            children = aChildren;
            value = children;
            filepath = aFilepath;
        }

        public void add(IComponent child)
        {
            children.Add(child);
            value = children;
        }

        public void remove(IComponent child)
        {
            children.Remove(child);
            value = children;
        }

        public IComponent getChild(int i)
        {
            if(children.Count >= i)
                return children[i];
            return null;
        }

        public void setValue(Object aValue)
        {
            value = aValue;
        }
    }
}
