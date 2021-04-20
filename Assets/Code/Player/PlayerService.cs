using Code.Input;
using External.Option;
using UnityEngine;

namespace Code.Player {
  public class PlayerService {
    private readonly PlayerState playerState;
    private readonly InputService inputService;

    public PlayerService(PlayerState playerState, InputService inputService) {
      this.playerState = playerState;
      this.inputService = inputService;
    }

    public int AddPlayer(ControlScheme controlScheme) {
      inputService.AddController(controlScheme);
      playerState.players.Add(new Player(controlScheme));
      return playerState.players.Count - 1;
    }

    public void Control(int playerId, IControllable controllable) {
      var player = GetPlayer(playerId);
      player.controllable.MatchSome(oldControllable => oldControllable.ClearController());
      player.controllable = controllable.Some();
      controllable.UpdateController(playerId, inputService.GetInputStream(player.controlScheme));
    }

    public void Damage(int playerId, float value) {
      GetPlayer(playerId).health -= value;
      if (value <= 0f) Debug.Log($"{playerId} is dead");
    }

    public Option<IControllable> GetControllable(int playerId) => GetPlayer(playerId).controllable;

    public Option<int> GetPlayerIdForControllable(IControllable controllable) =>
      playerState.players.FindIndexOrNone(player => player.controllable == controllable.Some());

    private Player GetPlayer(int playerId) => playerState.players[playerId];
  }
}