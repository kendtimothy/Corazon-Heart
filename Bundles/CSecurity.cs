using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Provides security functionalities.
    /// </summary>
    public class CSecurity
    {
        #region Data Members
        public CCrypto Crypto { get; set; }
        #endregion

        #region Constructor(s)
        public CSecurity()
        {
            // initialize objects
            InitializeObjects();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void InitializeObjects()
        {
            Crypto = new CCrypto();
        }
        #endregion
    }
}