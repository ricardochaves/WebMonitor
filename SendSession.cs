using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor
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
            return s.MakeRequest(@"SessionAPI/startNewSession", "key=" + WMGlobalSettings.Instance.Key);
        }
    }
}
