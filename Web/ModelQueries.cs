namespace Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class ModelQueries
    {
        public static Expression<Func<Model, bool>> ByNameContainsAwesome() =>
            x => x.Name.Contains("awesome");

        public static Expression<Func<Model, bool>> ByNameNotContainsAwesome() =>
            x => !x.Name.Contains("awesome");

        public static IEnumerable<Model> GetAllByNameContainsAwesome(this IRepository source) => source
            .GetAllBy(ByNameContainsAwesome());

        public static IEnumerable<Model> GetAllByNameNotContainsAwesome(this IRepository source) => source
            .GetAllBy(ByNameNotContainsAwesome());
    }
}