using System;
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

    private Guid playerId;
    private CancellationTokenSource controllableHealthStreamToken;
    private Player player;
    private PlayerState playerState;
    private PlayerStreams playerStreams;

    [Inject]
    public void Inject(PlayerState playerState, PlayerStreams playerStreams, Guid playerId) {
      this.playerState = playerState;
      this.playerStreams = playerStreams;
      this.playerId = playerId;
    }

    private void Start() {
      player = playerState
        .GetPlayerById(playerId);
      card.color = player.theme;

      playerStreams.GetHealthStreamById(playerId).Subscribe(UpdateHealth, this.GetCancellationTokenOnDestroy());
      playerStreams.GetControllableStreamById(playerId)
        .Subscribe(UpdateControllable, this.GetCancellationTokenOnDestroy());
    }

    private void UpdateHealth(Health health) {
      healthBar.fillAmount = health.Percent;
      if (health.Percent <= 0f) {
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
      controllableHealthStreamToken =
        CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
      healthStream.Subscribe(UpdateControllableHealth, controllableHealthStreamToken.Token);
    }

    private void HideControllableHealth() {
      controllableHealthStreamToken?.Cancel();
      controllableHealthBarCg.alpha = 0;
    }


    public class Factory : PlaceholderFactory<Guid, CharacterHudCard> { }
  }
}