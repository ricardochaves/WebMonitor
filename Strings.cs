using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor
{
    public static class Strings
    {
        public const string NOMESISTEMA = "WebMonitor";
        public const string AUTOR = "Ricardo Baltazar Chaves";
        public const string CAPTIONBUTTONCONFIG = "Settings";
        // public const string HOST = "http://localhost:33087/";
        public const string HOST = "http://bot.nerdsa.com.br/";

        //GUILD
        public const string URLINCLUIRGUILDMONEY = @"";
        public const string URLINCLUIRGUILDTOTAL = @"";
        public const string URLINCLUIRGUILDITENS = @"api/ItemUnitGuild";

        //SESSION
        public const string URLSTARTNEWSESSION = @"api/Session";
        public const string URLCHECKSESSION = @"api/Session/";
        public const string URLCLOSESESSION = @"api/Session/";

        //PLAYER
        public const string URLSENDPLAYERITENS = @"api/ItemUnitChar";
        public const string URLSENDPLAYERLOOT = @"api/Loot";
        public const string URLSENDPLAYERINFO = @"";
        public const string URLSENDPLAYERLOGOUT = @"";
        public const string URLSENDPLAYERMONEY = @"";
        public const string URLSENDPLAYERDEAD = @"";
        public const string URLSENDPLAYERLEVELUP = @"";

        //AH
        public const string URLSENDSALES = @"api/Sale";
    }
}
