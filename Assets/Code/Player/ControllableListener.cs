using Code.Input;
using UnityEngine;
using Cysharp.Threading.Tasks.Linq;
using Code.Option;

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
      UniTaskAsyncEnumerable.Pairwise(controllableState.controllable).Subscribe(HandleNew);
      controllableState.IfIsControlling(Subscribe);
    }

    private void HandleNew((Option<IControllable> oldControllable, Option<IControllable> newControllable) c) {
      c.oldControllable.MatchSome(Unsubscribe);
      c.newControllable.MatchSome(Subscribe);
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