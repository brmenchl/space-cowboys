using Code.Player;
using Zenject;

namespace Code.Input {
  public class InputHandler : ITickable {
    private readonly Pawn pawn;
    private readonly ControllableState controllableState;

    public InputHandler(Pawn pawn, ControllableState controllableState) {
      this.pawn = pawn;
      this.controllableState = controllableState;
      pawn.OnAlt += HandleAlt;
    }

    public void Tick() =>
      controllableState.IfIsControlling(c => {
        c.Thrust(pawn.inputState.movement.y);
        c.Turn(pawn.inputState.movement.x);
        if (pawn.inputState.isShooting) c.Shoot();
      });

    private void HandleAlt() =>
      controllableState.IfIsControlling(c => c.Alt());
  }
}