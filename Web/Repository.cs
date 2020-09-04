namespace Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository : IRepository
    {
        private IList<Model> _collection;

        public Repository() : this(new List<Model>()) {}
        
        public Repository(IList<Model> collection)
        {
            _collection = collection;
        }

        public IEnumerable<Model> GetAllBy(Expression<Func<Model, bool>> condition)
        {
            return _collection.Where(condition.Compile());
        }
    }
}
