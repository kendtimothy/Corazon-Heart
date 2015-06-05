using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorazonHeart
{
    public class CUri
    {
        public string GetCurrentURL()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }

        public string GetCurrentPath()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
        }

    }
}