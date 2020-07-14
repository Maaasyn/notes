# Pliki

`tslint.json` - zasady składni, sprawdza czy dobrze piszemy, daje nam to intelsensa.

`tsconfig.json`- Tutaj są zasady w jaki sposob ma byc kompilowany nasz kod na vanila js.

`package.json`- tutaj sa zainstalowane nasze pakiety, zwykle nic tutaj nie robimy bo instalujemy je za pomoca narzedzi, mamy tam różne dependencies, zależnie od stanu naszej aplikacji (produkcja, testy, development).

`karma.conf.js`- konfiguracje testów.

`browserslist`- customowe settingsy dla okreslonych przegladarek.

`angular.json`- ten plik kontroluje zachowanie naszego CLI. Zwykle sie tego nie modyfikuje. 

`.gitignore`- ignorowanie okreslonych plikow

`node_modules`- wszystkie nasze dependencies, nic nie modyfikujemy tutaj. 

`e2e`- end to end testy.

`dist`- tutaj jest kompilowany nasz kod zanim wrzucimy go na jakis hosting, ta bibioteka jest cały czas nadpisywana.

`src`- tutaj pracujemy i piszemy nasz kod. 

* `main.ts`- tutaj nasza aplikacja jest inicjowana 
* `index.html`- to jest shell naszej strony, pierwsza rzecz która wyświetla się użytkownikowi. Czesto daje się tu czcionki itd.
* `styles.scss`- gdy wrzucimy tutaj nasze klasy itd będa one dostepne w przestrzeni calego naszego projektu.
* `environments`- tutaj są nasze customowe ustawienia dla dla róznych środowisk.
* `assets`- statyczne pliki, zdjęcia, itd.
* `app`- tutaj bedziemy pisać cały nasz kod. tutaj tez CLI będzie generowalo nasze komponenty i dyrektywy.