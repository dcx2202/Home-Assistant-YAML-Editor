using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor
{
    public interface IComponent
    {
        void Add(IComponent child);
        void Remove(IComponent child);
        IComponent GetChild(int i);
        List<IComponent> GetChildren();
        IComponent GetParent();
        string GetFileName();
        void SetFileName(string aFileName);
    }
}
