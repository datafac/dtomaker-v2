using DTOMaker.Runtime.MsgPack3;
using MessagePack;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NewModels.Tests
{
    public class RoundtripTests
    {
        [Fact]
        public async Task RoundtripNewModelAsLeaf()
        {
            var cancellation = TestContext.Current.CancellationToken;
            using var dataStore = new DataFac.Storage.Testing.TestDataStore();
            var orig = new NewModels.Records.VarString() { Value = "The quick brown fox jumps over the lazy dog." };
            var send = new NewModels.MsgPack3.VarString(orig);
            await send.Pack(dataStore, cancellation);
            var buffer = EntityBase.Serialize<NewModels.MsgPack3.VarString>(send, cancellation);
            var recd = EntityBase.Deserialize<NewModels.MsgPack3.VarString>(buffer, cancellation);
            recd.ShouldNotBeNull();
            recd.IsFrozen.ShouldBeTrue();
            recd.IsPacked.ShouldBeTrue();
            await recd.UnpackAll(dataStore, cancellation);
            var copy = new NewModels.Records.VarString(recd);
            copy.ShouldBe(orig);
        }

        [Fact]
        public async Task RoundtripNewModelAsBase()
        {
            var cancellation = TestContext.Current.CancellationToken;
            using var dataStore = new DataFac.Storage.Testing.TestDataStore();
            var orig = new NewModels.Records.VarString() { Value = "The quick brown fox jumps over the lazy dog." };
            var send = new NewModels.MsgPack3.VarString(orig);
            await send.Pack(dataStore, cancellation);
            var buffer = EntityBase.Serialize<NewModels.MsgPack3.DomainBase>(send, cancellation);
            var recd = EntityBase.Deserialize<NewModels.MsgPack3.DomainBase>(buffer, cancellation) as NewModels.MsgPack3.VarString;
            recd.ShouldNotBeNull();
            recd.IsFrozen.ShouldBeTrue();
            recd.IsPacked.ShouldBeTrue();
            await recd.UnpackAll(dataStore, cancellation);
            var copy = new NewModels.Records.VarString(recd);
            copy.ShouldBe(orig);
        }
    }
}
