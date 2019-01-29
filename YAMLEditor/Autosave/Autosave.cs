using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAMLEditor.Logging;

namespace YAMLEditor.Autosave
{
    class Autosave
    {
        // 1000 = 1s
        private const int TIMER_DURATION = 600000;
        private ILogger logger;

        public Autosave(ILogger logger)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(autosave);
            timer.Interval = TIMER_DURATION;
            timer.Start();
            this.logger = logger;
        }

        private void autosave(object sender, EventArgs e)
        {
            YAMLEditorForm.Save();
            logger.WriteLine("'TIS WORKEN\n----------------------------------------\n");
        }
    }
}
