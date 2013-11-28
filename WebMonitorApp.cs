using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using WebMonitor.services;

using WebMonitor.factories;

using Styx.Common; //por causa do logging
using System.IO;
using System.Threading.Tasks;

namespace WebMonitor
{
    class WebMonitorApp
    {

        public Guild guild;
        public Character character;
        public Session session = new Session();
        private List<ItemUnitChar> itensChar = new List<ItemUnitChar>();
        private List<ItemUnitGuild> itensGuild = new List<ItemUnitGuild>();
        private ConverterJson conv = new ConverterJson();
        private SendPlayer sPlayer = new SendPlayer(new Send());
        private SendSession sSession = new SendSession(new Send());
        private SendGuild sGuild = new SendGuild(new Send());
        

        private DateTime lastchecksession = DateTime.Now;
        private DateTime lastupdateitensplayer = DateTime.Now;
        public WebMonitorApp(Guild g, Character c, Session s)
        {
            guild = g;
            character = c;
            session = s;

            TaskScheduler.UnobservedTaskException += (object sender, UnobservedTaskExceptionEventArgs excArgs) =>
            {
                
                Logging.WriteException(excArgs.Exception);
                excArgs.SetObserved();

            };

        }

        public void pulse()
        {
            try
            {
                if (session.id == 0)
                {
                    startSession();
                }
                else
                {
                    checkSession();
                }
                
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        #region "Guild"
        public void updateGuildGold(string guildName, string guildGold)
        {
            try
            {
                guild.gold =Convert.ToInt32(Util.GetGuildMoney());
                sGuild.SendGuildInfoMoney(conv.ConvertTOJson(guild));
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {
                    return true;
                });
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        public void updateGuildItens(string guildName, Int64 idGuild)
        {
            try
            {
                itensGuild = GuildFactory.GetInstanceGuild(guildName, idGuild);
                sGuild.SendGuildItens(conv.ConvertTOJson(itensGuild));
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        #endregion

        #region "Player"
        public void updateCharItens(List<ItemUnitChar> l)
        {
            try
            {
                if (DateTime.Compare(lastupdateitensplayer.AddMinutes(3), DateTime.Now) < 0 && session.id != 0)
                {
                    itensChar = l;
                    sPlayer.SendItensPlayer(conv.ConvertTOJson(itensChar));
                    lastupdateitensplayer = DateTime.Now;
                }

            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {

                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void sendPlayerLogout()
        {
            try
            {
                sPlayer.SendPlayerLogout(character.id);
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {

                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void updatePlayerMoney(long gold)
        {
            try
            {
                character.money = gold;
                sPlayer.SendPlayerMoney(conv.ConvertTOJson(character));
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void sendPlayerDead()
        {
            try
            {
                sPlayer.SendPlayerDead();
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {

                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void sendPlayerLevelUP()
        {
            try
            {
                sPlayer.SendPlayerLevelUP();
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {

                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region "Session"

        public void startSession()
        {

            try
            {

                Util.WriteLog("[DEBUG]sSession.getNewSession");

                string retorno;

                session.character = character;
                session.botDebug = "";
                retorno = sSession.getNewSession(conv.ConvertTOJson(session));

                session = (Session)conv.ConvertJSON<Session>(retorno);
                character = session.character;
                if (character.guild != null) { guild = character.guild; }

            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {

                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            

        }
        public void closeSession()
        {
            try
            {
                session.isEnd = 1;
                sSession.closeSession(conv.ConvertTOJson(session),session.id);
                sSession = null;
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {

                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public void changeSession(string botBase, string botDebug)
        {
            try
            {
                session.botBase = botBase;
                session.botDebug = botDebug;
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {

                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void checkSession()
        {
            
            try
            {
                if (DateTime.Compare(lastchecksession.AddMinutes(2), DateTime.Now) < 0 && session.id != 0)
                {
                    sSession.checkSession(conv.ConvertTOJson(session),session.id);
                    lastchecksession = DateTime.Now;
                }
            }
            catch (AggregateException aex)
            {
                aex.Handle((ex) =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region "Chat"

        public void sendLoot(Int64 idItem, Int64 qtd)
        {
            Loot l = new Loot();
            l.idItem = idItem;
            l.qtd = qtd;
            l.idChar = character.id;
            l.idSession = session.id;

            sPlayer.SendPlayerLoot(conv.ConvertTOJson(l));

        }

        #endregion

    }
}
