﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;

namespace WebMonitor.factories
{
    public static class ItemFactory
    {
        public static List<ItemUnit> GetInstancePlayer(LocalPlayer player)
        {
            try
            {
                List<ItemUnit> li = new List<ItemUnit>();

                foreach (WoWItem item in player.BagItems)
                {
                    ItemUnit i = new ItemUnit();
                    i.bagIndex = item.BagIndex;
                    i.bagSlot = item.BagSlot;
                    i.idBlizzard = item.ItemInfo.Id;
                    i.name = item.ItemInfo.Name;
                    i.stackCount = item.StackCount;
                    li.Add(i);
                }
                return li;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static List<ItemUnit> GetInstanceGuild(string guildName)
        {
            try
            {
                return Util.GetItensGuild(guildName);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }


    }
}
