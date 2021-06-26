using Be;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;

namespace Bll
{
    public class QueryString : NameValueCollection
    {
        private string _documento;

        public string Documento
        {
            get { return _documento; }
        }

        public QueryString() { }

        public QueryString(NameValueCollection clone) : base(clone)
        {

        }

        public void Add(string name, string value)
        {
            if (!(this[name] == null))
            {
                this[name] = value;
            }
            else
            {
                base.Add(name, value);
            }
        }

        public string ToString()
        {
            string[] parts = new string[this.Count];
            string[] keys = this.AllKeys;
            int i = 0;
            while ((i < keys.Length))
            {
                parts[i] = keys[i] + "=" + HttpContext.Current.Server.UrlEncode(this[keys[i]]);

                System.Math.Min(System.Threading.Interlocked.Increment(ref i), (i - 1));
            }
            string url = string.Join("&", parts);
            if (((!(url == null)
                        || !(url == String.Empty))
                        && !url.StartsWith("?")))
            {
                url = ("?" + url);
            }
            // If False Then
            //     url = Me._document + url
            // End If
            return url;
        }

        public QueryString(string vConversion) : base(FromUrl(vConversion))
        {
        }

        public static QueryString FromCurrent()
        {
            return FromUrl(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        public static QueryString FromUrl(string url)
        {
            string[] parts = url.Split("?".ToCharArray());
            QueryString qs = new QueryString();
            qs._documento = parts[0];
            if ((parts.Length == 1))
            {
                return qs;
            }
            string[] keys = parts[1].Split("&".ToCharArray());
            foreach (string key in keys)
            {
                string[] part = key.Split("=".ToCharArray());
                if ((part.Length == 1))
                {
                    qs.Add(part[0], "");
                }
                else
                {
                    qs.Add(part[0], part[1]);
                }
            }
            return qs;
            //Response.Redirect("frmDestino.aspx?cod=1&nombre=david");
        }
    }

}
