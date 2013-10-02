using Styx.Plugins;
using Styx.WoWInternals;
using Styx.CommonBot;
using Styx.Common;
using Styx;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Styx.WoWInternals.WoWObjects;
using System.Text;
using WebMonitor.factories;
using WebMonitor.modelo;
using WebMonitor.services;

namespace WebMonitor
{
    public class WebMonitor : HBPlugin
    {

        private static Boolean isInit = false;
        private DateTime startTime; //Data que o Bot deu Start
        private SendGuild sGuild = new SendGuild(new Send(), new ConverterJson());

        private bool DEBUG = true;

        WebMonitorApp app;
       

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


            try
            {
                if (WebMonitor.isInit == true)
                {
                    return;
                }

                //VINCULANDO ENVENTOS DO BOT
                Styx.CommonBot.BotEvents.OnBotStarted += onStart;
                Styx.CommonBot.BotEvents.OnBotStopped += onStop;

                Util.WriteLog("WebMonitor initialized.");

                WebMonitor.isInit = true;
            
            }
            catch (Exception ex)
            {

               throw new Exception (ex.Message,ex);
                
            }

        }
        public override void Dispose()
        {
            base.Dispose();

            Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;

            Util.WriteLog("WebMonitor disposed.");

        }

        #endregion

        #region EnventsON
        private void onStart(EventArgs args)
        {

            if (DEBUG) Util.WriteLog("[DEBUG]Inicio do onStart");

            //Util.WriteLog("Sessão iniciada: " + session);

            startTime = DateTime.Now;
            Styx.CommonBot.BotEvents.Player.OnPlayerDied += onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp += onLevel;
            
            Lua.Events.AttachEvent("GUILDBANKFRAME_OPENED", onGuildBankOpened);
            Lua.Events.AttachEvent("GUILDBANK_UPDATE_MONEY", onGuildBankUpdateMoney);
            Lua.Events.AttachEvent("PLAYER_LOGIN", onPlayerLogin);
            Lua.Events.AttachEvent("PLAYER_LOGOUT", onPlayerLogout);
            Lua.Events.AttachEvent("CLOSE_INBOX_ITEM", onPlayerLogout);

            StartApp();
            
            enviarDadosIniciais();

            Util.WriteLog("WebMonitor started.");
        }
        private void onStop(EventArgs args)
        {
            Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;
            app.closeSession();
           
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

            //Util.WriteLog("Guild Name:" + Util.GetGuildProfileName());
            //Util.WriteLog("Guild Money:"  + Util.GetGuildMoney().ToString());
            sGuild.SendGuildInfoMoney(Util.GetGuildProfileName(), Util.GetGuildMoney());
            
        }
        private void onPlayerLogin(object sender, LuaEventArgs args)
        {
            StartApp();
            app.startSession(Styx.CommonBot.BotManager.Current.Name);
            //Enviar dados da Guild do Personagem.
            sGuild.SendGuildInforTotal(Util.GetGuildProfileName(), Util.GetGuildMoney(), Util.GetTotalGuildsAccs());

        }
        private void onPlayerLogout(object sender, LuaEventArgs args)
        {
            app.sendPlayerLogout();
        }
        private void onGuildBankUpdateMoney(object sender, LuaEventArgs args)
        {
            //guildBankMoney = GetGuildBankMoney()
            sGuild.SendGuildInfoMoney(Util.GetGuildProfileName(), Util.GetGuildMoney());
            
        }
        private void onCloseInboxItem(object sender, LuaEventArgs args)
        {
            List<Item> li  = new List<Item>();
            foreach (WoWItem item in StyxWoW.Me.BagItems)
            {
                Item i = new Item();
                i.bagindex = item.BagIndex;
                i.bagslot = item.BagSlot;
                i.id = item.ItemInfo.Id;
                i.name = item.ItemInfo.Name;
                i.stackcount = item.StackCount;
                li.Add(i);
            }
            app.character.listItensBag = li;
            

        }

        #endregion

        private void StartApp()
        {
            app = new WebMonitorApp(GuildFactory.GetInstance(StyxWoW.Me), CharacterFactory.GetInstance(StyxWoW.Me));
            if (DEBUG) Util.WriteLog("[DEBUG]Vai chamar agora");
            app.startSession(Styx.CommonBot.BotManager.Current.Name);
            if (DEBUG) Util.WriteLog("[DEBUG]Chamou");
        }

        public void RefreshSession()
        {

        }
        public void enviarDadosIniciais()
        {
            //Nome personagem
            //StyxWoW.Me.Name;
            //StyxWoW.Me.BagItems 
            //StyxWoW.Me.BagsFull
            //StyxWoW.Me.Class
            //StyxWoW.Me.Copper
            //StyxWoW.Me.CurrentMap //OBJ
            //StyxWoW.Me.Faction //OBJ, Olhar outros Faction
            //StyxWoW.Me.FreeBagSlots
            //StyxWoW.Me.FreeNormalBagSlots   
            //StyxWoW.Me.Gold
            //StyxWoW.Me.Guid; //OBJ
            //StyxWoW.Me.GuildLevel
            //StyxWoW.Me.GuildRank
            //StyxWoW.Me.IsAFKFlagged
            //StyxWoW.Me.Location
            //StyxWoW.Me.MapId
            //StyxWoW.Me.MapName
            //StyxWoW.Me.Mounted
            //StyxWoW.Me.QuestLog
            //StyxWoW.Me.Race
            //StyxWoW.Me.RealmName
            //StyxWoW.Me.RealZoneText
            //StyxWoW.Me.Specialization
            //StyxWoW.Me.SubZoneText
            





            //StyxWoW.Me.Silver
        }

    }
}
