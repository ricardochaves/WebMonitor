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
        public readonly Session session;
        private SendPlayer sPlayer = new SendPlayer(new Send());
        
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

        public void startSession(Session s)
        {
            
        }

    }
}
