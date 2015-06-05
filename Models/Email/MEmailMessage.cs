using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorazonHeart.Models
{
    public class MEmailMessage
    {
        /// <summary>
        /// The ID of the email message.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The subject of the email message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The contents of the email message. This will be rendered as HTML if IsBodyHtml is set to true.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Defines whether body (Message) should be rendered as HTML or not while sending/displaying it.
        /// </summary>
        public bool IsBodyHtml { get; set; }
    }
}