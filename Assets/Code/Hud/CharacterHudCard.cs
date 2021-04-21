using System;
using Code.Input;
using Code.Players;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Hud {
  public class CharacterHudCard : MonoBehaviour {
    private Player player;
    private IDisposable disposable;

    [SerializeField] private Image avatar;

    [Inject]
    public void Inject(Player player) => this.player = player;

    private void Start() {
      UpdateHud(player.health);
      UpdateC(player.controllable);
      disposable = UniTaskAsyncEnumerable.EveryValueChanged(player, p => p.health).Subscribe(UpdateHud);
      disposable = UniTaskAsyncEnumerable.EveryValueChanged(player, p => p.controllable).Subscribe(UpdateC);
    }

    private void OnDestroy() => disposable.Dispose();

    private void UpdateHud(float health) {
      Debug.Log(health);
    }

    private void UpdateC(Option<IControllable> controllable) =>
      avatar.sprite = controllable.Match(c => c.Sprite, () => null);

    public class Factory : PlaceholderFactory<Player, CharacterHudCard> {
    }
  }
}