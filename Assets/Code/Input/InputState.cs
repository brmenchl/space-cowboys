using System.Collections.Generic;

namespace Code.Input {
  public class InputState {
    public readonly Dictionary<ControlScheme, ControllerInputStream> inputs =
      new Dictionary<ControlScheme, ControllerInputStream>();
  }
}