using System.Collections.Generic;

namespace YAMLEditor.Logging
{
    public class ComposedRecorder : IRecorder
    {
        private readonly IList<IRecorder> mRecoders = new List<IRecorder>();

        public void Append(IRecorder aRecorder)
        {
            if(mRecoders.Contains(aRecorder)) return;
            mRecoders.Add(aRecorder);
        }

        public void Remove(IRecorder aRecorder)
        {
            if(!mRecoders.Contains(aRecorder)) return;
            mRecoders.Remove(aRecorder);
        }

        public void Write(string aMessage)
        {
            foreach(IRecorder recorder in mRecoders)
                recorder.Write(aMessage);
        }
    }
}