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
        public void checkSession(string idSession)
        {
            try
            {
                Util.WriteLog("Atualizando a Session com id: " + idSession);
                s.MakeAsyncRequest(Strings.URLCHECKSESSION, "key=" + WMGlobalSettings.Instance.Key + "&sessionId=" + idSession);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
