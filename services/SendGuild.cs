using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace WebMonitor.services
{
    public class SendGuild
    {

        private ISend s;

        public SendGuild(ISend se)
        {
            s = se;
        }

        public void SendGuildInfoMoney(string GuildProfileName, string goldGuild)
        {
            try
            {

                string data = "key={0}&name={1}&gold={2}";
                data = String.Format(data,WMGlobalSettings.Instance.Key, GuildProfileName, goldGuild);
                s.MakeAsyncRequest(Strings.URLINCLUIRGUILDMONEY, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendGuildInforTotal(string GuildProfileName, string goldGuild, string AccsGuild)
        {

            try
            {
                string data = "key={0}&name={0}&gold={1}@acss={2}";
                data = String.Format(data,WMGlobalSettings.Instance.Key, GuildProfileName, goldGuild, AccsGuild);
                s.MakeAsyncRequest(Strings.URLINCLUIRGUILDTOTAL, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendGuildItens(string jGuild)
        {
            try
            {
                //Util.WriteLog(jGuild);
                string data = "key={0}&data={1}";
                data = String.Format(data, WMGlobalSettings.Instance.Key, jGuild);
                s.MakeAsyncRequest(Strings.URLINCLUIRGUILDITENS, data);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
