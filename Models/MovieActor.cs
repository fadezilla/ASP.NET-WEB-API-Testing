namespace ApiEfProject.Models
{
    public class MovieActor
    {
        public MovieActor(int movieId, int actorId)
        {
            this.MovieId = movieId;
            this.ActorId = actorId;
        }

        public Movie? Movie { get; set; }
        public int MovieId { get; set; }
        public Actor? Actor { get; set; }
        public int ActorId { get; set; }
    }
}