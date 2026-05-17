using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DTOMaker.Models.BinaryTree;

public readonly struct DictionaryFacade<TKey, TValue, TNode> : IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull, IComparable<TKey>
    where TNode : class, IBinaryTree<TKey, TValue, TNode>
{
    private readonly Func<TNode?> _getter;
    private readonly Action<TNode?> _setter;

    public DictionaryFacade(Func<TNode?> getter, Action<TNode?> setter)
    {
        _getter = getter;
        _setter = setter;
    }

    public TValue this[TKey key]
    {
        get
        {
            var node = _getter().Get<TKey, TValue, TNode>(key);
            return node is not null ? node.Value : throw new KeyNotFoundException($"Key not found: {key}");
        }
    }

    public IEnumerable<TKey> Keys => _getter().GetKeyValuePairs<TKey, TValue, TNode>().Select(kv => kv.Key);
    public IEnumerable<TValue> Values => _getter().GetKeyValuePairs<TKey, TValue, TNode>().Select(kv => kv.Value);
    public int Count => _getter()?.Count ?? 0;
    public bool ContainsKey(TKey key) => _getter().Get<TKey, TValue, TNode>(key) is not null;
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _getter().GetKeyValuePairs<TKey, TValue, TNode>().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool TryGetValue(TKey key, out TValue value)
    {
        var node = _getter().Get<TKey, TValue, TNode>(key);
        value = node is not null ? node.Value : default!;
        return node is not null;
    }
}