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
            try
            {
                Util.WriteLog(Strings.URLSTARTNEWSESSION);
                Util.WriteLog("key=" + WMGlobalSettings.Instance.Key);

                return s.MakeRequest(Strings.URLSTARTNEWSESSION, "key=" + WMGlobalSettings.Instance.Key);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public void closeSession(string idSession)
        {
            Util.WriteLog("Fechando=" + idSession);
            s.MakeRequest(Strings.URLCLOSESESSION, "key=" + WMGlobalSettings.Instance.Key + "&sessionId=" + idSession);
        }
    }
}
