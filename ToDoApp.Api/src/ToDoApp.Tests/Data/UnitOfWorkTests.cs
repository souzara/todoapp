using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using ToDoApp.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace ToDoApp.Tests.Data
{
    public class UnitOfWorkTests : UnitTestsBase
    {
        private readonly IConfigurationRoot configurationRoot;

        public UnitOfWorkTests()
        {
            OverrideRegistration(x => Substitute.For<IConfigurationRoot>());
            configurationRoot = InstanceOf<IConfigurationRoot>();
        }
        [Fact]
        public void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                InstanceOf<IUnitOfWork>();
            });
        }
        [Fact]
        public void ShouldNotThrowAnyException()
        {
            #region mock
            var configSection = Substitute.For<IConfigurationSection>();
            configSection.Value.Returns("Data Source=serverfake;Initial Catalog=TodoAppDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True");
            configurationRoot.GetSection(Arg.Is("ConnectionString")).Returns(configSection);
            #endregion
            var uow = InstanceOf<IUnitOfWork>();
            Assert.NotNull(uow);

        }
        [Fact]
        public void ShouldThrowArgumentNullExceptionInBeginMethod()
        {
            #region mock
            var configSection = Substitute.For<IConfigurationSection>();
            configSection.Value.Returns("Data Source=serverfake;Initial Catalog=TodoAppDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True");
            configurationRoot.GetSection(Arg.Is("ConnectionString")).Returns(configSection);
            #endregion
            var uow = InstanceOf<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>("repositories", () =>
            {
                uow.Begin();
            });
            Assert.Throws<ArgumentNullException>("repositories", () =>
            {
                uow.Begin(null);
            });
        }
    }
}
