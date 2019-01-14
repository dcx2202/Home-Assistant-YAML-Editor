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
        public List<IComponent> mChildren;
        public Object value;

        public YamlComponent(List<IComponent> aChildren = null)
        {
            mChildren = aChildren;
            value = mChildren;
        }

        public void add(IComponent child)
        {
            mChildren.Add(child);
            value = mChildren;
        }

        public void remove(IComponent child)
        {
            mChildren.Remove(child);
            value = mChildren;
        }

        public IComponent getChild(int i)
        {
            if(mChildren.Count >= i)
                return mChildren[i];
            return null;
        }

        public List<IComponent> getChildren()
        {
            return mChildren;
        }

        public Object getValue()
        {
            return value;
        }

        public void setValue(Object aValue)
        {
            value = aValue;
        }
    }
}
