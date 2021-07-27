using System;
using System.IO;
using System.Security.Cryptography;
using System.Net;

namespace ds
{
  public  class import
    {   //krjimi i nje funksioni qe shperben per krijimin e celesave dhe percaktimin e shtegut
        public static void Import(string Keyname,string shtegu)
        {
           //Krijimi i stringjeve per ruajtje te shtegut te celesit
            string privat = "C:\\keys\\" + Keyname + ".xml";
            string publik = "C:\\keys\\" + Keyname + ".pub.xml";
            var cs = new CspParameters() { };
            cs.Flags = CspProviderFlags.UseMachineKeyStore;
      //nese shtegu permban ".xml" kontorolloje nese ekziston ai qeles 
       if (shtegu.Contains(".xml"))
            {//metoda file.exist kontrollon nese ekziston shtegu paraprakisht
                if (File.Exists(shtegu))
                {   //stringu l per lexim te permbajtjes se atij shtegu
                    string l = File.ReadAllText(shtegu);
                  //nese ekzistojn edhe file publik dhe ai privat
                    if (File.Exists(publik) && File.Exists(privat))
                    {
                      Console.WriteLine("Gabim: Celesi " + Keyname + " ekziston paraprakisht.");

                    }
                  //perndryshe
                    else
                    {
                      //Nëse çelësi që po importohet është privat, atëherë automatikisht do te gjenerohet edhe pjesa publike dhe do te ruhen të dyte në
                       //direktoriumin e çelësave.
                        if (l.Contains("<P>"))
                        {

                            using (StreamReader reader = new StreamReader(shtegu))
                            {
                              //variabla permbajtja qe sherben per lexim te shtegut
                                string permbajtja = reader.ReadToEnd();
                     //Perdorimi i StreamWriter per shkrim ne Fajllin e caktuar perkatesisht ne fajllin e krijuar (privat)
                                using (StreamWriter sw = new StreamWriter(privat))
                                {  
                                  //ne file privat shkruaj permbajtjen
                                    sw.Write(permbajtja);
                                    sw.Close();
                                    Console.WriteLine("Celesi privat u ruajt ne fajllin " + privat);
                                } //Perdorimi i StreamWriter per shkrim ne Fajllin e caktuar perkatesisht ne fajllin e krijuar (publik)
                              // Nëse çelësi që po importohet është privat, atëherë  automatikisht gjenerohet edhe pjesa publike 
                                   using (StreamWriter sp = new StreamWriter(publik))
                                {
                                    RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();
                                    //celesi publik
                                    rsaKey.FromXmlString(permbajtja);
                                    sp.Write(rsaKey.ToXmlString(false));
                                    Console.WriteLine("Celesi publik u ruajt ne fajllin " + publik);
                                }
                            }

                        }
                        else
                        {//Perndryshe nese permbajtja eshte vetme me Modulus dhe Exponent atehere ruhet vetem qelesi publik.
                            using (StreamReader reader = new StreamReader(shtegu))
                            {
                         //Perdorimi i StreamWriter per shkrim ne Fajllin e caktuar perkatesisht ne fajllin e krijuar (publik) dhe ne 
                             //shtegun e caktuar publik mbishkruhet permbajtja e ketij fajlli
                                using (StreamWriter sw = new StreamWriter(publik))
                                {
                                    string permbajtja = reader.ReadToEnd();
                                    sw.Write(permbajtja);
                                    sw.Close();
                                }
                            }

                            Console.WriteLine("Celesi publik u ruajt ne fajllin " + publik);
                        } 
                    }
                }
           //nese file nuk ekziston 
                else
                {
                    Console.WriteLine("Fajlli nuk ekziston");
                }
            }
           //nese shtegu permban hhtps:// apo https:// konrolloje nese celesi ekziston paraprakisht nese nuk ekziston atehere
          //do të dërgohet një GET request në Url <path> dhe do të merret trupi i përgjigjes si vlera e çelësit.
    else if ((shtegu.Contains("https://")) || (shtegu.Contains("http://")))
            {
                if (File.Exists(publik))
                {
                  
                    Console.WriteLine("Ky celes ekziston paraprakisht");

                }
                else
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(shtegu);
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string html = reader.ReadToEnd();
                            StreamWriter pu = new StreamWriter(publik);
                            pu.Write(html);
                            pu.Close();
                        }
                    }

                    Console.WriteLine("Celesi publik u ruajt ne fajllin " + publik);
                }
            }
           //Nese fajlli nuk eshte celes valid shfaq tekstin
        else if (!(File.Exists(shtegu)))
            {
                Console.Write("Gabim: Fajlli i dhene nuk eshte celes valid.");
            }
          
            else
            {
                Console.Write("Gabim: Fajlli i dhene nuk eshte celes valid.");
            }
        }
  }
}
