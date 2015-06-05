using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorazonHeart.Models
{
    /// <summary>
    /// Defines a model for an emailing task - where individual and batch emails can be scheduled.
    /// </summary>
    public class MEmailTask
    {
        /// <summary>
        /// The ID of the emailing task.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The email message to send in this task.
        /// </summary>
        public MEmailMessage Message { get; set; } 

        /// <summary>
        /// The date and time the task is registered.
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// The date and time the task is scheduled to run.
        /// </summary>
        public DateTime ScheduleDate { get; set; }

        /// <summary>
        /// States whether the task is completed (true) or not (false).
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// The date and time the task is completed. Should be declared as NULL in case task is not completed yet.
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// The email client which will act as the sender to complete this task.
        /// </summary>
        public MEmailClient Sender { get; set; }

        /// <summary>
        /// The list of recipients to send this email to. In case of one recipient, make a list of email clients and add one client into the list.
        /// </summary>
        public virtual List<MEmailClient> Recipients { get; set; }
    }
}