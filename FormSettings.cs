using Styx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WebMonitor.factories;
using WebMonitor.modelo;

namespace WebMonitor
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            WMGlobalSettings.Instance.Load();
            txtKey.Text = WMGlobalSettings.Instance.Key;
            txtImpJson.Text = WMGlobalSettings.Instance.ImpJson;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            WMGlobalSettings.Instance.Key = txtKey.Text;
            WMGlobalSettings.Instance.ImpJson = txtImpJson.Text;
            WMGlobalSettings.Instance.Save();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //WebMonitorApp app;
            //app = new WebMonitorApp(GuildFactory.GetInstance(StyxWoW.Me), CharacterFactory.GetInstance(StyxWoW.Me), SessionFactory.GetInstance(StyxWoW.Me, Styx.CommonBot.BotManager.Current));

            //app.sendSale("Odin");

            List<ItemUnitChar> x = CharacterFactory.GetItensChar(StyxWoW.Me, 8);
            Util.WriteLog(x.Count.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }


    }
}
