﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WebMonitor
{
    public interface ISend
    {

        void MakeAsyncRequest(string url, string data, method method);
        string MakeRequest(string url, string data, method method);


    }
}
