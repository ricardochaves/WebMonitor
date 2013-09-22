using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace WebMonitor.services
{
    class SendGuild
    {

        private ISend s;
        private IConvertJson cj;

        public SendGuild(ISend se, IConvertJson c)
        {
            s = se;
            cj = c;
        }

        public void SendGuildInfoMoney(string GuildProfileName, string goldGuild)
        {
            string data = "name={0}&gold={1}";
            data = String.Format(data, GuildProfileName, goldGuild);
            s.MakeAsyncRequest(Strings.URLINCLUIRGUILDMONEY, data);
        }

        public void SendGuildInforTotal(string GuildProfileName, string goldGuild, string AccsGuild)
        {
            string data = "name={0}&gold={1}@acss={2}";
            data = String.Format(data, GuildProfileName, goldGuild, AccsGuild);
            s.MakeAsyncRequest(Strings.URLINCLUIRGUILDTOTAL, data);
        }
        


    }
}
