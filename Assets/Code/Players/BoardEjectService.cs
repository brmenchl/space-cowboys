using System;
using Code.Cowboy;
using static Code.Utilities.TransformHelpers;

namespace Code.Players {
  public class BoardEjectService {
    private const int ejectDistance = 3; // TODO: settings;
    private readonly CowboyFacade.Factory cowboyFactory;
    private readonly PlayerService playerService;

    public BoardEjectService(CowboyFacade.Factory cowboyFactory, PlayerService playerService) {
      this.cowboyFactory = cowboyFactory;
      this.playerService = playerService;
    }

    public void Eject(IControllable controllable) =>
      playerService.GetPlayerForControllable(controllable)
        .MatchSome(playerId => {
          controllable
            .CallValidated(c => c.Type == ControllableType.Vehicle,
              currentControllable => {
                var cowboy = cowboyFactory.Create(
                  RandomPositionNear(currentControllable.Position, ejectDistance),
                  RandomRotation()
                );
                cowboy.ApplyEjectForce();
                playerService.Control(playerId, cowboy);
              });
        });

    public void Board(IControllable controllable, IControllable boardSubject) =>
      playerService.GetPlayerForControllable(controllable)
        .MatchSome(playerId => {
          controllable
            .CallValidated(c => c.Type == ControllableType.Cowboy && boardSubject.Type == ControllableType.Vehicle,
              currentControllable => {
                Eject(boardSubject);
                currentControllable.Destroy();
                playerService.Control(playerId, boardSubject);
              });
        });
  }

  internal static class BoardEjectExtensions {
    public static void CallValidated(
      this IControllable controllable,
      Predicate<IControllable> predicate,
      Action<IControllable> f
    ) {
      if (!predicate(controllable)) throw new Exception("Invalid controllable");
      f(controllable);
    }
  }
}