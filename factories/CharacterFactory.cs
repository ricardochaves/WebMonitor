using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;

namespace WebMonitor.factories
{
    public static class CharacterFactory
    {
        public static Character GetInstance(LocalPlayer player)
        {
            Character c = new Character();
            c.name = player.Name;
            c.level = player.Level;
            c.realm = player.RealmName;
           
            return c;

        }
    }
}
