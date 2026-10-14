using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    //IdentityDbContext is a built-in context class containing
    //tables which will automatically manage user data

    //In other words the LibraryDbContext class is an abstraction of the database
    public class LibraryDbContext: IdentityDbContext<IdentityUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        //In a context class you may define additional/optional stuff
        //such as lazy loading, to apply an additional constraint,
        //to generate automatically a guid F3179A8B-CD2F-41D8-8158-34306C69255D and store it in specific fields upon creation/update
    }
}
