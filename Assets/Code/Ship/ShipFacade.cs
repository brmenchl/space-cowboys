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
    }

    public void Thrust(float amount) => moveHandler.Thrust(amount);

    public void Turn(float amount) => moveHandler.Turn(amount);

    public void Shoot() => shootHandler.Shoot();
    public void Alt() => OnEjected?.Invoke(model.transform.position);

    public event Action<Vector2> OnEjected;

    public void Damage(float damage) => model.Damage(damage);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, ShipFacade> {
    }
  }
}