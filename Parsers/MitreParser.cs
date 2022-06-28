using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;


namespace wpf1
{
    public class MitreParser
    {
                List<CVE> CVEs;

                public MitreParser(List<CVE> cves)
                {

                    CVEs = cves;
                }

                public bool Parse(string response)
                {
                    if(response != null)
                    {
                        try
                        {
                            Console.WriteLine("Starting to parse response from cve.mitre.org");
                            HtmlDocument doc = new HtmlDocument();
                            doc.LoadHtml(response);
                            List<HtmlNode> divvulns = new List<HtmlNode>(doc.DocumentNode.Descendants("div").Where(node => node.Id.Equals("TableWithRules")));
                            var vulns = divvulns[0].Descendants("td").ToList();
                            foreach(var vulnerability in vulns)
                            {
                                string cveName =  vulnerability.FirstChild.InnerHtml;
                                if(cveName.StartsWith("CVE")) 
                                {
                                    CVE tempcve = new CVE(cveName);
                                    tempcve.AddLink("https://cve.mitre.org/cgi-bin/cvename.cgi?name="+cveName);
                                    Console.WriteLine("Found " + cveName);
                                    CVEs.Add(tempcve);
                                }
                            }
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }

                    return false;
                }

                
    }
}