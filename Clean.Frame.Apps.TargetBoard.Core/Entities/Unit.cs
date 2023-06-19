namespace Clean.Frame.Apps.TargetBoard.Core.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  ICollection<MainBoard> MainBoards { get; set; } 
    }
}
