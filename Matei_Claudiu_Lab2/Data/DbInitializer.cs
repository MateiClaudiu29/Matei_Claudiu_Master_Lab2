using Microsoft.EntityFrameworkCore;
using Matei_Claudiu_Lab2.Models;
using System.Security.Policy;
using System.Drawing;

namespace Matei_Claudiu_Lab2.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                if(!context.Authors.Any())
                { 
                    context.Authors.AddRange(
                    new Author { FirstName = "Mihail", LastName = "Sadoveanu" },
                    new Author { FirstName = "George", LastName = "Calinescu" },
                    new Author { FirstName = "Mircea", LastName = "Eliade" });
                     context.SaveChanges();
                }

                context.Books.AddRange(
                    new Book { Title = "Baltagul", AuhorID = context.Authors.Single(author=>author.LastName=="Sadoveanu").ID, Price = Decimal.Parse("22") }, 
                    new Book { Title = "Enigma Otiliei", AuhorID = context.Authors.Single(author => author.LastName == "Calinescu").ID, Price = Decimal.Parse("18") }, 
                    new Book { Title = "Maytrei", AuhorID = context.Authors.Single(author => author.LastName == "Eliade").ID, Price = Decimal.Parse("27") });

                context.Customers.AddRange(
                    new Customer { Name = "Popescu Marcela", Adress = "Str. Plopilor, nr. 24", OrderDate = DateTime.Parse("1979-09-01") }, 
                    new Customer { Name = "Mihailescu Cornel", Adress = "Str. Bucuresti, nr.45, ap. 2", OrderDate = DateTime.Parse("1969-07-08") });

                context.SaveChanges();

                var orders = new Order[]
                {

                    new Order{BookID=1,CustomerID=1050,OrderDate=DateTime.Parse("2021-02-25")},
                    new Order{BookID=3,CustomerID=1045,OrderDate=DateTime.Parse("2021-09-28")},
                    new Order{BookID=1,CustomerID=1045,OrderDate=DateTime.Parse("2021-10-28")},
                    new Order{BookID=2,CustomerID=1050,OrderDate=DateTime.Parse("2021-09-28")},
                    new Order{BookID=4,CustomerID=1050,OrderDate=DateTime.Parse("2021-09-28")},
                    new Order{BookID=6,CustomerID=1050,OrderDate=DateTime.Parse("2021-10-28")}

                };

                foreach(Order e in orders)
                {
                    context.Orders.Add(e);
                }

                var publishers = new Models.Publisher[]
                {

                    new Models.Publisher{PublisherName="Humanitas",Adress="Str. Aviatorilor, nr. 40, Bucuresti"},
                    new Models.Publisher{PublisherName="Nemira",Adress="Str. Plopilor, nr. 35, Ploiesti"},
                    new Models.Publisher{PublisherName="Paralela 45",Adress="Str. Cascadelor, nr. 22, Cluj-Napoca"}

                };

                foreach (Models.Publisher p in publishers)
                {
                    context.Publishers.Add(p);
                }

                context.SaveChanges();

                var books = context.Books;

                var publishedbooks = new PublishedBook[]
                {
                    new PublishedBook { BookID = books.Single(c => c.Title == "Maytrei" ).Id, PublisherID = publishers.Single(i => i.PublisherName =="Humanitas").ID},
                    new PublishedBook { BookID = books.Single(c => c.Title == "Enigma Otiliei" ).Id, PublisherID = publishers.Single(i => i.PublisherName =="Humanitas").ID},
                    new PublishedBook { BookID = books.Single(c => c.Title == "Baltagul" ).Id, PublisherID = publishers.Single(i => i.PublisherName =="Nemira").ID},
                    new PublishedBook { BookID = books.Single(c => c.Title == "Fata de hartie" ).Id, PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID },
                    new PublishedBook { BookID = books.Single(c => c.Title == "Panza de paianjen" ).Id, PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID },
                    new PublishedBook { BookID = books.Single(c => c.Title == "De veghe in lanul de secara" ).Id, PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID}
                };

                foreach (PublishedBook pb in publishedbooks)
                {
                    context.PublishedBooks.Add(pb);
                }

                context.SaveChanges(); 

            }
        }
    }
}
