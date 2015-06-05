using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Provides language services
    /// </summary>
    public class CLanguage
    {
        #region Enum
        /* PUBLIC ENUM, DATA MEMBERS and METHODS */
        public enum Language
        {
            English = 1,
            French = 2,
            Indonesian = 3
        }
        #endregion

        #region Data Members
        /// <summary>
        /// The preferred language of the current user. User get and set to retrieve and modify this value.
        /// </summary>
        public Language PreferredLanguage
        {
            get
            {
                return GetLanguage();
            }
            set
            {
                SaveLanguage(value);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Convert a given string to language enum object.
        /// </summary>
        /// <param name="value">The string which represents the language.</param>
        /// <returns>Returns the language enum object if string value is valid, null otherwise.</returns>
        public Language? Convert(string value)
        {
            Language? result = null;

            // get the query string (in case new language is selected
            if (value != null)
            {
                // convert to lower for easier, non-strict comparison
                value = value.ToLower();
                switch (value)
                {
                    case "en":
                    case "english":
                        result = Language.English;
                        break;
                    case "id":
                    case "indonesian":
                        result = Language.Indonesian;
                        break;
                    case "fr":
                    case "french":
                        result = Language.French;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Get the culture which represents the language.
        /// </summary>
        /// <param name="language">The language to get the culture of.</param>
        /// <returns>Returns the culture which represents the language (e.g. 'en', 'fr')</returns>
        public string GetCulture(Language language)
        {
            // init isSuccess
            string result = "en";

            switch (language)
            {
                case Language.English:
                    result = "en";
                    break;
                case Language.French:
                    result = "fr";
                    break;
                case Language.Indonesian:
                    result = "id";
                    break;
            }

            return result;
        }

        public void Redirect(Language language)
        {
            string requestUrl = GetUri(language);

            // redirect to request URL
            HttpContext.Current.Response.Redirect(requestUrl);
        }
        public string GetUri(Language language)
        {
            // get the app object
            Corazon app = Corazon.Current;

            // get current URL and create a URI builder
            string currentUrl = app.Services.Uri.GetCurrentURL();
            var uriBuilder = new UriBuilder(currentUrl);

            // get list of query string and add the 'lang' qs
            NameValueCollection queryStrings = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryStrings["lang"] = language.ToString().ToLower();
            uriBuilder.Query = queryStrings.ToString();

            string requestUrl = uriBuilder.ToString();

            return requestUrl;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get language preferred by the current user. If there is none, Language.English is returned by default. This checks from either query string, cookie or session (if cookie is disabled).
        /// </summary>
        /// <returns>Returns the language preferred by the current user.</returns>
        private Language GetLanguage()
        {
            // use a temporary value, since Lnaguage value can be get either from qs or cookie.
            // If both doesn't return a useful value (i.e. temp = null), this function will return Language.English 
            Language? temp = null;

            // first, try to get from qs, since this will always reflect the latest changes to read from
            temp = GetLanguageFromQueryString();
            if (temp != null)
            {
                // getting from query string  and then
                // write the preference to browser's cookie
                SaveLanguageInCookie(temp.Value);
                //if (!SaveLanguageInCookie(temp.Value))
                //{
                //    // if browser cookie is disabled, then use session
                //    SaveLanguageInSession(temp.Value);
                //}
            }
            else
            {
                // try to get user's preference from browser cookie
                temp = GetLanguageFromCookie();

                if (temp == null)
                {
                    // if browser cookie is disabled, then use session
                    temp = GetLanguageFromSession();
                }
            }

            // in case temp is null, fall back to default value
            if (temp == null)
            {
                temp = Language.English;
            }

            // set the temp value to the public variable, Language
            return temp.Value;
        }

        /// <summary>
        /// Get language set from query string of the current request.
        /// </summary>
        /// <returns>Returns the language enum object if successful, null otherwise.</returns>
        public Language? GetLanguageFromQueryString()
        {
            Language? result = null;

            // get the query string and cast to Language?
            result = Convert(HttpContext.Current.Request.QueryString["lang"]);

            return result;
        }

        /// <summary>
        /// Get language set from browser cookie.
        /// </summary>
        /// <returns>Returns the language enum object if successful, null otherwise.</returns>
        public Language? GetLanguageFromCookie()
        {
            // default return value
            Language? result = null;
            try
            {
                // try to get user's preference from browser cookie
                HttpCookie languageCookie = HttpContext.Current.Request.Cookies["Language"];

                // try to parse the cookie value
                if (languageCookie != null)
                {
                    int dummy;
                    // check if the cookie is an integer
                    if (Int32.TryParse(languageCookie.Value, out dummy))
                    {
                        // try to cast the integer value to the defined enum type Language
                        result = (Language)dummy;
                    }
                }
            }
            catch (Exception) { }

            return result;
        }


        /// <summary>
        /// Get language set from current session.
        /// </summary>
        /// <returns>Returns the language enum object if successful, null otherwise.</returns>
        private Language? GetLanguageFromSession()
        {
            // default return value
            Language? result = null;
            try
            {
                // try to parse the cookie value
                if (HttpContext.Current.Session["Language"] != null)
                {
                    int dummy;
                    // check if the cookie is an integer
                    if (Int32.TryParse(HttpContext.Current.Session["Language"].ToString(), out dummy))
                    {
                        // try to cast the integer value to the defined enum type Language
                        result = (Language)dummy;
                    }
                }
            }
            catch (Exception) { }

            return result;
        }


        /// <summary>
        /// Save language preferred by the current user. This will attempt to save this preference in cookie or session (if cookie is disabled).
        /// </summary>
        /// <param name="language">The language to set as the preferred language.</param>
        /// <returns>Returns true if successful, false otherwise.</returns>
        private void SaveLanguage(Language language)
        {
            // first, try to store in the cookie
            //bool isSuccess = SaveLanguageInCookie(language);
            SaveLanguageInCookie(language);

            //if (!isSuccess)
            //{
            //    // browser cookie is disabled, try to save in session
            //    isSuccess = SaveLanguageInSession(language);
            //}

            //return isSuccess;
        }

        /// <summary>
        /// Save language preferreed by the current user in session.
        /// </summary>
        /// <param name="language">The language to set as the preferred language.</param>
        /// <returns>Returns true if successful, false otherwise.</returns>
        private void SaveLanguageInSession(Language language)
        {
            // init feedback isSuccess
            bool isSuccess = false;

            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session["Language"] = (int)language;
                if (HttpContext.Current.Session["Language"] != null)
                    isSuccess = true;
            }

            //return isSuccess;
        }

        /// <summary>
        /// Save language preferreed by the current user in browser cookie.
        /// </summary>
        /// <param name="language">The language to set as the preferred language.</param>
        /// <param name="cookieDayValidity">The number of days the preference is to be set.</param>
        /// <returns>Returns true if successful, false otherwise.</returns>
        private void SaveLanguageInCookie(Language language, int cookieDayValidity = 365)
        {
            // init feedback isSuccess
            bool isSuccess = false;

            // get the application URL running
            string url = HttpContext.Current.Request.Url.AbsoluteUri;

            // create a new cookie object and set its value
            HttpCookie cookie = new HttpCookie("Language");
            int cookieValue = (int)language;
            cookie.Value = cookieValue.ToString();

            // set the cookie to expire in 1 year
            cookie.Expires = DateTime.Now + TimeSpan.FromDays(cookieDayValidity);

            // important note:
            // if domain contains localhost, set the cookie.Domain: null, else set the actual domain if its live
            // setting cookie.Domain = localhost URL will not work!
            cookie.Domain = url.Contains("localhost") ? null : "ken.portgaz.com";

            // add the cookie to the user's browser
            HttpContext.Current.Response.SetCookie(cookie);

            /* FEEDBACK */
            // try to read the language set in browser cookie
            // and see if it is the same as the language to-set in this function
            //Language? cookieInBrowser = GetLanguageFromCookie();
            //if (cookieInBrowser != null && cookieInBrowser == language)
                //isSuccess = true;

            //return isSuccess;
        }
        #endregion

    }
}