namespace ToDoREST
{
    public interface ITodoSession
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(Todo entity);
        Task Delete(Todo entity);

        IQueryable<Todo> Todos { get; }
    }
}