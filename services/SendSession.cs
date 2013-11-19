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
                return s.MakeRequest(Strings.URLSTARTNEWSESSION, Json, "POST");
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
                s.MakeRequest(Strings.URLCLOSESESSION + SessionID, Json, "PUT");
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
                Util.WriteLog("Atualizando a Session com id: " + SessionID);
                s.MakeAsyncRequest(Strings.URLCHECKSESSION + SessionID, Json, "PUT");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
