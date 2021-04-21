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
    [SerializeField] private Image card;
    [SerializeField] private Image healthBar;
    private Player player;

    [Inject]
    public void Inject(Player player) => this.player = player;

    private void Start() {
      var token = this.GetCancellationTokenOnDestroy();
      player.health.Subscribe(UpdateHealth, token);
      player.controllable.Subscribe(UpdateControllableDisplay, token);
      card.color = player.theme;
    }

    private void UpdateHealth(float health) => healthBar.fillAmount = health / 100f;

    private void UpdateControllableDisplay(Option<IControllable> controllable) =>
      avatar.sprite = controllable.Match(c => c.Sprite, () => null);

    public class Factory : PlaceholderFactory<Player, CharacterHudCard> {
    }
  }
}