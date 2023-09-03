namespace AdminPageinMVC.Entity
{
    public class Lesson
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? VideoUrl { get; set; }

        public string? Information { get; set; }
        public Course Course { get; set; }
    }
}
