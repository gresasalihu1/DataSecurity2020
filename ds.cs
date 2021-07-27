using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace ds
{
    class Program
    {
        static DESCryptoServiceProvider DESalg = new DESCryptoServiceProvider();
        static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        static void Main(string[] args)
        {
            //gjenerimi i celesit 8byte  me ane te perdorimit RNGCryptoServiceProvider()
         RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            byte[] key = new byte[8];
            rngCsp.GetBytes(key);
            
            byte[] encryptedData;
            byte[] decryptedData;
            //nese nuk shenohet argumenti i pare
            if (args.Length == 0)
            {
                Console.WriteLine("Shenoni njeren nga komandat :create-user,delete-user,export-key,import-key,login,status,beale,playfair,write-message,read-message,morse-code");
                Environment.Exit(1);
            }
            //validimi i argumentit te pare
            string argument = args[0].ToLower();
            TokenStatus A = new TokenStatus();
            
            //komanda Beale
            if (argument == "beale")
            {
                  if (args.Length == 4)
                {
                     Beale beale = new Beale();

                     if (args[1].Equals("e"))
                   {
                         string plaintext = args[3];
                         string libri = args[2];
                         //plaintext duhet te permbaj vetem shkronja
                         if (Regex.Matches(plaintext, @"[a-zA-Z]").Count != 0)
                         {
                            Console.WriteLine(Beale.Enkriptimi(libri, plaintext));
                         }
                         else
                         {
                              Console.WriteLine("Keni dhene komanda jo valide.");
                              Environment.Exit(1);
                         }
                         
                    }
                   else if (args[1].Equals("d"))
                   {
                   var text = args[2];
                   string decrypt = args[3];
                       //ciphertext duhet te permbaj vetem numra
                   if (Regex.Matches(decrypt, @"[0-9]").Count != 0)
                   {
                        Beale.Dekriptimi(decrypt, text);
                   }
                   else
                   {
                       Console.WriteLine("Keni dhene komanda jo valide.");
                   }
                 }
               }
                else 
                   {
                    Console.WriteLine("Ju lutem shenoni 4 argumente:args[0]-Beale,args[1]-e/d,args[2]-libri.txt,args[3]-plaintext/ciphertext");
                    Environment.Exit(1);
                   }
            }
            
          //komanda playfair
            else if (argument == "playfair")
            {
              Playfair playfair = new Playfair();
              if (args.Length == 4)
              {
                if (args[1].Equals("e"))
                {
                    string plainText = args[3];
                    Playfair.initPlainText(plainText);
                    string qelesi = args[2];
                    //plaintext dhe celsi duhet te permbajn vetem shkronja
                    if ((Regex.Matches(plainText, @"[a-zA-Z]").Count != 0) && (Regex.Matches(qelesi, @"[a-zA-Z]").Count != 0))
                  {
                    Playfair.Enkriptimi(Playfair.Krijotabelen(qelesi), Playfair.Krijoplaintekstiin());
                  } 
                   else
                   {
                      Console.WriteLine("Keni dhene komanda jo valide.");
                   }
                }
                    else if (args[1].Equals("d"))
                {
                    string cipherText = args[3];
                    Playfair.initPlainText(cipherText);
                    string qelesi = args[2];
              //ciphertext dhe celesi duhet te permbajn vetem shkronja
              if ((Regex.Matches(cipherText, @"[a-zA-Z]").Count != 0) && (Regex.Matches(qelesi, @"[a-zA-Z]").Count != 0))
              {
              Playfair.Dekriptimi(Playfair.Krijotabelen(qelesi), Playfair.Krijoplaintekstiin());
              }
              else
              {
                  Console.WriteLine("Keni dhene komanda jo valide.");
                  Environment.Exit(1);
              }
           }             
                Console.ReadKey();
              }
                else
                {
                    Console.WriteLine("Ju lutem shenoni 4 argumente:args[0]-Playfair,args[1]-e/d,args[2]-qelesi,args[3]-plaintext/ciphertext");
                    Environment.Exit(1);
                }
            }
            //komanda morse-code
            else if (argument == "morse-code")
            {
               if (args.Length == 3)
              {    
               Translator morse = new Translator();

                string input = args[2];
                string encode_decode = args[1];
                Code m = new Code(input);
                // Metoda audio() perkrahet vetem ne Windows
                if (encode_decode == "audio")
                {
                    m.Audio(input);
                }
                else
                {
                    Console.WriteLine("{1}", m.GetInput(), m.Perkthe(encode_decode));
            
                }
              }else
              {
                   Console.WriteLine("Ju lutem shenoni 3 argumente:args[0]-morse-code,args[1]-encode/decode,args[2]-tekst/numra/shenja/./-");
                   Environment.Exit(1);
              }
            }
            //komanda export-key
           else if (argument == "export-key")
            {
               
                exportkey exportt = new exportkey();
                //nese vendoset edhe file se ku do te ruhet key
                if (args.Length==4)
                {
                    string KeyName1 = args[2];
                    string PP = args[1];
                    string exportfolder = args[3];
                    exportkey.Eksporti(KeyName1, PP, exportfolder);
                   
                }
               //perndryshe shfaqe permbajtjen e celesit ne console
                else if(args.Length==3)
                {
                    string KeyName1 = args[2];
                    string PP = args[1];
                    exportkey.Ek(KeyName1, PP);

                }
                else
                {
                    Console.WriteLine("Shenoni: export-key {hapesire} user {hapesire} public/private {hapesire} exportfolder apo export-key {hapesire} user {hapesire} public/private");
                    Environment.Exit(1);
                }
            }
            //komanda import-key
             else if (argument == "import-key")
            {
                if (args.Length == 3)
                {
                    import importk = new import();
                    {
                        string Keyname = args[1];
                        string shtegu = args[2];
                        //emri duhet te permbaj vetem shkronja edhe _.
                        if (Regex.IsMatch(Keyname, "^[a-zA-Z0-9_.]*$"))
                        {
                            import.Import(Keyname, shtegu);
                        }
                        else
                        {
                            Console.WriteLine("Keni dhene komanda jo valide");
                            Environment.Exit(1);

                        }

                    }
                }
                else
                {
                  Console.WriteLine("Shenoni :import-key {hapesire} user {hapesire} shtegu ");
                  Environment.Exit(1);
                }

            }
            //komanda write-message
              else if (argument == "write-message")
            {
                //nese vendosen vetem argumentet name dhe mesazhi
                if (args.Length == 3)
                {

                    string KeyName = args[1];
                    string KeyPath = "C://keys";
                    string publik = String.Concat(KeyPath, "//", KeyName, ".pub", ".xml");
                    if (File.Exists(publik))
                    {
                        byte[] Name = Encoding.UTF8.GetBytes(KeyName);
                        string part1 = Convert.ToBase64String(Name);
                        DESalg.GenerateIV();
                        string part2 = Convert.ToBase64String(DESalg.IV);


                        byte[] Data = read_write.EnkriptimiIMesazhit(args[2], key, DESalg.IV);
                        string mesazhi = Convert.ToBase64String(Data);

                        encryptedData = read_write.RSAEncrypt(key, publik);
                        string qelsi = Convert.ToBase64String(encryptedData);

                        try
                        {

                            Console.WriteLine(part1 + "." + part2 + "." + qelsi + "." + mesazhi);
                        }
                        catch
                        {


                            Console.WriteLine("Gabim: Celesi publik " + String.Concat(publik) + " nuk ekziston ");
                            Environment.Exit(1);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Gabim:Keni dhene komanda jo valide.");
                    }
                }
                 
                else if (args.Length == 4)
                {
                    //nese si args[3] merret nje file ne te cilin duam ta shkruajm permbajtjen e mesazhit
                    if (args[3].Contains(".txt") || args[3].Contains(".xml"))
                    {
                        try
                        {
                            string KeyName = args[1];
                            byte[] Name = Encoding.UTF8.GetBytes(KeyName);
                            string part1 = Convert.ToBase64String(Name);
                            DESalg.GenerateIV();
                            string part2 = Convert.ToBase64String(DESalg.IV);
                            string KeyPath = "C://keys";

                            byte[] Data = read_write.EnkriptimiIMesazhit(args[2], key, DESalg.IV);
                            string mesazhi = Convert.ToBase64String(Data);

                            string publik = String.Concat(KeyPath, "//", KeyName, ".pub", ".xml");
                            encryptedData = read_write.RSAEncrypt(key, publik);
                            string qelsi = Convert.ToBase64String(encryptedData);
                            if (File.Exists(publik))
                            {
                                string ciphertexti = part1 + "." + part2 + "." + qelsi + "." + mesazhi;
                                StreamWriter sw = new StreamWriter(args[3]);
                                sw.Write(ciphertexti);
                                sw.Close();
                                Console.WriteLine("Mesazhi i enkriptuar u ruajt ne fajllin " + args[3]);
                            }
                            else
                            {
                                Console.WriteLine("Komanda jo valide");
                            }
                        }
                        catch (CryptographicException e)
                        {
                            Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                            Environment.Exit(1);
                        }
                    }
                    //perndryshe nese eshte shenuar nje token komanda write-message do ta kryej kete funksion
                    else
                    {
                        try
                        {
                            string KeyName = args[1];
                            byte[] Name = Encoding.UTF8.GetBytes(KeyName);
                            string part1 = Convert.ToBase64String(Name);
                            DESalg.GenerateIV();
                            string part2 = Convert.ToBase64String(DESalg.IV);
                            string KeyPath = "C://keys";

                            byte[] Data = read_write.EnkriptimiIMesazhit(args[2], key, DESalg.IV);
                            string mesazhi = Convert.ToBase64String(Data);

                            string publik = String.Concat(KeyPath, "//", KeyName, ".pub", ".xml");
                            string statusi = A.statusi(args[3]);
                            encryptedData = read_write.RSAEncrypt(key, publik);
                            string qelsi = Convert.ToBase64String(encryptedData);
                            read_write obj = new read_write();
                            ;
                            if (!File.Exists(publik))
                            {
                                Console.WriteLine("Gabim: Celesi publik " + String.Concat(args[1]) + " nuk ekziston ");
                            }
                            else {
                                
                                string privat = String.Concat(KeyPath, "//", statusi, ".xml");

                             //nese tokeni eshte valid 
                            if ((A.Exp(args[3]) > DateTimeOffset.Now) && (A.VerifikoTokenin(args[3], A.statusi(args[3]), out string errorMessage)))
                                {
                                    byte[] derguesi = Encoding.UTF8.GetBytes(statusi);
                                    string part4 = Convert.ToBase64String(derguesi);
                                    byte[] signedData = read_write.Nenshkrimi(Data, privat);
                                    string part5 = Convert.ToBase64String(signedData);

                                    Console.WriteLine(part1 + "." + part2 + "." + qelsi + "." + mesazhi + "." + part4 + "." + part5);
                                }

                                else
                                {
                                    Console.WriteLine("Komanda ka deshtuar sepse tokeni nuk eshte valid !");
                                    Environment.Exit(1);
                                }
                            }
                            
                        }
                        catch
                        {
                            Console.WriteLine("Gabim:keni dhene komanda jo valide . ");
                            Environment.Exit(1);
                        }
                    }
                }
                  //nese shenojm name,mesazhin,tokenin dhe file ku duam ta ruajm mesazhin
                else if (args.Length == 5)
                {
                    
                    string KeyName = args[1];
                    byte[] Name = Encoding.UTF8.GetBytes(KeyName);
                    string part1 = Convert.ToBase64String(Name);
                    DESalg.GenerateIV();
                    string part2 = Convert.ToBase64String(DESalg.IV);
                    string KeyPath = "C://keys";

                    byte[] Data = read_write.EnkriptimiIMesazhit(args[2], key, DESalg.IV);
                    string mesazhi = Convert.ToBase64String(Data);

                    string statusi = A.statusi(args[3]);
                    string privat = String.Concat(KeyPath, "//", statusi, ".xml");
                    try
                    {
                        DESalg.GenerateIV();
                       
                        //nese tokeni eshte valid
                        if ((A.Exp(args[3]) > DateTimeOffset.Now) && (A.VerifikoTokenin(args[3], statusi, out string errorMessage)))
                        {
                            string publik = String.Concat(KeyPath, "//", KeyName, ".pub", ".xml");
                            encryptedData = read_write.RSAEncrypt(key, publik);
                            string qelsi = Convert.ToBase64String(encryptedData);
                            byte[] derguesi = Encoding.UTF8.GetBytes(statusi);
                            string part4 = Convert.ToBase64String(derguesi);
                            byte[] signedData = read_write.Nenshkrimi(Data, privat);
                            string part5 = Convert.ToBase64String(signedData);
                            string ciphertexti = part1 + "." + part2 + "." + qelsi + "." + mesazhi + "." + part4 + "." + part5;
                            StreamWriter sw = new StreamWriter(args[4]);
                            sw.Write(ciphertexti);
                            sw.Close();
                        }
                        Console.WriteLine("Mesazhi i enkriptuar u ruajt ne fajllin " + args[4]);
                    }
                    catch (CryptographicException e)
                    {
                        Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);

                    }
                }
                else
                {
                    Console.WriteLine("Shenoni : write-message {hapesire} marresi {hapesire} mesazhi apo write-message {hapesire} marresi {hapesire} mesazhi {hapesire} token ");
                    Environment.Exit(1);
                }
            }
            //komanda read-message
            else if (argument == "read-message")
            {
               //nese vendosim si argument  name dhe mesazhin
                if (args.Length == 2)
                {


                    read_write obj = new read_write();
                    bool fileExist = File.Exists(args[1]);
                    if (fileExist)
                    {
                        string strXmlParameters = "";
                        StreamReader sr = new StreamReader(args[1]);
                        strXmlParameters = sr.ReadToEnd();
                        sr.Close();
                        String[] hyrja = strXmlParameters.Split('.');
                        try
                        {
                           //Kqë nëse figuron pjesa e dërguesit/nënshkrimit në mesazh,
                           //atëherë do të tentohet verifikimi i atij nënshkrimi duke përdorur çelësin publik të dërguesit. 
                            if (hyrja.Length == 6)
                            {
                                string KeyPath = "C://keys";
                                string privati = String.Concat(KeyPath, "\\", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])), ".xml");
                                decryptedData = read_write.RSADecrypt(Convert.FromBase64String(hyrja[2]), privati);
                                string publiku = String.Concat(KeyPath, "\\", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])), ".pub", ".xml");
                                if (read_write.VerifikimiiNenshkrimit(Convert.FromBase64String(hyrja[3]), Convert.FromBase64String(hyrja[5]), publiku) == true)
                                {
                                    Console.WriteLine("Marresi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])));
                                    Console.WriteLine("Mesazhi: " + read_write.DekriptimiIMesazhit(Convert.FromBase64String(hyrja[3]), decryptedData, Convert.FromBase64String(hyrja[1])));
                                    Console.WriteLine("Derguesi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])));
                                    Console.WriteLine("Nenshkrimi: " + "Valid");
                                }
                                else
                                {
                                    Console.WriteLine("Marresi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])));
                                    Console.WriteLine("Mesazhi: " + read_write.DekriptimiIMesazhit(Convert.FromBase64String(hyrja[3]), decryptedData, Convert.FromBase64String(hyrja[1])));
                                    Console.WriteLine("Derguesi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])));
                                    Console.WriteLine("Nenshkrimi: " + "Mungon celesi publik " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])));
                                }
                            }
                            else
                            {
                                //Nëse mungon pjesa e dërguesit/nënshkrimit, atëherë komanda e injoron dhe vepron sikur në fazën e
                                //dytë. 
                                string strXmlParameeters = "";
                                StreamReader ssr = new StreamReader(args[1]);
                                strXmlParameeters = ssr.ReadToEnd();
                                ssr.Close();
                                String[] hyrjja = strXmlParameeters.Split('.');
                                try
                                {

                                    Console.WriteLine("Marresi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrjja[0])));
                                    string privati = String.Concat("C:\\keys", "\\", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrjja[0])), ".xml");
                                    decryptedData = read_write.RSADecrypt(Convert.FromBase64String(hyrjja[2]), privati);
                                    Console.WriteLine("Mesazhi: " + read_write.DekriptimiIMesazhit(Convert.FromBase64String(hyrjja[3]), decryptedData, Convert.FromBase64String(hyrjja[1])));
                                }
                                catch
                                {
                                    Console.WriteLine("Gabim: Celesi privat " + String.Concat("keys/", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrjja[0])), ".xml") + " nuk ekziston ");
                                    Environment.Exit(1);
                                }

                            }
                        }
                        catch
                        {
                            Console.WriteLine("Gabim: Celesi privat " + String.Concat("keys/", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])), ".xml") + " nuk ekziston ");
                        }

                    }

                  
                    else
                    {
                        String[] hyrja = args[1].Split('.');
                        try
                        {

                            string KeyPath = "C:\\keys";
                            string privati = String.Concat(KeyPath, "\\", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])), ".xml");
                            decryptedData = read_write.RSADecrypt(Convert.FromBase64String(hyrja[2]), privati);

                            if (hyrja.Length == 6)
                            {
                                string publiku = String.Concat(KeyPath, "\\", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])), ".pub", ".xml");
                                if (read_write.VerifikimiiNenshkrimit(Convert.FromBase64String(hyrja[3]), Convert.FromBase64String(hyrja[5]), publiku) == true)
                                {
                                    Console.WriteLine("Marresi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])));
                                    Console.WriteLine("Mesazhi: " + read_write.DekriptimiIMesazhit(Convert.FromBase64String(hyrja[3]), decryptedData, Convert.FromBase64String(hyrja[1])));
                                    Console.WriteLine("Derguesi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])));
                                    Console.WriteLine("Nenshkrimi: " + "Valid");
                                }
                                else
                                {
                                    Console.WriteLine("Marresi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])));
                                    Console.WriteLine("Mesazhi: " + read_write.DekriptimiIMesazhit(Convert.FromBase64String(hyrja[3]), decryptedData, Convert.FromBase64String(hyrja[1])));
                                    Console.WriteLine("Derguesi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])));
                                    Console.WriteLine("Nenshkrimi: " + "Mungon celesi publik " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[4])));
                                }
                            }
                            else
                            {
                                try
                                {

                                    Console.WriteLine("Marresi: " + new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])));

                                    decryptedData = read_write.RSADecrypt(Convert.FromBase64String(hyrja[2]), privati);
                                    Console.WriteLine("Mesazhi: " + read_write.DekriptimiIMesazhit(Convert.FromBase64String(hyrja[3]), decryptedData, Convert.FromBase64String(hyrja[1])));
                                }
                                catch
                                {
                                    Console.WriteLine("Gabim: Celesi privat " + String.Concat("keys/", new ASCIIEncoding().GetString(Convert.FromBase64String(hyrja[0])), ".xml") + " nuk ekziston ");
                                    Environment.Exit(1);
                                }
                            }


                        }
                        catch
                        {
                            Console.WriteLine("Gabim:Keni dhene komanda jovalide");
                        }
                    }

                }

                else if (args.Length != 2)
                {
                    Console.WriteLine("Sheno: read-message {hapesire} file/mesazh");
                }
            }
            //komanda create-user
             else if (argument == "create-user")
            {
                if (args.Length == 2)
                {
                    Createuser createe = new Createuser();

                    {
                        string KeyNName = args[1];
                        if (Regex.IsMatch(KeyNName, "^[a-zA-Z0-9_]*$"))
                        {
                            Createuser.Krijo(KeyNName);
                        }
                        else
                        {
                            Console.WriteLine("Keni dhene komanda jo valide");
                            Environment.Exit(1);
                        }
                    }
                }
                
                else
                {
                    Console.WriteLine("Shenoni : create-user {hapesire} KeyName");
                    Environment.Exit(1);
                }

            }
            //komanda delete-user
             else if (argument == "delete-user")
            {
                if (args.Length == 2)
                {
                    Deleteuser.Largo(args[1]);
                }
                else
                {
                    Console.WriteLine("Shenoni: delete-user {hapesire} user");
                    Environment.Exit(1);
                }
            }
            //komanda login
            else if (argument == "login")
            {
                if (args.Length == 2)
                {
                    try
                    {
                        login loginn = new login();
                        string username = args[1];
                        loginn.Login(username);
                    }

                    catch (Exception error)
                    {
                        throw new Exception(error.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Shenoni:create-user {hapesire} user");
                    Environment.Exit(1);
                }
            }
            //komanda status
            else if (argument == "status")
            {
                if (args.Length == 2)
                {
                    bool status = A.status(args[1]);

                    if (status)
                    {
                        Console.WriteLine("Valid: po");
                    }
                    else if (!status)
                    {
                        Console.WriteLine("Valid: jo");
                    }
                }
                else
                {
                    Console.WriteLine("Shenoni status {hapesire} token");
                }
            }
            else
            {
               Console.WriteLine("Shenoni njeren nga komandat :create-user,delete-user,export-key,import-key,login,status,beale,playfair,write-message,read-message,morse-code");
               Environment.Exit(1);
            
            }
        }
    }
}   
