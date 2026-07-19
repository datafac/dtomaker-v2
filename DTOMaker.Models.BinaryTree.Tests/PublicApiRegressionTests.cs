using DataFac.Storage;
using PublicApiGenerator;
using Shouldly;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace DTOMaker.Models.BinaryTree.Tests
{
    public class PublicApiRegressionTests
    {
        [Fact]
        public async Task CheckVerifySetup()
        {
            await VerifyChecks.Run();
        }

        [Fact]
        public void VersionCheck()
        {
            var assemblyVersion = typeof(IEntityBase).Assembly.GetName().Version;
            assemblyVersion.ShouldNotBeNull();
            assemblyVersion.ToString().ShouldBe("2.1.0.0");
        }

        [Fact]
        public async Task CheckPublicApi()
        {
            // act
            var options = new ApiGeneratorOptions()
            {
                IncludeAssemblyAttributes = false
            };
            string currentApi = ApiGenerator.GeneratePublicApi(typeof(IEntityBase).Assembly, options);

            // assert
            await Verifier.Verify(currentApi);
        }

    }
}