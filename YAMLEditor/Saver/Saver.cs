using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAMLEditor.AutoSave;

namespace YAMLEditor.Saver
{
    class Saver
    {
        private readonly Serializer serializer;

        /// <summary>
        /// Constructor where we pass an output TextWriter
        /// to pick where to print to
        /// </summary>
        /// <param name="output"></param>
        public Saver(TextWriter output)
        {
            serializer = new Serializer(output);
        }

        /// <summary>
        /// If no output is set, print to Console
        /// </summary>
        public Saver()
        {
            serializer = new Serializer(Console.Out);
        }



        public void Save<T>(T root, string fileName)
        {
            serializer.PrintYAML(root);
        }
    }
}
