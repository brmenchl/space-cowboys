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

    public void Tick() {
      if (!inputHandler.IsPossessed) return;
      Thrust(inputHandler.Movement.y);
      Turn(inputHandler.Movement.x);
    }

    private void Thrust(float amount) {
      var force = rigidbody.Transform.up * settings.speed * amount;
      rigidbody.AddForce(force);
    }

    private void Turn(float amount) {
      var torque = settings.turnSpeed * -amount;
      rigidbody.AddTorque(torque);
    }

    [Serializable]
    public class Settings {
      public float speed;
      public float turnSpeed;
    }
  }
}