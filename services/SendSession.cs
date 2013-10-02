using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor.services
{
    class SendSession
    {

        private ISend s;
        public SendSession(ISend se)
        {
            s = se;
        }

        public string getNewSession()
        {

            //ESSE METODO VAI SAIR
            try
            {
                return s.MakeRequest(Strings.URLSTARTNEWSESSION, "key=" + WMGlobalSettings.Instance.Key);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public string getNewSession(string Json)
        {
            try
            {
                return s.MakeRequest(Strings.URLSTARTNEWSESSION, "key=" + WMGlobalSettings.Instance.Key + "&data=" + Json);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void closeSession(string idSession)
        {
            s.MakeRequest(Strings.URLCLOSESESSION, "key=" + WMGlobalSettings.Instance.Key + "&sessionId=" + idSession);
        }
    }
}