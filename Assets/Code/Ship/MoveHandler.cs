using System;
using UnityEngine;

namespace Code.Ship {
  public class MoveHandler {
    private readonly Rigidbody2D rigidbody;
    private readonly Settings settings;
    private readonly Transform transform;

    private MoveHandler(Transform transform, Rigidbody2D rigidbody, Settings settings) {
      this.transform = transform;
      this.rigidbody = rigidbody;
      this.settings = settings;
    }

    public void Thrust(float amount) => rigidbody.AddForce(transform.up * settings.speed * amount);

    public void Turn(float amount) => rigidbody.AddTorque(settings.turnSpeed * -amount);

    [Serializable]
    public class Settings {
      public float speed;
      public float turnSpeed;
    }
  }
}