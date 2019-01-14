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
        void add(IComponent child);
        void remove(IComponent child);
        void setValue(Object value);
    }
}
