using Code.Input;
using UnityEngine;

namespace Code.Player {
  public class ControllableListener {
    private readonly EjectionManager ejectionManager;
    private readonly HealthManager healthManager;

    public ControllableListener(
      ControllableState controllableState,
      EjectionManager ejectionManager,
      HealthManager healthManager
    ) {
      this.ejectionManager = ejectionManager;
      this.healthManager = healthManager;
      controllableState.OnNewControllable += HandleNew;
      controllableState.IfIsControlling(Subscribe);
    }

    private void HandleNew(IControllable oldControllable, IControllable newControllable) {
      Unsubscribe(oldControllable);
      Subscribe(newControllable);
    }

    private void Unsubscribe(IControllable controllable) {
      if (controllable is IEjectable ejectable)
        ejectable.OnEjected -= HandleEject;
      if (controllable is IBoardable boardable)
        boardable.OnBoarded -= HandleBoard;
      if (controllable is IPlayerDamageable damageable)
        damageable.OnDamaged -= HandleDamage;
    }

    private void Subscribe(IControllable controllable) {
      if (controllable is IEjectable ejectable)
        ejectable.OnEjected += HandleEject;
      if (controllable is IBoardable boardable)
        boardable.OnBoarded += HandleBoard;
      if (controllable is IPlayerDamageable damageable)
        damageable.OnDamaged += HandleDamage;
    }


    private void HandleEject(Vector2 position) => ejectionManager.Eject(position);

    private void HandleBoard(IControllable toBeBoarded) => ejectionManager.Board(toBeBoarded);

    private void HandleDamage(float damage) => healthManager.Damage(damage);
  }
}