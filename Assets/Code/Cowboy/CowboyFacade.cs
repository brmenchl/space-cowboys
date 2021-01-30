using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyFacade : IControllable {
    private readonly MoveHandler moveHandler;
    private readonly ShootHandler shootHandler;

    public CowboyFacade(MoveHandler moveHandler, ShootHandler shootHandler) {
      this.moveHandler = moveHandler;
      this.shootHandler = shootHandler;
    }

    public void Thrust(float amount) {
      // noop
    }

    public void Turn(float amount) => moveHandler.Turn(amount);

    public void Shoot() => shootHandler.Shoot();

    public class Factory : PlaceholderFactory<Vector3, Quaternion, CowboyFacade> {
    }
  }
}