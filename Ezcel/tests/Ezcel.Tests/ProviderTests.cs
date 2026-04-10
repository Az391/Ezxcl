using Ezcel.Providers.Factory;
using NUnit.Framework;

namespace Ezcel.Tests
{
    [TestFixture]
    public class ProviderTests
    {
        [Test]
        public void TestProviderFactoryCreation()
        {
            var factory = new ProviderFactory();
            Assert.IsNotNull(factory);
        }

        [Test]
        public void TestGetAllProviders()
        {
            var factory = new ProviderFactory();
            var providers = factory.GetAllProviders();
            Assert.IsNotNull(providers);
            Assert.Greater(providers.Count, 0);
        }
    }
}