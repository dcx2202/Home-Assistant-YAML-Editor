using System;

namespace YAMLEditor.Logging
{
    public class ConsoleRecorder : IRecorder
    {
        public void Write(string aMessage)
        {
            Console.Write(aMessage);
        }
    }
}
