namespace ApiEfProject.Models
{
    public class Studio
    {
        //constructor
        public Studio(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        //one studio has many movies
        public ICollection<Movie>? Movies { get; set; }
    }
}