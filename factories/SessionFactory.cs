using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;

namespace WebMonitor.factories
{
    class SessionFactory
    {
        public static Session GetInstance(LocalPlayer player)
        {
            Session s = new Session();
            s.botBase = "";
            s.botDebug = "";
            s.character = null;
            s.tempo = DateTime.Now;
            
            return s;
        }    
    }
}
