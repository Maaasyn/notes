# Co i jak

Angular to framework do robienia miedzy innymi SPA (single page applications). Strona generowana jest na bierzaco, za pomocą Javascriptu, po stonie użytkownika, przez co odciąża się serwer.

### CLI

```
ng serve
//uruchamia

ng lint
//sprawdza czy skladnia sie zgadza z angularowym standardem.

ng testy
//odpala wszystkie testy

ng generate
//generuje, np component
```



##### Versioning

* Najpierw był Angular 1, inaczej **AngularJS**. Miał problemy i źle się zestarzał, nie używa się go. 
* Angular 2, kompletnie przepisany AngularJS, działa w pełni inaczej. 
* Później był Angular 4,5 .. a obecnie mamy angular 9.  Trójkę skipnięto, bo tak. 

Co 6 miesięcy jest nowy update, który dodaje nowe featury. 



W angularze cały czas korzysta się z **npm** żeby dodawać pakiety itd.



W komponencie.ts mamy pole `selector`. 

```typescript
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
  })
```

Selektor to string który przetrzymuje insformację o tym jak nasz komponent będzie się nazywał, dla innych komponentów. 



**Maint.ts** to pierwszy plik który jest odpalany, później odpala się app.module.ts danych kompontentów.  

**`OnInit`** to tak zwany lifecicle hook co pozwala odpalić jakiś kod kiedy komponent jest inicjalizowany. 



### Componenty

To główna rzecz w angularze. Budujemy całą nasza aplikacje komponując ją z różnych komponentów, które robimy. Zaczynamy zwykle od root-componentu który tryma całą nasza aplikację w ryzach. Tam będą nasze nowe komponenty tworzone. Komponenty sa re-usable.

Komponenty tworzymy za pomocą angular cli. 



@`Component` przyjmuje obiekt który zawiera kilka pól. 

* **template/ templateUrl**- Jest to obowiązkowa część komponentu, w nim zawiera się cały html naszego komponentu. Może byc napisany od razu w komponencie, za pomocą backticks `` albo zdelifiowant gdzies indziej, wtedy używamy opcji z Url

* **styles/ stylesUrl**- w tym polu definiujemy nasz wygląd, pole to przyjmuje tablice stringów. Również możemy używać backticks albo Url.

* **selector**- ciekawa sprawa, możemy nasz komponent przekazywać do innych komponentów jako element html, albo atrybut albo klase, robimy to w ten sam sposób jak w cssie. 

  

  ```typescript
  @Component({
    selector: 'app-server',
      //albo
  selector : '[app-server]'
      //albo
  selector : '.app-server'
  ```

  wtedy odpowiednio musimy konsumowac nasz komponent w parent-komponencie. 





### Databinding

Jest to sposob komunikacji z templejtu z naszym typescriptem w komponencie. 

mamy kilka bindingów: 

Z Ts do komponentu:

* string interpolation `{{data}}` w naszym htmlu, binduje dane z ts do htmls

* property binding `[property]="data"` łączy właściwość elementu z jakąś daną z ts do html.  Mozna bindować w ten sposób rownież do dyrektyw jak i innych komponentów. 

  

Z komponentu do ts:

* event binding `(event)="expression"`

Oba:

* Two way binding `[(ngModel)]="data"`

W eventbinding możemy wysyłać informacje na temat eventu który się wykonał, np 

```html
<input
  class="form-control"
  id="serverName"
  type="text"
  (input)="onUpdateServerName($event)">
```

 twoWayBinding ma składnie banana in the box. Używa się tam dyrektyw.



### Directives

Dyrektywy to takie komponenty ale bez swojego html'a i cssa. Zaczepia się go do innego elementu, i on zmienia jego zachowanie.

Możemy robić swoje dyrektywy przez `ng g d {name}`

To instrukcje w DOM. Components to tego typu instrukcje. Są też dyrektywy bez templatek. 

```html
<p appTurnGreen>Receives a green background!</p>
```

Zwykle są to selektory atrybutów. 

Jedym z takich directiwów jest `*ngIf`

##### Structual Directives

```html
<p *ngIf="serverCreated">Serwer was created, server name is {{serverName}}</p>
```

local reference

```html
<p *ngIf="serverCreated; else noServer">Serwer was created, server name is {{serverName}}</p>
<ng-template #noServer><p>No server was created.</p></ng-template>
```

##### Attribute directives

<u>(nie maja gwiazdki)</u> te dyrektywy nie dodają, bądź usuwają elementów. Działają one tylko na elementach do których zostały dodane. 

```html
<p [ngStyle]="{backgroundColor: getColor()}">Server id {{serverId}} is {{getServerStatus()}}</p>

//html
```

```jsx
 getColor() {
    return this.serverStatus === 'online' ? 'green' : 'red';
  }
//ts
```

pętla

```tsx
<app-server *ngFor="let item of servers"></app-server>
```

W dyrektywie <u>*ngFor</u> można brac index interacji o tak:

```tsx
<div *ngFor="let logItem of log; let i = index"></div>
```

### Components lifecycle

W Angularze praktycznie nic nie robimy w naszych konstruktorach, poza wstrzykiwaniem zależności. A to dlatego że nie ma gwarancji że bindingi z właściwościami będą dostępne, dopoki nie odpali się OnInit.

W **`OnInit`** fetchuje się dane z api, albo ustawia się ractive form itp. 

### Pipes

Aby stworzyc pipe piszemy w konsoli:

```
ng g pipe {name}
```

### Modules

Unikatową dla angulara cechą są ngModules.

Umożliwiają pakowanie swoich komponentów i używanie ich współdzielenie ich w ramach aplikacji w zorganizowany sposób. Mają one wpływ na performance naszego kodu, gdyż umożliwiają między innymi lazy loading.

```typescript
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class CoolModule { }

```

W środku modułu znajdziemy dekorator.

W `declarations` rejestrujemy komponenty które będą używane w tym module.  Jeżeli stworzymy komonent wewnątrz folderu naszego modułu, z automatu zostanie on dodany do deklaracji. 

Aby używać naszego komponentu gdzieś indziej, musimy go dodać do tych deklaracji, jak również aby móc używać całego modułu w naszej aplikacji, musimy go dodać do `app.module.ts`.  Defaultowo nasz moduł ma wszystkie delkaracje prywatne, więc aby go użyć, koniecznym jest go exportować, w tym celu w naszym dekoratorze dodajemy pole exports. o tak:

```typescript
@NgModule({
  declarations: [FooComponent],
    exports: [FooComponent],
  imports: [
    CommonModule
  ]
})
```

W skrócie:

* **declaration**- są dla komponentów zdefiniowanych w module.
* **import**- dla komponentów które chcesz dodać do modułu.
* **export**- sa dla komponentów ktore chcesz uzywać z innymi modułami

### Debugging

W `devtoolsach/source/main` możemy debugować nasz kod, bo webpack jakoś magicznie nam go łączy, że ze zwykłego js'a mamy ts. Angular supports sources maps, który mapuje jedno z drugim.



Właściwy dostep do naszego kodu znajdziemy w `webpack://`

**Augury** to extension które można dodać sobie do chroma, i pozwala na analize stanu, widzieć jego elementy, injection graphys, itp. Możemy widzieć też routy, moduły które używamy itd. 

### Routes

W pliku `app-routing.module.ts` możemy ustawic routing dla naszyh komponentów. Routing to nic innego jak oznaczenie, że dla danej ścieżki **URL** , że ma w to miejsce wrzucić komponent. 

Dla przykładu: 

```tsx
const routes: Routes = [
  {path: '', component: HomeComponent}
];
```

