using LanguageExt;
using Zenject;
using static LanguageExt.Prelude;

namespace Code.Player.Input {
  public class InputHandler : ITickable {
    private readonly Pawn pawn;
    private Option<IControllable> controllable;

    public InputHandler(Pawn pawn) => this.pawn = pawn;

    public void Tick() =>
      ifSome(controllable,
        c => {
          c.Thrust(pawn.inputState.movement.y);
          c.Turn(pawn.inputState.movement.x);
          if (pawn.inputState.isShooting) c.Shoot();
        });

    public void Possess(IControllable controllable) => this.controllable = Optional(controllable);
    public void Depossess() => controllable = None;
  }
}