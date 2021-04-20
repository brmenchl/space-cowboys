using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Code.Input {
  public class ControllerInputStream {
    public Vector2 movement = Vector2.zero;
    public bool primary;
    public bool alt;
    public readonly IUniTaskAsyncEnumerable<ControllerInputState> state;

    public ControllerInputStream() =>
      // ReSharper disable once InvokeAsExtensionMethod
      state = UniTaskAsyncEnumerable.EveryUpdate().Select(_ =>
        new ControllerInputState(movement, primary, alt)
      );
  }

  public readonly struct ControllerInputState {
    public readonly Vector2 movement;
    public readonly bool primary;
    public readonly bool alt;

    public ControllerInputState(Vector2 movement, bool primary, bool alt) {
      this.movement = movement;
      this.primary = primary;
      this.alt = alt;
    }
  }
}