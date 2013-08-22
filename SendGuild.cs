using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor
{
    class SendGuild
    {

        private ISend s;
        public SendGuild(ISend se)
        {
            s = se;
        }

        public void SendGuildEstoque(string GuildProfileName, string goldGuild, string AccsGuild)
        {
            string data = "name={0}&gold={1}&accounts={2}";
            data = String.Format(data, GuildProfileName, goldGuild,AccsGuild);
            s.MakeAsyncRequest(Strings.URLINCLUIRGUILD, data);
        }

    }
}
