using Microsoft.EntityFrameworkCore;

namespace Crs.Data
{
    public  interface ICrsContext
    {
        DbSet<UserDb> Users { get; set; }
        DbSet<RoleDb> Roles { get; set; }
        DbSet<CarDb> Cars { get; set; }
        DbSet<UserCarDb> UserCars { get; set; }
        DbSet<OrderDb> Orders { get; set; }
        DbSet<NewsDb> News { get; set; }
        int SaveChanges();
    }
}