using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Ship
{
  public class MoveHandler : ITickable
  {
    private readonly InputState inputState;
    private readonly ScreenWrappingRigidbody2D rigidbody;

    private float speed = 10;
    private float turnSpeed = 8;

    private MoveHandler(InputState inputState, ScreenWrappingRigidbody2D rigidbody)
    {
      this.inputState = inputState;
      this.rigidbody = rigidbody;
    }

    public void Tick()
    {
      Thrust(inputState.MovementInputState.y);
      Turn(inputState.MovementInputState.x);
    }

    private void Thrust(float amount)
    {
      var force = rigidbody.Up * speed * amount;
      rigidbody.AddForce(force);
    }

    private void Turn(float amount)
    {
      var torque = turnSpeed * -amount;
      rigidbody.AddTorque(torque);
    }
  }
}
