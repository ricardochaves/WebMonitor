using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor.services
{
    class SendSale
    {
        private ISend s;
        public SendSale(ISend se)
        {
            s = se;
        }

        public void SendSales(string jSales)
        {
            try
            {
                s.MakeAsyncRequest(Strings.URLSENDPLAYERITENS, jSales, method.Post);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in SendSales: " + ex.Message, ex);
            }
        }
    }
}
