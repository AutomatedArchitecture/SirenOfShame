using NUnit.Framework;
using TfsRestServices;

namespace SirenOfShame.Test.Unit.Services
{
    [TestFixture]
    public class TfsRestServiceTest
    {
        [Test]
        public void GivenTfsUrlWhoseSubdomainEqualsTheProjectCollection_WhenSubstituteName_ThenResultIsDefaultCollection()
        {
            var substituteName = TfsRestService.SubstituteName("http://myprojectcollection.tfsonline.com", "myprojectcollection");
            Assert.AreEqual("DefaultCollection", substituteName);
        }

        [Test]
        public void GivenTfsUrlWhoseSubdomainDiffersFromProjectCollection_WhenSubstituteName_ThenResultIsProjectCollection()
        {
            var substituteName = TfsRestService.SubstituteName("http://myprojectcollection.tfsonline.com", "OtherProjectCollection");
            Assert.AreEqual("OtherProjectCollection", substituteName);
        }

        [Test]
        public void GivenNonDnsTfsUrl_WhenSubstituteName_ThenResultIsProjectCollection()
        {
            var substituteName = TfsRestService.SubstituteName("http://10.0.0.1", "OtherProjectCollection");
            Assert.AreEqual("OtherProjectCollection", substituteName);
        }
    }
}
