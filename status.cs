using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml;




namespace ds
{
    public class TokenStatus
    {
        JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
        //funksioni i VerifikoTokenin i cili merr ne hyrje tokenin dhe permes celesit publik e verifikon 
        public bool VerifikoTokenin(string jwt, string publik, out string errorMessage)
        {
            string key = "";
            string exponent = "";

            XmlDocument document = new XmlDocument();
            document.Load("C://keys/" + publik + ".pub.xml");

            
            XmlNodeList list = document.GetElementsByTagName("Modulus");
            for (int i = 0; i < list.Count; i++)
            {
                key = list[i].InnerXml;
            }

            XmlNodeList lista = document.GetElementsByTagName("Exponent");
            for (int i = 0; i < list.Count; i++)
            {
                exponent = lista[i].InnerXml;
            }


            if (string.IsNullOrEmpty(jwt))
            {
                errorMessage = " Token eshte empty apo null .";
                return false;
            }

            var jwtArray = jwt.Split('.');
            if (string.IsNullOrEmpty(key))
            {
                errorMessage = " Public Key eshte empty ose jovalid .";
                return false;
            }

            if (string.IsNullOrEmpty(exponent))
            {
                errorMessage =  " Public Key eshte empty ose jovalid .";
                return false;
            }

            try
            {
                string publicKey =
                    (key.Length % 4 == 0 ? key : key + "====".Substring(key.Length % 4))
                    .Replace("_", "/").Replace("-", "+");
                var publicKeyBytes = Convert.FromBase64String(publicKey);

                var jwtSignatureFixed =
                    (jwtArray[2].Length % 4 == 0 ? jwtArray[2] : jwtArray[2] + "====".Substring(jwtArray[2].Length % 4))
                    .Replace("_", "/").Replace("-", "+");
                var jwtSignatureBytes = Convert.FromBase64String(jwtSignatureFixed);

                RSACryptoServiceProvider ObjRSA = new RSACryptoServiceProvider();
                ObjRSA.ImportParameters(
                    new RSAParameters()
                    {
                        Modulus = publicKeyBytes,
                        Exponent = Convert.FromBase64String(exponent)
                    }
                );

                SHA256 sha256 = SHA256.Create();
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(jwtArray[0] + '.' + jwtArray[1]));

                RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(ObjRSA);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                if (!rsaDeformatter.VerifySignature(hash, jwtSignatureBytes))
                {
                    errorMessage = "Ka deshtuar verfikimi .";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error verifying JWT token signature: " + ex.Message;
                return false;
                
            }

            errorMessage = string.Empty;
            return true;
        }
        //Funksioni status i cili merr tokenin ne hyrje
        public bool status(string jwt)
        {
            JwtSecurityToken token = Handler.ReadJwtToken(jwt);

            IEnumerable<Claim> User = token.Claims;
            String name = User.FirstOrDefault(user => user.Type.ToString().Equals("name")).Value;
            DateTimeOffset datetime = Exp(jwt);

            Console.WriteLine("User: " + name);
            Console.WriteLine("Skadimi: " + datetime.LocalDateTime);
            if (datetime < DateTimeOffset.Now)
            {
                return false;
            }

            if (!File.Exists("C://keys//" + name + ".pub.xml"))
            {
                return false;
            }

            if (!VerifikoTokenin(jwt, name, out string error))
            {
                Console.WriteLine(error);
                return false;
            }

            return true;
        }
        //Funksioni statusi i cili merr tokenin ne hyrje dhe permes keti funksioni e marrim derguesin te komanda read-message
        public string statusi(string jwt)
        {
            JwtSecurityToken token = Handler.ReadJwtToken(jwt);

            IEnumerable<Claim> User = token.Claims;
            String name = User.FirstOrDefault(user => user.Type.ToString().Equals("name")).Value;
            DateTimeOffset datetime = Exp(jwt);


            if (datetime < DateTimeOffset.Now)
            {
                return "Token-i ka skaduar ! ";
            }

            if (!File.Exists("C://keys//" + name + ".pub.xml"))
            {
                return "Mungon celesi publik " + name;
            }

            if (!VerifikoTokenin(jwt, name, out string error))
            {
                Console.WriteLine(error);
                return "Verifikimi ka deshtuar !";
            }

            return name;
        }

        //Percaktimi i kohes 
        public DateTimeOffset Exp(string tokenString)
        {
            var token = Handler.ReadJwtToken(tokenString);

            var Koha = token.Payload.First().Value;
            DateTimeOffset timeoffset = DateTimeOffset.FromUnixTimeSeconds(Koha.GetHashCode());
            return timeoffset;
        }
    }
} 
       
