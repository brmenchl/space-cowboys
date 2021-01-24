using Code.Player.Input;
using Code.Utilities.ScreenWrap;

namespace Code.Cowboy {
  public class MoveHandler {
    private readonly SWRigidbody2D rigidbody;

    private MoveHandler(InputHandler inputHandler, SWRigidbody2D rigidbody) {
      this.rigidbody = rigidbody;
      inputHandler.OnTurn += Turn;
    }

    private void Turn(float amount) {
      var torque = 3 * -amount;
      rigidbody.AddTorque(torque);
    }
  }
}