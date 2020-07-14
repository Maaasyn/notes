# Communication between components

Możemy to zrobić za pomocą dekoratora **@Input** . Jest do dekorator **pola** lub **właćiwości** naszej metody. 

Wtedy w naszym parrent component możemy inputować naszą wartość:

```html
<app-square [value]='"X"'></app-square>
// lub
<app-square value='X'></app-square>
```

W pierwszej opcji, wzięte będzie expression, więc możemy tam dac kod js'a.



jeżeli komponent nie ma żadnej logiki która pozwala mu modyfikować swój własny stan, nazywa się go **UI component** | **dumb**.



##### Parent => child

> via Input()

W naszym child-component robimy sobie zmienną i zdobimy ją dekoratorem **`@Input()`** . Brawo, można odpalać. 

```typescript
export class BoatComponent{
  @Input() boat: BoatModel;
}
```

Teraz z poziomu rodzica możemy wrzucić tam jakieś informacje. Rodzic, w swoim template, ma nasz komponent (po jego selektorze) **+**  do tego musi mieć atrybut z daną która ma być przekazana. 

```html
<app-boat [boat]="valueSendedFromParent"></app-boat>
```

I już, pole `valueSendedFromParent` zostało przypisane do pola `boat` w componencie child.

##### Child => parrent

> via @Output() and EventEmmiter

Ten sposób jest dobry kiedy chcemy udostepniać rodzicowi zmiany w takich rzeczach jak formularze, kliknięcia guzików, i inne eventy użytkownika. 

Tutaj jest troszkę trudniejsza sprawa. 

Najpierw w naszym childComponent musimy:



Zainicjalizować co chcemy wysyłać, do tego używamy dekoratora `@Output`;

Kolejnym korkiem jest naszemu polu z dekoratorem `@Output` zainicjalizować `EventEmmiter()`, jest to klasa przyjmująca argument generyczny. Finalnie, tworzymy metodę która wywoła nasz emitter, w tym przypadku jest to metoda `sendMessage()`;

```typescript
export class ChildComponent implements OnInit {

  message: string = 'Hola Mundo!';

  @Output() messageEvent = new EventEmitter<string>();

  sendMessage(){
    this.messageEvent.emit(this.message);
  }
```

rodzic:

```typescript
  receiveMessage($event){
    this.message = $event;
  }
```

```html
<app-child (messageEvent)="receiveMessage($event)"></app-child>
```

##### child => parent

> via ViewChild()

ViewChild pozwala jednemu komponentowi być wstrzyknięty w inny. umożliwiając rodzicowi dostep do wszystkich jego atrybutów i funkcji. wadą zaś będzie że childComponent nie będzie sprawny do póki widok nie będzie w pełni załadowany.

Dlatego bedziemy musieli dodać `AfterViewInit`., żeby odbierać dane z dziecka.

parent 

```typescript
export class ParentComponent implements AfterViewInit {
  message: string;

  @ViewChild(ChildComponent) child;


  ngAfterViewInit(): void {
this.message = this.child.message;
  }
```

Jak widzimy tutaj akurat porsta sprawa, bo działamy bezpośrednio na tym komponencie. 

##### any => any

> via a shared service

Używa się tej opcji kiedy przesyła się dane z niezależnych sobie komponentów. Rodzeństwa, wnuków itd. 

Kiedy potrzebne jest aby dane były ze soba zsynchronizowane, pomocny staje się `BehaviorSubject` od rxjs, zajmuje się on zapewnieniem że każdy komponent który konsumuje daną informację, dostaje najbardziej 'up-to-date' dane. 

W tym celu tworzy się service.

```typescript
import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable()
export class DataService {

  private messageSource = new BehaviorSubject<string>('default message');
  currentMessage = this.messageSource.asObservable();

  changeMessage(message: string){
    this.messageSource.next(message);
  }

```

następnie w componencie który subskrybuje ten observable.

```typescript
  constructor(private data: DataService) {
  }
//wstrzykujemy zaleznosc

ngOnInit(): void {    this.data.currentMessage.subscribe(message => this.message = message);
  }
```

Gdy później chcemy zmienić wartość w naszej wiadomości, możemy stworzyć taką o to wiadomość:

```typescript
newMessage(){
    this.data.changeMessage('Hello from Sibling');
}
```

