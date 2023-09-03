namespace AdminPageinMVC.Entity
{
    public class Result
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public Education Education { get; set; }
        public User User { get; set; }
    }
}
