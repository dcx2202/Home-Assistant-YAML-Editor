using System;
//using System.Runtime.Remoting.Activation;
using System.Text;

namespace YAMLEditor.Logging
{
    public sealed class Logger : ILogger
    {
        #region Singleton - usage: Logger.Instance 

        private Logger()
        {
        }

        public static Logger Instance { get; } = new Logger();

        #endregion

        #region ILogger Members

        public IRecorder Recorder { get; set; }

        public void Write(string aFormat, params object[] aArgs)
        {
            Recorder?.Write(string.Format(aFormat, aArgs));
        }

        public void Write(Exception aException)
        {
            Recorder?.Write(string.Format("Exception: {0}", aException.Message));
        }

        public void WriteLine()
        {
            Recorder?.Write(Environment.NewLine);
        }

        public void WriteLine(string aFormat, params object[] aArgs)
        {
            Recorder?.Write(string.Format(aFormat, aArgs) + Environment.NewLine);
        }

        public void WriteLine(Exception aException)
        {
            int level = 0;

            var builder = new StringBuilder();
            for(var exception = aException; exception != null; exception = exception.InnerException)
            {
                builder.AppendLine($"[{ ++level }] { exception.Message }");
            }

            WriteLine(builder.ToString());
        }

        #endregion
    }
}
