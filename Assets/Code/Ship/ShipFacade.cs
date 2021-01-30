using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipFacade : IControllable {
    private readonly ShipModel model;
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;

    public ShipFacade(ShipModel model, MoveHandler moveHandler, ShootHandler shootHandler, Transform transform) {
      this.transform = transform;
      this.model = model;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
    }

    public void Thrust(float amount) => moveHandler.Thrust(amount);

    public void Turn(float amount) => moveHandler.Turn(amount);

    public void Shoot() => shootHandler.Shoot();
    public Transform transform { get; }

    public void Damage(float damage) => model.Damage(damage);

    public class Factory : PlaceholderFactory<Vector3, Quaternion, ShipFacade> {
    }
  }
}