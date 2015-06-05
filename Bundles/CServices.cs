using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorazonHeart;

namespace CorazonHeart
{
    /// <summary>
    /// Provides external services.
    /// </summary>
    public class CServices
    {
        #region Data Members
        public CLanguage Language { get; set; }
        public CEmailService EmailService { get; set; }
        public CGenerate Generate { get; set; }
        public CInteraction Interaction { get; set; }
        public CUri Uri { get; set; }
        public CCompression Compression { get; set; }
        public CObfuscation Obfuscation { get; set; }
        #endregion

        #region Constructor(s)
        public CServices()
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
            Language = new CLanguage();
            EmailService = new CEmailService();
            Generate = new CGenerate();
            Interaction = new CInteraction();
            Uri = new CUri();
            Compression = new CCompression();
            Obfuscation = new CObfuscation();
        }
        #endregion
    }
}