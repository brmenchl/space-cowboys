using System.Collections.Generic;

namespace Code.Input {
  public class InputState {
    public readonly Dictionary<ControlScheme, ControllerInputState> inputs =
      new Dictionary<ControlScheme, ControllerInputState>();
  }
}