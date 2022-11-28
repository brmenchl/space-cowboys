using System;
using Code.Input;
using External.Option;
using UnityEngine;

namespace Code.Players {
  public class PlayerService {
    private readonly InputService inputService;
    private readonly PlayerState playerState;
    private readonly Player.Factory playerFactory;
    private static readonly Color[] playerColors = { Color.HSVToRGB(0.68f, 0.5f, 1f), Color.HSVToRGB(0f, 0.5f, 1f) };

    public PlayerService(
      PlayerState playerState,
      InputService inputService,
      Player.Factory playerFactory
    ) {
      this.playerState = playerState;
      this.inputService = inputService;
      this.playerFactory = playerFactory;
    }

    public Guid AddPlayer(ControlScheme controlScheme) {
      inputService.AddController(controlScheme);
      var player = playerFactory.Create(controlScheme, playerColors[playerState.players.Count]);
      playerState.players.Add(player);
      return player.id;
    }

    public void Control(Guid playerId, IControllable controllable) {
      var player = playerState.GetPlayerById(playerId);
      player.controllable.Value.MatchSome(oldControllable => oldControllable.ClearController());
      player.controllable.Value = controllable.Some();
      // controllable.UpdateController(player.theme,inputService.GetInputStream(player.controlScheme)); // TODO: controllable state
    }

    public void Damage(Guid playerId, float value) {
      var player = playerState.GetPlayerById(playerId);
      player.health.Value -= value;
      if (player.health.Value <= 0f) {
        player.controllable.Value.MatchSome(c => c.Destroy());
        player.controllable.Value = Option.None<IControllable>();
      }
    }


    public Option<Guid> GetPlayerForControllable(IControllable controllable) =>
      playerState.GetPlayerBy(player => player.controllable == controllable.Some()).Map(p => p.id);
  }
}