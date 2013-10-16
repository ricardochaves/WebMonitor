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

        public static List<ItemUnit> GetItensChar(LocalPlayer player)
        {
            List<WoWItem> l = player.BagItems;
            List<ItemUnit> li = new List<ItemUnit>();

            foreach (WoWItem item in l)
            {
                ItemUnit i = new ItemUnit();

                i.idBlizzard = (int)item.ItemInfo.Id;
                i.stackCount = item.StackCount;
                i.bagSlot = item.BagSlot;
                i.bagIndex = item.BagIndex;

                li.Add(i);

            }

            return li;

        }
    }
}
