using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Code.Utilities {
  public class ThrottledFunction {
    private readonly TimeSpan delay;
    private readonly Action fn;
    private Option<DateTime> lastCall;

    private ThrottledFunction(Action fn, float rate) {
      this.fn = fn;
      delay = TimeSpan.FromSeconds(1 / rate);
    }

    public void Call() {
      var now = DateTime.Now;
      if (biexists(lastCall, time => now.Subtract(time) > delay, _ => true)) {
        lastCall = now;
        fn();
      }
    }

    public static ThrottledFunction ThrottleByRate(Action fn, float rate) =>
      new ThrottledFunction(fn, rate);
  }
}