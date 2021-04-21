using Code.Cowboy;
using Code.Input;
using Code.Players;
using Code.Ship;
using Cysharp.Threading.Tasks;
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

    public void Initialize() => Run().Forget();

    private async UniTaskVoid Run() {
      var ship = shipFactory.Create(new Vector2(0, 0), Quaternion.AngleAxis(90, Vector3.forward));
      var player1 = playerService.AddPlayer(ControlScheme.WasdKeyboard);
      playerService.Control(player1, ship);

      await UniTask.Delay(1500);

      var cowboy = cowboyFactory.Create(new Vector2(0, -5), Quaternion.identity);
      var player2 = playerService.AddPlayer(ControlScheme.ArrowsKeyboard);
      playerService.Control(player2, cowboy);

      shipFactory.Create(new Vector2(5, 0), Quaternion.identity);
    }
  }
}