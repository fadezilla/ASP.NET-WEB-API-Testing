namespace ApiEfProject.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TicketPrice { get; set; }

        //forgein key
        public int GenreId { get; set; }
        //Navigation property
        public Genre? Genre { get; set; }
        //forgein key
        public int StudioId { get; set; }
        //Navigation property
        public Studio? Studio { get; set; }
        //Many movies have many Actors
        public virtual ICollection<MovieActor>? MovieActors { get; set; }
    }
}