# RazorPages

Wszystko co można w MVC można też w razor pages.

- Routingi
- Modele
- ActionResultes
- TagHelpers
- Itd

### Razor pages skladaja sie 2 części:

- Razor Page (UI/View)
- Page Model (Containst Handlers) - Troche taka kobinacja modeli, viewmodeli i controllerow.



Jezeli chcemy edytowac konfiguracje projektu wbijamy do: 

![image-20200315111039116](C:\Users\marcin\AppData\Roaming\Typora\typora-user-images\image-20200315111039116.png)

 i pojawi nam się okno konfiguracji, domyślnie takie:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
    <RootNamespace>aspnet_core_RazorPages_TutorialOdCiapura</RootNamespace>
  </PropertyGroup>


</Project>

```

Głównie obracamy się w folderze Pages.

Np W naszym `Index.cshtml` znajduje się również nasz swego rodzaju `controller`.  To tam definiujemy modele dla naszego projektu.

### Routing

Routing in aspnet razorpages mapuje URL z fizycznym plikiem na dysku. To z kolei wiąże się z tym że razor pages potrzeboje root foleru. Customowy folder możemy definiować w `Startup.cs`. 

Domyslnie, root to nasz `Pages` folder. 

`Index.cshtml` to domyślny folder. 

### Tag Helpers

Tag helpers pozwalaja nam na server-side code to paricipate in creaing and rendering HTML elements in Razor files.

tak wyglada przykladowy tag-helperk

```html
 <a class="nav-link text-dark" asp-area="" asp-page="/Index">Home</a>
```

> Te helpery to taki zamiennik dla HTML helperów z asp.net niżej zamieszcze przyklady.

```csharp
<!-- HTML Helper-->
@Html.Label("FirstName", "FirstName :" , new {@class = "form-control"})
    
<!-- Tag Helper-->
<label class="form-control" asp-for="FirstName"></label>
```

```csharp
<!-- HTML Helper-->
@Html.LabelFor(m => m.FirstName, new {@class = "form-control"})
    
<!-- Tag Helper-->
<label asp-for="FirstName" class="col-md-2 control-label"></label>
```

