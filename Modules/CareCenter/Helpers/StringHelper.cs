using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Angel.DNN.CareCenter
{
    public class StringHelper
    {
        /// <summary>
        /// Format a string so it can be safely used in a url as a page name
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        /// <param name="allowSlashes"></param>
        public static string FormatForUrl(string pageName, bool allowSlashes)
        {
            // strip single quotes and apostrophes
            pageName = rxSingleQuote.Replace(pageName, ""); 

            // replace non alphanumeric chars with hyphen
            pageName = allowSlashes ? rxNonAlphaNumericAllowSlash.Replace(pageName, "-") : rxNonAlphaNumeric.Replace(pageName, "-");

            // clean up any extraneous hyphens we might have
            pageName = rxOneOrMoreHyphens.Replace(pageName, "-"); 
            if(pageName.EndsWith("-"))
            {
                pageName = pageName.Substring(0, pageName.Length - 1);
            }

            return pageName;
        }

        public static string YesNoString(object o)
        {
            if (o != null)
            {
                string s = o.ToString();
                if (s == "1" || s.ToLower() == "true" || s.ToLower() == "yes")
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
            return "No";
        }

        public static string Repeat(string s, int numOfTimes)
        {
            if (numOfTimes == 1)
            {
                return s;
            }
            else if (numOfTimes > 1)
            {
                StringBuilder repeated = new StringBuilder(s);
                for (int i = 1; i < numOfTimes; i++)
                {
                    repeated.Append(s);
                }
                return repeated.ToString();
            }
            return "";
        }

        public static string TruncateTo(string s, int length)
        {
            if (s == null)
            {
                return "";
            }

            if (s.Length <= length)
            {
                return s;
            }
            else
            {
                return s.Substring(0, length);
            }
        }

        public static string TruncateWithEllipsis(string s, int length)
        {
            if (s.Length > length)
            {
                return TruncateTo(s, length) + "...";
            }
            else
            {
                return s;
            }
        }

        public static string DecodeAndStripHtml(string s)
        {
            return StripHtml(HttpUtility.HtmlDecode(s));
        }

        public static string StripHtml(string s)
        {
            return rxStripHtml.Replace(s, "");
        }

        public static string SanitizeUserInput(string s)
        {
            return rxAnythingInsideAngleBrackets.Replace(s, "");
        }

        #region Regular Expressions

        private static readonly Regex rxAnythingInsideAngleBrackets = new Regex(
            @"<[^>]*>",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        private static readonly Regex rxStripHtml = new Regex(
            @"<[^>]*>",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        /// <summary>
        /// Match any character that is NOT [0-9] or [A-Z] or [a-z] in a numbered capture group
        /// </summary> 
        private static readonly Regex rxNonAlphaNumeric = new Regex(
            @"([^0-9A-Za-z])",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        private static readonly Regex rxNonAlphaNumericAllowSlash = new Regex(
            @"([^0-9A-Za-z/])",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        /// <summary>
        /// Matches the single quote character and Unicode equivalents
        /// </summary>
        private static readonly Regex rxSingleQuote = new Regex(
            @"('|\u2019)",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        /// <summary>
        /// Matches a hyphen character and the Unicode equivalents
        /// </summary>
        private static readonly Regex rxOneOrMoreHyphens = new Regex(
            @"-+",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.Compiled
            );

        #endregion
    }
    
}
