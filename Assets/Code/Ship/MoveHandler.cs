using System;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using Zenject;

namespace Code.Ship
{
  public class MoveHandler : ITickable
  {
    private readonly InputState inputState;
    private readonly ScreenWrappingRigidbody2D rigidbody;
    private readonly Settings settings;

    private MoveHandler(Settings settings, InputState inputState, ScreenWrappingRigidbody2D rigidbody)
    {
      this.settings = settings;
      this.inputState = inputState;
      this.rigidbody = rigidbody;
    }

    public void Tick()
    {
      Thrust(inputState.Movement.y);
      Turn(inputState.Movement.x);
    }

    private void Thrust(float amount)
    {
      var force = rigidbody.Up * settings.speed * amount;
      rigidbody.AddForce(force);
    }

    private void Turn(float amount)
    {
      var torque = settings.turnSpeed * -amount;
      rigidbody.AddTorque(torque);
    }

    [Serializable]
    public class Settings
    {
      public float speed;
      public float turnSpeed;
    }
  }
}
