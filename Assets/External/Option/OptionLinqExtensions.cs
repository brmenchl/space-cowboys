using System;

namespace Code.Option {
  public static class LinqExt {
    public static Option<TResult>
      Select<TSource, TResult>(this Option<TSource> source, Func<TSource, TResult> selector) => source.Map(selector);

    public static Option<TResult> SelectMany<TSource, TResult>(
      this Option<TSource> source,
      Func<TSource, Option<TResult>> selector) => source.FlatMap(selector);

    public static Option<TResult> SelectMany<TSource, TCollection, TResult>(
      this Option<TSource> source,
      Func<TSource, Option<TCollection>> collectionSelector,
      Func<TSource, TCollection, TResult> resultSelector) =>
      source.FlatMap(src => collectionSelector(src).Map(elem => resultSelector(src, elem)));

    public static Option<TSource> Where<TSource>(this Option<TSource> source, Func<TSource, bool> predicate) =>
      source.Filter(predicate);
  }
}