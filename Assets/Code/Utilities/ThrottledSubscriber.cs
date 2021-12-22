using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Code.Utilities {
  public static class UniTaskAsyncEnumerableExtensions {
    private static Func<T, UniTask> ThrottledSubscriber<T>(Action<T> fn, int msDelay) =>
      async t => {
        fn(t);
        await UniTask.Delay(msDelay);
      };

    public static void SubscribeAwaitThrottled<T>(
      this IUniTaskAsyncEnumerable<T> source,
      Action<T> onNext,
      int msDelay,
      CancellationToken cancellationToken = default
    ) =>
      source.SubscribeAwait(ThrottledSubscriber(onNext, msDelay), cancellationToken);
  }
}