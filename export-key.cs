using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
namespace ds
{
    class exportkey
    {  //krjimi i nje funksioni qe shperben per krijimin e celesave,caktimin se a eshte publik apo privat dhe exportfolder
        public static void Eksporti(string KeyName, string PP, string exportfolder)
        {
       
            if (PP == "public")
            {
                 string pub = "C:\\keys\\" + KeyName + ".pub.xml";
                if (File.Exists(pub))
                {
                    using (StreamReader reader = new StreamReader(pub))
                    {//Lexojm të gjithë karakteret nga pozicioni aktual deri në fund të rrjedhës. 
                        string html = reader.ReadToEnd();

                        RSACryptoServiceProvider objRSAa = new RSACryptoServiceProvider();
                            //Perdorimi i StreamWriter per shkrim ne Fajllin e caktuar perkatesisht ne fajllin e krijuar 
                       using (StreamWriter sp = new StreamWriter(Path.Combine(exportfolder)))
                        {
                           RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();
                            rsaKey.FromXmlString(html);
                            sp.Write(rsaKey.ToXmlString(false));
                         }
                        Console.WriteLine("Celesi publik u ruajt ne fajllin " + exportfolder + ".");
                    }
                }
                else
                {
                    Console.WriteLine("Gabim: Celesi public " + KeyName + " nuk ekziston.");

                }
               
            }
         if (PP == "private")
            {
                    string priv = "C:\\keys\\" + KeyName + ".xml";
                    if (File.Exists(priv))
                    {
                    using (StreamReader reader = new StreamReader(priv))
                    {
                        string html = reader.ReadToEnd();
                        RSACryptoServiceProvider objRSAa = new RSACryptoServiceProvider();
                        
                        using (StreamWriter sp = new StreamWriter(Path.Combine(exportfolder)))
                        {
                            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();
                            rsaKey.FromXmlString(html);
                            sp.Write(rsaKey.ToXmlString(true));
                            Console.WriteLine("Celesi privat u ruajt ne fajllin " + exportfolder + ".");
                        }                    
                    }

                    }

                    else
                    {
                        Console.WriteLine("Gabim: Celesi privat " + KeyName + " nuk ekziston.");
                    }
            }
        }
        //krjimi i nje funksioni qe shperben per krijimin e celesave,caktimin se a eshte publik apo privat dhe shfaqjen e tyre ne console
        
         public static void Ek(string KeyName, string PP)
        {
            if (PP == "public")
            {
                string pub = "C:\\keys\\" + KeyName + ".pub.xml";
                if (File.Exists(pub))
                {
                    using (StreamReader reader = new StreamReader(pub))
                    {
                        string html = reader.ReadToEnd();
                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                        rsa.FromXmlString(html);
                        Console.Write(rsa.ToXmlString(false));
                    }
                }
                else
                {
                    Console.WriteLine("Gabim: Celesi public " + KeyName + " nuk ekziston.");
                }  
            }
             if (PP == "private")
            {
                
                    string priv = "C:\\keys\\" + KeyName + ".xml";
                // Ndonjëherë mund ta kemi vetëm çelësin publik të një shfrytëzuesi, prandaj nëse e kërkojmë çelësin
                //privat do të shfaqet një mesazh gabimi.
                if (File.Exists(priv))
                {
                   
                    using (StreamReader reader = new StreamReader(priv))
                    {
                        string html = reader.ReadToEnd();
                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                        rsa.FromXmlString(html);
                        Console.Write(rsa.ToXmlString(true));
                    }
                }
                else
                {
                    Console.WriteLine("Gabim: Celesi privat " + KeyName + " nuk ekziston.");
                }
       
                }
            }
        }

    }
       
       
