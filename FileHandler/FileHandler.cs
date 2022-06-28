using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace wpf1
{
    public class FileHandler
    {
        public FileHandler()
        {

        }   

        public bool WriteToFile(List<CVE> cves, string software)
        {
            if(cves.Count == 0) return false;
            int lineswritten = 0;
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if(path != null)
            {
                path += Path.DirectorySeparatorChar;
                path += software;
            }
            else
            {
                Console.WriteLine("Encountered Error while determening current working directory");
                return false;
            } 
            
            

            try
            {
                
                if(File.Exists(path))
                {
                    string lastCVEName;
                    using (StreamReader filereader = new StreamReader(path))
                    {
                        string[] lines = filereader.ReadToEnd().Split(Environment.NewLine);
                        string[] lastCVEName_Links = lines[lines.Length-2].Split(':');
                        lastCVEName = lastCVEName_Links[0];
                        filereader.Close();
                    }

                    int lastFileIndex = 0;
                    foreach(CVE cve in cves)
                    {
                        if(cve.Name == lastCVEName)
                        {
                            break;
                        }
                        else
                        {
                            lastFileIndex++;
                        }
                    }
                    using (FileStream fileappender = new FileStream(path,FileMode.Append,FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fileappender))
                        {
                            for(int i = lastFileIndex + 1; i < cves.Count;i++)
                            {
                                string line = cves[i].Name + cves[i].GetAllLinks();
                                sw.WriteLine(line);
                                lineswritten++;
                            }
                            sw.Flush();
                            sw.Close();
                        }
                        fileappender.Close();
                    }
                }
                else
                {
                    using(StreamWriter sw = new StreamWriter(path))
                    {
                        foreach(CVE cve in cves)
                        {
                            string line = cve.Name + cve.GetAllLinks();
                            sw.WriteLine(line);
                            lineswritten++;
                        }
                        sw.Flush();
                        sw.Close();
                    }
                }
                Console.WriteLine("Successfully written "+ lineswritten + " new CVE(s) to File " + software);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Successfully written "+ lineswritten + " new CVE(s) to File " + software);
                Console.WriteLine("Encountered Error" + e.Message);
                return false;
            }
        }     
    }
}