using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class MovieDataContext:DbContext
    {
        public DbSet<MovieDetails> Movies { get; set; }

        static MovieDataContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MovieDataContext>());
        }
    }
}