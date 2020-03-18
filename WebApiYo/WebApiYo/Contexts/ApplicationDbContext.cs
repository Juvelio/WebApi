using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYo.Models;

namespace WebApiYo.Contexts
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //var roleAdmin = new IdentityRole()
            //{ Id = "89086180-b978-4f90-9dbd-a7040bc93f41", Name = "admin", NormalizedName = "admin" };

            //builder.Entity<IdentityRole>().HasData(roleAdmin);


            //var roleOperador = new IdentityRole()
            //{ Id = "022459e6-d5b2-4ed1-bb9f-e5ba96649a6e", Name = "operador", NormalizedName = "operador" };
            //builder.Entity<IdentityRole>().HasData(roleOperador);

            //pagina Generador de GUID en línea (https://www.guidgenerator.com/online-guid-generator.aspx)

            base.OnModelCreating(builder);
        }
    }
}
