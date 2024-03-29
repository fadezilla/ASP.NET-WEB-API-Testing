namespace ApiEfProject.Models
{
    public class Actor
    {
        public Actor(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        //one Actor has many movieActors (many to many)
        public virtual ICollection<MovieActor>? MovieActors { get; set; }
        
    }
}