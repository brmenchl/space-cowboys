using System;
using Code.Utilities.ScreenWrap;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Ship {
  public class ShipModel {
    private readonly GameObject gameObject;
    private float health;

    public ShipModel(SWRigidbody2D rigidbody2D, Vector3 position, Quaternion rotation, Settings settings) {
      rigidbody2D.SetPositionAndRotation(position, rotation);
      gameObject = rigidbody2D.gameObject;
      health = settings.startingHealth;
    }

    public void Damage(float damage) {
      health -= damage;

      if (health <= 0f) Die();
    }

    private void Die() {
      Object.Destroy(gameObject);
    }

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}