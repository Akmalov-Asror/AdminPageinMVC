namespace AdminPageinMVC.Entity
{
    public class TaskAnswer
    {
        public int Id { get; set; }

        public string? Answer { get; set; }
        public Task Task { get; set; }
    }
}
