using System;
using Code.Input;
using Code.Player;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyFacade : IControllable, IPlayerDamageable, IBoardable {
    private readonly CowboyModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;

    public CowboyFacade(
      CowboyModel model,
      MoveHandler moveHandler,
      ShootHandler shootHandler
    ) {
      this.model = model;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
    }

    public event Action<IControllable> OnBoarded;

    public void Thrust(float amount) {
      // noop
    }

    public void Turn(float amount) => moveHandler.Turn(amount);

    public void Shoot() => shootHandler.Shoot();

    public void Alt() {
      // LASSO
    }

    public event Action<float> OnDamaged;
    public void Destroy() => model.Destroy();

    public void Eject() => moveHandler.Eject();

    public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade> {
    }
  }
}