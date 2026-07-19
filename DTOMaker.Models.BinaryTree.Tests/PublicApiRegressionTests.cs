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

#if NET7_0_OR_GREATER
        [Fact]
        public async Task CheckPublicApi_net70()
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
#else
        [Fact]
        public async Task CheckPublicApi_Net48()
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
#endif

    }
}