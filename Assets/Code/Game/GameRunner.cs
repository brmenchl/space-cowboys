using Code.Cowboy;
using Code.Input;
using Code.Players;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameRunner : IInitializable {
    private readonly CowboyFacade.Factory cowboyFactory;
    private readonly PlayerService playerService;
    private readonly ShipFacade.Factory shipFactory;

    public GameRunner(
      ShipFacade.Factory shipFactory,
      CowboyFacade.Factory cowboyFactory,
      PlayerService playerService
    ) {
      this.shipFactory = shipFactory;
      this.cowboyFactory = cowboyFactory;
      this.playerService = playerService;
    }

    public void Initialize() {
      var ship = shipFactory.Create(new Vector3(0, 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
      var player1 = playerService.AddPlayer(ControlScheme.WasdKeyboard);
      playerService.Control(player1, ship);

      var cowboy = cowboyFactory.Create(new Vector3(0, -5, 0), Quaternion.identity);
      var player2 = playerService.AddPlayer(ControlScheme.ArrowsKeyboard);
      playerService.Control(player2, cowboy);
    }
  }
}