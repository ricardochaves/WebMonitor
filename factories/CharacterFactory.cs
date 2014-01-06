using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using Styx.WoWInternals.WoWObjects;
using Styx.WoWInternals;

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
            string itens = @"local tamanhoBag = 0
                            local tabelaCounts
                            local tabelaIds
                            local qtd, idItem, idBag, name, posicao, tamanho
                            local mt = {{}}
                            local pName = '{0}'
                            local pRealm = '{1}'
                            for k,v in pairs(DataStore_ContainersDB.global.Characters) do  
                                posicao = strfind(k,pRealm)
                                tamanho = strlen(pRealm) + 1
                                if posicao ~= nil then
                                    name = string.sub(k,posicao+tamanho,50)
                                end
                                if name == pName then  
                                    for k2,v2 in pairs(v) do
                                        if k2 == 'Containers' then
                                        for k3,v3 in pairs(v2) do
                                            tamanhoBag = v3['size']
                                            tabelaCounts = v3['counts']
                                            tabelaIds =  v3['ids']
                                            for Count = 1 , tamanhoBag, 1 do
                                                if tabelaIds[Count] ~= nil then
                                                    idItem = tabelaIds[Count]
                                                    if tabelaCounts[Count] == nil then
                                                    qtd = 1
                                                    else
                                                    qtd = tabelaCounts[Count]
                                                    end
                                                    if k3 ~= nil then
                                                    idBag = k3:gsub('Bag100', '-1')
                                                    idBag = idBag:gsub('Bag', '')
                                                    table.insert(mt, idBag .. '|' .. Count .. '|' .. idItem .. '|' .. qtd )
                                                    end
                                                end
                                            end
                                        end
                                        end
                                    end  
                                end  
                            end  
                            return unpack(mt)";

            

            try
            {
                itens = String.Format(itens, player.Name, player.RealmName);
                List<string> luaRet = Lua.GetReturnValues(itens);

                Char c = new Char();
                c = Convert.ToChar("|");
                
                foreach (var item in luaRet)
                {

                    string[] it = item.Split(c);

                    ItemUnitChar itU = new ItemUnitChar();
                    itU.bagIndex = Convert.ToInt32(it[0]);
                    itU.bagSlot = Convert.ToInt32(it[1]);
                    itU.idItem = Convert.ToInt32(it[2]);
                    itU.stackCount = Convert.ToUInt32(it[3]);
                    itU.idChar = idChar;
                    li.Add(itU);
                }
                return li;
            }
            catch (Exception ex)
            {

                throw ex;
            }



            //foreach (WoWItem item in l)
            //{
            //    ItemUnitChar i = new ItemUnitChar();

            //    i.idItem = (int)item.ItemInfo.Id;
            //    i.stackCount = item.StackCount;
            //    i.bagSlot = item.BagSlot;
            //    i.bagIndex = item.BagIndex;
            //    i.idChar = idChar;
            //    li.Add(i);

            //}

            


        }
    }
}
