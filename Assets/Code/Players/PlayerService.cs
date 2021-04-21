using Code.Input;
using External.Option;
using UnityEngine;

namespace Code.Players {
  public class PlayerService {
    private readonly InputService inputService;
    private readonly PlayerState playerState;

    public PlayerService(PlayerState playerState, InputService inputService) {
      this.playerState = playerState;
      this.inputService = inputService;
    }

    public Player AddPlayer(ControlScheme controlScheme) {
      inputService.AddController(controlScheme);
      var player = new Player(controlScheme);
      playerState.players.Add(player);
      return player;
    }

    public void Control(Player player, IControllable controllable) {
      player.controllable.MatchSome(oldControllable => oldControllable.ClearController());
      player.controllable = controllable.Some();
      controllable.UpdateController(inputService.GetInputStream(player.controlScheme));
    }

    public void Damage(Player player, float value) {
      player.health -= value;
      if (value <= 0f) Debug.Log($"player is dead");
    }

    public Option<Player> GetPlayerForControllable(IControllable controllable) =>
      playerState.players.FirstOrNone(player => player.controllable == controllable.Some());
  }
}