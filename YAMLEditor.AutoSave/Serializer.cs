using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace YAMLEditor.AutoSave
{   
    public class Serializer
    {
        private readonly TextWriter output;

        public Serializer(TextWriter output)
        {
            this.output = output;
        }

        public void PrintYAML<T>(T root)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(root);
            Console.WriteLine(yaml);
        }
    }
}
