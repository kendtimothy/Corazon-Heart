using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorazonHeart.Models
{
    public class MEmailProvider
    {
        /// <summary>
        /// The ID of the provider.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the provider.
        /// Sample Data: Gmail
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A System.String that contains the name or IP address of the host used for SMTP transactions.
        /// Sample Data: smtp.gmail.com
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// An System.Int32 greater than zero that contains the port to be used on host.
        /// Sample Data: 587
        /// </summary>
        public int Port { get; set; }
    }
}