using System;
using System.Text;
using System.Text.RegularExpressions;
using Styx.Helpers;
using Styx.Common;
using Styx.CommonBot;
using System.Linq;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Globalization;
using ObjectManager = Styx.WoWInternals.ObjectManager;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using Styx;


namespace WebMonitor
{
    public static class Util
    {
        public static void WriteLog(string MSG)
        {
            Logging.Write(string.Format("{0}: {1}", Strings.NOMESISTEMA, MSG));
        }

        public static string GetGuildProfileName(string character = null, string server = null)
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

        public static string GetCharacterProfileName(string character = null, string server = null)
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

        public static string GetGuildMoney()
        {
            string script = @"local guildBankMoney = GetGuildBankMoney() return guildBankMoney";
            long retVal = Lua.GetReturnVal<long>(script, 0);
            return retVal.ToString();
        }

        public static string GetTotalGuildsAccs()
        {
            string script = @"numGuildMembers, numOnline, numOnlineAndMobile = GetNumGuildMembers() return numGuildMembers";
            long retVal = Lua.GetReturnVal<long>(script, 0);
            return retVal.ToString();
        }

    }
    static class Exts
    {
        public static uint ToUint(this string str)
        {
            uint val;
            uint.TryParse(str, out val);
            return val;
        }

        static Encoding _encodeUTF8 = Encoding.UTF8;

        /// <summary>
        /// Converts a string to a float using En-US based culture
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToSingle(this string str)
        {
            float val;
            float.TryParse(str, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign
            , CultureInfo.InvariantCulture, out val);
            return val;
        }

        /// <summary>
        /// Converts a string to a formated UTF-8 string using \ddd format where ddd is a 3 digit number. Useful when importing names into lua that are UTF-16 or higher.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToFormatedUTF8(this string text)
        {
            StringBuilder buffer = new StringBuilder(_encodeUTF8.GetByteCount(text));
            byte[] utf8Encoded = _encodeUTF8.GetBytes(text);
            foreach (byte b in utf8Encoded)
            {
                buffer.Append(string.Format("\\{0:D3}", b));
            }
            return buffer.ToString();
        }
        /// <summary>
        /// This is a fix for WoWPoint.ToString using current cultures decimal separator.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToInvariantString(this WoWPoint text)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2}", text.X, text.Y, text.Z);
        }
    }

}
