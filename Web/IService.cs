using System.Collections.Generic;

namespace Web
{
    public interface IService
    {
        IEnumerable<Model> NOT_COVERED_CONDITION_GetAwesome();
        IEnumerable<Model> NOT_COVERED_CONDITION_GetNotAwesome();
        IEnumerable<Model> GetAwesome();
        IEnumerable<Model> GetNotAwesome();
    }
}