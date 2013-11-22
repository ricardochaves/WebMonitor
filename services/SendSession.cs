using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor.services
{
    public class SendSession
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
                return s.MakeRequest(Strings.URLSTARTNEWSESSION, Json, method.Post);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void closeSession(string Json, string SessionID)
        {
            try
            {
                s.MakeRequest(Strings.URLCLOSESESSION + SessionID, Json, method.Put);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public void checkSession(string Json, string SessionID)
        {
            try
            {
                s.MakeAsyncRequest(Strings.URLCHECKSESSION + SessionID, Json, method.Put);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
