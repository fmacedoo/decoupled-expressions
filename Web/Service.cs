using System.Collections.Generic;

namespace Web
{
    public class Service : IService
    {
        private readonly IRepository _repository;

        public Service(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Model> GetAwesome()
        {
            return _repository.GetAllByNameContainsAwesome();
        }

        public IEnumerable<Model> GetNotAwesome()
        {
            return _repository.GetAllByNameNotContainsAwesome();
        }

        public IEnumerable<Model> NOT_COVERED_CONDITION_GetAwesome()
        {
            return _repository.GetAllBy(o => o.Name.Contains("awesome"));
        }

        public IEnumerable<Model> NOT_COVERED_CONDITION_GetNotAwesome()
        {
            return _repository.GetAllBy(o => !o.Name.Contains("awesome"));
        }
    }
}