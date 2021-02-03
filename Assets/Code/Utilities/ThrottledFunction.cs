using System;

namespace Code.Utilities {
  public class ThrottledFunction {
    private readonly TimeSpan delay;
    private readonly Action fn;
    private bool hasCalled;
    private DateTime lastCall;

    private ThrottledFunction(Action fn, float rate) {
      this.fn = fn;
      delay = TimeSpan.FromSeconds(1 / rate);
    }

    public void Call() {
      var now = DateTime.Now;
      if (hasCalled == false || now.Subtract(lastCall) > delay) {
        lastCall = now;
        hasCalled = true;
        fn();
      }
    }

    public static ThrottledFunction ThrottleByRate(Action fn, float rate) =>
      new ThrottledFunction(fn, rate);
  }
}