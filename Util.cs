using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Styx.Common;

namespace WebMonitor
{
    public static class Util
    {
        public static void WriteLog(string MSG)
        {
            Logging.Write(string.Format("{0}: {1}", Strings.NOMESISTEMA, MSG));
        }
    }
}
