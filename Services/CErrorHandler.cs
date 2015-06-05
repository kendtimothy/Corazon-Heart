using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Provides custom error handling mechanism.
    /// </summary>
    public class CErrorHandler
    {
        public static void Register(Exception exception)
        {
            try
            {
                // temp, later on we have to store this exception
                throw exception;
            }
            catch (Exception)
            {
                // temp, so that error messge is displayed
                throw;
            }
        }
    }
}