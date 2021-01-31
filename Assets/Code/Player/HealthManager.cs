using UnityEngine;

namespace Code.Player {
  public class HealthManager {
    private float health = 100f;
    private readonly ControllableState controllableState;

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