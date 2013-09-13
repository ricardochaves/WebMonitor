using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;

namespace WebMonitor.factories
{
    class CharacterFactory
    {
        public static Character GetInstance(LocalPlayer player)
        {
            return new Character();
        }
    }
}
