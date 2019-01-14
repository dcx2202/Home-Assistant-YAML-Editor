using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace YAMLEditor
{
    class YamlLeaf : IComponent
    {
        string mValue;

        public YamlLeaf(string aValue)
        {
            mValue = aValue;
        }

        public void add(IComponent child)
        {
            throw new NotImplementedException();
        }

        public void remove(IComponent child)
        {
            throw new NotImplementedException();
        }

        public IComponent getChild(int i)
        {
            return null;
        }

        public List<IComponent> getChildren()
        {
            return null;
        }

        public Object getValue()
        {
            return mValue;
        }

        public void setValue(string aValue)
        {
            mValue = aValue;
        }
    }
}
