using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;

namespace WebMonitor.factories
{
    public static class ItemFactory
    {
        public static List<Item> GetInstance(LocalPlayer player)
        {
            List<Item> li = new List<Item>();

            foreach (WoWItem item in player.BagItems)
            {
                Item i = new Item();
                i.bagindex = item.BagIndex;
                i.bagslot = item.BagSlot;
                i.id = item.ItemInfo.Id;
                i.name = item.ItemInfo.Name;
                i.stackcount = item.StackCount;
                li.Add(i);
            }
            return li;
        }

    }
}
