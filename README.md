
# Grupa8 - A3
## TEMA: Aplikacija za postavljanje pitanja i odgovora 

## Članovi tima:

* [Admir Ihtijarević](https://github.com/Admir-Walker)
* [Amira Kunalić](https://github.com/akunalic1)
* [Amar Begovac](https://github.com/abegovac2)

## Opis teme:

Sistem (u nastavku web aplikacija) je namijenjen svim ljudima koji se bave različitim djelatnostima, kako ljudima sa iskustvom, tako i sa ljudima bez iskustva, odnosno početnicima. Ipak, najveću korist od ove aplikacije će imati programeri i profili ljudi iz IT struke. Kada prilikom rada naiđu na problem koji ne znaju riješiti samostalno, upotrebom naše aplikacije povećavaju izglede da nađu rješenje.

Pretragu na našoj web aplikaciji mogu raditi registrovani korisnici, ali i oni koji to nisu, tj. koji su u ulozi gosta. Naravno, brojne su prednosti ukoliko se korisnik odluči registrovati na našu stranicu.

Registrovani korisnici rješenje mogu dobiti na dva načina, dok neregistrovani samo na jedan način. Zajednički način koji dijele odnosi se na pretragu, tj. upotrebu našeg search engine-a. Ukoliko odgovor na traženo pitanje ne postoji u bazi podataka, registrovani korisnici mogu postaviti pitanje i za očekivati je da će za kratko vrijeme dobiti odgovor na isto (ovo vrijeme se proporcionalno smanjuje sa povećanjem broja registrovanih korisnika u sistemu).

U slučaju postavljanja pitanja, postojeći korisnici će moći vidjeti pitanje i dati odgovor, naravno, postavljač pitanja ce biti obaviješten putem notifikacija na stranici, i putem emaila.

Registrovani korisnici mogu vršiti brisanje i modifikaciju svojih pitanja. Kako bi korisnici našeg sistema mogli što prije doći do tačnog odgovora, postoji sistem rejtinga za svaki odgovor, registrovani korisnici mogu da povećaju, odnosno smanje rejting nekog odgovora. Što je rejting odgovora bolji, on će se prije nači na listi odgovora, tako da besmisleni i netačni odgovori će se nači na dnu što omogućava svim korisnicima prijatnije korisničko iskustvo.

Uz sve navedeno, registrovani korisnici će moći komunicirati međusobno sa drugim registrovanim korisnicima.

## Akteri:

* Korisnici
  * Registrovani korisnici
  * Neregistrovani korisnici – gosti
* Administratori
* Sistem za preporuke
* Sistem za obavještavanje


## Funkcionalnosti:

Funkcionalni zahtjevi su usko vezani za aktere sistema, tako da su u skladu sa time, napravljene i podjele ovih zahtjeva, navedene u nastavku.

### Gost:

*	Pretraga pitanja u sistemu, po tekstu pitanja ili po tagovima 
*	Pregled pitanja po popularnosti
*	Pravljenje korisničkog računa

### Korisnik:

*	Nasljeđuje funkcionalne zahtjeve neregistrovanih korisnika
*	Postavljanje pitanja i izvršenje klasifikacije pitanja određivanjem tagova
*	Postavljanje odgovara na pitanja
*	Prepravka pitanja i odgovora
*	Pretraga sopstvenih neodgovorenih pitanja
*	Brisanje pitanja i odgovora
*	Uređivanje vlastitog profila
*	Kreiranje chat grupa sa 1 ili više korisnika


### Administrator:

*	Nasljeđuje funkcionalne zahtjeve registrovanih korisnika
*	Promjena tagova na pitanju ukoliko nisu dovoljno desktriptivni
*	Brisanje pitanja ukoliko nisu u skladu sa pravilima platforme
*	Označavanje pitanja kao duplikat
*	Označavanje odgovora kao prihvaćenog
*	Brisanje korisnika sa platforme

### Sistem za preporuke:
* Generisanje preporuke pitanja (kod registrovanih korisnika) na osnovu historije pretrage
*	Generisanje preporuke tagova prilikom unosa taga
* Izdvajanje popularnih pitanja

### Sistem za obavještavanje
* Obavještavanje korisnika u slučaju modifikacije korisničkog računa
*	Obavještavanje korisnika u slučaju aktivnosti na pitanjima
*	Slanje obavjesti svim korisnicima u grupnim razgovorima


## Nefunkcionalni zahtjevi

Nefunkcionalni zahtjevi su osobine sistema koje sistem nudi kao platforma svim korisnicima, te se zbog toga neće vršiti podjela ovih zahtjeva kao u odjeljku ispred, nego će se izvršiti njihovo navođenje u nastavku.
* Poruke u chat grupama će se prenositi u realnom vremenu (u roku od 1 s)
* Sistem će obavještavati korisnika, u roku od 2 s, za različite aktivnosti (npr. aktivnost na profilu, aktivnost na pitanju, aktivnost u chat grupama)
* Svakim refreshom na pitanje se vrši update, u roku od 1 s, liste sa odgovorima i rejting
* Sistem će prilikom izbora taga, vršiti preporuku taga u roku od 1 s
* Prilikom pretrage pitanja, bilo po tagovima ili po tekstu, sistem će prikazivati pitanja paginacijom , ovaj proces u zavisnosti od broja parametara može trajati maximalno do 10 s
* Web aplikacija će se moći koristiti i na drugim uređajima koji ne spadaju u grupu personalnih računara, npr. tableti, mobiteli itd.

Aplikacija je deployana na:
https://bitstack.azurewebsites.net/
