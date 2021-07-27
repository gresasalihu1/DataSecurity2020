using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;


namespace ds
{
    
    class Deleteuser
    {
      //krjimi i funksionit Largo qe sherben per me i fshi celesat
      public static void Largo(string KeyName)
      {
              
               //path ku ruhet qelesi
                string KeyPath = "C://keys";
                string publik = String.Concat(KeyPath, "\\", KeyName, ".pub", ".xml");
                string privat = String.Concat(KeyPath, "\\", KeyName, ".xml");
                //funksioni qe sherben per te shiquar se a ekziston paraprakisht celesi
                bool DoesKeyExist(string name)
                {
                   //krijimi i CspParameters
                    var cspParams = new CspParameters
                    {

                        Flags = CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore,


                        KeyContainerName = name
                    };
                
                    try
                    {
                        //krijimi i nje instance te re te RSACryptoServiceProvider() qe merr si paramater cspParams qe i krijuam 
                        //me pare
                        var rsa = new RSACryptoServiceProvider(cspParams);



                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    return true;

                }
                //nese ekziston ai celes
                if (DoesKeyExist(KeyName))
                {
                  Connection C = new Connection();
                  String query = "DELETE FROM users WHERE name=" + "'" + KeyName + "';";
                   MySqlDataReader row;
                   row = Connection.databaza(query);
                   Console.WriteLine("Eshte larguar shfrytezuesi " + KeyName);

                    //nese ekzistojn Path te dhene
                    if (File.Exists(Path.Combine(KeyPath, privat)) && File.Exists(Path.Combine(KeyPath, publik)))
                    {
                        var cp = new CspParameters
                        {
                            KeyContainerName = KeyName,
                            Flags = CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore,
                        };


                       //Krijimi i instances rsa
                        var rsa = new RSACryptoServiceProvider(cp)
                        {
                            //fshij permbajtjen e atij celesi
                            PersistKeyInCsp = false
                        };
                        rsa.Clear();
                        //fshij edhe file qe e permbajn celesin me emrin e dhene
                        File.Delete(Path.Combine(KeyPath, privat));
                        File.Delete(Path.Combine(KeyPath, publik));
                        //paraqit mesazhet e caktuara
                        Console.WriteLine("Eshte larguar celesi privat " + String.Concat("keys/", KeyName, ".xml"));
                        Console.WriteLine("Eshte larguar celesi publik " + String.Concat("keys/", KeyName, ".pub", ".xml"));
                    }
                   
                    //nese ekziston vetem celesi publik
                   //largon çelësin publik të shfrytëzuesit.
                     else if (File.Exists(Path.Combine(KeyPath, publik)))
                     {
                        
                        //krijimi i CspParametrave
                        var cp = new CspParameters
                        {
                            KeyContainerName = KeyName,
                            Flags = CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore,
                        };


                        //Krijimi i instances rsa
                        var rsa = new RSACryptoServiceProvider(cp)
                        {
                           //ateher fshije permbajtjen e atij celesi 
                            PersistKeyInCsp = false
                        };
                        rsa.Clear();
                         //fshije edhe file qe e permban ate celes
                        File.Delete(Path.Combine(KeyPath, publik));
                        Console.WriteLine("Eshte larguar celesi publik " + String.Concat("keys/", KeyName, ".pub", ".xml"));

                      }

                }
                //nese celesi nuk ekziston paraqite mesazhin qe nuk ekziston celesi me ate emer
                 else if (!DoesKeyExist(KeyName))
                {
                    Console.WriteLine("Gabim: Shfrytezuesi " + KeyName + " nuk ekziston.");
                }
         }
    }
}
      
    
