using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Lab.RealWorld
{
    [Injectable]
    public interface IPoke
    {
        string Poke();
    }

    [Injectable]
    [InjectFor(typeof(IPoke))]
    public class DummyPoke : IPoke
    {
        private string _siteUri;
        public DummyPoke(string siteUri)
        {
            this._siteUri = siteUri;
        }
        public string Poke()
        {
            return "Hey I am dummy site" ;   
        }
    }

    [Injectable]
    public class WebPoke : IPoke
    {
        private string _siteUri;

        public WebPoke(string siteUri)
        {
            this._siteUri = siteUri;
        }

        public string Poke()
        {
            string s = ":(";

            WebClient client = new WebClient();
            
            using (Stream data = client.OpenRead(this._siteUri))
            {
                using (StreamReader reader = new StreamReader(data))
                {
                    s = reader.ReadToEnd();
                    Console.WriteLine(s);
                    data.Close();
                    reader.Close();
                }
            }

            return s;
        }
    }
}
