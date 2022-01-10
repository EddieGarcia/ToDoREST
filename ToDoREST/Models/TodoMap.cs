using NHibernate;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ToDoREST
{
    public class TodoMap : ClassMapping<Todo>
    {
        public TodoMap()
        {
            Id(x => x.Id, m => {
                m.Generator(Generators.Guid);
                m.Type(NHibernateUtil.Guid);
                m.Column("Id");
                m.UnsavedValue(Guid.Empty);
            });
            Property(x => x.Title, m => m.Length(1000));
            Property(x => x.Summary, m => m.Length(MsSql2000Dialect.MaxSizeForLengthLimitedString + 1));
        }
    }
}
