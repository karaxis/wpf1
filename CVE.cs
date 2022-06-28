using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wpf1
{
    public class CVE
    {
        public String Name{get;}
        List<string> Links;
        public CVE(string name)
        {
            Name=name;
            Links = new List<string>();
        }

        public void AddLink(string URI)
        {
            Links.Add(URI);
        }

        public bool Equals(CVE cve)
        {
            if(cve == null) return false;
            if(cve.Name == this.Name) return true;
            return false;
        }

        public string GetAllLinks()
        {
            string allLinks = ": ";

            foreach(string link in Links)
            {
                allLinks += link + ", ";
            }
            return allLinks;
        }
    }
}