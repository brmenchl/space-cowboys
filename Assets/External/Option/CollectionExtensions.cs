// Note: Several of the below implementations are closely inspired by the corefx source code for FirstOrDefault, etc.

using System;
using System.Collections.Generic;
using System.Linq;

namespace External.Option {
  public static class CollectionExt {
    /// <summary>
    ///   Flattens a sequence of optionals into a sequence containing all inner values.
    ///   Empty elements are discarded.
    /// </summary>
    /// <param name="source">The sequence of optionals.</param>
    /// <returns>A flattened sequence of values.</returns>
    public static IEnumerable<T> Values<T>(this IEnumerable<Option<T>> source) {
      if (source == null) throw new ArgumentNullException(nameof(source));
      foreach (var option in source)
        if (option.isSome)
          yield return option.Value;
    }

    /// <summary>
    ///   Returns the value associated with the specified key if such exists.
    ///   A dictionary lookup will be used if available, otherwise falling
    ///   back to a linear scan of the enumerable.
    /// </summary>
    /// <param name="source">The dictionary or enumerable in which to locate the key.</param>
    /// <param name="key">The key to locate.</param>
    /// <returns>An Option&lt;TValue&gt; instance containing the associated value if located.</returns>
    public static Option<TValue> GetValueOrNone<TKey, TValue>(
      this IEnumerable<KeyValuePair<TKey, TValue>> source,
      TKey key) {
      switch (source) {
        case null:
          throw new ArgumentNullException(nameof(source));
        case IDictionary<TKey, TValue> dictionary: {
          return dictionary.TryGetValue(key, out var value) ? value.Some() : value.None();
        }
        case IReadOnlyDictionary<TKey, TValue> readOnlyDictionary: {
          return readOnlyDictionary.TryGetValue(key, out var value) ? value.Some() : value.None();
        }
        default:
          return source
            .FirstOrNone(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key))
            .Map(pair => pair.Value);
      }
    }

    /// <summary>
    ///   Returns the first element of a sequence if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the first element from.</param>
    /// <returns>An Option&lt;T&gt; instance containing the first element if present.</returns>
    public static Option<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source) {
      switch (source) {
        case null:
          throw new ArgumentNullException(nameof(source));
        case IList<TSource> list: {
          if (list.Count > 0) return list[0].Some();
          break;
        }
        case IReadOnlyList<TSource> readOnlyList: {
          if (readOnlyList.Count > 0) return readOnlyList[0].Some();
          break;
        }
        default: {
          using (var enumerator = source.GetEnumerator())
            if (enumerator.MoveNext())
              return enumerator.Current.Some();
          break;
        }
      }

      return Option.None<TSource>();
    }

    /// <summary>
    ///   Returns the first element of a sequence, satisfying a specified predicate,
    ///   if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the first element from.</param>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An Option&lt;T&gt; instance containing the first element if present.</returns>
    public static Option<TSource> FirstOrNone<TSource>(
      this IEnumerable<TSource> source,
      Func<TSource, bool> predicate) {
      if (source == null) throw new ArgumentNullException(nameof(source));

      foreach (var element in source)
        if (predicate(element))
          return element.Some();

      return Option.None<TSource>();
    }

    public static Option<int> FindIndexOrNone<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate) {
      if (source == null) throw new ArgumentNullException(nameof(source));
      var index = source.ToList().FindIndex(predicate);
      return index >= 0 ? index.Some() : Option.None<int>();
    }

    /// <summary>
    ///   Returns the last element of a sequence if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the last element from.</param>
    /// <returns>An Option&lt;T&gt; instance containing the last element if present.</returns>
    public static Option<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source) {
      switch (source) {
        case null:
          throw new ArgumentNullException(nameof(source));
        case IList<TSource> list: {
          var count = list.Count;
          if (count > 0) return list[count - 1].Some();
          break;
        }
        case IReadOnlyList<TSource> readOnlyList: {
          var count = readOnlyList.Count;
          if (count > 0) return readOnlyList[count - 1].Some();
          break;
        }
        default: {
          using (var enumerator = source.GetEnumerator())
            if (enumerator.MoveNext()) {
              TSource result;
              do
                result = enumerator.Current;
              while (enumerator.MoveNext());

              return result.Some();
            }
          break;
        }
      }

      return Option.None<TSource>();
    }

    /// <summary>
    ///   Returns the last element of a sequence, satisfying a specified predicate,
    ///   if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the last element from.</param>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An Option&lt;T&gt; instance containing the last element if present.</returns>
    public static Option<TSource> LastOrNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
      switch (source) {
        case null:
          throw new ArgumentNullException(nameof(source));
        case IList<TSource> list: {
          for (var i = list.Count - 1; i >= 0; --i) {
            var result = list[i];
            if (predicate(result)) return result.Some();
          }
          break;
        }
        case IReadOnlyList<TSource> readOnlyList: {
          for (var i = readOnlyList.Count - 1; i >= 0; --i) {
            var result = readOnlyList[i];
            if (predicate(result)) return result.Some();
          }
          break;
        }
        default: {
          using (var enumerator = source.GetEnumerator())
            while (enumerator.MoveNext()) {
              var result = enumerator.Current;
              if (predicate(result)) {
                while (enumerator.MoveNext()) {
                  var element = enumerator.Current;
                  if (predicate(element)) result = element;
                }

                return result.Some();
              }
            }
          break;
        }
      }

      return Option.None<TSource>();
    }

    /// <summary>
    ///   Returns an element at a specified position in a sequence if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the element from.</param>
    /// <param name="index">The index in the sequence.</param>
    /// <returns>An Option&lt;T&gt; instance containing the element if found.</returns>
    public static Option<TSource> ElementAtOrNone<TSource>(this IEnumerable<TSource> source, int index) {
      if (source == null) throw new ArgumentNullException(nameof(source));

      if (index >= 0)
        switch (source) {
          case IList<TSource> list: {
            if (index < list.Count) return list[index].Some();
            break;
          }
          case IReadOnlyList<TSource> readOnlyList: {
            if (index < readOnlyList.Count) return readOnlyList[index].Some();
            break;
          }
          default: {
            using (var enumerator = source.GetEnumerator())
              while (enumerator.MoveNext()) {
                if (index == 0) return enumerator.Current.Some();
                index--;
              }
            break;
          }
        }

      return Option.None<TSource>();
    }
  }
}