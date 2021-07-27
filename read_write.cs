using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace ds
{
  public class read_write
   {
        static DESCryptoServiceProvider DESalg = new DESCryptoServiceProvider();
        static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
       
            
      //Enkriptimi i Mesazhit me DES me celsin e gjeneruar me ane te RNGCryptoServiceProvider dhe me IV e instances DES te gjenerar rastesisht
      public static byte[] EnkriptimiIMesazhit(string mesazhi, byte[] Key, byte[] IV)
      {
            try
            {
                DESalg.Mode = CipherMode.CBC;
                // Krijimi i nje  MemoryStream.
                MemoryStream mStream = new MemoryStream();

                // Krijimi i CryptoStream duke perdorur MemoryStream,
                // celesin  dhe  initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    new DESCryptoServiceProvider().CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                //Konverton stringun ne nje byte array.
                byte[] toEncrypt = new ASCIIEncoding().GetBytes(mesazhi);

                // Shkrimi i bytearray ne CryptoStream
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // Formimi i nje array me bytes
                // qe merr informata nga MemoryStream 
                // dhe me pas e mban pjesen e enkriptuar
                byte[] ret = mStream.ToArray();

                //Mbylli streams.
                cStream.Close();
                mStream.Close();

                // Kthen bufferin e enkriptuar (ret)
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
      }
     //Dekriptimi i Mesazhit me qelsin e dekriptuar nga objekti i RSA 
     public static string DekriptimiIMesazhit(byte[] Data, byte[] Key, byte[] IV)
     {
            try
            {
                // Krijo nje MemoryStream duke perdorur
                // te dhenat ne forme te bajtave (Data)
                DESalg.Mode = CipherMode.CBC;
                MemoryStream msDecrypt = new MemoryStream(Data);
                  //Krijimi i CryptoStream duke perdorur MemoryStream duke ja pasuar qelsin dhe IV perkates
                
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    new DESCryptoServiceProvider().CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Krijimi i bufferit qe qe te mbaje te dhenat e dekriptuara 
                byte[] fromEncrypt = new byte[Data.Length];

                // Leximi i te dhenave
                // dhe vendosja e tyre ne bufferin fromEncrypt
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convertimi i bajtave ne string dhe kthyerja e tyre
                return new ASCIIEncoding().GetString(fromEncrypt);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
       }
       //Enkriptimi i qelsit te DES-it me qelsin publik te merrsit
      public static byte[] RSAEncrypt(byte[] DataToEncrypt, string pathi)
      {
            try
            {
                byte[] encryptedData;
             //  Inicializon një objekt RSA nga informacioni kryesor .
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    string strXmlParameters = "";
                    StreamReader sr = new StreamReader(pathi);
                    strXmlParameters = sr.ReadToEnd();
                    sr.Close();
                    //Importon informacionet e celesit RSA.
                  //nese  nevojiten vetem informacionet e celesit publik
                   
                    RSA.FromXmlString(strXmlParameters);
                    //Encrypton te dhenat e pasuara 
                    encryptedData = RSA.Encrypt(DataToEncrypt, true);
                }
                return encryptedData;
            }
            //Gjen dhe shfaq ndonje gabim CryptographicException
            
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
      }
        //Dekriptimi i te dhenave perkatesisht i qelsit qe e marrim me ane te funskionit ConvertFromBase64String 
          //Duke perdorur qelsin privat te emrit qe e ka marr nga Consola. 
        public static byte[] RSADecrypt(byte[] DataToDecrypt, string pathi)
        {
            try
            {
                byte[] decryptedData;
                //Krijimi i instances se RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    string strXmlParameters = "";
                    StreamReader sr = new StreamReader(pathi);
                    strXmlParameters = sr.ReadToEnd();
                    sr.Close();

                    RSA.FromXmlString(strXmlParameters);
                    //Importo  RSA Key informacionet. Duhet
                    //te permbaj dhe informacione te private key.
                    // RSA.ImportParameters(RSAKeyInfo);

                    //Dekriptimi i te dhenave qe jane jepur ne forme te bajtave.  
                    
                    decryptedData = RSA.Decrypt(DataToDecrypt, true);
                }
                return decryptedData;
            }
            //Gjen dhe shfaq ndonje gabim CryptographicException
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    
     public static byte[] Nenshkrimi(byte[] DataToSign, string pathi)
     {
        try
        {
            // Perdorimi i instances RSACryptoServiceProvider
            // per perdorim te qelsave
            string strXmlParameters = "";
            StreamReader sr = new StreamReader(pathi);
            strXmlParameters = sr.ReadToEnd();
            sr.Close();
            //Importon informacionet e celesit RSA.
            //nese  nevojiten vetem informacionet e celesit publik
            RSA.FromXmlString(strXmlParameters);
            // Ben hash dhe i nenshkruan e dhenat.Perdorimi i SHA1CryptoServiceProvider
            // per te specifikuar perdorimin e SHA1 per gjetjen e hash-it.
            return RSA.SignData(DataToSign, new SHA1CryptoServiceProvider());
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return null;
        }
    }

    public static bool VerifikimiiNenshkrimit(byte[] DataToVerify, byte[] SignedData, string pathi)
    {
        try
        {
            // Perdorimi i instances RSACryptoServiceProvider
            // per perdorim te qelsave
            string strXmlParameters = "";
            StreamReader sr = new StreamReader(pathi);
            strXmlParameters = sr.ReadToEnd();
            sr.Close();
            RSA.FromXmlString(strXmlParameters);
          
            // Verifikimi i te dhenave duke perdor nenshkrimin .Perdorimi i SHA1CryptoServiceProvider
            // per te specifikuar perdorimin e SHA1 per gjetjen e hash-it.
            return RSA.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return false;
        }
    }
          
   }
        
}
