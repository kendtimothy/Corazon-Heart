using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using CorazonHeart.Models;
using System.Net;

namespace CorazonHeart
{
    /// <summary>
    /// Provides SMTP client services.
    /// GitHub - https://github.com/kendtimothy/Corazon/blob/master/Corazon/Services/CEmailService.cs
    /// By Ken D Timothy, Jakarta, Indonesia - 2015
    /// 
    /// DEPENDENCIES:
    ///     Corazon/Models
    ///     (https://github.com/kendtimothy/Corazon/blob/master/Corazon/Models)
    /// </summary>
    public class CEmailService
    {
        #region Data Members
        public static MEmailClient Sender = new MEmailClient();
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Static constructor to initalize static variables.
        /// </summary>
        static CEmailService()
        {
            // initialize sender based on the credential defined in web.config
            // You can also modify it to take from your own database
            Sender.ID = 0;
            Sender.ClientName = ConfigurationManager.AppSettings["EmailDisplayName"];
            Sender.ClientAddress = ConfigurationManager.AppSettings["EmailID"];
            Sender.ClientSecret = ConfigurationManager.AppSettings["EmailPassword"];
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Send an email message from the sender to the receiver.
        /// </summary>
        /// <param name="sender">The sender of the email, with the credentials initialized.</param>
        /// <param name="recipient">The receiver of the email.</param>
        /// <param name="message">The message to send.</param>
        /// <returns>Returns true if email is successfully sent, false otherwise.</returns>
        public bool Send(MEmailClient sender, MEmailClient recipient, MEmailMessage message)
        {
            // initialize the boolean which serves as a feedback whether this task is successful/not
            bool isSuccess = false;

            try
            {
                // set the mail message to be sent with From, To, Subject, etc.
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(recipient.ClientAddress);

                // set the sender details (email ID and display name of the sender)
                mailMessage.From = new MailAddress(sender.ClientAddress, sender.ClientName);

                // get the email subject and message
                mailMessage.Subject = message.Subject;
                mailMessage.Body = message.Message;

                // set the mail message to render the text as HTML / Non-HTML
                mailMessage.IsBodyHtml = message.IsBodyHtml;

                // set an SMTP client object (which defines the protocol used to send email messages)
                SmtpClient smtpClient = new SmtpClient(sender.Provider.Host, sender.Provider.Port);
                smtpClient.EnableSsl = true;		// HTTPS
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;

                // set the sender credentials
                smtpClient.Credentials = new NetworkCredential(sender.ClientAddress, sender.ClientSecret);

                // MAX timeout to wait for the server to respond (set to 10 seconds)
                smtpClient.Timeout = 10000;

                // send the email message
                smtpClient.Send(mailMessage);

                // smtpClient.Send will generate an exception in case of failure
                // hence, if it reaches here, the email has been successful sent
                isSuccess = true;
            }
            catch (Exception)
            {
                // An exception occurs, hence, the email has not been sent.
                isSuccess = false;
            }

            // return the feedback whether the task is completed successfully/not
            return isSuccess;
        }

        /// <summary>
        /// Run an emailing task right now, regardless of the scheduled time of the task.
        /// </summary>
        /// <param name="task">The emailing task to run.</param>
        /// <returns>Returns the number of emails successfully sent.</returns>
        public int Run(MEmailTask task)
        {
            // initialize the integer value which counts the number of emails sent
            int sendCount = 0;

            // get the essential details out of the task
            MEmailClient sender = task.Sender;
            List<MEmailClient> recipients = task.Recipients;
            MEmailMessage message = task.Message;

            // send the message from the sender to each recipient
            foreach(MEmailClient recipient in recipients) {
                // send the message to the recipient
                bool isSuccess = Send(sender, recipient, message);

                // increment the send count if the message is sent to the recipient successfully
                if(isSuccess)
                    sendCount++;
            }

            // return the feedback of how many emails are completed successfully
            return sendCount;
        }
        #endregion

        #region Public Methods with Default Sender
        /// <summary>
        /// Send an email message from the sender to the receiver.
        /// </summary>
        /// <param name="recipient">The receiver of the email to send.</param>
        /// <param name="message">The message to send.</param>
        /// <returns>Returns true if email is successfully sent, false otherwise.</returns>
        public bool SendWithDefaultSender(MEmailClient recipient, MEmailMessage message)
        {
            return Send(Sender, recipient, message);
        }

        /// <summary>
        /// Run an emailing task right now, regardless of the scheduled time of the task.
        /// </summary>
        /// <param name="task">The emailing task to run.</param>
        /// <returns>Returns the number of emails successfully sent.</returns>
        public int RunWithDefaultSender(MEmailTask task)
        {
            // modify the task sender
            task.Sender = Sender;

            return Run(task);
        }
        #endregion
    }
}