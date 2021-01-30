using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Player {
  public class PlayerController {
    private float health;

    public PlayerController(InputHandler inputHandler, IControllable startingControllable) {
      health = 100f;
      inputHandler.Possess(startingControllable);
    }

    public void Damage(float damage) {
      health -= damage;

      if (health <= 0f) Die();
    }

    public void Eject() {
      // Spawn cowboy
      // Apply force
    }

    private void Die() => Debug.Log("This player is dead");

    // De-spawn cowboy?
    public class Factory : PlaceholderFactory<ControlScheme, IControllable, PlayerController> {
    }
  }
}