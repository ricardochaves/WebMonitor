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
        public static List<ItemUnit> GetInstance(LocalPlayer player)
        {
            List<ItemUnit> li = new List<ItemUnit>();

            foreach (WoWItem item in player.BagItems)
            {
                ItemUnit i = new ItemUnit();
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
