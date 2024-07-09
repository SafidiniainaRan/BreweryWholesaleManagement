using BreweryWholesaleManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BreweryContext(
                serviceProvider.GetRequiredService<DbContextOptions<BreweryContext>>()))
            {
                if (context.Beers.Any() || context.Brewers.Any() || context.Wholesalers.Any() || context.WholesalerStocks.Any())
                {
                    return;
                }

                var brewers = new Brewer[]
                {
                new Brewer { Name = "BrewDog", CreatedAt = DateTime.Now },
                new Brewer { Name = "Stone Brewing", CreatedAt = DateTime.Now },
                new Brewer { Name = "Sierra Nevada", CreatedAt = DateTime.Now },
                new Brewer { Name = "Lagunitas", CreatedAt = DateTime.Now },
                new Brewer { Name = "Heineken", CreatedAt = DateTime.Now },
                new Brewer { Name = "Guinness", CreatedAt = DateTime.Now }
                };

                context.Brewers.AddRange(brewers);
                context.SaveChanges();

                var beers = new Beer[]
                {
                new Beer { Name = "Punk IPA", AlcoholContent = 5.6, Price = 3.5m, BrewerId = brewers[0].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Stone IPA", AlcoholContent = 6.9, Price = 4.5m, BrewerId = brewers[1].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Pale Ale", AlcoholContent = 5.6, Price = 4.0m, BrewerId = brewers[2].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Lagunitas IPA", AlcoholContent = 6.2, Price = 5.0m, BrewerId = brewers[3].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Elvis Juice", AlcoholContent = 6.5, Price = 3.8m, BrewerId = brewers[0].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Stone Ripper", AlcoholContent = 5.7, Price = 3.8m, BrewerId = brewers[1].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Hazy Little Thing", AlcoholContent = 6.7, Price = 4.2m, BrewerId = brewers[2].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Lagunitas DayTime", AlcoholContent = 4.0, Price = 4.7m, BrewerId = brewers[3].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Heineken", AlcoholContent = 5.0, Price = 2.5m, BrewerId = brewers[4].Id, CreatedAt = DateTime.Now },
                new Beer { Name = "Guinness Draught", AlcoholContent = 4.2, Price = 3.0m, BrewerId = brewers[5].Id, CreatedAt = DateTime.Now }
                };

                context.Beers.AddRange(beers);
                context.SaveChanges();

                var wholesalers = new Wholesaler[]
                {
                new Wholesaler { Name = "Distributor A", CreatedAt = DateTime.Now },
                new Wholesaler { Name = "Distributor B", CreatedAt = DateTime.Now },
                new Wholesaler { Name = "Distributor C", CreatedAt = DateTime.Now },
                new Wholesaler { Name = "Distributor D", CreatedAt = DateTime.Now },
                new Wholesaler { Name = "Distributor E", CreatedAt = DateTime.Now }
                };

                context.Wholesalers.AddRange(wholesalers);
                context.SaveChanges();

                var wholesalerStocks = new WholesalerStock[]
                {
                new WholesalerStock { WholesalerId = wholesalers[0].Id, BeerId = beers[0].Id, Quantity = 100, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[0].Id, BeerId = beers[1].Id, Quantity = 150, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[1].Id, BeerId = beers[2].Id, Quantity = 200, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[1].Id, BeerId = beers[3].Id, Quantity = 250, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[2].Id, BeerId = beers[4].Id, Quantity = 300, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[2].Id, BeerId = beers[5].Id, Quantity = 350, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[3].Id, BeerId = beers[6].Id, Quantity = 400, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[3].Id, BeerId = beers[7].Id, Quantity = 450, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[4].Id, BeerId = beers[8].Id, Quantity = 500, CreatedAt = DateTime.Now },
                new WholesalerStock { WholesalerId = wholesalers[4].Id, BeerId = beers[9].Id, Quantity = 550, CreatedAt = DateTime.Now }
                };

                context.WholesalerStocks.AddRange(wholesalerStocks);
                context.SaveChanges();
            }
        }
    }
}
