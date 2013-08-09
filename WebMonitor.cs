using Styx.Plugins;
using Styx.WoWInternals;
using Styx.CommonBot;
using Styx.Common;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Styx.MemoryManagement;


namespace WebMonitor
{
    class WebMonitor : HBPlugin
    {

        private static Boolean isInit = false;
        private DateTime startTime; //Data que o Bot deu Start


        #region Construtor
        public WebMonitor() { }
        #endregion

        #region Overrides
        public override string Author
        {
            get { return Strings.AUTOR; }
        }
        public override string Name
        {
            get { return Strings.NOMESISTEMA; }
        }
        public override System.Version Version
        {
            get { return new System.Version(1, 0, 0); }
        }
        public override bool WantButton { get { return true; } }
        public override string ButtonText { get { return Strings.CAPTIONBUTTONCONFIG; } }
        
        public override void OnButtonPress()
        {
            new FormSettings().ShowDialog();
        }
        public override void Pulse()
        {
            //PARA TESTAR SE O PULSE ESTÁ RODANDO
            //Logging.Write(string.Format("[MasterControl]: pulse"));
            //updater();
        }
        
        public override void Initialize()
        {



            if (WebMonitor.isInit == true)
            {
                return;
            }

            //VINCULANDO ENVENTOS DO BOT
            Styx.CommonBot.BotEvents.OnBotStarted += onStart;
            Styx.CommonBot.BotEvents.OnBotStopped += onStop;

            Util.WriteLog("WebMonitor initialized.");
            
            
            
            //Styx.CommonBot.BotEvents.Profile.OnNewOuterProfileLoaded
            //Styx.CommonBot.BotEvents

            //LEVANTANDO QUAIS EVENTOS ELE VAI PEGAR NO JOGO.
            //AQUI VAMOS PEGAR TUDO O QUE QUEREMOS.
            //Lua.Events.AttachEvent("GUILDBANKFRAME_OPENED", GbankUpdate);
            //Lua.Events.AttachEvent("GUILDBANK_UPDATE_MONEY", GbankUpdate);
            ////Lua.Events.AttachEvent("LOOT_OPENED", LOOTOPENED);
            ////Lua.Events.AttachEvent("LOOT_SLOT_CLEARED", LOOTSLOTCLEARED);
            //Lua.Events.AttachEvent("ZONE_CHANGED", ZONECHANGED);
            //Lua.Events.AttachEvent("ZONE_CHANGED_INDOORS", ZONECHANGEDINDOORS);
            //Lua.Events.AttachEvent("ZONE_CHANGED_NEW_AREA", ZONECHANGEDNEWAREA);

            //Lua.Events.AttachEvent("CHAT_MSG_LOOT", CHATMSGLOOT);
            //AppDomain.CurrentDomain.ProcessExit += CurrentDomainProcessExit;
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;


            ////CONTROLES DE TEMPO, TENTAR ENTENDER DEPOIS
            //lastUpdate = DateTime.Now;
            //startTime = DateTime.Now;

            //Logging.Write(string.Format("[MasterControl]: Version {0} Loaded.", Version.ToString()));
            //var profile = Styx.CommonBot.BotManager.Current;
            //Logging.Write(string.Format("[MasterControl]: Styx.CommonBot.BotManager.Current: {0}.", profile));

            ////IniciaSessao();
            //WebMonitor.isInit = true;
            

            
            

        }
        public override void Dispose()
        {
            base.Dispose();

            Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;

            Util.WriteLog("WebMonitor disposed.");

            //Styx.CommonBot.BotEvents.Player.OnMobKilled -= onMobkill;

            //Lua.Events.DetachEvent("GUILDBANKFRAME_OPENED", GbankUpdate);
            //Lua.Events.DetachEvent("GUILDBANK_UPDATE_MONEY", GbankUpdate);
            ////Lua.Events.DetachEvent("LOOT_OPENED", LOOTOPENED);
            ////Lua.Events.DetachEvent("LOOT_SLOT_CLEARED", LOOTSLOTCLEARED);
            //Lua.Events.DetachEvent("CHAT_MSG_LOOT", CHATMSGLOOT);

            //Lua.Events.DetachEvent("ZONE_CHANGED", ZONECHANGED);
            //Lua.Events.DetachEvent("ZONE_CHANGED_INDOORS", ZONECHANGEDINDOORS);
            //Lua.Events.DetachEvent("ZONE_CHANGED_NEW_AREA", ZONECHANGEDNEWAREA);


            //AppDomain.CurrentDomain.ProcessExit -= CurrentDomainProcessExit;
            //AppDomain.CurrentDomain.UnhandledException -= CurrentDomainUnhandledException;

            
            //times.Dispose();
            //MasterControl.isInit = false;

            //FinalizaSessao();

            //Logging.Write("[MasterControl]: Session Finished - ID: {0}: ", Sessao.id);


        }

        #endregion

        #region EnventsON
        private void onStart(EventArgs args)
        {
            startTime = DateTime.Now;
            Styx.CommonBot.BotEvents.Player.OnPlayerDied += onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp += onLevel;

            Lua.Events.AttachEvent("GUILDBANKFRAME_OPENED", onGuildBankOpened);

            Util.WriteLog("WebMonitor started.");
        }
        private void onStop(EventArgs args)
        {
            Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;
            Util.WriteLog("WebMonitor stoped");
        }
        private void onDead()
        {

            //WSBot.estMorte m = new WSBot.estMorte();

            //using (Styx.StyxWoW.Memory.AcquireFrame())
            //{
            //    m.mortes = Convert.ToInt32(Styx.CommonBot.GameStats.Deaths);
            //    m.morteshora = Convert.ToInt32(Styx.CommonBot.GameStats.DeathsPerHour);
            //    m.dt = DateTime.Now;
            //    m.RealZoneText = RetornaZoneName();
            //    m.SubZoneText = RetornaSubZoneName();
            //}

            //b.IncluiNovaMorte(Sessao, m, SECURETY_KEY);

            //Logging.Write("[MasterControl]: Morto!");

            //VERIFICAR O SCREENSHOT
            //if (MasterControlSettings.Instance.scDied) screenie();

        }
        private void onLevel(BotEvents.Player.LevelUpEventArgs args)
        {

            //WSBot.estLevelUp lvl = new WSBot.estLevelUp();

            //lvl.lvl = Convert.ToInt32(Styx.StyxWoW.Me.Level);
            //lvl.data = DateTime.Now;

            //b.IncluirLevelUp(Sessao, lvl, SECURETY_KEY);

            //Logging.Write("[MasterControl]: Level up!");

            //if (!status.ContainsValue("Level up!"))
            //    status.Add("message", "Level up!");

            //if (MasterControlSettings.Instance.scLevel) screenie();
        }
        private void onGuildBankOpened(object sender, LuaEventArgs args)
        {

        }
        #endregion


    }
}
