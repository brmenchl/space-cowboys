using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Code.Player.Input {
  public class InputHandler {
    private Option<Pawn> pawn;

    public bool IsPossessed => isSome(pawn);
    public void IfPossessed(Action<InputState> onPossessed) => ifSome(pawn, p => onPossessed(p.inputState));

    public void Depossess() => pawn = None;

    public void Possess(Pawn p) =>
      pawn.BiIter(
        _ => throw new Exception("Cannot possess a game object that is already possessed"),
        _ => pawn = Some(p)
      );
  }
}