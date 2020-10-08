using System;
using UnityEngine;

namespace Code.Player.Input
{
  public class InputHandler
  {
    private InputState inputState;

    public bool HasLinkedInputState => inputState != null;

    public void ClearInputState()
    {
      inputState = null;
    }

    public void LinkInputState(InputState inputState)
    {
      this.inputState = inputState;
    }

    public Vector2 Movement
    {
      get
      {
        if (!HasLinkedInputState)
        {
          throw new Exception("Cannot read input from unlinked InputHandler");
        }

        return inputState.Movement;
      }
    }
  }
}
