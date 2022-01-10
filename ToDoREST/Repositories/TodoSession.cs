using NHibernate;

namespace ToDoREST
{
    public class TodoSession : ITodoSession
    {
        private readonly NHibernate.ISession _session;
        private ITransaction _transaction;

        public TodoSession(NHibernate.ISession session)
        {
            _session = session;
        }

        public IQueryable<Todo> Todos => _session.Query<Todo>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task Save(Todo entity)
        {
            await _session.SaveOrUpdateAsync(entity);
            await _session.FlushAsync();
        }

        public async Task Delete(Todo entity)
        {
            await _session.DeleteAsync(entity);
            await _session.FlushAsync();
        }
    }
}
