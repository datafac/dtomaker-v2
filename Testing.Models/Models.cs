using System;
using DTOMaker.Models;

namespace Testing.Models;

[Entity(1)] public interface IRequired_Int64 : IEntityBase { [Member(1)] Int64 Field { get; set; } }
[Entity(2)] public interface IOptional_Int64 : IEntityBase { [Member(1)] Int64? Field { get; set; } }
[Entity(3)] public interface IRequired_String : IEntityBase { [Member(1)] String Field { get; set; } }
[Entity(4)] public interface IOptional_String : IEntityBase { [Member(1)] String? Field { get; set; } }

[Entity(5)]
public interface ISimple : IEntityBase
{
    [Member(1)] Int64 Field1 { get; set; }
    [Member(2)] Int64? Field2 { get; set; }
    [Member(3)] String? Field3 { get; set; } // todo 32 bytes
    [Member(4)] String Field4 { get; set; } // todo 32 bytes
}
