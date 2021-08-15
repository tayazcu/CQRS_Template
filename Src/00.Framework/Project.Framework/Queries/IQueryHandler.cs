using Project.Framework.DependencyInjection;

namespace Project.Framework.Queries
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}
