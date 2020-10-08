using System;
using UnityEngine;

namespace Code.Player.Input
{
  public class InputHandler : IDisposable
  {
    private Pawn pawn;

    public bool IsPossessed => pawn != null;

    public Vector2 Movement
    {
      get
      {
        if (!IsPossessed) throw new Exception("InputHandler is not possessed.");

        return pawn.InputState.Movement;
      }
    }

    public void Dispose()
    {
      if (IsPossessed) pawn.OnPossessableDestroy();
    }

    public void Depossess()
    {
      pawn = null;
    }

    public void Possess(Pawn pawn)
    {
      this.pawn = pawn;
    }
  }
}
