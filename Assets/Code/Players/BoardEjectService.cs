using System;
using Code.Cowboy;
using External.Option;
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
      playerService.GetPlayerForControllable(controllable).MatchSome(player => {
        player.controllable.Value
          .CallValidated(c => c.Type == ControllableType.Vehicle,
            currentControllable => {
              var cowboy = cowboyFactory.Create(
                RandomPositionNear(currentControllable.Position, ejectDistance),
                RandomRotation()
              );
              cowboy.ApplyEjectForce();
              playerService.Control(player, cowboy);
            });
      });

    public void Board(IControllable controllable, IControllable boardSubject) =>
      playerService.GetPlayerForControllable(controllable).MatchSome(player => {
        controllable.Some()
          .CallValidated(c => c.Type == ControllableType.Cowboy && boardSubject.Type == ControllableType.Vehicle,
            currentControllable => {
              Eject(boardSubject);
              currentControllable.Destroy();
              playerService.Control(player, boardSubject);
            });
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