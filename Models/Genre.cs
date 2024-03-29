namespace ApiEfProject.Models
{
    public class Genre
    {
        //constructor
        public Genre(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        //one Genre has many movies
        public ICollection<Movie>? Movies { get; set; }
    }
}