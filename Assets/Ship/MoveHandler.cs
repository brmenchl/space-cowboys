using UnityEngine;
using Zenject;

public class MoveHandler : ITickable
{
  private InputState inputState;
  private ScreenWrappingRigidbody2D rigidbody;

  private float speed = 10;
  private float turnSpeed = 8;

  private MoveHandler(InputState inputState, ScreenWrappingRigidbody2D rigidbody)
  {
    this.inputState = inputState;
    this.rigidbody = rigidbody;
  }

  public void Tick()
  {
    Thrust(inputState.movementInputState.y);
    Turn(inputState.movementInputState.x);
  }

  private void Thrust(float amount)
  {
    Vector2 force = rigidbody.Up * speed * amount;
    rigidbody.AddForce(force);
  }

  private void Turn(float amount)
  {
    float torque = turnSpeed * -amount;
    rigidbody.AddTorque(torque);
  }
}
