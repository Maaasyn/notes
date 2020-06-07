# Podsumowanie i ćwiczenie. 

**Cheatsheet -** [Building-Web-APIs-Cheat-Sheet.pdf](..\..\..\..\..\Kursy\Programowanie\c#\udemy\Mosh\The Complete ASP.NET MVC 5 Course\[Tutsgalaxy.com] - The Complete ASP.NET MVC 5 Course\06 Building RESTful Services with ASP.NET Web API\attached_files\071 Cheat Sheet\Building-Web-APIs-Cheat-Sheet.pdf) 

**Materiały pomocnicze -**  [Resolving-exception-for-updating-Id.pdf](..\..\..\..\..\Kursy\Programowanie\c#\udemy\Mosh\The Complete ASP.NET MVC 5 Course\[Tutsgalaxy.com] - The Complete ASP.NET MVC 5 Course\06 Building RESTful Services with ASP.NET Web API\attached_files\072 Exercise\Resolving-exception-for-updating-Id.pdf) 

1. Zbudować CRUD RestApi dla Movies. (Możliwe że będą problemy z ID. Wytłumaczone jest to w materialach pomocniczych.)

Problemy:

Ten framework do mappowania ma pare wad, na przykład kiedy mapujemy nasz ObjectDto do naszego Entity, pojawia się problem z Id, szczegółnie patknąłem się z tym problemem w chwili gdy chciałem wysłać `PUT` na wskazany przez siebie adres, i nie mogłem, bo nie wysyłałem Id. Tbh, dużo problemów potencjalnie stwarza ten automapper, trzrba będzie się mu przyjrzeć w wersji 9. 

Na chwilę obecną jednak mój problem będzie rozwiązany o tak:

W pliku `MappingProfiles.cs`

```csharp
//In MappingProﬁle:
CreateMap<Movie,MovieDto>().ForMember(m => m.Id,opt => opt.Ignore());	
  
//The same conﬁguration should be applied to mapping of customers:
CreateMap<Customer,CustomerDto>().ForMember(c => c.Id,opt => opt.Ignore());	
```

---

To nie działa jak powinno. lepszą metodą u mnie okazała się jakaś pajacera z mapowaniem w mojej klasie. Poniżej znajduje się kod mojej pajacery.

```csharp
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aspnet_vidly.Dtos;
using aspnet_vidly.Models;
using AutoMapper;

namespace aspnet_vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/movies
        [HttpGet]
        //IEnumerable<MovieDto>
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movies);
        }


        //GET api/movies/1
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie= _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = Mapper.Map<Movie, MovieDto>(movie);
            return Ok(movieDto);
        }

        //POST api/movies
        [HttpPost]
        public IHttpActionResult PostMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie =  Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        //PUT api/movies/1
        [HttpPut]
        public IHttpActionResult PutMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            movieDto.Id = id;
            Mapper.Map(movieDto, movie);
            _context.SaveChanges();

            return Ok(movieDto);
        }

        //DELETE api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);

            return Ok();
        }
    }
}

```

