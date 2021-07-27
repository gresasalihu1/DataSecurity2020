using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ds
{
   public class Playfair
    {
      //krijojm nje liste(tl)
    public static List<char> tl = new List<char>();
        //qelesin ktheje ne shkronja te vogla
    public static void initPlainText(string teksti) //krijojm nje funksion qe si parameter e ka tekstin
        {
            //te gjitha shkronjat e tekstit i kthejm ne shkronja te vogla
            teksti = teksti.ToLower();
            
             //per secilen variabel te ne tekst:
            foreach (var t in teksti)
            {   
                //nese ajo variabel eshte e zbrazet vazhdo
                if (t == ' ') continue;
                //nese ajo variabel eshte J ather vendos I
                else if (t == 'j') tl.Add('i');
                 //shto t
                tl.Add(t);
            }
        }
      public static List<char> Krijoplaintekstiin()
        {   //krijojm nje liste te re e emertojm lista1
            List<char> lista1 = new List<char>();
            
           
            for (int i = 0; i < tl.Count - 1; i++) //Krahason dy karaktere dhe nese jane te njejte karakterin e dyte e bene X
            {
                if (tl[i] == tl[i + 1])
                {
                   
                    lista1.Add(tl[i]);
                    lista1.Add('x');
                    continue;
                }
                 //perndryshe shto karakterin e ardhshem
                else lista1.Add(tl[i]);

            }
            lista1.Add(tl[tl.Count - 1]);
            
            ////nese gjatesia e listes eshte tek ateher shto X 
            //p.sh kemi fjalen siguria ateher vendosim x dhe do fitojm siguriax
            if (lista1.Count % 2 == 1) lista1.Add('x');

            return lista1;
        }
      public static List<char> merrAlfabetin()
        {
            List<char> lista1 = new List<char>();
            //marrin alfabetin nga shkronja A ne Z
            for (char i = 'a'; i <= 'z'; i++)
            { //nese shkronja eshte j  kaloje
                if (i == 'j') continue;
                //shto shkronjen
                lista1.Add(i);
            }
            return lista1; //kthe listen e fituar
        }
        //e kemi kriju edhe tabelen per piket shtese
       public static List<char> Krijotabelen(string qelesi)
        { //qelesin ktheje ne shkronja te vogla
            qelesi = qelesi.ToLower();
             //krijo nje liste
            List<char> lista1 = new List<char>();
             //krijojm nje liste qe e merr Alfabetin qe e krijuam me par
            List<char> alfabeti = merrAlfabetin();
            lista1 = qelesi.Distinct().ToList(); //Largon shkronjat duplikate nga qelesi


            for (int i = 0; i < lista1.Count; i++) //Krahason karakteret e qelesit dhe te alfabetit dhe i largon duplikatet
            {
                for (int j = 0; j < alfabeti.Count; j++)
                {
                    if (lista1[i] == alfabeti[j])//nese dy shkronja te njejta
                    {
                        alfabeti.Remove(alfabeti[j]);//largo njeren
                    }
                }
            }
             //per qdo vlere ne alfabet
             foreach (var vlera in alfabeti)
            {    //shto vleren ne listen1
                lista1.Add(vlera);
            }
            //kthe listen e fituar
            return lista1;
        }
        //krijojm nje funksion per enkriptim qe si parameter dy lista:listen table dhe listen plaintext
       public static void Enkriptimi(List<char> table, List<char> plainText)
        {
            
            char[,] lista1 = new char[5, 5];//krijojm  matricen  5x5
            int s = 0;
            //Shendrro nje varg nje dimensional ne varg dydimensional
            for (int i = 0; i <25; i++)
            {
                // (i/5)-rreshti dhe (i%5)-kolona
                lista1[i /5, i % 5] = table[s];
                s++;
            }

            //Printon tabelen ne vargje dydimensionale
            for (int i = 0; i < 25; i++)
            {  //nese kemi arritur te kolona e fundit kalo ne rresht te ri
                if (i % 5 == 0) Console.WriteLine();
                 //shtyp shkronjat e matrices
                Console.Write(lista1[i / 5, i % 5] + "  ");

            }
            Console.WriteLine();
            Console.Write("Teksti pas enkriptimit :");

            //Enkriptimi
            for (int i = 0; i < plainText.Count; i = i + 1)
            {

                for (int j = 0; j < plainText.Count; j = j + 2)//Pas qdo dy shkronjave paraqit hapsire
                {
                    Console.Write(" ");
                }


                char k1 = plainText[i]; //karakteri i pare
                char k2 = plainText[i + 1];//karakteri i dyte
                int row1 = 0, row2 = -1, col1 = 0, col2 = -1;

                for (int j = 0; j < 25; j++)
                {

                    if (lista1[j / 5, j % 5] == k1) // merr indeksin e karakterit te pare te rreshtit dhe kolones
                    {
                        row1 = j / 5;
                        col1 = j % 5;
                    }
                    if (lista1[j / 5, j % 5] == k2) // merr indeksin e karakterit te dyte te rreshtit dhe kolones
                    {
                        row2 = j / 5;
                        col2 = j % 5;
                    }
                }

                //rregulla 1: nese karakteri i pare dhe i dyti jane ne te njejtin rreshte atehere:
                if (row1 == row2)
                {   //merret elementi ne rreshtin e njejt por ne kolonen e ardhshme(nga ana e djatht) te secilit karakter
                    Console.Write(lista1[row1, (col1 + 1) % 5] + "" + lista1[row2, (col2 + 1) % 5]);
                }
                //Rregulla 2: nese karakteri i pare dhe i dyti jane ne te njejten kolone
                else if (col1 == col2)
                {  //merr elementin qe ndodhet ne rreshtin e ardhshem por ne kolonen e njejte
                    Console.Write(lista1[(row1 + 1) % 5, col1] + "" + lista1[(row2 + 1) % 5, col2]);
                }
                 //Rregulla 3 : nese nuk jane as ne te njejtin rreshtin as ne te njejten kolone
                else
                {  //merr elementin qe ndodhet ne rreshtin e njejte por ne kolonen e karakterit tjeter
                    Console.Write(lista1[row1, col2] + "" + lista1[row2, col1]);

                }
                i++;
            }
        }
         //funksioni per dekriptim ku si parameter merret lista table dhe lista ciphertext
      public static void Dekriptimi(List<char> table, List<char> ciphertext)
        {
            
            char[,] lista1 = new char[5, 5];//matrica 5x5

            int z = 0;
            //Shendrro nje varg nje dimensional ne varg dydimensional
            for (int i = 0; i < 25; i++)
            {
                // (i/5)-rreshti, (i%5)-kolona
                lista1[i / 5, i % 5] = table[z];
                z++;
            }

            //Printon tabelen ne vargje dydimensionale
            for (int i = 0; i < 25; i++)
            {
                //nese kemi arritur te kolona e fundit kalo ne rresht te ri
                if (i % 5 == 0) Console.WriteLine();
                Console.Write(lista1[i / 5, i % 5] + "  ");
            }

            Console.WriteLine();
            Console.Write("Teksti pas dekriptimit : ");
            for (int i = 0; i < ciphertext.Count; i++)
            {
                char k1 = ciphertext[i]; //karakteri i pare
                char k2 = ciphertext[i + 1];//karakteri i dyte
                int row1 = 0, row2 = -1, col1 = 0, col2 = -1;

                for (int j = 0; j <25; j++)
                {

                    if (lista1[j / 5, j % 5] == k1) //  merr indeksin e karakterit te pare te rreshtit dhe kolones
                    {
                        row1 = j / 5;
                        col1 = j % 5;
                    }
                    if (lista1[j / 5, j % 5] == k2) // merr indeksin e karakterit te dyte te rreshtit dhe kolones
                    {
                        row2 = j / 5;
                        col2 = j % 5;
                    }
                }
               //Rregulla 1: nese karakteri i pare dhe i dyti jane ne te njejtin rresht
                if (row1 == row2)
                {
                    // nese jemi ne kolonen 0 kalo ne kolonen e 5
                    if (col1 == 0) col1 = 5;
                    if (col2 == 0) col2 = 5;
                    //merret elementi ne rreshtin e njejt por ne kolonen e meparshme te secilit karakter
                    Console.Write(lista1[row1, (col1 - 1) % 5] + "" + lista1[row2, (col2 - 1 % 5)]);
                }
                
               //Rregulla 2: nese karakteri i pare dhe i dyti jane ne te njejten kolone
                else if (col1 == col2)
                {
                    // nese jemi ne rreshtin 0 kalo ne kolonen e 5
                    if (row1 == 0) row1 = 5;
                    if (row2 == 0) row2 = 5;
                    
                    ////merr elementin qe ndodhet ne rreshtin e  meparshem  por ne kolonen e njejt
                    Console.Write(lista1[((row1 - 1)) % 5, col1] + "" + lista1[(row2 - 1) % 5, col2]);
                }
                
                  //Rregulla 3 : nese nuk jane as ne te njejtin rreshtin as ne te njejten kolone
                else
                {
                    ////merr elementin qe ndodhet ne rreshtin e njejte por ne kolonen e karakterit tjeter
                    Console.Write(lista1[row1, col2] + "" + lista1[row2, col1]);
                }
                i++;
            }
        }
    }
}


