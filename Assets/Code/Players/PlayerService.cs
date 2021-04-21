using System.Linq;
using Code.Input;
using External.Option;
using UnityEngine;

namespace Code.Players {
  public class PlayerService {
    private readonly InputService inputService;
    private readonly PlayerState playerState;
    private static readonly Color[] playerColors = { Color.HSVToRGB(0.68f, 0.5f, 1f), Color.HSVToRGB(0f, 0.5f, 1f) };

    public PlayerService(PlayerState playerState, InputService inputService) {
      this.playerState = playerState;
      this.inputService = inputService;
    }

    public Player AddPlayer(ControlScheme controlScheme) {
      inputService.AddController(controlScheme);
      var player = new Player(controlScheme, playerColors[playerState.players.Count]);
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
      if (value <= 0f) Debug.Log("player is dead");
    }

    public Option<Player> GetPlayerForControllable(IControllable controllable) =>
      playerState.players.FirstOrNone(player => player.controllable == controllable.Some());
  }
}