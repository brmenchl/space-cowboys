using System;

namespace Code.Input {
  public enum ControlScheme {
    WasdKeyboard,
    ArrowsKeyboard
  }

  public static class ControlSchemeExtensions {
    public static string ToString(this ControlScheme controlScheme) {
      switch (controlScheme) {
        case ControlScheme.WasdKeyboard:
          return "WASDKeyboard";
        case ControlScheme.ArrowsKeyboard:
          return "ArrowsKeyboard";
        default:
          throw new ArgumentOutOfRangeException(nameof(controlScheme), controlScheme, null);
      }
    }
  }
}