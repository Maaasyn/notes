## Eventy, EventHandlery i inne szmery bajery.

---

Eventy są proste jak budowa cepa. Jakiś obiekt, na przykład Jaś chce dać znać innym obiektom o swoim stanie, dla przekładu: 

> Jaś jest zdrowy. Jaś zachorował. Jaś informuje mamę o swoim stanie, natychmiast jak zachoruje.

W tym wypadku Jaś jest sobie tzw publisherem, on publikuje innym obiektom swój stan,

Mama natomiast jest tak zwannym subskrybentem, osoba zainteresowaną stanem Jasia. Zakodujmy to więc.

```csharp
public class Jaś
{
    public bool CzyZdrowy {get;set;} = true;
    //inicjalizujemy od razu zmienną bool na true, bo defaultowo jest gość zdrowy.
    
    public event EventHandler<EventArgs> ChorobaEvent;
    //Stworzony został przez nas eventHandler. Jak będzie się coś działo to on będzie dawał znać subskrybentom.
    
    public void Zachoruj()
    {
        CzyZdrowy = false;
        ChorobaEvent.Invoke(this ,EventArgs.Empty); 
        //Tu Jasiek nadaje wiadomość do wszystkich subskrybentów.
        //Metoda ta przyjmuje 2 argumenty, jeden to jest sender, dlatego tutaj jest this (jako ze interesuje nas ten konkretny egzemplarz Jasia, oraz przyjmuje Obiekt klasy EventArgs, w tym wypadku nie przekazujemy żadnych dodatkowych informacji, więc przekażemy statyczny obiekt EventArgs.Empty)
    }
    
}
```

Także, jak mamy już naszego Publiszera, przyszła kolej na subscrybeta.

```csharp
public class Mama
{
    public void KopsnijSyrop(object o, EventArgs e) 
    {
        
        //klasa która chce mieć info od publishera musi miec metode która ma takie same argumenty.
    }
}
```

No i dodajemy do naszego EventHandlera naszą drogą mamę.

```csharp
class Program
{
    static Main (param string[]) 
    {
    var Janek = new Jas();
    var Mamusia = new Mama();
        
    Janek.ChorobaEvent += Mamusia.KopsinjSyrop;
    }
}
```

I tym pięknym sposobem mamy nasz event.

