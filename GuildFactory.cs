using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;

namespace WebMonitor.factories
{
    public static class GuildFactory
    {
        public static Guild GetInstance(LocalPlayer player)
        {
            return new Guild();
        }     
    }
}
