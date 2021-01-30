using Code.Cowboy;
using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Player {
  public class PlayerController {
    private readonly IControllable controllable;
    private readonly CowboyFacade.Factory cowboyFactory;
    private readonly InputHandler inputHandler;
    private float health;

    public PlayerController(
      InputHandler inputHandler,
      IControllable startingControllable,
      CowboyFacade.Factory cowboyFactory
    ) {
      health = 100f;
      this.inputHandler = inputHandler;
      controllable = startingControllable;
      this.cowboyFactory = cowboyFactory;
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

    // De-spawn cowboy?
    private void Die() => Debug.Log("This player is dead");

    public class Factory : PlaceholderFactory<ControlScheme, IControllable, PlayerController> {
    }
  }
}