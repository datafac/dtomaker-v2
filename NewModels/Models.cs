using DataFac.Storage;
using DTOMaker.Models;
using DTOMaker.Runtime;
using MessagePack;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

// Todo:
// - Support records with init-only properties
// - do we need a builder pattern?
// - Support MessagePack 3.x

#if NET6_0_OR_GREATER
#else
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Adding this fixes CS0518 errors.
    /// </summary>
    internal static class IsExternalInit { }
}
#endif

namespace DTOMaker.Runtime
{
}

namespace NewModels
{
    [Entity(1)]
    public interface IT_BaseImplName__ : IEntityBase
    {
    }

    [Entity(4)]
    public interface IT_AbstractEntity_ : IT_BaseImplName__
    {
    }

    [Entity(5)]
    public interface IT_ConcreteEntity_ : IT_AbstractEntity_
    {
        [Member(1)][Name("val")] string Value { get; }
    }
}

// <generated>
// in Domain.g.cs
namespace NewModels
{
    public interface IEntityBase_Writable : IEntityBase { }
    public interface IT_BaseImplName___Writable : IT_BaseImplName__, IEntityBase_Writable { }
    public interface IT_AbstractEntity__Writable : IT_AbstractEntity_, IT_BaseImplName___Writable { }
    public interface IT_ConcreteEntity__Writable : IT_ConcreteEntity_, IT_AbstractEntity__Writable
    {
        new string Value { set; }
    }
}
// </generated>

namespace NewModels.Records
{
    public abstract record EntityBase : IEntityBase
    {
        public bool IsFrozen => true;
        public void Freeze() { }
        protected abstract EntityBase OnShallowCopy();
        public IEntityBase ShallowCopy() => OnShallowCopy();

        public EntityBase() { }
        public EntityBase(EntityBase source) { }
        public EntityBase(IEntityBase source) { }
    }

    public abstract record T_BaseImplName_ : EntityBase, IT_BaseImplName__
    {
        public T_BaseImplName_() { }
        public T_BaseImplName_(T_BaseImplName_ source) : base(source) { }
        public T_BaseImplName_(IT_BaseImplName__ source) : base(source) { }
    }

    public abstract record T_AbstractEntity_ : T_BaseImplName_, IT_AbstractEntity_
    {
        public T_AbstractEntity_() { }
        public T_AbstractEntity_(T_AbstractEntity_ source) : base(source) {  }
        public T_AbstractEntity_(IT_AbstractEntity_ source) : base(source) { }
    }

    public sealed record T_ConcreteEntity_ : T_AbstractEntity_, IT_ConcreteEntity_
    {
        public string Value { get; init; } = string.Empty;
        public T_ConcreteEntity_() { }
        public T_ConcreteEntity_(T_ConcreteEntity_ source) : base(source) { Value = source.Value; }
        public T_ConcreteEntity_(IT_ConcreteEntity_ source) : base(source) { Value = source.Value; }
        protected override EntityBase OnShallowCopy() => this;
    }
}

namespace NewModels.Classes
{
    public abstract class EntityBase : IEntityBase
    {
        public EntityBase() { }
        public EntityBase(EntityBase source) { }
        public EntityBase(IEntityBase source) { }

        protected abstract EntityBase OnShallowCopy();
        public IEntityBase ShallowCopy() => OnShallowCopy();

