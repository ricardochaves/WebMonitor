using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Styx.Helpers;
using System.IO;

namespace WebMonitor
{
    class WMGlobalSettings : Settings
    {
        public static readonly WMGlobalSettings Instance = new WMGlobalSettings();
        public WMGlobalSettings()
            : base(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Plugins\WebMonitor\Settings\Global-Settings.xml"))
        {
        }

        [Setting, DefaultValue("")]
        public string Key { get; set; }

        [Setting, DefaultValue("N")]
        public string ImpJson { get; set; }
    }
}
