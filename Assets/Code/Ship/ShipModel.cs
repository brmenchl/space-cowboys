using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Ship {
  public class ShipModel {
    public readonly Transform transform;
    private float health;

    public ShipModel(Transform transform, Vector3 position, Quaternion rotation, Settings settings) {
      this.transform = transform;
      transform.position = position;
      transform.rotation = rotation;
      health = settings.startingHealth;
    }

    public event Action OnDestroyed;

    public void Damage(float damage) {
      health -= damage;

      if (health <= 0f) Destroy();
    }

    private void Destroy() {
      OnDestroyed?.Invoke();
      Object.Destroy(transform.gameObject);
    }

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}