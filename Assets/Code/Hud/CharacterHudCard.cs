using System.Threading;
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
    [SerializeField] private Image controllableHealthBar;
    [SerializeField] private CanvasGroup controllableHealthBarCg;

    private Player player;
    private CancellationTokenSource controllableHealthStreamToken;

    [Inject]
    public void Inject(Player player) => this.player = player;

    private void Start() {
      var token = this.GetCancellationTokenOnDestroy();
      player.healthPercentStream.Subscribe(UpdateHealth, token);
      player.controllable.Subscribe(UpdateControllable, token);
      card.color = player.theme;
    }

    private void OnDestroy() {
      controllableHealthStreamToken?.Cancel();
    }

    private void UpdateHealth(float healthPercent) {
      healthBar.fillAmount = healthPercent;
      if (healthPercent <= 0f) {
        Destroy(gameObject);
      }
    }

    private void UpdateControllableHealth(float healthPercent) => controllableHealthBar.fillAmount = healthPercent;

    private void UpdateControllable(Option<IControllable> controllable) {
      controllable.MatchSome(c => {
        avatar.sprite = c.Sprite;
        c.health.Match(BindControllableHealth, HideControllableHealth);
      });
      avatar.sprite = controllable.Match(c => c.Sprite, () => null);
    }

    private void BindControllableHealth(ReadOnlyAsyncReactiveProperty<float> healthStream) {
      controllableHealthBarCg.alpha = 1;
      controllableHealthStreamToken?.Cancel();
      controllableHealthStreamToken = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
      healthStream.Subscribe(UpdateControllableHealth, controllableHealthStreamToken.Token);
    }

    private void HideControllableHealth() {
      controllableHealthStreamToken?.Cancel();
      controllableHealthBarCg.alpha = 0;
    }


    public class Factory : PlaceholderFactory<Player, CharacterHudCard> {
    }
  }
}