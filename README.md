# Football League
## Uputstvo za upotrebu

### Uvod
Football League je aplikacija sa grafičkim korisničkim interfejsom realizovana u C# programskom jeziku upotrebom WPF (Windows Presentation Foundation) framework-a. Aplikacija koristi MySQL bazu podataka kao primarni način čuvanja svih podataka koji su vezani za ovu aplikaciju. Ova aplikacija ima za cilj da korisniku, prije svega, pruži ugodno korisničko iskustvo, a zatim i mogućnost da prati najbolje svjetske fudbalske lige, klubove i igrače. Za upotrebu aplikacije neophodno je posjednovanje računara (desktop ili laptop) uz odgovarajuće okruženje koje će služiti za pokretanje aplikacije.

### Prijava na sistem
Aplikacija podržava dva tipa korisničkih naloga, nalog za administratora i nalog za klasičnog korisnika. Pokretanjem aplikacije otvara se forma za prijavu korisnika, koja je identična i za klasičnog korisnika i za administratora naše aplikacije a izgleda kao na slici ispod.
![Forma za prijavu](https://github.com/user-attachments/assets/0b54d805-a333-4f97-94d6-07a439fea6e9)
Korisnik može odabrati željeni jezik za prijavu putem opcije smještene na vrhu forme. Podrazumjevano je primjenjen engleski jezik ali pored njega postoji i opcija da odabere lokalni jezik. Podaci koji se unose tokom prijave su korisničko ime, koje je jedinstveno na nivou sistema, te lozinka. Nakon tog procesa korisnik mora da odabere režim rada, odnosno mora specifikovati ulogu koju će on imati u sistemu. Ako je nalog koji on posjeduje administratorskog tipa onda može da odabere bilo koju od dvije ponuđene opcije, ali ako je njegov nalog, nalog klasičnog korisnika, njemu se zabranjuje pristup aplikaciji u administratorskom režimu rada. Klikom na dugme Login korisnik pristupa glavnom dijelu aplikacije o kojem će nešto više biti rečeno kasnije.

### Registracija korisnika
Korisniku se prilikom pokretanja aplikacije podrazumjevano prikazuje forma za prijavu, ali ako korisnik nije registrovan na naš sistem, neophodno je prvo obaviti proces registracije. Forma za registraciju izgleda kao na slici ispod.
![Forma za registraciju](https://github.com/user-attachments/assets/fad1f9ba-8f8f-48ea-bf03-59936ac24f27)
Prilikom procesa registracije od korisnika se zahtjeva da unese osnove podatke o sebi kao što su ime, prezime, korisničko ime i lozinka. Unos lozinke se zahtjeva dva puta, čime se smanjuje broj slučajeva u kojima korisnici naprave nenamjernu grešku prilikom kreiranja vlastitog naloga.

### Klasični korisnik
Forma koja će korisniku biti prikazana u slučaju uspješne prijave predstavljena je na sljedećoj slici.
![Početna forma](https://github.com/user-attachments/assets/63d6b839-43e9-42d5-b910-39bebba32cb4)
Na lijevoj strani se nalazi meni aplikacije na kojem su prikazane različite opcije. Inicijalno se prikazuje prva opcija koja predstavlja tabelu rezultata timova u okviru neke odabrane lige.Korisnik može da odabere ligu, a potom i konkretnu sezonu u okviru lige, iz combo-box elemenata iznad tabele. Nakon odabira lige i sezone u tabeli je predstavljen trenutni poredak timova po broju bodova. Osim broja bodova korisnik može da vidi i broj odigranih utakmica, broj ostvarenih pobjeda, poraza te nerješenih utakmica, ali i statistiku vezanu za broj postignutih i primljenih golova, kao i gol razliku.

Neposredno ispod opcije za preled tabele se nalazi opcija za prikaz odigranih utakmica čijim odabirom dobijamo sljedeću formu.
![Forma za utakmice](https://github.com/user-attachments/assets/6e7a27fd-e460-4205-bad3-7f829a0c6173)
Slično kao kod prethodnog prikaza gdje smo prvo morali da odaberemo ligu i sezonu tako i ovdje moramo da uz to još odaberemo i broj kola. Nakon toga, prikazuju se sve utakmica koje su odigrane u tom kolu, za odabranu sezonu i ligu. Korisnik može jasno da vidi koji su timovi igrali, koji je bio rezultat, ko je sudio tu utakmicu te datum kada je utakmica odigrana.

Naredna opcija je pregled timova, gdje su tabelarno predstavljene osnovne informacije o svim timovima. Pod osnovnim informacijama podrazumjeva se grb, naziv, lokcija, datum osnivanja, broj trofeja, glavni trener kluba i naziv stadiona. Kako to izgleda iz perspektive korisnika prikazano je na slici ispod.
![Forma za timove](https://github.com/user-attachments/assets/85cd5398-23a0-4fc8-8be1-7feefe508f81)
Za svaki tim iz tabele imamo dva dugmeta. Klikom na prvo dugme korisnik specifikuje da zahtjeva prikaz dodatnih informacija za taj fudbalski klub, odnosno može da se smatra kao neki vid "praćenja" tog kluba. Detaljnije o tome će biti navedeno u narednom pasusu. Drugo dugme korisnika vodi na prikaz igrača koji igraju u tome klubu. Kako to izgleda možete da vidite na sljedećoj slici.
![Forma za igrače](https://github.com/user-attachments/assets/3ed57719-adf4-4406-9572-4d668764a165)
Ova forma korisniku daje uvid u osnovne informacije o igračima u timu ali i njihovoj statistici. Tu obuhvatamo prikaz broja dresa igrača, njegovo ime, prezime i broj godina te broj minuta u dresu tog kluba, broj golova koje je postigao, asistencija koje je podijelio, i broj žutih odnosno crvenih kartona koje je dobio za vrijeme boravka u tom klubu.
