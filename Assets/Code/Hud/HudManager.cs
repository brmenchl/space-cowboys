using System;
using System.Collections.Generic;
using System.Linq;
using Code.Player;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Zenject;

namespace Code.Hud {
  public class HudManager : MonoBehaviour {
    [SerializeField] private GameObject bottomHud;
    private readonly List<CharacterHudCard> cards = new List<CharacterHudCard>();

    [Inject]
    public void Inject(PlayerState playerState, CharacterHudCard.Factory cardFactory) =>
      UniTaskAsyncEnumerable.EveryValueChanged(playerState, ps => ps.players.Count).Subscribe(count => {
        for (var i = cards.Count; i < count; i++) {
          var characterHudCard = cardFactory.Create(i);
          characterHudCard.gameObject.transform.SetParent(bottomHud.transform);
          cards.Add(characterHudCard);
        }
      });

    // private void OnDestroy() {
    //   foreach (var disposable in disposables) {
    //     disposable.Dispose();
    //   }
    //   disposables = Enumerable.Empty<IDisposable>();
    // }
  }
}