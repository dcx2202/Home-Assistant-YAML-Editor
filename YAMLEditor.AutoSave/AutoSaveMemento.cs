using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMLEditor.AutoSave
{
    class AutoSaveMemento
    {
        public string mContent { get; set; }

        public AutoSaveMemento(string content)
        {
            mContent = content;
        }
    }
}
