using System;

namespace External.Option {
  public static class OptionExt {
    /// <summary>
    /// Wraps an existing value in an Option&lt;T&gt; instance.
    /// </summary>
    /// <param name="value">The value to be wrapped.</param>
    /// <returns>An optional containing the specified value.</returns>
    public static Option<T> Some<T>(this T value) => Option.Some(value);

    /// <summary>
    /// Creates an empty Option&lt;T&gt; instance from a specified value.
    /// </summary>
    /// <param name="value">A value determining the type of the optional.</param>
    /// <returns>An empty optional.</returns>
    public static Option<T> None<T>(this T value) => Option.None<T>();

    /// <summary>
    /// Creates an Option&lt;T&gt; instance from a specified value.
    /// If the value does not satisfy the given predicate,
    /// an empty optional is returned.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>An optional containing the specified value.</returns>
    public static Option<T> SomeWhen<T>(this T value, Func<T, bool> predicate) =>
      predicate(value) ? Option.Some(value) : Option.None<T>();

    /// <summary>
    /// Converts a Nullable&lt;T&gt; to an Option&lt;T&gt; instance.
    /// </summary>
    /// <param name="value">The Nullable&lt;T&gt; instance.</param>
    /// <returns>The Option&lt;T&gt; instance.</returns>
    public static Option<T> ToOption<T>(this T value) =>
      value != null ? Option.Some(value) : Option.None<T>();

    /// <summary>
    /// Converts a Nullable&lt;T&gt; to an Option&lt;T&gt; instance.
    /// </summary>
    /// <param name="value">The Nullable&lt;T&gt; instance.</param>
    /// <returns>The Option&lt;T&gt; instance.</returns>
    public static Option<T> ToOption<T>(this T? value) where T : struct =>
      value.HasValue ? Option.Some(value.Value) : Option.None<T>();

    /// <summary>
    /// Flattens two nested optionals into one. The resulting optional
    /// will be empty if either the inner or outer optional is empty.
    /// </summary>
    /// <param name="option">The nested optional.</param>
    /// <returns>A flattened optional.</returns>
    public static Option<T> Flatten<T>(this Option<Option<T>> option) =>
      option.FlatMap(innerOption => innerOption);
  }
}