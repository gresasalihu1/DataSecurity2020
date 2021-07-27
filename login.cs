using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ds
{
    public class login
    {

        static RSACryptoServiceProvider objRSA = new RSACryptoServiceProvider();
        public void Login(string username)
        {
            string query = "Select * FROM users WHERE name =" + "'" + username + "'";
            string pass = "";
            DataSet ds;
            ds = Connection.DataSet(query);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Console.Write("Jepni fjalekalimin:");
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        pass += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            pass = pass.Substring(0, (pass.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                string dbPassword = ds.Tables[0].Rows[0]["password"].ToString();
                string dbSalt = ds.Tables[0].Rows[0]["saltt"].ToString();



                HashAlgorithm algorithm = new SHA256Managed();
                byte[] ssalt = System.Text.Encoding.UTF8.GetBytes(pass + dbSalt);
                byte[] hash = algorithm.ComputeHash(ssalt);
                string SaltedHashPassword = Convert.ToBase64String(hash);

                if (dbPassword.Equals(SaltedHashPassword))
                {
                    var payloadOBJ = new JwtPayload
            {
                {"exp: ", DateTimeOffset.UtcNow.AddMinutes(20).ToUnixTimeSeconds()},
                {"name", username}
            };

                    string payload = JsonConvert.SerializeObject(payloadOBJ);
                    Console.WriteLine("\nToken: " + SignToken(payload, username));

                }
                else
                    Console.WriteLine("\nGabim: Shfrytezuesi ose fjalekalimi i gabuar.");
            }
            else
            {
                Console.WriteLine("\nGabim: Shfrytezuesi nuk ekziston!");
            }
        }
        
        //Base64UrlEncode
          private static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0];
            output = output.Replace('+', '-');
            output = output.Replace('/', '_');
            return output;
        }

        
         private string SignToken(string payload, string name)
        {
            List<string> pjeset = new List<string>();

            var header = new { alg = "RS256", typ = "JWT" };

            byte[] headerB = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            byte[] payloadB = Encoding.UTF8.GetBytes(payload);

            pjeset.Add(Base64UrlEncode(headerB));
            pjeset.Add(Base64UrlEncode(payloadB));

            string str = string.Join(".", pjeset.ToArray());

            byte[] bytesToSign = Encoding.UTF8.GetBytes(str);
            string path = "C://keys//" + name + ".xml";
            StreamReader reader = new StreamReader(path);
            string parametrat = reader.ReadToEnd();
            objRSA.FromXmlString(parametrat);
            byte[] key = objRSA.ExportRSAPrivateKey();

            var privat = Asn1Object.FromByteArray(key);
            var privatS = RsaPrivateKeyStructure.GetInstance((Asn1Sequence)privat);

            ISigner nenshkrimi = SignerUtilities.GetSigner("SHA256withRSA");

            nenshkrimi.Init(true, new RsaKeyParameters(true, privatS.Modulus, privatS.PrivateExponent));

            nenshkrimi.BlockUpdate(bytesToSign, 0, bytesToSign.Length);
            byte[] gensignature = nenshkrimi.GenerateSignature();

            pjeset.Add(Base64UrlEncode(gensignature));
            return string.Join(".", pjeset.ToArray());
        }


    }

}
