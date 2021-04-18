using System;
using Code.Input;
using Code.Player;
using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipFacade : IControllable, IEjectable {
    private readonly ShipModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;

    public ShipFacade(ShipModel model, MoveHandler moveHandler, ShootHandler shootHandler) {
      this.model = model;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
      model.OnDestroyed += Eject;
    }

    public void Thrust(float amount) => moveHandler.Thrust(amount);
    public void Turn(float amount) => moveHandler.Turn(amount);
    public void Shoot() => shootHandler.Shoot();
    public void Alt() => Eject();

    public event Action<Vector2> OnEjected;

    public void Eject() => OnEjected?.Invoke(model.transform.position);
    public void Damage(float damage) => model.Damage(damage);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, ShipFacade> {
    }
  }
}