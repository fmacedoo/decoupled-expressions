namespace Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using FizzWare.NBuilder;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MoqMeUp;
    using Neleus.LambdaCompare;
    using Web;

    [TestClass]
    public class ServiceTest : MoqMeUp<Service>
    {
        private readonly IService _service;

        public ServiceTest()
        {
            _service = this.Build();
        }

        [TestMethod]
        public void ModelConditions_ByNameContainsAwesome()
        {
            var models = Builder<Model>
                .CreateListOfSize(5)
                .TheFirst(3)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = Faker.Company.Name())
                .TheNext(1)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = $"{Faker.Company.Name()} awesome")
                .TheNext(1)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = $"The awesome {Faker.Company.Name()}")
                .Build();

            this.Get<IRepository>()
                .Setup(o => o.GetAllBy(It.IsAny<Expression<Func<Model, bool>>>()))
                .Returns(models);

            var awesomeModels = models
                .Where(ModelConditions.ByNameContainsAwesome().Compile());

            Assert.AreEqual(2, awesomeModels.Count());
        }

        [TestMethod]
        public void ModelConditions_ByNameNotContainsAwesome()
        {
            var models = Builder<Model>
                .CreateListOfSize(5)
                .TheFirst(3)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = Faker.Company.Name())
                .TheNext(1)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = $"{Faker.Company.Name()} awesome")
                .TheNext(1)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = $"The awesome {Faker.Company.Name()}")
                .Build();

            this.Get<IRepository>()
                .Setup(o => o.GetAllBy(It.IsAny<Expression<Func<Model, bool>>>()))
                .Returns(models);

            var awesomeModels = models
                .Where(ModelConditions.ByNameNotContainsAwesome().Compile());

            Assert.AreEqual(3, awesomeModels.Count());
        }

        [TestMethod]
        public void Service_GetAwesome_IsCalled()
        {
            var awesomeModels = _service.NOT_COVERED_CONDITION_GetAwesome();

            this.Get<IRepository>().Verify(o => 
                o.GetAllBy(
                    It.Is<Expression<Func<Model, bool>>>(condition => 
                        Lambda.Eq(condition, ModelConditions.ByNameContainsAwesome())
                    )
                ),
                Moq.Times.Once());
        }

        [TestMethod]
        public void Service_NOT_COVERED_CONDITION_GetAwesome()
        {
            var models = Builder<Model>
                .CreateListOfSize(5)
                .TheFirst(3)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = Faker.Company.Name())
                .TheNext(1)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = $"{Faker.Company.Name()} awesome")
                .TheNext(1)
                .With(o => o.Id = Guid.NewGuid())
                .And(o => o.Name = $"The awesome {Faker.Company.Name()}")
                .Build();

            this.Get<IRepository>()
                .Setup(o => o.GetAllBy(It.IsAny<Expression<Func<Model, bool>>>()))
                .Returns(models);

            var awesomeModels = _service.NOT_COVERED_CONDITION_GetAwesome();

            Assert.IsNotNull(awesomeModels);
            Assert.AreEqual(5, awesomeModels.Count());
        }
    }
}
