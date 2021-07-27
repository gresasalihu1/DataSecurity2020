using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ds
{
    public class Translator
    {
       public static Dictionary<string, string> MorseDictionary = new Dictionary<string, string>()
        {
           //Alfabeti
            {"a", ".-"},
            {"b", "-..."},
            {"c", "-.-."},
            {"d", "-.."},
            {"e", "."},
            {"f", "..-."},
            {"g", "--."},
            {"h", "...."},
            {"i", ".."},
            {"j", ".---"},
            {"k", "-.-"},
            {"l", ".-.."},
            {"m", "--"},
            {"n", "-."},
            {"o", "---"},
            {"p", ".--."},
            {"q", "--.-"},
            {"r", ".-."},
            {"s", "..."},
            {"t", "-"},
            {"u", "..-"},
            {"v", "...-"},
            {"w", ".--"},
            {"x", "-..-"},
            {"y", "-.--"},
            {"z", "--.."},
           //Numrat
            {"0", "-----"},
            {"1", ".----"},
            {"2", "..---"},
            {"3", "...--"},
            {"4", "....-"},
            {"5", "....."},
            {"6", "-...."},
            {"7", "--..."},
            {"8", "---.."},
            {"9", "----."},
           //Formatimi
            {" ", "/" },
           //Shenjat e pikesimit
            { ".", ".-.-.-" },
            { ",", "--..--" },
            { "?", "..--.." },
            { "'", ".----." },
            { "!", "-.-.--" },
            { "/", "-..-." },
            { "(", "-.--." },
            { ")", "-.--.-" },
            { "&", ".-..." },
            { ":", "---..." },
            { ";", "-.-.-." },
            { "=", "-...-" },
            { "+", ".-.-." },
            { "-", "-....-" },
            { "_", "..--.-" },
            { "$", "...-..-" },
            { "@", ".--.-." }, 
        };
        //Me ane te keti funksioni Kthejm karakteret ne Morse si varg i . dhe -, pasi qe ne fjalorin MorseDictionary jane te ruajtura shkronjat dhe shifrat si TKey
        //ndersa vlerat perkatese ne Morse per to jane ruajtur si TValue
        //Per vleren hyrese kontrollojme nese MorseDictionary permban ate vlere nese po atehere ate vlere e ruajme ne variablen perkthimi 
        public static string KtheCharNeMorse(char c)
        {
            string perkthimi = " ";
            string s = c.ToString().ToLower();
            if (MorseDictionary.ContainsKey(s))
            {
                perkthimi = MorseDictionary[s];
            }
            perkthimi += " ";
            
            return perkthimi;
        }
        //Me ane te keti funksioni kthejm vleren hyrese te dhene si Morse ne Karaktere perkatese, kontrollojme ne cdo rresht te MorseDictionary
        //Nese hyrja perputhet me ndonjeren prej vlerave ne MorseDictionary atehere kthejme qelsin ne rastin tone karakterin perkates
        public static string KtheMorseNeChar(string s)
        {   
            string perkthimi = " ";
          
            foreach (KeyValuePair<string, string> rreshti in MorseDictionary)
            {
                if(rreshti.Value.Equals(s))
                {
                    return perkthimi = rreshti.Key.ToString();
                }
            }
            return perkthimi;
        }
    }        
    
    public class Code
    {   
        //Vlera hyrese prej perdoruesit
        private string Input;
        
        //Ruan perkthimin e Input-it
        private string Perkthimi;
        private string Argumentet;
        //Konstruktori
        public Code(string input)
        {
            this.Input = input;
        }
        //Kthen vleren hyrese para perkthimit
        public string GetInput()
        {
            return Input;
        }
        // Me ane te keti funksoni kontrollojme nese hyrjet jane valide si dhe varsisht nga argumenti qe jepet behet enkodimi ose dekodimi i vleres hyrese
        public string Perkthe(string argumentet)
        {
            this.Argumentet = argumentet;
            //Kontrollo nese hyrjet jane Valide perndryshe jep detaje se si duhet te jete ajo
            if (Regex.Matches(Input, @"[a-zA-Z0-9.,?'!/()$@_+-=;:&]").Count != 0)
            {
                if (Argumentet == "encode")
                {
                    return KtheNeMorse();
                }
                else if (Argumentet == "decode")
                    return KtheNeLatin();
            }
            else
            {
                Console.WriteLine("Keni jepur karaktere jo valide. Teksti juaj duhet te permbaje ndonjeren prej Shkronjave te alfabetit Latin ose numer ose Karakteret : .,?'!/()$@_+-=;:&");
                Environment.Exit(0);
            }
            return argumentet;
         }
        
        public string KtheNeMorse()
        {
            //Kthen vleren hyrese ne Morse Kod duke shikuar karakter per karakter te dhene ne hyrje
            foreach (char c in Input)
            {
                Perkthimi += Translator.KtheCharNeMorse(c);
            }

            return Perkthimi.Trim();
         }
         public string KtheNeLatin()
         {   //Kthen vleren hyrese ne shkronja latine,numra,shenja te pikesimit ose " " hapsire 
             string[] ndarjaEShkronjave = Input.Split();
             foreach (string s in ndarjaEShkronjave)
             {
                 Perkthimi += Translator.KtheMorseNeChar(s);
             }
             return Perkthimi;
         }
        
          public void Audio(string input)
          { // Me ane te keti funksioni mundesojme degjimin e vleres hyrese qe duhet te jepet me ane te . dhe - nese karakteri perkates pra eshte . ose -
            // Atehere ndegjohet zeri qe mundesohet me ane te Beep() qe do te jete me frekuence dhe kohezgjatja  te zaktuar
            //Perndryshe nese ne hyrje kemi Space(qe karakterizon ndarje te karaktereve) apo /(qe karakterizon ndarje te fjaleve)p.s"takohemi neser" atehere kjo hapsira mes fjaleve ne Morse paraqitet si "/" dhe do kete pauze 
            //Si dhe kontrollohet nese mund te egzekutohet ose jo me ane te try catch
            try
            {
                this.Input = input;
                int frekuenca = 415;
                int dotDuration = 300; //Kohëzgjatja e nje "."
                int dashDuration = dotDuration * 3; //Kohëzgjatja e nje "-"
                int charSpaceDuration = dotDuration * 3; //Pauza mes karaktereve
                int wordSpaceDuration = dotDuration * 7; //Pauza mes fjaleve
                foreach (char c in Input)
                {
                    if (c == '.')
                    {
                        Console.Beep(frekuenca, dotDuration);
                    }
                    else if (c == '-')
                    {
                        Console.Beep(frekuenca, dashDuration);
                    }
                    else if (c == ' ')
                    {
                        Thread.Sleep(charSpaceDuration);
                    }
                    else if (c == '/')
                    {
                        Thread.Sleep(wordSpaceDuration);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Kontrollo argumentin e dhene ai duhet te permbaje . ose - per t'u degjuar. ",e);
            }
        } 
    }
}
