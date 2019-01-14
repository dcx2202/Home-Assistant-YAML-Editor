using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor
{
    interface IComponent
    {
        IComponent getChild(int i);
        List<IComponent> getChildren();
        void add(IComponent child);
        void remove(IComponent child);
        Object getValue();
        void setValue(string value);
    }
}
