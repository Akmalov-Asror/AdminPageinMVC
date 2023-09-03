namespace AdminPageinMVC.Entity
{
    public class Review
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public Education Education { get; set; }

        public User User { get; set; }
    }
}
