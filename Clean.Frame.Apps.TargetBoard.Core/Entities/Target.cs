namespace Clean.Frame.Apps.TargetBoard.Core.Entities
{
    public class Target
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Score { get; set; }
        public string Progress { get; set; }
        public string? Message { get; set; }
        public int AspectId { get; set; }
        public virtual Aspect Aspect { get; set; }
    }
}