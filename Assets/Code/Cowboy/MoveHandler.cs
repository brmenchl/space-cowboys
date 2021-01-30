using Code.Utilities.ScreenWrap;

namespace Code.Cowboy {
  public class MoveHandler {
    private readonly SWRigidbody2D rigidbody;

    private MoveHandler(SWRigidbody2D rigidbody) => this.rigidbody = rigidbody;

    public void Turn(float amount) {
      var torque = 3 * -amount;
      rigidbody.AddTorque(torque);
    }
  }
}