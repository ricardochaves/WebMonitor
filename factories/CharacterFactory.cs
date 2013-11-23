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
            c.id = 0;
            c.name = player.Name;
            c.level = player.Level;
            c.realm = player.RealmName;
            c.battlegroup = "Falta pegar";
            c.classe = player.Class.ToString();
            c.race = player.Race.ToString();
            c.guild = GuildFactory.GetInstance(player);
            return c;

        }

        public static List<ItemUnitChar> GetItensChar(LocalPlayer player, Int64 idChar)
        {
            List<WoWItem> l = player.BagItems;
            List<ItemUnitChar> li = new List<ItemUnitChar>();

            foreach (WoWItem item in l)
            {
                ItemUnitChar i = new ItemUnitChar();

                i.idItem = (int)item.ItemInfo.Id;
                i.stackCount = item.StackCount;
                i.bagSlot = item.BagSlot;
                i.bagIndex = item.BagIndex;
                i.idChar = idChar;
                li.Add(i);

            }

            return li;

        }
    }
}
