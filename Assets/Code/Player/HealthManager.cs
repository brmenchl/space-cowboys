using System;
using UnityEngine;

namespace Code.Player {
  public class HealthManager {
    private readonly ControllableState controllableState;
    private float health;

    public HealthManager(ControllableState controllableState, Settings settings) {
      this.controllableState = controllableState;
      health = settings.startingHealth;
    }

    public void Damage(float damage) {
      health -= damage;
      if (health <= 0f) Die();
    }

    private void Die() =>
      controllableState.IfIsControlling(c => {
        Debug.Log("You Dead");
      });

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}