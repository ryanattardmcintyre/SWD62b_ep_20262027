using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Steps to create a migration class (which is needed to create/update the database)
 * 
 * 1. See that you have no errors after compilation
 * 2. Make the website as the Startup project
 * 3. Open Package Manager console
 * 4. Set the Default Project to where the context class is within the Package Manager Console window
 * 5. type: add-migration "InitialMigration" -context [press tab] LibraryDbContext
 * 6. Hit Enter
 * 7. you check your generated Migration class, which is created in DataAccess > Migrations
 * 8. type: update-database -context [press tab] LibraryDbContext
 * 
 * note: if you need to update the database because you decided to add a new property, then you have to 
 *       re-execute from step 5 onwards after building and checked that there are no errors
 */ 






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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //applying LazyLoading to activate the use of Navigational Properties
            //e.g. Category because without it you will get NULL
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
