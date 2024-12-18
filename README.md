# Football League

## Uputstvo za upotrebu

### _Uvod_
Football League je aplikacija sa grafičkim korisničkim interfejsom realizovana u C# programskom jeziku upotrebom WPF (Windows Presentation Foundation) framework-a. Aplikacija koristi MySQL bazu podataka kao primarni način čuvanja svih podataka koji su vezani za ovu aplikaciju. Ova aplikacija ima za cilj da korisniku, prije svega, pruži ugodno korisničko iskustvo, a zatim i mogućnost da prati najbolje svjetske fudbalske lige, klubove i igrače. Za upotrebu aplikacije neophodno je posjednovanje računara (desktop ili laptop) uz odgovarajuće okruženje koje će služiti za pokretanje aplikacije. Veličina ekrana za prikaz aplikacije nije bitan jer se dimenzije aplikacije mogu prilagodi prema preferencijama korisnika.

### _Prijava na sistem_
Aplikacija podržava dva tipa korisničkih naloga, nalog za administratora i nalog za klasičnog korisnika. Pokretanjem aplikacije otvara se forma za prijavu korisnika, koja je identična i za klasičnog korisnika i za administratora naše aplikacije a izgleda kao na slici ispod.


![Forma za prijavu](https://github.com/user-attachments/assets/0b54d805-a333-4f97-94d6-07a439fea6e9)


Korisnik može odabrati željeni jezik za prijavu putem opcije smještene na vrhu forme. Podrazumjevano je primjenjen engleski jezik ali pored njega postoji i opcija da odabere lokalni jezik. Podaci koji se unose tokom prijave su korisničko ime, koje je jedinstveno na nivou sistema, te lozinka. Nakon tog procesa korisnik mora da odabere režim rada, odnosno mora specifikovati ulogu koju će on imati u sistemu. Ako je nalog koji on posjeduje administratorskog tipa onda može da odabere bilo koju od dvije ponuđene opcije, ali ako je njegov nalog, nalog klasičnog korisnika, njemu se zabranjuje pristup aplikaciji u administratorskom režimu rada. Klikom na dugme Login korisnik pristupa glavnom dijelu aplikacije o kojem će nešto više biti rečeno kasnije.

### _Registracija korisnika_
Korisniku se prilikom pokretanja aplikacije podrazumjevano prikazuje forma za prijavu, ali ako korisnik nije registrovan na naš sistem, neophodno je prvo obaviti proces registracije. Forma za registraciju izgleda kao na slici ispod.


![Forma za registraciju](https://github.com/user-attachments/assets/fad1f9ba-8f8f-48ea-bf03-59936ac24f27)


Prilikom procesa registracije od korisnika se zahtjeva da unese osnove podatke o sebi kao što su ime, prezime, korisničko ime i lozinka. Unos lozinke se zahtjeva dva puta, čime se smanjuje broj slučajeva u kojima korisnici naprave nenamjernu grešku prilikom kreiranja vlastitog naloga.

### _Klasični korisnik_
Forma koja će korisniku biti prikazana u slučaju uspješne prijave predstavljena je na sljedećoj slici.


![Početna forma](https://github.com/user-attachments/assets/63d6b839-43e9-42d5-b910-39bebba32cb4)


Na lijevoj strani se nalazi meni aplikacije na kojem su prikazane različite opcije. Inicijalno se prikazuje prva opcija koja predstavlja tabelu rezultata timova u okviru neke odabrane lige. Korisnik može da odabere ligu, a potom i konkretnu sezonu u okviru lige, iz combo-box elemenata iznad tabele. Nakon odabira lige i sezone u tabeli je predstavljen trenutni poredak timova po broju bodova. Osim broja bodova korisnik može da vidi i broj odigranih utakmica, broj ostvarenih pobjeda, poraza te nerješenih utakmica, ali i statistiku vezanu za broj postignutih i primljenih golova, kao i gol razliku.

Neposredno ispod opcije za preled tabele se nalazi opcija za prikaz odigranih utakmica čijim odabirom dobijamo sljedeću formu.


![Forma za utakmice](https://github.com/user-attachments/assets/6e7a27fd-e460-4205-bad3-7f829a0c6173)


Slično kao kod prethodnog prikaza gdje smo prvo morali da odaberemo ligu i sezonu tako i ovdje moramo da uz to još odaberemo i broj kola. Nakon toga, prikazuju se sve utakmica koje su odigrane u tom kolu, za odabranu sezonu i ligu. Korisnik može jasno da vidi koji su timovi igrali, koji je bio rezultat, ko je sudio tu utakmicu te datum kada je utakmica odigrana.

Naredna opcija je pregled timova, gdje su tabelarno predstavljene osnovne informacije o svim timovima. Pod osnovnim informacijama podrazumjeva se grb, naziv, lokcija, datum osnivanja, broj trofeja, glavni trener kluba i naziv stadiona. Kako to izgleda iz perspektive korisnika prikazano je na slici ispod.


![Forma za timove](https://github.com/user-attachments/assets/85cd5398-23a0-4fc8-8be1-7feefe508f81)


Za svaki tim iz tabele imamo dva dugmeta. Klikom na prvo dugme, odnosno zvono, korisnik specifikuje da zahtjeva prikaz dodatnih informacija za taj fudbalski klub, odnosno može da se smatra kao neki vid "praćenja" tog kluba. Detaljnije o tome će biti navedeno u narednom pasusu. Drugo dugme korisnika vodi na prikaz igrača koji igraju u tome klubu. Kako to izgleda možete da vidite na sljedećoj slici.


![Forma za igrače](https://github.com/user-attachments/assets/3ed57719-adf4-4406-9572-4d668764a165)


Ova forma korisniku daje uvid u osnovne informacije o igračima u timu ali i njihovoj statistici. Tu obuhvatamo prikaz broja dresa igrača, njegovo ime, prezime i broj godina te broj minuta u dresu tog kluba, broj golova koje je postigao, asistencija koje je podijelio, i broj žutih odnosno crvenih kartona koje je dobio za vrijeme boravka u tom klubu.

Maloprije smo spomenuli za šta služi dugme u obliku zvona ali nismo ulazili u detalje pa je sada red da o tome kažemo nešto više. Praćenje timova omogućava korisniku da vidi u kakvoj su formi njegovi omiljeni timovi, na način da mu se prikazuju rezultati sa posljednjih nekoliko utakmica svakog tima kojeg je on zapratio. Na taj način je korisniku obezbjeđen efikasniji način rada, jer ne mora da pretražuje utakmice u već navedenoj formi za prikaz utakmica. Slika koja slijedi predstavlja izgled prethodno opisane funkcionalnosti.


![Forma za prikaz omiljenih timova](https://github.com/user-attachments/assets/78637034-ed78-401c-8134-6cbab8000450)


Na kraju samog menija imamo i tipku za odjavu sa sistema koja kada se stisne vodi korisnika na formu za prijavu na sistem.

### _Administrator_
Forma koja će administratoru biti prikazana u slučaju uspješne prijave predstavljena je na sljedećoj slici.


![Forma za naloge](https://github.com/user-attachments/assets/62f6b39f-1821-45e7-a30d-9d162a11d43e)


Na lijevoj strani se nalazi meni aplikacije na kojem su prikazane različite opcije od kojih su neke identične kao kod klasičnog korisnika. Inicijalno se prikazuje tabela koja sadrži sve korisnike našeg sistema, kako klasnične korisnike tako i administratore. Za svakog od njih može da se vidi njegov identifikator, ime, prezime i korisničko ime. Pored toga obezbjeđene su i opcije za blokiranje i promociju korisnika. Blokiranje korisnika je moguće samo ako je on klasičan korisnik, odnosno ne možemo da blokiramo nalog nekog administratora. Promocija korisnika zapravo predstavlja unapređenje naloga korisnika iz standardnog u administratorski, i to je moguće jedino ako korisnik nije trenutno blokiran. To je ujedno i jedini način da se u okviru aplikacije kreira administratorki nalog.


Prva opcija na meniju predstavlja opciju čijim odabirom administrator može da dodaje nove utakmice u bazu podataka. Kako to izgleda možemo da vidimo na slici ispod.


![Forma za dodavanje utakmica](https://github.com/user-attachments/assets/f54bcfa2-89aa-4c87-a8ec-d02f4b38ca88)


Administrator odabirom opcija iz combo-box komponenata pri vrhu ekrana može da vidi koje utakmice su već unesene za datu ligu, sezonu i kolo. Naravno nije moguće da jedan tim odigra dvije utakmice unutar istog kola u istoj sezoni za istu ligu pa je to ograničenje definisano i u okviru ovog sistema. Kada administrator unese neku utakmicu, ona će zapravo biti unijeta za tu, odabranu ligu, sezonu i kolo. Osim ta tri parametra neophodno je odabrati domaći i gostujući tim, te unijeti broj golova koji je jedni odnosno drugi postigli. Za kraj mora da se specifikuje kada se utakmica odigrala te ko ju je sudio. Takođe, administrator ima mogućnost da dodaje nove lige, sezone i kola, uz pomoć tri dugmeta pozicionirana na lijevoj strani. Odabirom bilo koje od ove tri opcije dolazi do otvaranja novog prozora unutar kojeg je potrebno unijeti adekvatne informacije. Navedeni prozori za unos su prikazani na slikama koje slijede.


![Prozor za ligu](https://github.com/user-attachments/assets/d3071546-de5a-4083-862e-719c1f09b1cf)
![Prozor za sezonu](https://github.com/user-attachments/assets/12fedca2-abe7-4b20-8413-697f5f82b22e)
![Prozor za kolo](https://github.com/user-attachments/assets/7d99e4b2-0496-4080-b1b8-3ad190b93fe2)


Za svaku utakmicu administrator može da unese i statistiku igrača, kako domaćeg tako i gostujućeg tima. To se ostvaruje klikom na ikonicu u tabeli, koja se nalazi pored naziva tima. Odabirom te opcije možemo da vidimo prikaz kao na sljedećoj slici.


![Forma za dodavanje statistike igrača](https://github.com/user-attachments/assets/fd3b4086-485d-4a5e-a919-aace484c343f)


Prvi korak je odabir igrača za kojeg ćemo unijeti podatke u tabelu. Nakon toga, prelazimo na unos broja odigranih minuta, postignutih golova i podjeljenih asistencija na toj utakmici. Prije same potvrde unosa podataka, administrator ima tri opcije koje može označiti. Odabirom prve opcije, administrator specifikuje da je igrač bio starter na utakmici, odnosno da je igrao od prve minute. Druga i treća opcija služe za označavanje da je igrač dobio žuti ili crveni karton. Kada se stisne dugme za potvrdu unosa statistika igrača, ista će biti dodana u tabelu koja je prikaza na prethodnoj slici, ali i u bazu podataka.


Naredna stavka na meniju omogućava administratoru da dodaje nove timove u sistem. Nakon odabira ove opcije administrator može da vidi ono što je prikazano na sljedećoj slici.


![Forma za dodavanje timova](https://github.com/user-attachments/assets/554dd389-0bf1-403c-8ccc-fdf00811117d)


