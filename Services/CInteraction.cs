using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace CorazonHeart
{
    /// <summary>
    /// Provides UI interaction and script handling functionalities.
    /// </summary>
    public class CInteraction
    {
        /// <summary>
        /// Display an alert box which will be shown on the screen for ms milliseconds.
        /// </summary>
        /// <param name="control">The control where the script need to be registered. It can be a Page, UserControl, or an UpdatePanel.</param>
        /// <param name="msg">The message to be displayed.</param>
        /// <param name="ms">The time the message needs to be shown in millisecond.</param>
        public void Display(Control control, string msg, int ms = 2000)
        {
            // build the script
            string functionScript = "displayAlert('" + msg + "', " + ms + ");";

            RegisterClientScript(control, functionScript);
        }

        /// <summary>
        /// Register this function to be called when a partial/full postback occurs.
        /// </summary>
        /// <param name="control">The control where the script need to be registered. It can be a Page, UserControl, or an UpdatePanel.</param>
        /// <param name="script">The name of the JS function which need to be registered.</param>
        public void RegisterClientScript(Control control, string script)
        {
            // register the startup script event to call the function
            ScriptManager.RegisterStartupScript(control, control.GetType(), Guid.NewGuid().ToString(), script, true);
        }


        public void RegisterClientScript(Control control, string functionName, string param)
        {
            // initialize the function call as a string script
            string script = string.Format("{0}('{1}')", functionName, param);

            // register the startup script event to call the function
            ScriptManager.RegisterStartupScript(control, control.GetType(), Guid.NewGuid().ToString(), script, true);

        }

        public void DisplayInvalidInput(Control control)
        {
            string msg = "Invalid inputs. Please check again.";
            Display(control, msg);
        }
    }
}