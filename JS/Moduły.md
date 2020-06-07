## Moduły, importy i eksporty



W JS, jak we wszystkich innych językach programowania mamy możliwość reużywania komponentów, bibiliotek i tego typu cudeniek.

Moduły pozwalają na rozbicie kodu na mniejsze porcję czyli kod z jednego pliku będzie exportowany dzięki czemu będzie dostępny w drugim pliku poprzez importowanie go.

<img src="https://github.com/samanthaming/samanthaming.com/blob/master/images/tidbits/79-module-cheatsheet.jpg?raw=true" alt="79-module-cheatsheet.jpg"  />

### export

`default export` można popełnić raz na plik. 

```js
//export default
class User {
    constructor(name, age){
        this.name = name
        this.age = age
    }
}


function printName(user){
    console.log('User name is ${user.name}')
}

export default User 
// ^^^ możemy to zrobić również na początku zmiennej którą chcemy upublicznić. Tbh jest to troche jak dawanie zmiennej modyfikatora public.
export {printName}
```



