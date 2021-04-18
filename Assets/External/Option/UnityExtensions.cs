using UnityEngine;

namespace Code.Option {
  public static class UnityExtensions {
    public static Option<T> TryGetComponent<T>(this GameObject gameObject) => gameObject.GetComponent<T>().ToOption();
  }
}