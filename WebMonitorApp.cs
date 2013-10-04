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
        public readonly Session session = new Session();
        private ConverterJson conv = new ConverterJson();
        private SendPlayer sPlayer = new SendPlayer(new Send());
        private SendSession sSession = new SendSession(new Send());
        private DateTime lastchecksession = new DateTime();

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


        public void updateCharItens(List<Item> l)
        {
            character.listItensBag = l;
            sPlayer.SendItensPlayer(character);
        }

        public void sendPlayerLogout()
        {
            sPlayer.SendPlayerLogout(character.id);
        }



        #region "Session"

        public void startSession(string botBase)
        {
            Util.WriteLog("[DEBUG]sSession.getNewSession");
            
            session.character = character;
            session.botBase = botBase;
            session.botDebug = "";
            session.id = sSession.getNewSession(conv.ConvertTOJson(session));
            
            Util.WriteLog(session.id);

        }

        public void closeSession()
        {
            sSession.closeSession(session.id);
            sSession = null;

        }

        public void changeSession(string botBase, string botDebug)
        {
            session.botBase = botBase;
            session.botDebug = botDebug;
        }

        private void checkSession()
        {
            
            try
            {
                if (DateTime.Compare(lastchecksession.AddMinutes(2), DateTime.Now) > 0)
                {
                    Logging.WriteDiagnostic("checkSession: " + DateTime.Now.ToString());
                    sSession.checkSession(session.id);
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
