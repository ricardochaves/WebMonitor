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
        private ConverterJson conv = new ConverterJson();
        private SendPlayer sPlayer = new SendPlayer(new Send());
        private SendSession sSession = new SendSession(new Send());
        private SendGuild sGuild = new SendGuild(new Send());

        private DateTime lastchecksession = DateTime.Now;

        public WebMonitorApp(Guild g, Character c)
        {
            guild = g;
            character = c;

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
                checkSession();
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
                sGuild.SendGuildInfoMoney(Util.GetGuildProfileName(), Util.GetGuildMoney());
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
        public void updateGuildItens(string guildName)
        {
            try
            {
                Logging.Write("Inicio");
                guild.guildTabs = ItemFactory.GetInstanceGuild(guildName);
                Logging.Write("pegouitens");
                sGuild.SendGuildItens(conv.ConvertTOJson(guild));
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
        public void updateCharItens(List<ItemUnit> l)
        {
            try
            {
                character.listItensBag = l;
                sPlayer.SendItensPlayer(conv.ConvertTOJson(character));
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
        #endregion

        #region "Session"

        public void startSession(string botBase)
        {

            try
            {
                Util.WriteLog("[DEBUG]sSession.getNewSession");

                string retorno;

                session.character = character;
                session.botBase = botBase;
                session.botDebug = "";
                retorno = sSession.getNewSession(conv.ConvertTOJson(session));
                
                session = (Session)conv.ConvertJSON<Session>(retorno);
                character = session.character;

                Util.WriteLog("Session.id: " + session.id);

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
                sSession.closeSession(session.id);
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
                if (DateTime.Compare(lastchecksession.AddMinutes(2), DateTime.Now) < 0)
                {
                    sSession.checkSession(session.id);
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

    }
}
