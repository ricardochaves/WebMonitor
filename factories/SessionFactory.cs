using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;
using Styx.CommonBot;

namespace WebMonitor.factories
{
    class SessionFactory
    {
        public static Session GetInstance(LocalPlayer player, BotBase bot )
        {
            Session s = new Session();
            s.botBase = bot.Name;
            s.botDebug = "";
            s.map = player.CurrentMap.Name;
            s.id = "0";
            s.isEnd = 0;
            return s;
        }    
    }
}
