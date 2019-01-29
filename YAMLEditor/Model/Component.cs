using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YAMLEditor.Commands;
using YAMLEditor.Patterns;

namespace YAMLEditor
{
    class Component : IComponent
    {
        private string name;
        public string filename;

        public IComponent parent;
        public List<IComponent> children;

        /// <summary>
        /// Property grid value
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            
            // Called when the user changes a component's value in the property grid
            set
            {
                // Input validation
                if(value.All(c => Char.IsLetterOrDigit(c) || c.Equals('_') || c.Equals("") || c.Equals(' ')))
                {
                    // Can only remove the component's value if it's a leaf
                    if (children.Count != 0 && value == "")
                    {
                        MessageBox.Show("Can't set this node's value as empty", "Error");
                    }
                    else
                    {
                        // Cache the old name
                        string oldvalue = this.name;

                        // Get all the parents
                        List<IComponent> parents = new List<IComponent>();
                        YAMLEditorForm.GetParents(parents, this);

                        // Remove the root
                        parents.Remove(parents.Last());

                        Dictionary<string, List<IComponent>> dic = new Dictionary<string, List<IComponent>> { { oldvalue, parents } };

                        YAMLEditorForm.changedComponents.Add(dic, this);

                        // Update the name (updates the composite)
                        this.name = value;

                        // Create a new command with a reference to the component that changed, the old value and the new
                        ICommand command = new Command(this, oldvalue, this.name);

                        // Execute the command (adds it to the undo queue and updates the tree and composite
                        YAMLEditorForm.Manager.Execute(command);
                    }
                }
                else
                    MessageBox.Show("Please use only letters, numbers, spaces, underscores or nothing (to delete)", "Error");
            }
        }

        /// <summary>
        /// Property grid filename
        /// </summary>
        public string Filename
        {
            get { return this.filename; }
        }

        public Component(string aName, string aFileName, IComponent aParent)
        {
            children = new List<IComponent> { };
            parent = aParent;
            filename = aFileName;
            name = aName;
        }

        /// <summary>
        /// Adds a new child to this component's children
        /// </summary>
        /// <param name="child"></param>
        public void Add(IComponent child)
        {
            children.Add(child);
        }

        /// <summary>
        /// Removes a given child from this component's children
        /// </summary>
        /// <param name="child"></param>
        public void Remove(IComponent child)
        {
            children.Remove(child);
        }

        /// <summary>
        /// Returns this component's child at the given index if it exists, null otherwise
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public IComponent GetChild(int i)
        {
            if(children.Count >= i)
                return children[i];
            return null;
        }

        /// <summary>
        /// Returns this component's children
        /// </summary>
        /// <returns></returns>
        public List<IComponent> GetChildren()
        {
            return children;
        }

        /// <summary>
        /// Returns this component's parent
        /// </summary>
        /// <returns></returns>
        public IComponent GetParent()
        {
            return parent;
        }

        /// <summary>
        /// Sets this component's parent
        /// </summary>
        /// <param name="aParent"></param>
        public void SetParent(IComponent aParent)
        {
            parent = aParent;
        }

        /// <summary>
        /// Returns this component's filename (where it's located)
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return filename;
        }

        /// <summary>
        /// Sets this component's filename (in the program, not in the file system)
        /// </summary>
        /// <param name="aFileName"></param>
        public void SetFileName(string aFileName)
        {
            if (children.Count > 0)
            {
                foreach (IComponent child in children)
                    child.SetFileName(aFileName);
            }

            filename = aFileName;
        }

        /// <summary>
        /// Sets this component's name
        /// </summary>
        /// <param name="aName"></param>
        public void SetName(string aName)
        {
            name = aName;
        }
    }
}
