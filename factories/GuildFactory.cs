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
            Guild g = new Guild();
            g.name = Util.GetGuildProfileName();
            g.realm = player.RealmName;
            g.gold = Convert.ToInt64(Util.GetGuildMoney());
            //g.guildTabs = new List<ItemUnit>();
            return g;
        }


        public static List<ItemUnitGuild> GetInstanceGuild(string guildName, Int64 idGuild)
        {
            try
            {
                return Util.GetItensGuild(guildName, idGuild);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
