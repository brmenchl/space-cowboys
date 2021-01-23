namespace Code.Cowboy {
  using Player.Input;

  using Utilities.ScreenWrap;

  using Zenject;

  public class MoveHandler : ITickable {
    private readonly InputHandler inputHandler;
    private readonly SWRigidbody2D rigidbody;

    private MoveHandler(InputHandler inputHandler, SWRigidbody2D rigidbody) {
      this.inputHandler = inputHandler;
      this.rigidbody = rigidbody;
    }

    public void Tick() {
      if (!inputHandler.IsPossessed) return;
      Turn(inputHandler.Movement.x);
    }

    private void Turn(float amount) {
      var torque = 3 * -amount;
      rigidbody.AddTorque(torque);
    }
  }
}