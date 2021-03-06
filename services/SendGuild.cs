﻿using System;
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

        public void SendGuildInfoMoney(string jGuild)
        {
            try
            {

                string data = "key={0}&data={1}";
                data = String.Format(data,WMGlobalSettings.Instance.Key, jGuild);
                //s.MakeAsyncRequest(Strings.URLINCLUIRGUILDMONEY, data);
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
                //s.MakeAsyncRequest(Strings.URLINCLUIRGUILDTOTAL, data);
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
               s.MakeAsyncRequest(Strings.URLINCLUIRGUILDITENS, jGuild,method.Post);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