        #region IFreezable implementation
        private volatile bool _frozen = false;
        public bool IsFrozen => _frozen;
        protected virtual void OnFreeze() { }
        public void Freeze()
        {
            if (_frozen) return;
            _frozen = true;
            OnFreeze();
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowIsFrozen(string? memberName)
        {
            throw new InvalidOperationException($"Cannot call {memberName} when frozen.");
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void CheckNotFrozen([CallerMemberName] string? memberName = null)
        {
            if (_frozen) ThrowIsFrozen(memberName);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowIsNotFrozenException(string? methodName) => throw new InvalidOperationException($"Cannot call {methodName} when not frozen.");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ThrowIfNotFrozen([CallerMemberName] string? methodName = null)
        {
            if (!_frozen) ThrowIsNotFrozenException(methodName);
        }
        #endregion
    }

    public abstract class T_BaseImplName_ : EntityBase, IT_BaseImplName___Writable
    {
        protected override void OnFreeze() { base.OnFreeze(); }
        public T_BaseImplName_() { }
        public T_BaseImplName_(T_BaseImplName_ source) : base(source) { }
        public T_BaseImplName_(IT_BaseImplName__ source) : base(source) { }
    }

    public abstract class T_AbstractEntity_ : T_BaseImplName_, IT_AbstractEntity__Writable
    {
        protected override void OnFreeze() { base.OnFreeze(); }
        public T_AbstractEntity_() { }
        public T_AbstractEntity_(T_AbstractEntity_ source) : base(source) { }
        public T_AbstractEntity_(IT_AbstractEntity_ source) : base(source) { }
    }

    public sealed class T_ConcreteEntity_ : T_AbstractEntity_, IT_ConcreteEntity__Writable
    {
        protected override EntityBase OnShallowCopy() => new T_ConcreteEntity_(this);
        protected override void OnFreeze() { base.OnFreeze(); }
        public string Value { get; set { CheckNotFrozen(); field = value; } } = string.Empty;
        public T_ConcreteEntity_() { }
        public T_ConcreteEntity_(T_ConcreteEntity_ source) : base(source) { Value = source.Value; }
        public T_ConcreteEntity_(IT_ConcreteEntity_ source) : base(source) { Value = source.Value; }
    }
}

namespace NewModels.MsgPack3
{
    using DTOMaker.Runtime.MsgPack3;
    using MessagePack;
    using System.Threading;

    [MessagePackObject(SuppressSourceGeneration = true)]
    [Union(5, typeof(T_ConcreteEntity_))]
    public abstract class T_BaseImplName_ : EntityBase, IT_BaseImplName___Writable
    {
        protected override void OnFreeze() { base.OnFreeze(); }
        public T_BaseImplName_() { }
        public T_BaseImplName_(T_BaseImplName_ source) : base(source) { }
        public T_BaseImplName_(IT_BaseImplName__ source) : base(source) { }
    }

    [MessagePackObject(SuppressSourceGeneration = true)]
    [Union(5, typeof(T_ConcreteEntity_))]
    public abstract class T_AbstractEntity_ : T_BaseImplName_, IT_AbstractEntity__Writable
    {
        protected override void OnFreeze() { base.OnFreeze(); }
        public T_AbstractEntity_() { }
        public T_AbstractEntity_(T_AbstractEntity_ source) : base(source) { }
        public T_AbstractEntity_(IT_AbstractEntity_ source) : base(source) { }
    }

    [MessagePackObject(SuppressSourceGeneration = true)]
    public sealed class T_ConcreteEntity_ : T_AbstractEntity_, IT_ConcreteEntity__Writable
    {
        protected override int OnGetEntityId() => 5;
        protected override IEntityBase OnShallowCopy() => new T_ConcreteEntity_(this);
        protected override void OnFreeze() { base.OnFreeze(); }
        [Key(1)]
        public string Value { get; set { CheckNotFrozen(); field = value; } } = string.Empty;
        public T_ConcreteEntity_() { }
        public T_ConcreteEntity_(T_ConcreteEntity_ source) : base(source) { Value = source.Value; }
        public T_ConcreteEntity_(IT_ConcreteEntity_ source) : base(source) { Value = source.Value; }

        protected override ValueTask OnPack(IDataStore dataStore, CancellationToken cancellation) => base.OnPack(dataStore, cancellation);
        protected override ValueTask OnUnpack(IDataStore dataStore, int depth, CancellationToken cancellation) => base.OnUnpack(dataStore, depth, cancellation);
        protected override ReadOnlyMemory<byte> OnSerializeqqq(CancellationToken cancellation) => MessagePackSerializer.Serialize<T_ConcreteEntity_>(this, _options, cancellation);
    }
}
