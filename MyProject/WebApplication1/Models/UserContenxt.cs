

using System.Data.Entity;

namespace MyWebApi.Models
{
    public class UserContenxt : DbContext
    {
        public UserContenxt():
            base("UserContenxt")
        {

        }

        public DbSet<User> User { get; set; }
    }
}