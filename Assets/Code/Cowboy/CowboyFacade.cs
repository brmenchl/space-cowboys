using System;
using Code.Input;
using Code.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyFacade : IControllable, IPlayerDamageable, IBoardable {
    private readonly Lasso.Lasso.Factory lassoFactory;
    private readonly CowboyModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;
    private bool hasFiredLasso;

    public CowboyFacade(
      CowboyModel model,
      MoveHandler moveHandler,
      ShootHandler shootHandler,
      Lasso.Lasso.Factory lassoFactory
    ) {
      this.model = model;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
      this.lassoFactory = lassoFactory;
    }

    public event Action<IControllable> OnBoarded;

    public void Thrust(float amount) {
      // noop
    }

    public void Turn(float amount) => moveHandler.Turn(amount);

    public void Shoot() => shootHandler.Shoot();

    public void Alt() => FireLasso().Forget();

    public event Action<float> OnDamaged;

    private async UniTaskVoid FireLasso() {
      if (!hasFiredLasso) {
        hasFiredLasso = true;
        lassoFactory.Create();
        await UniTask.Delay(1000);
        hasFiredLasso = false;
      }
    }

    public void Destroy() => model.Destroy();

    public void Eject() => moveHandler.Eject();

    public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade> {
    }
  }
}