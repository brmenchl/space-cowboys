using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Player {
  public class PlayerController {
    private readonly Pawn pawn;
    private float health;

    public PlayerController(Pawn pawn, IPossessable startingPossessable) {
      this.pawn = pawn;
      health = 100f;
      startingPossessable.Possess(pawn);
    }

    public void Damage(float damage) {
      health -= damage;

      if (health <= 0f) Die();
    }

    public void Eject() {
      // Spawn cowboy
      // Apply force
    }

    private void Die() {
      Debug.Log("This player is dead");
      // De-spawn cowboy?
    }

    public class Factory : PlaceholderFactory<ControlScheme, IPossessable, PlayerController> {
    }
  }
}