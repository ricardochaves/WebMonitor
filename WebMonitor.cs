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
            try
            {
                app.pulse();            
            }
            catch (Exception ex)
            {

                //Logging.WriteException(ex);
            }
            
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

               Logging.WriteException( ex);
                
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

        #region BotEvents
        private void onStart(EventArgs args)
        {

            try
            {
                Logging.WriteDiagnostic("[DEBUG]Inicio do onStart");

                //Util.WriteLog("Sessão iniciada: " + session);

                startTime = DateTime.Now;
                Styx.CommonBot.BotEvents.Player.OnPlayerDied += onDead;
                Styx.CommonBot.BotEvents.Player.OnLevelUp += onLevel;

            

                Lua.Events.AttachEvent("GUILDBANKFRAME_OPENED", onGuildBankOpened);
                Lua.Events.AttachEvent("GUILDBANK_UPDATE_MONEY", onGuildBankUpdateMoney);
                Lua.Events.AttachEvent("GUILDBANKFRAME_CLOSED", onGuildBankClosed);
                Lua.Events.AttachEvent("PLAYER_LOGIN", onPlayerLogin);
                Lua.Events.AttachEvent("PLAYER_LOGOUT", onPlayerLogout);
                Lua.Events.AttachEvent("CLOSE_INBOX_ITEM", onPlayerLogout);
            
                StartApp();

                app.updateCharItens(CharacterFactory.GetItensChar(StyxWoW.Me));



                enviarDadosIniciais();

                Util.WriteLog("WebMonitor started.");

            }
            catch (Exception ex)
            {

                //Logging.WriteException(ex);
            }
            


        }
        private void onStop(EventArgs args)
        {
            try
            {
                Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
                Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;
                app.closeSession();
           
                Util.WriteLog("WebMonitor stoped");
            }
            catch (Exception ex)
            {
                
                Logging.WriteException( ex);
            }

        }
        #endregion

        #region GuildBankEvents
        private void onGuildBankOpened(object sender, LuaEventArgs args)
        {
            try
            {
                app.updateGuildGold(Util.GetGuildProfileName(), Util.GetGuildMoney());
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }

            
        }
        private void onGuildBankUpdateMoney(object sender, LuaEventArgs args)
        {
            try
            {
                app.updateGuildGold(Util.GetGuildProfileName(), Util.GetGuildMoney());
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }
        }
        private void onGuildBankClosed(object sender, LuaEventArgs args)
        {
            try
            {
                app.updateGuildItens(Util.GetGuildProfileName());
            }
            catch (Exception ex)
            {
                
                Logging.WriteException(ex);
            }
        }
        #endregion

        #region PlayerEvents
        private void onPlayerLogin(object sender, LuaEventArgs args)
        {
            try
            {
                StartApp();
                app.startSession(Styx.CommonBot.BotManager.Current.Name);

            }
            catch (Exception ex)
            {

                Logging.WriteException(ex);
            }

        }
        private void onPlayerLogout(object sender, LuaEventArgs args)
        {
            try
            {
                app.sendPlayerLogout();
            }
            catch (Exception ex)
            {

                Logging.WriteException(ex);
            }
            
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
        #endregion

        #region MailBoxEvents
        private void onCloseInboxItem(object sender, LuaEventArgs args)
        {
            
            

        }
        #endregion

        #endregion

        private void StartApp()
        {
            try
            {
                app = new WebMonitorApp(GuildFactory.GetInstance(StyxWoW.Me), CharacterFactory.GetInstance(StyxWoW.Me));
                Logging.WriteDiagnostic("[DEBUG]Vai chamar agora");
                app.startSession(Styx.CommonBot.BotManager.Current.Name);
                Logging.WriteDiagnostic("[DEBUG]Chamou");
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }

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
