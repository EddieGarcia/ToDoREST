namespace ToDoREST
{
    public class Todo
    {
        public virtual Guid? Id { get; set; } 
        public virtual string? Title { get; set; }
        public virtual string? Summary { get; set; }
    }
}
