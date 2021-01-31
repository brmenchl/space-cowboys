using UnityEngine;

namespace Code.Player {
  public class HealthManager {
    private readonly ControllableState controllableState;
    private float health = 100f;

    public HealthManager(ControllableState controllableState) => this.controllableState = controllableState;

    public void Damage(float damage) {
      health -= damage;
      if (health <= 0f) Die();
    }

    private void Die() =>
      controllableState.IfIsControlling(c => {
        Debug.Log("You Dead");
      });
  }
}