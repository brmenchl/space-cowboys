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

    public PlayerService(PlayerState playerState, InputService inputService, Player.Factory playerFactory) {
      this.playerState = playerState;
      this.inputService = inputService;
      this.playerFactory = playerFactory;
    }

    public Player AddPlayer(ControlScheme controlScheme) {
      inputService.AddController(controlScheme);
      var player = playerFactory.Create(controlScheme, playerColors[playerState.players.Count]);
      playerState.players.Add(player);
      return player;
    }

    public void Control(Player player, IControllable controllable) {
      player.controllable.Value.MatchSome(oldControllable => oldControllable.ClearController());
      player.controllable.Value = controllable.Some();
      controllable.UpdateController(player.theme, inputService.GetInputStream(player.controlScheme));
    }

    public void Damage(Player player, float value) {
      player.health.Value -= value;
      if (player.health.Value <= 0f) {
        player.controllable.Value.MatchSome(c => c.Destroy());
        player.controllable.Value = Option.None<IControllable>();
      }
    }

    public Option<Player> GetPlayerForControllable(IControllable controllable) =>
      playerState.players.FirstOrNone(player => player.controllable == controllable.Some());
  }
}