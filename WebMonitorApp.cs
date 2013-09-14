using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;

namespace WebMonitor
{
    class WebMonitorApp
    {

        public readonly Guild guild;
        public readonly Character character;

        public WebMonitorApp(Guild g, Character c)
        {
            guild = g;
            character = c;
        }
    }
}
