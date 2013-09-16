using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;

namespace WebMonitor
{
    class WebMonitorApp
    {

        public readonly Guild guild;
        public readonly Character character;
        private SendPlayer sPlayer = new SendPlayer(new Send());
        
        public WebMonitorApp(Guild g, Character c)
        {
            guild = g;
            character = c;
        }

        public void updateCharItens(List<Item> l)
        {
            character.listItensBag = l;
            sPlayer.SentItensPlayer(character);
        }

        public void sendPlayerLogout()
        {
            sPlayer.SendPlayerLogout(character.id);
        }

    }
}
