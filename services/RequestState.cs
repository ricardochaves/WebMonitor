﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WebMonitor.services
{
    public class RequestState
    {
        public int BufferSize { get; private set; }
        public StringBuilder ResponseContent { get; set; }
        public byte[] BufferRead { get; set; }
        public HttpWebRequest Request { get; set; }
        public HttpWebResponse Response { get; set; }
        public Stream ResponseStream { get; set; }

        public RequestState()
        {
            BufferSize = 1024;
            BufferRead = new byte[BufferSize];
            ResponseContent = new StringBuilder();
            Request = null;
            ResponseStream = null;
        }
    }
}
