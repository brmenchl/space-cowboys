using System;
using System.Collections.Generic;

namespace Code.Utilities.Extensions {
  public static class Collections {
    public static void ForEach<T>(this IEnumerable<T> xs, Action<T> fn) {
      foreach (var x in xs) {
        fn(x);
      }
    }
  }
}