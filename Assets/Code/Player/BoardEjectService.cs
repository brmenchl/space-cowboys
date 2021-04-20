using System;
using Code.Cowboy;
using Code.Input;
using External.Option;
using UnityEngine;

namespace Code.Player {
  public class BoardEjectService {
    private readonly CowboyFacade.Factory cowboyFactory;
    private readonly PlayerService playerService;

    public BoardEjectService(CowboyFacade.Factory cowboyFactory, PlayerService playerService) {
      this.cowboyFactory = cowboyFactory;
      this.playerService = playerService;
    }

    public void Eject(int playerId) {
      const int ejectDistance = 3; // TODO: settings;
      playerService.GetControllable(playerId)
        .CallValidated(c => c.Type == ControllableType.Vehicle,
          currentControllable => {
            var cowboy =
              cowboyFactory.Create(currentControllable.Position +
                                   (UnityEngine.Random.insideUnitCircle.normalized * ejectDistance),
                Quaternion.AngleAxis(UnityEngine.Random.Range(-180, 180), Vector3.forward));
            cowboy.ApplyEjectForce();
            playerService.Control(playerId, cowboy);
          });
    }

    private void Eject(IControllable controllable) =>
      playerService.GetPlayerIdForControllable(controllable).MatchSome(Eject);

    public void Board(int playerId, IControllable boardSubject) =>
      playerService.GetControllable(playerId)
        .CallValidated(c => c.Type == ControllableType.Cowboy && boardSubject.Type == ControllableType.Vehicle,
          currentControllable => {
            Eject(boardSubject);
            currentControllable.Destroy();
            playerService.Control(playerId, boardSubject);
          });
  }

  internal static class BoardEjectExtensions {
    public static void CallValidated(this Option<IControllable> controllable,
      Predicate<IControllable> pred,
      Action<IControllable> f) =>
      controllable.Match(c => {
          if (!pred(c)) throw new Exception("Invalid controllable");
          f(c);
        },
        () => throw new Exception("Controllable does not exist")
      );
  }
}