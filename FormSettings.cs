using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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


    }
}
