# Arrow functions

Składnia funkcji w jsie.

```js
function myFnc(){
...
}
```

Składnia funkcji po wrowadzeniu arrow funcion (poprzednia dalej istnieje, jest natomiast mniej używana)

```javascript
const myFnc = () => {
...
}
```

Dzieki temu, unikamy `this` który był problematyczny bo dynamicznie się zmieniał. Obecnie this będzie przyporządkowane do zmiennej która przechowuje funkcje (jak mniemam). 

### Składnia arrow functions

> Składnia funkcji nie przyjmującej żadnych argumentów. 

```js
const greet = () => console.log("Hello");

//po prostu puste nawiasy
```



> Dla jednego argumentu

```js
const myFnc = number => number * 2;
//Jednoliniowiec, nie musieliśmy pisać słowa return, oraz przyjmujemy tylko jeden argument.

//lub
const myFnc => (number) => numbere *2;

//lub
const myFnc = (number) => {
    return number * 2;
}
//To samo co wyżej ale nie co inna składnia
```

> Dla wielu argumentów

```js
const printXTimes (word, x) => {
    for(let i = 0; i < x; i++){
            console.log(word);
        }
}
```

