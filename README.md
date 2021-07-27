# DS_2020_Gr26
Zhvillimi i projektit gjendet në folderin ds përkatësisht në klasen ds.cs në të cilen permes objekteve i kemi thirrur klasat që në vete përmbajnë funksionet për Komanden Beale(beale-cipher.cs) për Komanden Morse Code (morse-code.cs) për Komanden Playfair(playfair-cipher.cs) për Komanden Create-user(create-user.cs) për Komanden Delete-user(delete-user.cs) për Komanden export-key(export-key) për Komanden import-key(import-key) për Komanden write-message dhe për Kamanden read-message(read_write.cs).Programi është zhvilluar ne gjuhen programuese C# dhe implementohet ne Console.

# Komanda Beale
Beale cipher është një algoritëm qe e shfrytëzon një tekst dokument si libër.Me këtë metodë zëvendësohet cdo shkronjë në mesazhin
sekret me indeksin e një  fjale në libër që fillon me atë shkronjë.Gjithashtu duhet pasur kujdes që per shkaqe sigurie mos të enkriptohet shkronja e njejtë me të njëjtin numer.Ndërsa nëse duam ta dekpritojm mesazhin  sekret atëherë  numri në atë mesazh paraqet indeksin e një fjale në  libër dhe marrim shkronjën e parë të saj.
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit.
![](Images/Beale.png)
# Komanda Morse Code
Komanda Morse është një kod që përdoret për të koduar mesazhe që përbëhen nga shkronja dhe shifra. Cdo shkronjë në alfabetin Latin si dhe shifrat janë ekuivalent me një seri . dhe -. në Morse Code. 
Ruajtja e shkronjave&shifrave dhe vlerave përkatese të tyre në Morse Code është bërë duke krijuar objektin MorseDictionary në kuader te klasës Dictionary si dhe për secilin funksion është dhëne përshkrimi si koment në program.
Kërkesa shtesë --audio është bërë në atë menyrë që të ekzekutohet në momentin kur si argument është "audio" dhe teksti hyrës është në Morse Code atëherë ndëgjohet ajo vlerë hyrëse si nga makina Morse.
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit.
![](Images/Morse.PNG)
# Komanda Playfair
Komanda Playfair bazohet në matricën 5x5.Në atë matricë e vendosim alfabetin mirëpo pasi alfabeti i ka 26 shkronja, i dhe j e marrin të njëjtën qeli.Matrica qelës formohet duke u bazuar në një keyword.Nëse një shkronjë përsëritet më shumë se 1 herë ajo shënohet vetëm herën e parë.Mesazhi sekret duhet të ndahet në grupacion nga dy shkronja.Nëse mesazhi sekret nuk ka nr cift të shkronjave duhet të vendosim 'x'.Rregullat për enkriptim:
1.Nëse shkronjat jane në rreshta të njëjtë atëherë duhet të merret elementi pasues
2.Nëse ato jane në kolona të njëjta atëherë merret el. i rreshtit pasues por i kolonës të njëjtë.
3.Nëse ato nuk janë as në kolona e as në rreshta të njejtë atëherë merret el. i rreshtit të njëjtë por ne kolonën e karakterit tjetër.
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit.

![](Images/Playfair.png)
Gjenerimi i tabeles ne baze te keyword per piket shtese

# Komanda Export-key
Eksporton çelësin publik ose privat të shfrytëzuesit nga direktoriumi i çelësave.Nëse shkruajmë 3 argumente vartësisht nga lloji i celësit,përmbajtja e tij na u paraqitet në Console,përndryshe nëse i shkruajmë 4 argumente celësi ruhet në fajllin të cilin ne ia përcaktojme.Ndonjëherë mund ta kemi vetëm çelësin publik të një shfrytëzuesi, prandaj nëse e kërkojmë çelësin
privat do të shfaqet një mesazh gabimi.
![](Images/export-key.png)
# Komanda Import-key
Importon çelësin publik ose privat të shfrytëzuesit nga shtegu i dhënë dhe e vendos në direktoriumin
e çelësave.Çelësi i importuar mund të jetë publik ose privat.
Sintaksa: ds import-key <name> <path>
![](Images/import1.png)
![](Images/importkey2.png)

# Komanda Create-user
 -- > Faza 2 :
