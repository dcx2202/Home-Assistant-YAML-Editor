using System.Collections.Generic;

namespace YAMLEditor
{
    public interface IComponent
    {
        string Name { get; set; }
        string Filename { get; }
        void Add(IComponent child);
        void Remove(IComponent child);
        IComponent GetChild(int i);
        List<IComponent> GetChildren();
        IComponent GetParent();
        void SetParent(IComponent aParent);
        string GetFileName();
        void SetFileName(string aFileName);
        void SetName(string aName);
    }
}
