# RestApi 

Kontrollery api tworzymy w ten sam sposob w jaki robilimy zwyke, z tą różnicą że wybieramy Opcje `Web Api.`

Kiedyś trzeba było dodawać dependencies do Global.axax, teraz najwidoczniej nie trzeba bo miałem inną odpowiedź na żądanie utworzenia api kontrolera niż mosh. 

Okej. Gety możemy stworzyć bardzo łatwo, tak samo posty. W domyśle, frame work zrozumie że:

```csharp
public Customer PostCustomer(Customer customer)
```

tworzy nam dany zasób, ale nie jest to optymalna nazwa, gdyż jeżeli bedziemy refaktoryzować nasz kod, na powiedzmy `CreateCustomer` to ta akcja nie będzie już `Postem`. Lepszym nawykiem jest nazywanie naszych postów jawnie i czytelnie.

```csharp
//POST /api/customer/1
[HttpPost]
public Customer PostCustomer(Customer customer)
{...}
```

Mamy kilka przyjętych konwencji i kodów których się używa do komunikacji serwera, np słynny kod `404` oznacza brak zasobów. Z kolei `200` oznacza że wszystko wporzo. 



Konwencją przy putach i postach jest aby albo zwracać to co zostało zmienione, w naszym przypadku to Customer, albo nic nie zwracać.



Kiedy do naszego `HttpPost` zostanie przekazany zły argument, wywalamy kod `400`

Tak samo jak dostaniemy zły argument w `HttpPut` to slemy `400`- `HttpStatusCode.BadRequest`



Kiedy w `PUT` albo `DELETE` zasobu nie znajedziemy, wywalamy `404` , `HttpStatusCode.NotFound`.

Cały kod przykładowego prostego api.

```csharp
 public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //GET /api/customer/1
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        //POST /api/customer/1
        [HttpPost]
        public Customer PostCustomer(Customer customer)
        {
            if ((!ModelState.IsValid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        [HttpPut]
        public Customer PutCustomer(int id,Customer customer)
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

            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.Birthday= customer.Birthday;
            customerInDb.Name= customer.Name;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            _context.SaveChanges();
            return customer;
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
```



Żeby w ogóle mieć jaki kolwiek kontakt z routingiem `api` dodajemy do naszego Application_Start() w Global.asax.cs

```csharp
            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            });
```

