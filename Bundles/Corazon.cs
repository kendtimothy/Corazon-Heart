using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Corazon main object which contains all the functionalities in this library. Get the current object via Corazon.Current.
    /// </summary>
    public class Corazon
    {
        #region Data Members
        /// <summary>
        /// Get the current Corazon object from the session.
        /// </summary>
        public static Corazon Current
        {
            get
            {
                // attempt to get the object from session
                object sessionObject = null;

                if(HttpContext.Current.Session != null)
                    sessionObject = HttpContext.Current.Session["Corazon"];

                // check if session is null or not
                if (sessionObject != null)
                {
                    // object exists, typecast the object from session and return it
                    return (Corazon)sessionObject;
                }
                else
                {
                    return new Corazon();
                }
            }
            set
            {
                // store the app object in the session["Corazon"]
                HttpContext.Current.Session["Corazon"] = value;
            }
        }

        /// <summary>
        /// Formatting class object, which encompasses all classes under this category.
        /// </summary>
        public CFormatting Formatting { get; set; }

        /// <summary>
        /// Security class object, which encompasses all classes under this category.
        /// </summary>
        public CSecurity Security { get; set; }
        
        /// <summary>
        /// Services class object, which encompasses all classes under this category.
        /// </summary>
        public CServices Services { get; set; }

        /// <summary>
        /// A boolean value marking whether the current user is logged in to the website or not.
        /// </summary>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// The primary connection string of the application.
        /// </summary>
        public string ConnectionString { get; set; }
        #endregion

        #region Constructor(s)
        public Corazon()
	    {
            // initialize objects
            InitializeObjects();

            // initialize connection string
            InitializeConnection();

            // set is customer logged in = false (since no customer object is passed here)
            IsLoggedIn = false;
	    }

        //public Corazon(CCustomer customer)
        //{
        //    // initialize objects
        //    InitializeObjects();

        //    // initialize connection string
        //    InitializeConnection();

        //    // set is customer logged in = true
        //    CustomerIsLoggedIn = true;

        //    // modify the customer object to the one passed
        //    Management.Customer = customer;
        //}
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void InitializeObjects()
        {
            Formatting = new CFormatting();
            Security = new CSecurity();
            Services = new CServices();
        }
        private void InitializeConnection()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        #endregion
    }
}