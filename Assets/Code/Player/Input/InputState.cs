using UnityEngine;

namespace Code.Player.Input
{
  public class InputState
  {
    public Vector2 Movement = Vector2.zero;

    public bool IsEnabled { get; private set; }

    public void Enable()
    {
      IsEnabled = true;
    }

    public void Disable()
    {
      IsEnabled = false;
    }
  }

}
