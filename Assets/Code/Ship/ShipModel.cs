using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Ship {
  public class ShipModel {
    private readonly GameObject gameObject;
    private float health;

    public ShipModel(Transform transform, Vector3 position, Quaternion rotation, Settings settings) {
      transform.position = position;
      transform.rotation = rotation;
      gameObject = transform.gameObject;
      health = settings.startingHealth;
    }

    public void Damage(float damage) {
      health -= damage;

      if (health <= 0f) Die();
    }

    private void Die() => Object.Destroy(gameObject);

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}