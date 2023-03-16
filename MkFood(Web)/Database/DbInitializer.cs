using MKFood.DB.Models;

namespace MkFood_Web_.Database
{
    public static class DbInitializer
    {
        public static void Initialize(MKFoodDbContext context)
        {
            //Create Db if Not Exist
            //if(context.Database.EnsureDeleted())
                context.Database.EnsureCreated();

        }
    }
}
