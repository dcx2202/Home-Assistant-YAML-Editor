using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor
{
    public interface IComponent
    {
        string Name { get; set; }
        void add(IComponent child);
        void remove(IComponent child);
        IComponent getChild(int i);
        List<IComponent> getChildren();
        IComponent getParent();
        void setParent(IComponent aParent);
        string getFileName();
        void setFileName(string aFileName);
        void setName(string aName);
    }
}
