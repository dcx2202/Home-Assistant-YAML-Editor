using System.IO;

namespace YAMLEditor.Logging
{
    public class FileRecorder : IRecorder
    {
        private StreamWriter Stream { get; set; }

        public FileRecorder(string aFileName)
        {
            Stream = new StreamWriter(aFileName, true) { AutoFlush = true };
        }

        #region IRecorder Members

        public void Write(string aMessage)
        {
            Stream.WriteLine(aMessage);
        }

        #endregion
    }
}
