namespace ApiEfProject.Models
{
    public class Review
    {
        //constructor
        public Review(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        //One review has many movies
        //foreign key
        public int MovieId { get; set; }
        //Navigation property
        public Movie? Movie { get; set; }
    }
}