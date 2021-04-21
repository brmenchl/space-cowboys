using Code.Players;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Hud {
  public class CharacterHudCard : MonoBehaviour {
    [SerializeField] private Image avatar;
    [Inject] private Player player;

    private void Start() {
      var token = this.GetCancellationTokenOnDestroy();
      player.health.Subscribe(UpdateHealth, token);
      player.controllable.Subscribe(UpdateControllableDisplay, token);
    }

    private void UpdateHealth(float health) => Debug.Log($"{player.controllable} {health}");

    private void UpdateControllableDisplay(Option<IControllable> controllable) =>
      avatar.sprite = controllable.Match(c => c.Sprite, () => null);

    public class Factory : PlaceholderFactory<Player, CharacterHudCard> {
    }
  }
}