using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.Models;
using Data.Data_Server;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MenuPage.Pages
{
    public class SecondPageModel : PageModel
    {
        private readonly AppDbContext _context;

        public SecondPageModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> PizzaMenu { get; set; }

        public List<Burgers> BurgerMenu { get; set; }

        public List<Dinners> DinnersMenu { get; set; }

        public List<Kebab> KebabMenu { get; set; }

        public List<Casseroles> CasserolesMenu { get; set; }

        public List<Fries> FriesMenu { get; set; }
        public async Task OnGetAsync()
        {
            // Pobierz wszystkie produkty z bazy danych
            var products = await _context.Products.ToListAsync();

            // Filtruj produkty w pamiêci, aby znaleŸæ tylko te, które maj¹ "Pizza" w nazwie
            PizzaMenu = products.ToList();

            BurgerMenu = await _context.Burgers.ToListAsync();

            DinnersMenu = await _context.Dinners.ToListAsync();

            KebabMenu = await _context.Kebab.ToListAsync();

            CasserolesMenu = await _context.Casseroles.ToListAsync();

            FriesMenu = await _context.Fries.ToListAsync();
        }
    }
}
