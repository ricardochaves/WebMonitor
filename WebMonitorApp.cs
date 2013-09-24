using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;
using WebMonitor.services;

namespace WebMonitor
{
    class WebMonitorApp
    {

        public readonly Guild guild;
        public readonly Character character;
        public readonly Session session = new Session();
        private SendPlayer sPlayer = new SendPlayer(new Send());
        private SendSession sSession = new SendSession(new Send());


        public WebMonitorApp(Guild g, Character c)
        {
            guild = g;
            character = c;
            
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
            session.id = sSession.getNewSession();
            session.character = character;
            session.dateLogin = DateTime.Now;
            session.botBase = botBase;
            session.botDebug = "";
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
        #endregion





    }
}
