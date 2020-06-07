# Linux podział plików

***

Struktura folderów w linuxie: Ciekawoski i inne takie szmery: W linuxie, taka jak w unixie, standardem jest ze wszystko jest plikiem, nawet urządzenia takie jak mysz i klawiatura.

![Opis graficzny](https://1.bp.blogspot.com/-jvnXYE60AX8/VSJKZh91MFI/AAAAAAAAAGI/4hztMSbuFeI/s1600/opensource-technologies-44-638.jpg) 

**bin**
Są tam wszystkie podstawowe programy (binaries) takie jak cat, ls, chmod i wiele więcej. 

**sbin**
 Systemowe binarki. Głównie rzeczy związane z administracją i z stystem user mode (czyli root).  

**boot**
Pliki związane z bootowaniem się systemu. 

**cdrom**
Raczej już legacy mounting point na płyte cd. 

**dev** 
To tutaj zyją sobie urządzenia hardware. Dla przykładu dysk oznaczony jest sda, a jego partycje sda1, sda2... sda[n]. Z plików w tym katalogu korzystają głównie programy, rzadko uzytkownicy. 

**etc**  
Tutaj śpią konfiguracje systemowe (system-wide (czyli nie lokalne dla kazdego uzytkownika.)). np etc/apt mamy folder sources.list. 

**home** 
Tutaj każdy user ma swój własny foder do którego tylko on, bądz admin ma dostęp. Trzymane tu są też konfiguracje aplikacji danego użytkownika systemu. 

**lib /lib32/ lib64** 
Tu są biblioteki, z których korzystają programy z bin i sbin.  

**media** 
Znajdziemy tu nasze zamontowane dyski, płyty cd, pendrivy, dyski zewnetrznetrzne itp. Tam znajdzie się ekwiwalent dysku D;E; itp Rożni się od mnt tym że system sam tu montuje rzeczy.

**mnt** (*mount*)  
Znajdziemy tu nasze zamontowane dyski, płyty cd, pendrivy, dyski zewnetrznetrzne itp. Tam znajdzie się ekwiwalent dysku D;E; itp. montujemy tutaj nasze rzeczy manualnie. Punkt montowania innych niz natywne systemy plikow. 

**opt** (*optional*) 
tam najczesciej znajduje się ręcznie zainstalowane oprogramowanie od dostawców distro. Zawiera statycznie kompilowane aplikacje. 

**proc** 
Wirtualny system plików informujacy o stane systemu i poszczegolnych procesów, w większości pliki tesktowe. Tutaj znajdują sie sudo files. 

**root** 
folder domowy roota. Dostęp do tego folderu ma tylko root.  

**run**
Zawartość tego folderu jest w ramie. Używane jest przez procesy różne, a teraz także przezemnie od kiedy się o tym dowiedziałem. 

**snap** 
Tutaj są przetrzymywane pakiety snap (używa go głownie ubuntu). Są to samodzielne programy które działają z grubsza inaczej niż inne programy. (tam jest np gnome) 

**srv** (*service* *data*) 
Tutaj jest data która jest accessed by external users kiedy łączą się z np naszym serverem http. 

**sys**
Podobnie jak run, jest w ramie. Tak nasz sprzęt widzi kernel. Czyli wszystkie meta dane jak producent sprzetu, gdzie jest włożony, nic turbo istotnego. /sys to pudełko, /dev to content 

**tmp** (*temperorary*) 
pliki tymczasowe, jak np dokument tekstowy w trakcie jego pisania. 

**usr** (*user application space*) 
Tutaj śpią pliki aplikacji które będą używane przez użytkowników, znane tez jako UNIX system reasourse. Wszystkie aplikacje zainstalowane tam uznawane są za "non-essential" żeby system operował 

**var** 
Tutaj są files i aplikacje które potencjanie będą rosnąć, np procesy które scrashowały, logi systemu, maile .