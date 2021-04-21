using System;
using Code.Players;
using Cysharp.Threading.Tasks.Linq;
using External.Option;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Hud {
  public class CharacterHudCard : MonoBehaviour {
    [SerializeField] private Image avatar;
    private IDisposable disposable;
    [Inject] private Player player;

    private void Start() {
      UpdateHud(player.health);
      UpdateC(player.controllable);
      disposable = UniTaskAsyncEnumerable.EveryValueChanged(player, p => p.health).Subscribe(UpdateHud);
      disposable = UniTaskAsyncEnumerable.EveryValueChanged(player, p => p.controllable).Subscribe(UpdateC);
    }

    private void OnDestroy() => disposable.Dispose();

    private void UpdateHud(float health) => Debug.Log(health);

    private void UpdateC(Option<IControllable> controllable) =>
      avatar.sprite = controllable.Match(c => c.Sprite, () => null);

    public class Factory : PlaceholderFactory<Player, CharacterHudCard> {
    }
  }
}