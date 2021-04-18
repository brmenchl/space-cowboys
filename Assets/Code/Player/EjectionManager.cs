using Code.Cowboy;
using Code.Input;
using Code.Ship;
using UnityEngine;

namespace Code.Player {
  public class EjectionManager {
    private readonly ControllableState controllableState;
    private readonly CowboyFacade.Factory cowboyFactory;

    public EjectionManager(CowboyFacade.Factory cowboyFactory, ControllableState controllableState) {
      this.cowboyFactory = cowboyFactory;
      this.controllableState = controllableState;
    }

    public void Eject(Vector2 position) {
      const int ejectDistance = 3;
      var cowboy =
        cowboyFactory.Create(position + (Random.insideUnitCircle.normalized * ejectDistance),
          Quaternion.AngleAxis(Random.Range(-180, 180), Vector3.forward));
      cowboy.Eject();
      controllableState.Control(cowboy);
    }

    public void Board(IControllable toBeBoarded) =>
      controllableState.IfIsControlling(c => {
        if (c is CowboyFacade cf && toBeBoarded is IEjectable ejectable) {
          ejectable.Eject();
          cf.Destroy();
          controllableState.Control(toBeBoarded);
        }
      });
  }
}