# Service

Services mogą udzielać danych, niezależnie od tego gdzie są w component tree, service zawsze może być wstrzyknięty do tego komponentu, i będzie wtedy posiadał te same dane i funkcjonalności.

Pierwsze co widzimy po wykonaniu komendy

```
ng g s {nama}
```

```typescript
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClockService {

  constructor() { }
}
```

nasza klasa ma dekorator `@Injectable`, dzięki temu możemy tą klasę łatwo wstrzelić w konstruktorze naszych komponentów. 

arg `providedIn: 'root'` mówi nam, że ten service będzie globalnym singletonem, przrkazanym na roocie aplikacji. Innymi słowy klasa ta będzie inicjalizowana tylko raz, więc właściwości tej klasy się nie zmienią, nawet po zniknięciu komponentu, wciąż będzie mozna dojść do aktualnych wartości w servicie. 

`statefulService`- ten typ servicu ma jakieś dane które będą przekazywane między wieloma komponentami. 

`stateless`- ten typ udostępnia tylko metody, wiec w zasadzie jest mikro biblioteką metod pomocniczych, które programista chce użyć w różnych kompontentach żeby rozszerzyć ich funkcjonalność itp. 

Kiedy chcemy nasz service wstrzyknąć robimy to następująco:

```typescript
export class FooComponent implements OnInit {

  constructor(private clockService: ClockService) { }

  ngOnInit(): void {
    console.log(this.clockService.tick);
  }
```

Typescript działa tak, że jeżeli podamy zasięg zmiennej jako agument do konstruktora, to typescript niejawnie nam z tego stworzy ptywatne pole. Jeżeli natomiast chcemy używać danych z naszego servicu w np naszej templatce, koniecznym jest użycie składni `public`.