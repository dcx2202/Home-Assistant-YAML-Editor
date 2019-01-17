﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.RepresentationModel;
using YAMLEditor;
using YAMLEditor.Commands;
using YAMLEditor.Patterns;

namespace YAMLEditor
{
    class Component : IComponent
    {
        public List<IComponent> children;
        public string filename;
        private string name;
        public IComponent parent;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                // Keep the old name
                string oldvalue = this.name;

                // Update the name (updates the composite)
                this.name = value;

                // Create a new command with a reference to the component that changed, the old value and the new
                ICommand command = new Command(this, oldvalue, this.name);

                // Execute the command (adds it to the undo queue and updates the tree and composite
                YAMLEditorForm.Manager.Execute(command);
            }
        }

        public Component(string aName, string aFileName, IComponent aParent)
        {
            children = new List<IComponent> { };
            parent = aParent;
            filename = aFileName;
            name = aName;
        }

        public void add(IComponent child)
        {
            children.Add(child);
        }

        public void remove(IComponent child)
        {
            children.Remove(child);
        }

        public IComponent getChild(int i)
        {
            if(children.Count >= i)
                return children[i];
            return null;
        }

        public List<IComponent> getChildren()
        {
            return children;
        }

        public IComponent getParent()
        {
            return parent;
        }

        public string getFileName()
        {
            return filename;
        }

        public void setFileName(string aFileName)
        {
            if (children.Count > 0)
            {
                foreach (IComponent child in children)
                    child.setFileName(aFileName);
            }

            filename = aFileName;
        }

        public void setName(string aName)
        {
            name = aName;
        }
    }
}
