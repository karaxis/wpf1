using System.Data;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;
using System.Net;
using HtmlAgilityPack;
namespace wpf1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<CVE> cves = new List<CVE>();
            string software = null;
            string version = null;
            if(args.Length == 0)
            {
                Console.WriteLine("Usage: <exact_software_name> <version>");
            }
            else
            {
                Console.WriteLine("Welcome to the CVE Webscraper");
                Console.WriteLine("Starting to scrape...");
                software = args[0];
                if(version is null)
                {
                    HttpClient client = new HttpClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                    client.DefaultRequestHeaders.Accept.Clear();
                    software.Replace('_',' ');
                    Console.WriteLine("Awaiting response from cve.mitre.org");
                    var response = await client.GetStringAsync("https://cve.mitre.org/cgi-bin/cvekey.cgi?keyword="+software); 
                    MitreParser mp = new MitreParser(cves); 
                    if(mp.Parse(response))Console.WriteLine("Parsed cve.mitre.org successfully"); 
                    else Console.WriteLine("Parse failed");
                    Console.WriteLine(cves.Count + "CVEs found for " + software);
                    cves.Reverse();
                    FileHandler fh = new FileHandler();
                    if(fh.WriteToFile(cves, software))Console.WriteLine("Successfully written all new CVEs file "+software);              
                }
                else
                {
                  //TODO Version scraping  
                }
            }
        }

    }
}


