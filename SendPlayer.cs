﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor
{
    class SendPlayer
    {
        private ISend s;
        public SendPlayer(ISend se)
        {
            s = se;
        }

        public int SendPlayerInfo(string name, string classe, string race, int lvl, string realm)
        {

            return Convert.ToInt16(s.MakeRequest(Strings.URLSENDPLAYERINFO, ""));
        }

    }
}