﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class MailSettings
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
