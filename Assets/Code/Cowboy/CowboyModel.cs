using UnityEngine;

namespace Code.Cowboy {
  public class CowboyModel {
    private readonly GameObject gameObject;
    private float health;

    public CowboyModel(Transform transform, Vector3 position, Quaternion rotation) {
      transform.position = position;
      transform.rotation = rotation;
      gameObject = transform.gameObject;
      health = 50;
    }

    public void Damage(float damage) {
      health -= damage;

      if (health <= 0f) Die();
    }

    private void Die() => Object.Destroy(gameObject);
  }
}