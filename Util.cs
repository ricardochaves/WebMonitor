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
using WebMonitor.modelo;

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

        public static List<ItemUnitGuild> GetItensGuild(string guildName, Int64 idGUild)
        {

            try
            {
                ItemUnitGuild itU;
                List<ItemUnitGuild> litU = new List<ItemUnitGuild>();

                string itens = @"
                                local itemCount = 0  
                                local mt = {{}}
                                for k,v in pairs(DataStore_ContainersDB.global.Guilds) do  
                                    if string.find(k,'{0}') and v.Tabs then  
                                        for k2,v2 in ipairs(v.Tabs) do  
                                            if v2 and v2.ids then  
                                                for k3,v3 in pairs(v2.ids) do  
                                                    table.insert(mt,k2 .. '|' .. k3 .. '|' .. v3 .. '|' .. (v2.counts[k3] or 1))
                                                end  
                                            end  
                                        end  
                                    end  
                                end  
                                return unpack(mt)
                                ";

                itens = String.Format(itens, guildName);
                List<string> luaRet = Lua.GetReturnValues(itens);

                Char c = new Char()  ;
                c = Convert.ToChar("|");

                foreach (var item in luaRet)
                {
                    string[] it = item.Split(c);

                    itU = new ItemUnitGuild();
                    itU.tabSlot = Convert.ToInt32(it[0]);
                    itU.tabIndex = Convert.ToInt32(it[1]);
                    itU.idItem = Convert.ToInt32(it[2]);
                    itU.stackCount = Convert.ToUInt32(it[3]);
                    itU.idGuild = idGUild;

                    litU.Add(itU);

                }
                return litU;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Sale> GetSales(string CharName, DateTime Day)
        {

            string itens = @"local TSM = select(2, ...)
                            TSM = LibStub('AceAddon-3.0'):GetAddon('TSM_Accounting')
                            local L = LibStub('AceLocale-3.0'):GetLocale('TradeSkillMaster_Accounting') 

                            local mt = {{}}
                            local item

                            local NOME_CHAR = '{0}'
                            local dataagora = time({1}year={2}, month={3}, day={4}{5})

                            for itemString, data in pairs(TSM.items) do 
   
                                for _, record in pairs(data['sales']) do
      
                                    if (NOME_CHAR ==  record.player) then
         
                                        if (dataagora < record.time) then
            
                                            item = record.time  .. '*'
            
                                            item = item .. string.sub(itemString,6, strfind(itemString,':',6)-1) .. '*'
            
                                            if (record.key == 'Auction') then
                                                item = item .. '1*' 
                                            else
                                                item = item .. '0*' 
                                            end
            
                                            item = item .. record.stackSize .. '*' 
                                            item = item .. floor(record.quantity / record.stackSize) .. '*' 
                                            item = item .. record.copper .. '*' 
                                            if (record.otherPlayer == 'Merchant') then
                                               item = item .. '' .. '*' 
                                            else
                                               item = item .. record.otherPlayer .. '*' 
                                            end
            
                                            table.insert(mt,item)
            
                                        end
                                    end
                                end      
                            end

                            return unpack(mt)
                            ";

            
            //item = item .. record.copper*record.quantity
            Sale sl;
            List<Sale> litsl = new List<Sale>();

            itens = String.Format(itens, CharName, "{", Day.Year.ToString(), Day.Month.ToString(), Day.Day.ToString(), "}");
            List<string> luaRet = Lua.GetReturnValues(itens);
            
            Util.WriteLog(itens);
            
            if (luaRet == null) {
                Util.WriteLog("nulo");
                return null;
            }

            Char c = new Char();
            c = Convert.ToChar("*");
            Util.WriteLog("aqui");
            foreach (var item in luaRet)
            {
                string[] it = item.Split(c);

                sl = new Sale();

                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                origin = origin.AddSeconds(Convert.ToInt64(it[0])); // convert from milliseconds to seconds

                sl.dtSale = origin;
                sl.idItem = Convert.ToInt32(it[1]);
                sl.localSale = Convert.ToInt32(it[2]);
                sl.qtd = Convert.ToInt32(it[3]);
                sl.moneyUnit = Convert.ToInt64(it[4]);
                sl.Buyer = Convert.ToString(it[5]);

                litsl.Add(sl);


                Util.WriteLog(sl.dtSale.ToString() + sl.idItem.ToString() + sl.localSale.ToString() + sl.qtd.ToString() + sl.moneyUnit.ToString() + sl.Buyer);

            }

            return litsl;


        }

        public static void LogExceptions(Exception ex)
        {
            try
            {
                Logging.WriteException(ex);
            }
            catch (Exception)
            {
                
            }
            
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
