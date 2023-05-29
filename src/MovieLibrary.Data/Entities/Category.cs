using System.Collections.Generic;

namespace MovieLibrary.Data.Entities
{
    public class Category
    {
        public Category()
        {
            MovieCategories = new List<MovieCategory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MovieCategory> MovieCategories { get; set; }
    }
}
