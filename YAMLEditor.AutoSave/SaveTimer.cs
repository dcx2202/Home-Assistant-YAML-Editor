using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using YAMLEditor.Patterns;

namespace YAMLEditor.AutoSave
{
    class SaveTimer
    {
        // 1000 -> 1s
        const int TIMER_DURATION = 30000;
        private ICommandManager mCommandManager;
        
        public static void Main()
        {
            Timer saveTimer = new Timer(TIMER_DURATION);
            saveTimer.Elapsed += SaveTimer_Elapsed;
            saveTimer.Start();
        }

        private static void SaveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Auto Saved");
        }

    }
}
