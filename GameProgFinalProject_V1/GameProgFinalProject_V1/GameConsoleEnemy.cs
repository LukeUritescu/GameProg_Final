using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgFinalProject_V1
{
    class GameConsoleEnemy : Enemy
    {
        GameConsole console;
        public GameConsoleEnemy()
        {
            this.console = null;
        }

        public GameConsoleEnemy(GameConsole console)
        {
            this.console = console;
        }

        public override void Log(string s)
        {
            if (console != null)
            {
                console.GameConsoleWrite(s);
            }
            else
            {
                base.Log(s);
            }
        }
    }
}
