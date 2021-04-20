using System;
using System.Collections.Generic;

namespace External.Option {
  /// <summary>
  /// Represents an optional value.
  /// </summary>
  /// <typeparam name="T">The type of the value to be wrapped.</typeparam>
  [Serializable]
  public struct Option<T> : IEquatable<Option<T>>, IComparable<Option<T>> {
    private readonly T value;

    public bool isSome { get; }
    public bool isNone => !isSome;

    internal T Value => value;

    internal Option(T value, bool isSome) {
      this.value = value;
      this.isSome = isSome;
    }

    public static implicit operator T(Option<T> option) {
      if (option.isNone) throw new Exception("Cannot extract value from None");
      return option.value;
    }

    /// <summary>
    /// Determines whether two optionals are equal.
    /// </summary>
    /// <param name="other">The optional to compare with the current one.</param>
    /// <returns>A boolean indicating whether or not the optionals are equal.</returns>
    public bool Equals(Option<T> other) {
      if (isNone && other.isNone) return true;
      if (isSome && other.isSome) return EqualityComparer<T>.Default.Equals(value, other.value);
      return false;
    }

    /// <summary>
    /// Determines whether two optionals are equal.
    /// </summary>
    /// <param name="obj">The optional to compare with the current one.</param>
    /// <returns>A boolean indicating whether or not the optionals are equal.</returns>
    public override bool Equals(object obj) => obj is Option<T> option && Equals(option);

    /// <summary>
    /// Determines whether two optionals are equal.
    /// </summary>
    /// <param name="left">The first optional to compare.</param>
    /// <param name="right">The second optional to compare.</param>
    /// <returns>A boolean indicating whether or not the optionals are equal.</returns>
    public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);

