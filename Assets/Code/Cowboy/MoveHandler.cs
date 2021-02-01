using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Cowboy {
  public class MoveHandler {
    private readonly Rigidbody2D rigidbody;
    private readonly Settings settings;

    public MoveHandler(Rigidbody2D rigidbody, Settings settings) {
      this.rigidbody = rigidbody;
      this.settings = settings;
    }

    public void Turn(float amount) {
      var torque = settings.turnSpeed * -amount;
      rigidbody.AddTorque(torque);
    }

    public void Eject() =>
      rigidbody.AddForce(settings.ejectForce * Random.insideUnitCircle.normalized, ForceMode2D.Impulse);

    [Serializable]
    public class Settings {
      public float turnSpeed;
      public float ejectForce;
    }
  }
}