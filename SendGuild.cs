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

        public void SendGuildEstoque()
        {
            string data = "name={0}&key={1}@gold={2}";
            data = String.Format(data, Util.GetGuildProfileName(), "");
            s.MakeAsyncRequest(Strings.URLINCLUIRGUILD, data);
        }

    }
}
