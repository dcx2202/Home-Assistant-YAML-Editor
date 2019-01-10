namespace YAMLEditor.Logging
{
    public class TraceRecorder : IRecorder
    {
        public void Write(string aMessage)
        {
            System.Diagnostics.Trace.WriteLine(aMessage);
        }

    }
}