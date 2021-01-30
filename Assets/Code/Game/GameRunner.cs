using Code.Player;
using Code.Player.Input;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameRunner : IInitializable {
    private readonly PlayerController.Factory playerFactory;
    private readonly ShipFacade.Factory shipFactory;

    public GameRunner(PlayerController.Factory playerFactory,
      ShipFacade.Factory shipFactory) {
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