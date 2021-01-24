using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Code.Utilities {
  public static class FireRateHelper {
    public static Action ThrottleByRate(Action fn, float callsPerSecond) =>
      new ThrottleWrapper(callsPerSecond, fn).TryFn;

    private class ThrottleWrapper {
      private readonly TimeSpan delay;
      private readonly Action fn;
      private Option<DateTime> lastCall;

      public ThrottleWrapper(float rate, Action fn) {
        this.fn = fn;
        delay = TimeSpan.FromSeconds(1 / rate);
      }

      public void TryFn() {
        var now = DateTime.Now;
        if (biexists(lastCall, time => now.Subtract(time) > delay, _ => true)) {
          lastCall = now;
          fn();
        }
      }
    }
  }
}