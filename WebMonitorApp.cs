using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using WebMonitor.services;

using Styx.Common;

namespace WebMonitor
{
    class WebMonitorApp
    {

        public readonly Guild guild;
        public readonly Character character;
        public Session session = new Session();
        private ConverterJson conv = new ConverterJson();
        private SendPlayer sPlayer = new SendPlayer(new Send());
        private SendSession sSession = new SendSession(new Send());
        private DateTime lastchecksession = DateTime.Now;

        public WebMonitorApp(Guild g, Character c)
        {
            guild = g;
            character = c;
        }

        public void pulse()
        {
            try
            {
                checkSession();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        #region "Player"
        public void updateCharItens(List<Item> l)
        {
            try
            {
                character.listItensBag = l;
                sPlayer.SendItensPlayer(character);
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
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        public void atualizaEstoque(List<Item> li)
        {
            character.listItensBag = li;
            sPlayer.SendItensPlayer(character);

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
                Util.WriteLog("Retorno: " + retorno);

                session = (Session)conv.ConvertJSON<Session>(retorno);

            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }
           
            Util.WriteLog("Session.id: " + session.id);

        }

        public void closeSession()
        {
            try
            {
                sSession.closeSession(session.id);
                sSession = null;
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
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        #endregion





    }
}
