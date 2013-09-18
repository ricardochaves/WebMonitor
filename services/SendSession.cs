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
            return s.MakeRequest(Strings.URLSTARTNEWSESSION, "key=" + WMGlobalSettings.Instance.Key);
        }
        public void closeSession(string idSession)
        {
            s.MakeRequest(Strings.URLSTARTCLOSESESSION, "key=" + WMGlobalSettings.Instance.Key + "&idSession=" + idSession);
        }
    }
}
