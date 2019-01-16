using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace YAMLEditor.AutoSave
{
    class AutoSaver
    {
        // 1000 -> 1s
        const int TIMER_DURATION = 30000;
        private static AutoSaveMemento mMemento;

        //FALTA POR AQUI UM mContent DE UM TIPO QQL PARA GUARDAR DUNNO YET
        private static string mContent;

        
        public static void Main()
        {
            Timer saveTimer = new Timer(TIMER_DURATION);
            saveTimer.Elapsed += SaveTimer_Elapsed;
            saveTimer.Start();
        }

        public static void SaveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Save();
            Console.WriteLine("Auto Saved");
        }

        public static void Save()
        {
            mMemento = new AutoSaveMemento(mContent);
        }

        public static void Restore()
        {
            mContent = mMemento.mContent;
        }
    }
}