    /// <summary>
    /// Determines whether two optionals are unequal.
    /// </summary>
    /// <param name="left">The first optional to compare.</param>
    /// <param name="right">The second optional to compare.</param>
    /// <returns>A boolean indicating whether or not the optionals are unequal.</returns>
    public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);

    /// <summary>
    /// Generates a hash code for the current optional.
    /// </summary>
    /// <returns>A hash code for the current optional.</returns>
    public override int GetHashCode() => isSome ? value.GetHashCode() : 0;

    /// <summary>
    /// Compares the relative order of two optionals. An empty optional is
    /// ordered before a non-empty one.
    /// </summary>
    /// <param name="other">The optional to compare with the current one.</param>
    /// <returns>An integer indicating the relative order of the optionals being compared.</returns>
    public int CompareTo(Option<T> other) {
      if (isSome && other.isNone) return 1;
      if (isNone && other.isSome) return -1;
      return Comparer<T>.Default.Compare(value, other.value);
    }

    /// <summary>
    /// Determines if an optional is less than another optional.
    /// </summary>
    /// <param name="left">The first optional to compare.</param>
    /// <param name="right">The second optional to compare.</param>
    /// <returns>A boolean indicating whether or not the left optional is less than the right optional.</returns>
    public static bool operator <(Option<T> left, Option<T> right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines if an optional is less than or equal to another optional.
    /// </summary>
    /// <param name="left">The first optional to compare.</param>
    /// <param name="right">The second optional to compare.</param>
    /// <returns>A boolean indicating whether or not the left optional is less than or equal the right optional.</returns>
    public static bool operator <=(Option<T> left, Option<T> right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines if an optional is greater than another optional.
    /// </summary>
    /// <param name="left">The first optional to compare.</param>
    /// <param name="right">The second optional to compare.</param>
    /// <returns>A boolean indicating whether or not the left optional is greater than the right optional.</returns>
    public static bool operator >(Option<T> left, Option<T> right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines if an optional is greater than or equal to another optional.
    /// </summary>
    /// <param name="left">The first optional to compare.</param>
    /// <param name="right">The second optional to compare.</param>
    /// <returns>A boolean indicating whether or not the left optional is greater than or equal the right optional.</returns>
    public static bool operator >=(Option<T> left, Option<T> right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a string that represents the current optional.
    /// </summary>
    /// <returns>A string that represents the current optional.</returns>
    public override string ToString() => isSome ? $"Some({value})" : "None";

    /// <summary>
    /// Converts the current optional into an enumerable with one or zero elements.
    /// </summary>
    /// <returns>A corresponding enumerable.</returns>
    public IEnumerable<T> ToEnumerable() {
      if (isSome) {
        yield return value;
      }
    }

    /// <summary>
    /// Returns an enumerator for the optional.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<T> GetEnumerator() {
      if (isSome) {
        yield return value;
      }
    }

    /// <summary>
    /// Determines if the current optional contains a specified value.
    /// </summary>
    /// <param name="value">The value to locate.</param>
    /// <returns>A boolean indicating whether or not the value was found.</returns>
    public bool Contains(T value) => isSome && this.value.Equals(value);

    /// <summary>
    /// Determines if the current optional contains a value
    /// satisfying a specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns>A boolean indicating whether or not the predicate was satisfied.</returns>
    public bool Exists(Func<T, bool> predicate) => isSome && predicate(value);

    /// <summary>
    /// Returns the existing value if present, and otherwise an alternative value.
    /// </summary>
    /// <param name="alternative">The alternative value.</param>
    /// <returns>The existing or alternative value.</returns>
    public T ValueOr(T alternative) => isSome ? value : alternative;

    /// <summary>
    /// Returns the existing value if present, and otherwise an alternative value.
    /// </summary>
    /// <param name="alternativeFactory">A factory function to create an alternative value.</param>
    /// <returns>The existing or alternative value.</returns>
    public T ValueOr(Func<T> alternativeFactory) => isSome ? value : alternativeFactory();

    /// <summary>
    /// Uses an alternative value, if no existing value is present.
    /// </summary>
    /// <param name="alternative">The alternative value.</param>
    /// <returns>A new optional, containing either the existing or alternative value.</returns>
    public Option<T> Or(T alternative) => isSome ? this : Option.Some(alternative);

    /// <summary>
    /// Uses an alternative value, if no existing value is present.
    /// </summary>
    /// <param name="alternativeFactory">A factory function to create an alternative value.</param>
    /// <returns>A new optional, containing either the existing or alternative value.</returns>
    public Option<T> Or(Func<T> alternativeFactory) =>
      isSome ? this : Option.Some(alternativeFactory());

    /// <summary>
    /// Uses an alternative optional, if no existing value is present.
    /// </summary>
    /// <param name="alternativeOption">The alternative optional.</param>
    /// <returns>The alternative optional, if no value is present, otherwise itself.</returns>
    public Option<T> Else(Option<T> alternativeOption) => isSome ? this : alternativeOption;

    /// <summary>
    /// Uses an alternative optional, if no existing value is present.
    /// </summary>
    /// <param name="alternativeOptionFactory">A factory function to create an alternative optional.</param>
    /// <returns>The alternative optional, if no value is present, otherwise itself.</returns>
    public Option<T> Else(Func<Option<T>> alternativeOptionFactory) => isSome ? this : alternativeOptionFactory();

    /// <summary>
    /// Evaluates a specified function, based on whether a value is present or not.
    /// </summary>
    /// <param name="some">The function to evaluate if the value is present.</param>
    /// <param name="none">The function to evaluate if the value is missing.</param>
    /// <returns>The result of the evaluated function.</returns>
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) => isSome ? some(value) : none();

    /// <summary>
    /// Evaluates a specified action, based on whether a value is present or not.
    /// </summary>
    /// <param name="some">The action to evaluate if the value is present.</param>
    /// <param name="none">The action to evaluate if the value is missing.</param>
    public void Match(Action<T> some, Action none) {
      if (isSome)
        some(value);
      else
        none();
    }

    /// <summary>
    /// Evaluates a specified action if a value is present.
    /// </summary>
    /// <param name="some">The action to evaluate if the value is present.</param>
    public void MatchSome(Action<T> some) {
      if (isSome) some(value);
    }

    /// <summary>
    /// Evaluates a specified action if no value is present.
    /// </summary>
    /// <param name="none">The action to evaluate if the value is missing.</param>
    public void MatchNone(Action none) {
      if (!isSome) none();
    }

    /// <summary>
    /// Transforms the inner value in an optional.
    /// If the instance is empty, an empty optional is returned.
    /// </summary>
    /// <param name="mapping">The transformation function.</param>
    /// <returns>The transformed optional.</returns>
    public Option<TResult> Map<TResult>(Func<T, TResult> mapping) =>
      Match(
        value => Option.Some(mapping(value)),
        Option.None<TResult>
      );

    /// <summary>
    /// Transforms the inner value in an optional
    /// into another optional. The result is flattened,
    /// and if either is empty, an empty optional is returned.
    /// </summary>
    /// <param name="mapping">The transformation function.</param>
    /// <returns>The transformed optional.</returns>
    public Option<TResult> FlatMap<TResult>(Func<T, Option<TResult>> mapping) =>
      Match(
        mapping,
        Option.None<TResult>
      );

    /// <summary>
    /// Empties an optional if a specified predicate
    /// is not satisfied.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns>The filtered optional.</returns>
    public Option<T> Filter(Func<T, bool> predicate) =>
      isSome && !predicate(value) ? Option.None<T>() : this;
  }
}