using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMonitor.modelo;

namespace WebMonitor.services
{
    public class SendPlayer
    {
        private ISend s;
        public SendPlayer(ISend se)
        {
            s = se;
        }

        public int SendPlayerInfo(string name, string classe, string race, int lvl, string realm)
        {
            try
            {
                return Convert.ToInt16(s.MakeRequest(Strings.URLSENDPLAYERINFO, ""));
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            
        }

        public void SendPlayerLogout(long idPlayer)
        {
            try
            {
                s.MakeAsyncRequest(Strings.URLSENDPLAYERLOGOUT, "idPlayer=" + idPlayer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public void SendItensPlayer(Character c)
        {
            try
            {
                s.MakeAsyncRequest(Strings.URLSENDPLAYERITENS, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
