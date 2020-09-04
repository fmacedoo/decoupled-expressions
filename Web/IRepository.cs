namespace Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository
    {
        IEnumerable<Model> GetAllBy(Expression<Func<Model, bool>> condition);
    }
}
