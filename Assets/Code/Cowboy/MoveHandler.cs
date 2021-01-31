using UnityEngine;

namespace Code.Cowboy {
  public class MoveHandler {
    private readonly Rigidbody2D rigidbody;

    public MoveHandler(Rigidbody2D rigidbody) => this.rigidbody = rigidbody;

    public void Turn(float amount) {
      var torque = 3 * -amount;
      rigidbody.AddTorque(torque);
    }

    public void Eject() {
      const int amount = 10;
      rigidbody.AddForce(amount * Random.insideUnitCircle.normalized, ForceMode2D.Impulse);
    }
  }
}