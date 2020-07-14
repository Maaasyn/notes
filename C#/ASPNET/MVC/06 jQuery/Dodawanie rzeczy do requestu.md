# Dodawanie rzeczy do requestu

Czasem  trzeba dodac dodatkowe obiekty z naszej bazy do naszej odpowiedz. W takim wypadku musimy:

1. Zincludować tablice która chccemy `Eager loadować.`

```csharp
 // GET /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer,CustomerDto>);
        }

```

> Robimy to za pomocą metody Include.



1. Z racji że chcemy aby nasze Dto były kompletnie niezależne od naszych modeli tworzymy nowy dto, reprezentujacy zbiór który chcemy uwzględnić.

```csharp
namespace aspnet_vidly.Dtos
{
    public class MembershipTypeDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
```

Oraz w klasie zależnej od naszego Dto, dodajemy nasz nowo zdeciniowany Dto.

```csharp
        public MembershipTypeDto MembershipType { get; set; }
```

Już prawie jesteśmy w domu. Jeszcze tylko

3. Dodajemy konfiguracje naszego mapowania w mappingProfile.

```csharp
          CreateMap<MembershipType, MembershipTypeDto>();
          CreateMap<MembershipTypeDto, MembershipType>();
```

