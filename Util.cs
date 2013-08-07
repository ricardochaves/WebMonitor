using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.Helpers;
using Styx.WoWInternals;

namespace WebMonitor
{
    public static class Util
    {
        public static void WriteLog(string MSG)
        {
            Logging.Write(string.Format("{0}: {1}", Strings.NOMESISTEMA, MSG));
        }

        private static string GetGuildProfileName(string character = null, string server = null)
        {
            var profile = GetCharacterProfileName(character, server);
            if (string.IsNullOrEmpty(profile))
                return null;
            string lua = string.Format("local val = DataStoreDB.global.Characters[\"{0}\"] if val and val.guildName then return val.guildName end return '' ", profile.ToFormatedUTF8());
            var guildName = Lua.GetReturnVal<string>(lua, 0);
            if (!string.IsNullOrEmpty(guildName))
                return guildName;
            return null;
        }

        private static string GetCharacterProfileName(string character = null, string server = null)
        {
            var hasDataStore = Lua.GetReturnVal<bool>("if DataStoreDB and DataStore_ContainersDB then return 1 else return 0 end", 0);
            if (hasDataStore)
            {
                if (string.IsNullOrEmpty(character))
                    character = Lua.GetReturnVal<string>("return UnitName('player')", 0);

                if (string.IsNullOrEmpty(server))
                    server = Lua.GetReturnVal<string>("return GetRealmName()", 0);

                List<string> profiles = Lua.GetReturnValues("local t={} for k,v in pairs(DataStoreDB.global.Characters) do table.insert(t,k) end return unpack(t)");
                return (from profile in profiles
                        let elements = profile.Split('.')
                        where
                            elements[1].Equals(server, StringComparison.InvariantCultureIgnoreCase) &&
                            elements[2].Equals(character, StringComparison.InvariantCultureIgnoreCase)
                        select profile).FirstOrDefault();
            }
            return null;
        }


    }
}
