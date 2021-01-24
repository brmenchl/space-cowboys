using Code.Utilities.ScreenWrap;
using UnityEngine;

namespace Code.Cowboy {
  public class CowboyModel {
    private readonly GameObject gameObject;
    private float health;

    public CowboyModel(SWRigidbody2D rigidbody2D, Vector3 position, Quaternion rotation) {
      rigidbody2D.SetPositionAndRotation(position, rotation);
      gameObject = rigidbody2D.gameObject;
      health = 50;
    }

    public void Damage(float damage) {
      health -= damage;

      if (health <= 0f) Die();
    }

    private void Die() => Object.Destroy(gameObject);
  }
}