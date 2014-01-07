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


using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;


namespace WebMonitor.Remoting
{
    [ServiceContract]
    interface IRemotingApi
    {
        [OperationContract(IsOneWay = true)]
        void AtualizaEstoque(long IdChar);

        [OperationContract]
        string GetIdChar(string name, string realm);
    
    }
}


namespace WebMonitor
{

    public class WebMonitor : HBPlugin
    {

        private static Boolean isInit = false;
        private DateTime startTime; //Data que o Bot deu Start
        
        WebMonitorApp app;
       

        #region Construtor
        public WebMonitor() 
        { 
           
        }
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
                Util.LogExceptions(ex);
            }
            
        }

        public override void OnEnable()
        {
            try
            {
                if (WebMonitor.isInit == true)
                {
                    return;
                }

                //VINCULANDO ENVENTOS DO BOT
                Styx.CommonBot.BotEvents.OnBotStartRequested += onStart;
                Styx.CommonBot.BotEvents.OnBotStopRequested += onStop;
                Styx.CommonBot.BotEvents.Player.OnPlayerDied += onDead;
                Styx.CommonBot.BotEvents.Player.OnLevelUp += onLevel;
                //Styx.CommonBot.BotEvents.Player.OnMobLooted += onLevel;
                //Styx.CommonBot.BotEvents.Player.OnMobKilled += onLevel;

                Lua.Events.AttachEvent("GUILDBANKFRAME_OPENED", onGuildBankOpened);
                Lua.Events.AttachEvent("GUILDBANK_UPDATE_MONEY", onGuildBankUpdateMoney);
                Lua.Events.AttachEvent("GUILDBANKFRAME_CLOSED", onGuildBankClosed);
                Lua.Events.AttachEvent("PLAYER_LOGIN", onPlayerLogin);
                Lua.Events.AttachEvent("PLAYER_LOGOUT", onPlayerLogout);
                Lua.Events.AttachEvent("CLOSE_INBOX_ITEM", onCloseInboxItem);
                Lua.Events.AttachEvent("CHAT_MSG_LOOT", CHATMSGLOOT);

                Util.WriteLog("WebMonitor initialized.");

                WebMonitor.isInit = true;

            }
            catch (Exception ex)
            {

               Logging.WriteException( ex);
                
            }

        }
        public override void OnDisable()
        {

            Styx.CommonBot.BotEvents.OnBotStartRequested -= onStart;
            Styx.CommonBot.BotEvents.OnBotStopRequested -= onStop;
            Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;

            Lua.Events.DetachEvent("GUILDBANKFRAME_OPENED", onGuildBankOpened);
            Lua.Events.DetachEvent("GUILDBANK_UPDATE_MONEY", onGuildBankUpdateMoney);
            Lua.Events.DetachEvent("GUILDBANKFRAME_CLOSED", onGuildBankClosed);
            Lua.Events.DetachEvent("PLAYER_LOGIN", onPlayerLogin);
            Lua.Events.DetachEvent("PLAYER_LOGOUT", onPlayerLogout);
            Lua.Events.DetachEvent("CLOSE_INBOX_ITEM", onCloseInboxItem);
            Lua.Events.DetachEvent("CHAT_MSG_LOOT", CHATMSGLOOT);

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

                startTime = DateTime.Now;

                StartApp();

                Util.WriteLog("DEPOIS DO START: IDSESSION: " + app.session.id);
                Util.WriteLog("DEPOIS DO START: IDCHAR: " + app.character.id);
                if (app.guild != null) { Util.WriteLog("DEPOIS DO START: Guild: " + app.guild.id); }

                Util.WriteLog("WebMonitor started.");

            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }
            


        }
        private void onStop(EventArgs args)
        {
            try
            {

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
                app.updateGuildItens(Util.GetGuildProfileName(), app.guild.id);
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
                app.startSession();

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
            try
            {
                app.sendPlayerDead();
            }
            catch (Exception ex)
            {

                Logging.WriteException(ex);
            }
            
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

            try
            {
                app.sendPlayerLevelUP();
            }
            catch (Exception ex)
            {

                Logging.WriteException(ex);
            }



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
            try
            {
                
                app.updateCharItens(CharacterFactory.GetItensChar(StyxWoW.Me, app.character.id));
                app.updatePlayerMoney(Convert.ToInt64((StyxWoW.Me.Copper) + (StyxWoW.Me.Silver * 100) + (StyxWoW.Me.Gold * 10000)));

            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }
        }
        #endregion

        #endregion

        #region Chat
        
        private void CHATMSGLOOT(object sender, LuaEventArgs args)
        {


           
            //PEGANDO ITEM ID
            string itemID;
            itemID = args.Args[0].ToString();
            itemID = itemID.Substring(itemID.IndexOf("item:") + 5, itemID.Length - (itemID.IndexOf("item:") + 5));
            itemID = itemID.Substring(0, itemID.IndexOf(":"));

            

            //PEGANDO QUANTIDADE DO LOOT
            string qtd;
            string tempQTD;

            qtd = args.Args[0].ToString();
            tempQTD = qtd.Substring(qtd.IndexOf("|h|r") + 4, qtd.Length - (qtd.IndexOf("|h|r") + 4));
            if (tempQTD.Substring(0, 1) == "x")
            {
                qtd = tempQTD.Substring(1, tempQTD.IndexOf(".") - 1);
            }
            else
            {
                qtd = "1";
            }

            app.sendLoot(Convert.ToInt64(itemID), Convert.ToInt64(qtd));
        
        }
        #endregion

        private void StartApp()
        {
            try
            {
                app = new WebMonitorApp(GuildFactory.GetInstance(StyxWoW.Me), CharacterFactory.GetInstance(StyxWoW.Me), SessionFactory.GetInstance(StyxWoW.Me, Styx.CommonBot.BotManager.Current));
                app.startSession();
                Logging.WriteDiagnostic("[WebMonitor]: Sessão id: " + app.session.id);
            }
            catch (Exception ex)
            {
                Logging.WriteException(ex);
            }

        }


        #region API



        #endregion

    }





    static public class WebMonitorAPI
    {
   
        public static void AtualizaEstoque(string IdChar)
        {
            List<ItemUnitChar> l = CharacterFactory.GetItensChar(StyxWoW.Me, Convert.ToInt64(IdChar));
            SendPlayer s = new SendPlayer(new Send());
            ConverterJson conv = new ConverterJson();
            string jSon = conv.ConvertTOJson(l);
            s.SendItensPlayer(jSon);

        }

        public string GetIdChar(string name, string realm)
        {
            GET.executeRequest("");
            ConverterJson conv = new ConverterJson();

            return "";
        }
   
    }




}

