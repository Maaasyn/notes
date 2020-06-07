# Testing api

Okej. Jeżeli jawnie nie pokazemy że oczekujemy odpowiedzi Json, to otrzymamy response jako application/xml.

Będziemy przy testowaniu naszych endpointow używać aplikacji zwanej `Postman` która mnie wkurwia bo zamiast być młotkiem, stara się być i piłą, wiertarką, stugaczką deszczownicą i szczoteczką do zębów.

Wklepujemy w nim nasz endpoint i strzelamy w niego.

Ogólnie, na chwilę obencą nasze klasy w kontrolerze przyjmują jako argument obiekt który jest naszą encją. Jest to nie właściwe zachowanie, bo może prowadzić do potencjalnych szkód w naszym systemie. Lepszym rozwiązaniem jest używanie tak zwanych `dto` *(data transfer object)* 

### Automapper:

To convention based mapping tool bo mapuje ze soba zmienne na podstawie ich nazw czy coś takiego.

```bash
PM> install-package automapper -version:4.1
```

Potem w `App_start` tworzymy nową klasę, np `MappingProfile`

```csharp
namespace aspnet_vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Customer, CustomerDto>();
        }
    }
}
```

A w `Global.asax.cs` musimy wywołać naszą metodę przy starcie.

```csharp
namespace aspnet_vidly
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            ...
```

### Automapper 

Automapper to skomplikowana sprawa. Wrzucam caly kod z mojego kontrolera.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aspnet_vidly.Dtos;
using aspnet_vidly.Models;
using AutoMapper;

namespace aspnet_vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }

        //GET /api/customer/1
        [HttpGet]
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Mapper.Map<Customer, CustomerDto>(customer);
        }

        //POST /api/customers
        [HttpPost]
        public CustomerDto PostCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
        }

        [HttpPut]
        public void PutCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }

        //DELETE api/customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}

```

### Konfiguracja webAPI aby zwracalo json object pisany camel notation

> Ja nie miałem tego pliku, więc musiałem go sobie stworzyć. 
>
> app_start/ WebApiConfig.cs

```csharp
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace aspnet_vidly.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            GlobalConfiguration.Configure(config =>
            {
                var settings = config.Formatters.JsonFormatter.SerializerSettings;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.Formatting = Formatting.Indented;

                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new
                    {
                        id = RouteParameter.Optional
                    }
                );
            });

        }
    }
}
```

### IHttpActionResult

Zwykle chcemy zwracać przy poście kod zwrotny 201, więc nalezy zamiast zwracać obiekt `customerDto` to `IHttpActionResult`. Według konwencji RestFullApi przy udanym utworzeniu jakiegos obiektu, powinniśmy również zwrócić URI nowego obiektu. 