Komanda Create-user e krijon nje RSA key pair të celësave.
Me anë të funksionit Krijo() e kemi bërë të mundshme këtë.
Ndërsa me anë te funksionit DoesKeyExist() mund ta kontrollojm nese ekziston paraprakisht ai celës.Nese jo atëherë na  mundësohet krijimi i atij celësi.
Në console si argument të parë  shënojm këtë komandë Create-user ndërsa si argument të dytë e shenojm emrin që duam ta emërtojm celesin që e ruajm si ("name.pub.xml") - për celësin publik     dhe ("name.xml") - për celësin privat.

![](Images/create-user.png)

Celësi duhet të permbaj vetëm shkronja numra dhe _  .Celesi nuk duhet të përmbaj karaktere tjera dhe asesi hapësira.
![](Images/validimi.png)


Nëse celësi ekziston paraprakisht:
![](Images/doeskeyexist.png)

 -- > Faza 3 :
 Në fazën e 3-të është zgjeruar komanda create-user nga faza e 2-të ashtu që gjatë krijimit të shfrytëzuesit të kërkohet edhe
fjalëkalimi si dhe të dhënat të ruhen në bazë të shenimeve .
Fjalëkalimi kërkohet përmes inputit, pasi që nuk preferohet të figurojë në histori. Fjalëkalimi duhet të ketë gjatësinë së paku 6 karaktere dhe duhet të përmbajë së paku një numër ose simbol.Fjalëkalimi i shfrytëzuesitështë ruajtur në formë të sigurt përmes hash algoritmeve dhe salting në .
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit dhe egzekutimi me sukses i kësaj komande bazë të shenimeve.
![](create1.png)
![](Images/c1.png)
![](create.png)

# Komanda Delete-user
 -- > Faza 2 :
Komanda Delete-user sherben per ti larguar të gjithë çelësat ekzistues të shfrytëzuesit.Nëse ekziston vetëm celësi publik atëherë e fshin vetëm atë,ndërsa nëse nuk ekziston fare ai celës atëherë paraqitet mesazhi i caktuar.
Si argument të parë e shënojm këtë komandë ndërsa si argument të dytë e shënojmë KeyName.

![](Images/delete-user.png)


Ndërsa nëse shënojmë më shumë argumente se sa që duhet:
![](Images/validimi2.png)

 -- > Faza 3 :
Në fazën e 3-të është zgjeruar komanda delete-user nga faza e 2-të ashtu që kur të thirret kjo komandë do të fshihen edhe të gjitha të dhënat e shfrytëzuesit nga baza e shënimeve dhe të gjithë celesat e ti.
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit dhe egzekutimi me sukses i kësaj komande .
![](Images/delete.png)

# Komanda login
Teston çiftin shfrytëzues/fjalëkalim. Në rast suksesi lëshohet një token i nënshkruar i cili mund të
përdoret për autentikim të shfrytëzuesit.
Për nënshkrim të tokenit përdoret çelësi privat i shfrytëzuesit, ndërsa për vërtetim të nënshkrimit përdoret çelësi publik i
shfrytëzuesit.
Tokeni skadon pas 20 minutave. Tokeni mund të përdoret vetëm për shfrytëzuesin për të cilin është
lëshuar. Sintaksa per kete komand : ds login <name>
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit.
           
Realizimi me sukses i pikës shtesë: Të lexohet fjalëkalimi pa echo të simboleve në ekran (sikur read -s).
![](Images/login.png)
           
# Komanda status
Jep informata rreth tokenit sic jane User, A eshte valid apo jo dhe koha e skadimit.
Nëse tokeni ka skaduar, nuk ka nënshkrim valid, ose nuk ekziston shfrytëzuesi, atëherë tokeni nuk
konsiderohet valid.
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit.
![](status.png)

# Komanda Write Message
  -- > Faza 2 :
E shkruan një mesazh të enkriptuar të dedikuar për një shfrytëzues.
Është krijuar funksioni Enkriptimi i Mesazhit përmes DES me çelësin key të gjeneruar permes RNGCryptoServiceProvider dhe IV e gjenerauar permes instances së klasës DESCryptoServiceProvider, ky çelës i DES-it enkriptohet permes RSA me anë të funksionit RSAEncrypt me çelësin publik keys/<name>.pub.xml. pra me celesin publik të marresit pastaj ky çelës i enkriptuar me RSA convertohet në base64 dhe shfaqet në Console apo ruhet në dokumentin qe jep shfrytezuesi, në Console shfaqet gjithashtu emri i marresit në base64,IV në base 64 si dhe mesazhi i enkriptuar në base64 pra sipas skemës ne vijim:
           ciphertext = base64(utf8(<name>)) . base64(<iv>). base64(rsa(<key>)) . base64(des(<message>))
  -- > Faza 3:
