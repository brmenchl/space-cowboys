using System;
using LanguageExt;
using Zenject;
using static LanguageExt.Prelude;

namespace Code.Player.Input {
  public class InputHandler : ITickable {
    private Option<Pawn> pawn;

    public bool IsPossessed => isSome(pawn);

    public void Tick() =>
      IfPossessed(state => {
        OnThrust?.Invoke(state.movement.y);
        OnTurn?.Invoke(state.movement.x);
        if (state.isShooting) OnShoot?.Invoke();
      });

    public event Action<float> OnThrust;
    public event Action<float> OnTurn;
    public event Action OnShoot;
    public void IfPossessed(Action<InputState> onPossessed) => ifSome(pawn, p => onPossessed(p.inputState));

    public void Depossess() => pawn = None;

    public void Possess(Pawn p) =>
      pawn.BiIter(
        _ => throw new Exception("Cannot possess a game object that is already possessed"),
        _ => pawn = Some(p)
      );
  }
}