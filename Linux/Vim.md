# Vim 

Mamy 2 głowne mody:
• insert
• normal

insert - aby go odpalic klikamy i
normal - aby do niego wyjsc, klikamy ESC

## Poruszane

W normal modzie  mozemy sie przemieszczac za pomoca klawiszy  h j k l gdzie:
• h to lewo
• j to dół
• k to góra
• l to prawo

Możemy się też poruszać słowami za pomocą klawiszy w b e gdzie:
• w zabiera nas na początek słowa (begin)
• e na koniec słowa (end)
• b na początek słowa ale do tyłu (backward)

Poruszanie można z łatwością łączyć z jakąś liczbą. Powiedzmy kobinacja (naciskamy po kolei, nie na raz)
3 l przesunie nasz kursor o 3 w prawo, 9 b o 9 słów do tyłu itd.

Aby przejść na sam początek linii, należy nacisnąć 0.

### Advanced

Możemy poruszać się też bezwzględnie. 

gg zabierze nas na samą górę dokumentu, a G na dół. 

kombinacją  CTRL+G możemy szybko podejżeć w jakiej linii obecnie się znajdujemy. 

Po wstukaniu <liczba>G zostaniemy przesienienie do wiersza numer <liczba> 

Aby wrócić tam gdzie sie było (np po wyszukiwaniu) warto użyć kombinacji:
• CTRL-O aby wrócić tam gdzie się było. 
• CTRL-I aby iść do przodu.

Aby znaleść domykający nawias naciskamy %

## Pisanie

Aby wejsc do insert mode należy wcisnąć I  badź a.

**i** - wstawi tekst przed kursorem.
**a|A** - wstawi tekst na koncu słowa | linii

Możemy również pisać jakąś fraze X razy. Wystarczy wstukać np **30i** **siema ESC** i naszym oczą ukarze się 30 razy napisane siema.

Aby cofnąć wprowadzone zmiany klikamy **u** (undo),
a **ctrl+R** aby powtórzyć.

## Wyszukiwanie

Za pomocą klawisza **f** możemy łatwo znaleść słowo zaczynające się na określoną literę. powiedzmy fq da nam najbliższe słowo rozpoczynajace się na **q**.

Możemy równiesz wyszukiwać za pomocą:
• **/** aby szukać do przodu
• **?** aby szukać do tyłu.

składnia wygląda:  / <wyszukiwana-fraza> ENTER

I teraz, jeżeli ta funkcja znajdzie sobie naszą frazę, możemy przewijać do następnego 'rekordu' w którym jest to określenie. Używamy do tego klawisza:
• n aby iść do kolejnej frazy
• N aby iść w przeciwnym kierunku do kolejnej frazy.


## Kopiowanie i wstawianie

Klawisz p odpowiada za wklejanie ostatniego usunięcia. (albo tego co skopiujemy) 

Aby zastąpić poprzedni znak należy użyć klawisza r (replace) 

Klawisz c (change) odpowiada za częściowe usuwanie i wstawianie kursora w to miejsce. Np jak chemy zmieni tylko część słowa, z Kotamina na Katarzyna to damy kursor na K i możemy użyć połączenia ce aby usunąć słowo do końca i pisac.

## Usuwanie

Możemy usuwać pojedynczy znak za pomocą klawisza x.
Całe słowa, bądź zadania można usuwać i zmieniać za pomocą d gdzie:
• d czeka na kolejny argument.
∘ dw usunie nam słowo | do początku nastęnego wyrazy WYŁĄCZAJĄC pierwszy znak.
∘ d$ będzie usuwać do do końca linii.
∘ de usunie do końca bieżącego wyrazu, razem z ostatnim znakiem.
∘ dd usunie nam całą linie.

## Korekta

Aby cofnąć się w czasie o jedno polecenie wystarczy nacisnąć **u**.

Aby cofnąć zmiany dla całej linii naciskamy **U**.

Aby cofnąć cofnięcia tak o klikamy **CTRL + R**

### Advanced

**:[% |  #1,#2]s/<słowo>/<słowo-2>/ [g] | [gc]**
Możemy wten sposób zamieniać jakieś słowo na inne słowo. 
flaga g oznacza zamienienie globalne, czyli zamieni wszystkie wystapienia <słowo> na <słowo-2> w całej linii.

Z flagą % zastąpimy wystąpienie w całym pliku

Znak **#** w tym wypadku sygnalizuje linie, więc użyte jako flagga będzie znaczyło, że zmienimy wystapienie danego ciągu znaków od linii #1 do linii **#2**

Flaga **gc** będziemy zastąpywać występienia, a z każdym wystąpeiniem zostaniemy zapytani czy na pewno chemy to zrobić.


## Dwukropek

**:q!** - wyjście bez zapisywania
**:q** - wyjście
**:w** - zapisanie

Z poziomu vima możemy wykonywać polecenia powłoki. w tym celu należy wpisać :! a następnie komendę powłoki, np ls

**:! <komenda-powłoki>**

