using System;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;

namespace Code.Ship {
  public class MoveHandler {
    private readonly SWRigidbody2D rigidbody;
    private readonly Settings settings;

    private MoveHandler(Settings settings, InputHandler inputHandler, SWRigidbody2D rigidbody) {
      this.settings = settings;
      this.rigidbody = rigidbody;
      inputHandler.OnThrust += Thrust;
      inputHandler.OnTurn += Turn;
    }

    private void Thrust(float amount) => rigidbody.AddForce(rigidbody.Transform.up * settings.speed * amount);

    private void Turn(float amount) => rigidbody.AddTorque(settings.turnSpeed * -amount);

    [Serializable]
    public class Settings {
      public float speed;
      public float turnSpeed;
    }
  }
}