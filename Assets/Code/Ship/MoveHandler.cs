using System;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using Zenject;

namespace Code.Ship {
  public class MoveHandler : ITickable {
    private readonly InputHandler inputHandler;
    private readonly SWRigidbody2D rigidbody;
    private readonly Settings settings;

    private MoveHandler(Settings settings, InputHandler inputHandler, SWRigidbody2D rigidbody) {
      this.settings = settings;
      this.inputHandler = inputHandler;
      this.rigidbody = rigidbody;
    }

    public void Tick() =>
      inputHandler.IfPossessed(state => {
        Thrust(state.movement.y);
        Turn(state.movement.x);
      });

    private void Thrust(float amount) => rigidbody.AddForce(rigidbody.Transform.up * settings.speed * amount);

    private void Turn(float amount) => rigidbody.AddTorque(settings.turnSpeed * -amount);

    [Serializable]
    public class Settings {
      public float speed;
      public float turnSpeed;
    }
  }
}