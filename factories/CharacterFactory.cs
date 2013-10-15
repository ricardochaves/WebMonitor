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

        public static List<Item> GetItensChar(LocalPlayer player)
        {
            List<WoWItem> l = player.BagItems;
            List<Item> li = new List<Item>();

            foreach (WoWItem item in l)
            {
                Item i = new Item();

                i.id = (int)item.ItemInfo.Id;
                i.stackcount = item.StackCount;
                i.bagslot = item.BagSlot;
                i.bagindex = item.BagIndex;

                li.Add(i);

            }

            return li;

        }
    }
}
