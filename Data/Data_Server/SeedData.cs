using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data_Server
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Sprawdź, czy baza danych zawiera już jakieś produkty
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new Product { Name = "Margarita", Description = "sos pomidorowy, ser, oregano", Price26cm = 23m, Price32cm = 26m, Price42cm = 35m },
                    new Product { Name = "Fungi", Description = "sos pomidorowy, ser, pieczarki, oregano", Price26cm = 22m, Price32cm = 26m, Price42cm = 33m },
                    new Product { Name = "Salami", Description = "sos pomidorowy, ser, salami, oregano", Price26cm = 23m, Price32cm = 28m, Price42cm = 35m },
                    new Product { Name = "Salerno", Description = "sos pomidorowy, ser, salami, pieczarki, oregano", Price26cm = 25m, Price32cm = 29m, Price42cm = 36m },
                    new Product { Name = "Rimini", Description = "sos pomidorowy, ser, szynka, oregano", Price26cm = 23m, Price32cm = 27m, Price42cm = 35m },
                    new Product { Name = "Cotto", Description = "sos pomidorowy, ser, szynka, salami, papryka", Price26cm = 26m, Price32cm = 29m, Price42cm = 37m },
                    new Product { Name = "Capricossa", Description = "sos pomidorowy, ser, szynka, pieczarki, oregano", Price26cm = 25m, Price32cm = 29m, Price42cm = 35m },
                    new Product { Name = "Tunna", Description = "sos pomidorowy, ser, tuńczyk, kukurydza, zap. Majonez, oregano", Price26cm = 27m, Price32cm = 30m, Price42cm = 37m },
                    new Product { Name = "Bella", Description = "sos pomidorowy, ser, oliwki, szynka, papryka, oregano", Price26cm = 24m, Price32cm = 29m, Price42cm = 35m },
                    new Product { Name = "Quatro Stragioni", Description = "sos pomidorowy, ser, tuńczyk, krab, krewetki, oregano", Price26cm = 29m, Price32cm = 33m, Price42cm = 38m },
                    new Product { Name = "Maestro", Description = "sos pomidorowy, ser, kurczak, cebula, papryka, ogórek, oregano", Price26cm = 28m, Price32cm = 31m, Price42cm = 36m },
                    new Product { Name = "Serowa", Description = "sos pomidorowy, ser, camembert, parmezan, cheddar, oscypek, oregano", Price26cm = 34m, Price32cm = 39m, Price42cm = 47m },
                    new Product { Name = "Hawajska", Description = "sos pomidorowy, ser, szynka, ananas, oregano", Price26cm = 26m, Price32cm = 29m, Price42cm = 37m },
                    new Product { Name = "Tropicana", Description = "sos pomidorowy, ser, szynka, ananas, banan, truskawka", Price26cm = 28m, Price32cm = 31m, Price42cm = 38m },
                    new Product { Name = "Favorita", Description = "sos, ser, szynka, salami, ogórek, pieczarki, kukurydza, oregano", Price26cm = 29m, Price32cm = 33m, Price42cm = 40m },
                    new Product { Name = "Corvo", Description = "sos, ser, szynka, boczek, pieczarki, oregano", Price26cm = 27m, Price32cm = 30m, Price42cm = 38m },
                    new Product { Name = "Vegetariana", Description = "sos, ser, pieczarki, papryka, kukurydza, brokuł, oregano", Price26cm = 28m, Price32cm = 33m, Price42cm = 41m },
                    new Product { Name = "Góralska", Description = "sos, ser, oscypek, boczek, szynka, cebula, pieczarki, oregano", Price26cm = 31m, Price32cm = 35m, Price42cm = 42m },
                    new Product { Name = "Chłopska", Description = "sos, ser, szynka, salami, boczek, kiełbasa, cebula, pieczarki, oregano", Price26cm = 32m, Price32cm = 36m, Price42cm = 43m },
                    new Product { Name = "Mięsna uczta", Description = "sos, ser, salami, boczek, kiełbasa, kabanos, oregano", Price26cm = 33m, Price32cm = 39m, Price42cm = 47m },
                    new Product { Name = "Mexicana", Description = "sos, ser, salami, boczek, jalapeno, wołowina, fasola czerwona, oregano", Price26cm = 30m, Price32cm = 37m, Price42cm = 44m },
                    new Product { Name = "Diaboli", Description = "sos, ser, salami, boczek, kabanos, cebula, pieprz cayenne, tabasco, oregano", Price26cm = 29m, Price32cm = 36m, Price42cm = 45m },
                    new Product { Name = "Americana", Description = "sos, ser, salami, cebulka prażona, pieczarki, oregano", Price26cm = 27m, Price32cm = 31m, Price42cm = 35m },
                    new Product { Name = "Qurito", Description = "sos, podwójny ser, podwójny kurczak, podwójna kukurydza, oregano", Price26cm = 37m, Price32cm = 44m, Price42cm = 52m },
                    new Product { Name = "BBQ", Description = "sos, ser, boczek, kurczak, cebula, sos BBQ, oregano", Price26cm = 27m, Price32cm = 34m, Price42cm = 43m },
                    new Product { Name = "Bianco", Description = "sos biały, ser, boczek, szynka, kukurydza, pomidorki koktajlowe, Zap. Majonez, cebula, oregano", Price26cm = 36m, Price32cm = 44m, Price42cm = 49m },
                    new Product { Name = "Uovo", Description = "sos, ser, camembert, boczek, jajko, oregano", Price26cm = 28m, Price32cm = 32m, Price42cm = 38m },
                    new Product { Name = "PARMA", Description = "sos, ser, szynka parmeńska, pomidorki koktajlowe, rukola, parmezan", Price26cm = 29m, Price32cm = 38m, Price42cm = 49m },
                    new Product { Name = "Rusticana", Description = "sos, ser, oliwki, rukola, pomidorki koktajlowe, parmezan, oregano", Price26cm = 27m, Price32cm = 34m, Price42cm = 42m },
                    new Product { Name = "Felicita", Description = "sos, ser, szynka, salami, pieczarki, papryka, kukurydza, ser w rancie", Price26cm = 38m, Price32cm = 47m, Price42cm = 51m },
                    new Product { Name = "Własna kompozycja", Description = "sos, ser, 5 dowolnych składników do wyboru", Price26cm = 38m, Price32cm = 45m, Price42cm = 51m }
                );

                context.Casseroles.AddRange(
                    new Casseroles { Name = "Ser, pieczarki", Prize = 11m },
                    new Casseroles { Name = "Ser, szynka, pieczarki", Prize = 13m },
                    new Casseroles { Name = "Ser, boczek, cebula", Prize = 14m },
                    new Casseroles { Name = "Ser, cebula, kurczak", Prize = 14m },
                    new Casseroles { Name = "Zapiekanka kebab", Prize = 17m }
                );


                context.Fries.AddRange(
                    new Fries { Name = "Małe", Prize = 9.5m },
                    new Fries { Name = "Duże", Prize = 12.5m },
                    new Fries { Name = "Małe z serem", Prize = 11.5m },
                    new Fries { Name = "Duże z serem", Prize = 13.5m },
                    new Fries { Name = "Krocone", Prize = 15.5m }
                );

                context.Burgers.AddRange(
    new Burgers { Name = "1. Hamburger", Description = "ogórek, pomidor, sałata, majonez, sos hamburgerowy", Price = 13m },
    new Burgers { Name = "2. Cheeseburger", Description = "ser, ogórek, pomidor, sałata, majonez, sos hamburgerowy", Price = 14m },
    new Burgers { Name = "3. Kentucky Burger", Description = "Sałata, pomidor, cebula, cheddar, kurczak Kentucky", Price = 23m },
    new Burgers { Name = "4. Burger Classic", Description = "wołowina, ogórek, pomidor, sałata, cebula, musztarda, ketchup", Price = 18m },
    new Burgers { Name = "5. Burger cheese and bacon", Description = "wołowina, cheddar, boczek, ogórek, pomidor, sałata, cebula, ketchup, majonez", Price = 22m },
    new Burgers { Name = "6. Burger na ostro", Description = "wołowina, cheddar, ogórek, pomidor, sałata, cebula, jalapeno, boczek, ketchup, majonez", Price = 27m },
    new Burgers { Name = "7. Burger extra", Description = "wołowina, ogórek, pomidor, sałata, cebula, cheddar, bekon, majonez, musztarda", Price = 30m },
    new Burgers { Name = "8. Burger BBQ", Description = "wołowina, sałata, pomidor, ogórek, cebula, cebula prażona, sos BBQ", Price = 31m },
    new Burgers { Name = "9. Tortilla Kentucky", Description = "Sałata, pomidor, rzodkiewka, cheddar, kurczak Kentucky", Price = 19m }
);

                context.Dinners.AddRange(
                    new Dinners { Name = "1. Nuggetsy z frytkami", Description = "sałatka lub sos", Price = 24m },
                    new Dinners { Name = "2. Stripsy z frytkami", Description = "sałatka lub sos", Price = 24m },
                    new Dinners { Name = "3. Pierogi ruskie z mięsem/ serem i owocami sezonowymi (12 szt.)", Description = "", Price = 23m }
                );

                context.Kebab.AddRange(
                    new Kebab { Name = "1. Kebab na talerzu", Description = "", Price = 28m },
                    new Kebab { Name = "2. Kebab w bułce", Description = "", Price = 23m },
                    new Kebab { Name = "3. Tortilla", Description = "", Price = 21m }
                );

                context.SaveChanges();
            }
        }
    }
}