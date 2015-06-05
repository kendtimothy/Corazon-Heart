using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Provides data type and input formatting services.
    /// </summary>
    public class CFormatting
    {
        #region Data Members
        public CInputFormat InputFormat { get; set; }
        public CDateFormat DateFormat { get; set; }
        public CFormatExchange FormatExchange { get; set; }
        #endregion

        #region Constructor(s)
        public CFormatting()
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
            InputFormat = new CInputFormat();
            DateFormat = new CDateFormat();
            FormatExchange = new CFormatExchange();
        }
        #endregion
    }
}