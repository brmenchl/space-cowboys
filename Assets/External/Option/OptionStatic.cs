using System;

namespace External.Option {
  /// <summary>
  /// Provides a set of functions for creating optional values.
  /// </summary>
  public static class Option {
    /// <summary>
    /// Wraps an existing value in an Option&lt;T&gt; instance.
    /// </summary>
    /// <param name="value">The value to be wrapped.</param>
    /// <returns>An optional containing the specified value.</returns>
    public static Option<T> Some<T>(T value) {
      if (value == null) {
        throw new ArgumentNullException(nameof(value), "Cannot create a Some of null");
      }
      return new Option<T>(value, true);
    }

    /// <summary>
    /// Creates an empty Option&lt;T&gt; instance.
    /// </summary>
    /// <returns>An empty optional.</returns>
    public static Option<T> None<T>() => new Option<T>(default, false);
  }
}