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
        public List<IComponent> mChilds;

        public YamlComponent(List<IComponent> aChilds = null)
        {
            mChilds = aChilds;

        }

        public void add(IComponent child)
        {
            mChilds.Add(child);
        }

        public void remove(IComponent child)
        {
            mChilds.Remove(child);
        }

        public IComponent getChild(int i)
        {
            if(mChilds.Count >= i)
                return mChilds[i];
            return null;
        }

        public List<IComponent> getChildren()
        {
            return mChilds;
        }

        public Object getValue()
        {
            return getChildren();
        }

        public void setValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