Në fazën e 3-të është zgjeruar komanda write-message nga faza e 2-të ku pervec marresit, mesazhit shkruajm edhe Tokenin e shfrytezuesit i cili po dergon mesazhin, ne këtë fazë është krijuar funksioni  Nenshkrimi me ate te cilit nenshkruhet mesazhi i enkriptuar.
Vlera sender është emri i shfrytëzuesit që i korrespondon tokenit token. Komanda dështon nëse
tokeni nuk është valid ose ka skaduar. Nëse mungon pjesa e tokenit atëherë egzekutimi i komandes vazhdon si në fazen e 2-të.
Nëse validohet tokeni me sukses, atëherë nënshkrimi bëhet me çelësin privat të dërguesit sender dhe në Console shfaqet emri i marresit në base64,IV në base 64, mesazhi i enkriptuar në base64, emri i shfrytezuesit te tokenit ne base64 si dhe nenshkrimi i mesazhit te enkriptuar në base 64 pra sipas skemës ne vijim:
ciphertext = base64(utf8(<name>)) . base64(<iv>) . base64(rsa(<key>)). base64(des(<message>)) . base64(utf8(<sender>)).                                                              base64(signature(des(<message>)))
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit.

# Komanda Read Message
  -- > Faza 2 :
E dekripton dhe e shfaq në Console mesazhin e enkriptuar.
Është krijur funksioni RSADecrypt për dekriptimin e çelësit qe merret nga hyerja duke e konvertuar nga base64 ne byte gjithashtu dekriptimi bëhet me çelësin privat keys/<name>.xml që merret nga hyrja duke e marr emrin dhe bashkangjit atë emer me fajllin ku janë gjeneruar çelësat nese nuk mund te gjendet çelësi privat atëhere shfaqet mesazh gabimi, çelësin i dekriptuar nga RSADecrypt-funksioni shfrytezohet nga funksioni DekriptimiiMesazhit që e dekripton mesazhin me atë çelës.
  -- > Faza 3 : 
Në fazën e 3-të është zgjeruar komanda read-message nga faza e 2-të ashtu që nëse figuron pjesa e dërguesit/nënshkrimit në mesazh,
atëherë do të tentohet verifikimi i atij nënshkrimi duke përdorur çelësin publik të dërguesit, është krijuar funksioni VerifikimiiNenshkrimit i cili e bene verifikimin e nenshkrimit.
Nëse mungon pjesa e dërguesit/nënshkrimit, atëherë komanda e injoron dhe vepron sikur në fazën e
dytë. 
           
Shkrimi i argumenteve në mënyrë adekuate bëhet siq është paraqitur më poshtë pra ashtu si sintaksa në kërkesën e projektit.
![](Images/read1.png)
![](Images/read2%20(2).png)
![](Images/read3.png)
![](Images/read4.png)
![](read-write4.png)
![](read-write5.png)
![](read-write6.png)
![](read-write2.png)


Validimet
![](validimet.png)
# --> Referencat : 
 Per komanden Morse orientimi ka qene kryesisht ne keto dy linqe : https://www.geeksforgeeks.org/morse-code-implementation/ ,
 https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=netframework-4.7.
  Per komandat Beale dhe Playfair kryesisht jemi orientuar tek keto linqe : https://www.reddit.com/r/dailyprogrammer/ ,  https://www.geeksforgeeks.org/csharp-programming-language/  
  Per komandat Read dhe Write Message kryesisht jemi orientuar tek keto linqe :https://www.c-sharpcorner.com/article/,https://docs.microsoft.com/en-us/dotnet/ 
  Per komandat Create, Login dhe Status kryesisht jemi orientuar tek keto linqe : https://th.programqa.com/question/34403823/, https://tutel.me/c/programming/questions/34403823/verifying+jwt+signed+with+the+rs256+algorithm+using+public+key+in+c , 
https://codingstill.com/2018/02/create-and-sign-jwt-token-with-rs256-using-the-private-key/, https://codingstill.com/2016/01/verify-jwt-token-signed-with-rs256-using-the-public-key/, https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmlnodelist.count?view=netcore-3.1
