using Code.Input;
using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameRunner : IInitializable {
    private readonly PlayerFacade.Factory playerFactory;
    private readonly ShipFacade.Factory shipFactory;

    public GameRunner(
      PlayerFacade.Factory playerFactory,
      ShipFacade.Factory shipFactory
    ) {
      this.playerFactory = playerFactory;
      this.shipFactory = shipFactory;
    }

    public void Initialize() {
      var ship1 = shipFactory.Create(new Vector3(0, 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
      playerFactory.Create(ControlScheme.WasdKeyboard, ship1);

      var ship2 = shipFactory.Create(new Vector3(3, 0, 0), Quaternion.identity);
      playerFactory.Create(ControlScheme.ArrowsKeyboard, ship2);
    }
  }
}