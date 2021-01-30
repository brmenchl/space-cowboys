using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyFacade : IControllable {
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;

    public CowboyFacade(Transform transform, MoveHandler moveHandler, ShootHandler shootHandler) {
      this.transform = transform;
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
    }

    public void Thrust(float amount) {
      // noop
    }

    public void Turn(float amount) => moveHandler.Turn(amount);

    public void Shoot() => shootHandler.Shoot();
    public Transform transform { get; }

    public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade> {
    }
  }
}