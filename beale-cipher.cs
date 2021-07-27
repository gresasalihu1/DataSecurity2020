using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ds
{
   public class Beale
    {
        //funksioni ne baze te cilit e enkriptojm mesazhin qe duam ta fshehim(plaintext) duke u bazuar ne nje liber(text).
         public static string Enkriptimi(string libri, string plaintext)
        {
            // per lexim  te file ( libri.txt)
            libri = File.ReadAllText(@"libri.txt");
     
            //me i kthy te gjitha shkronjat e librit ne shkronja te vogla
            libri = libri.ToLower();
            
            //me i kthy te gjitha shkronjat e plaintext ne shkronja te vogla
            plaintext = plaintext.ToLower();
            
            //me i nda secilen karakter te librit me hapsira
            string[] Dokument = libri.Split(' ');
            
            //  perdorimi i  metodes ToCharArray()  per kopjimin e karaktereve  nga string plaintext
            //ne nje koleksion te Unicode character.
            char[] plain = plaintext.ToCharArray();
            string ch = "";
            string ch1 = "";
 
            //Random class mundeson gjenerimin e  nje numri random
            Random random = new Random();
 
            ////krijimi i nje for loop
            //ku i merr vleren deri tek gjatesia e plaintext
            for (int i = 0; i < plaintext.Length; i++)
            {
                string strCh = "";
                //nese plaintext[i] eshte i zbrazet vazhdo
                if (plaintext[i] == ' ')
                {
                    continue;
                }     
                //krijimi i nje for loop
                // ku j merr vlerat deri tek gjatesia e Dokument
             for (int j = 0; j < Dokument.Length; j++)
             {

                 string str = Dokument[j];
                //StartsWith percakton nese fillimi i stringut str pershtatet me te stringut plain[i]
                if (str.StartsWith(plain[i].ToString()))
                {
                 //nese pershtatet atehere stringut strCh jepi vleren :
                 strCh += j + 1 + " "; 

                }
             }
         //krijojm nje varg qe merr vleren e strch i ndare me ' '
         string[] strArray = strCh.Split(' ');
                
         //k merr nje numer random deri te gjatesia e tekstit
          int k = random.Next(strArray.Length - 1);
                
          //strArray merr indeksin k(random) dhe e kthen ne string
          ch1 += strArray[k].ToString() + " ";
                
          //ch e mbushim me vleren ch1 nga 0 deri te gjatesia ch1.Length-1
          ch = ch1.Substring(0, ch1.Length - 1);
      }
           
   return ch;
}
 public static void Dekriptimi(string ciphertext, string tex)
{          // per lexim  te file ( libri.txt)
            tex = File.ReadAllText(@"libri.txt");
            //e ndajm chipertext me hapsira
            string[] t = ciphertext.Split(' ');
             //deklarojm nje varg te string qe ka size sa t
            string[] st = new string[t.Length];
            //e ndajm librin me hapsira
            string[] Dokument = tex.Split(' ');
 

   for (int i = 0; i < t.Length; i++)
            {    //Me ane te metodes Int32.Parse() e kemi  konvertu stringun ne integer 32bitesh per 
                 //me marr indeksin e fjaleve ne dokument.
                st[i] = Dokument[(Int32.Parse(t[i])) - 1];
            }
            string cplain = "";
 
 
    for (int l = 0; l < st.Length; l++)
            {
               cplain += st[l].Substring(0, 1) + "";
            }
            Console.Write(cplain);
       }
    }
}
