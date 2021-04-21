using System;
using System.Collections.Generic;
using System.Linq;
using Code.Players;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Zenject;

namespace Code.Hud {
  public class HudManager : MonoBehaviour {
    private CharacterHudCard.Factory cardFactory;
    private PlayerStreams playerStreams;
    private readonly List<CharacterHudCard> cards = new List<CharacterHudCard>();
    private IDisposable disposable;
    [SerializeField] private GameObject bottomHud;

    [Inject]
    public void Inject(PlayerStreams playerStreams, CharacterHudCard.Factory cardFactory) {
      this.playerStreams = playerStreams;
      this.cardFactory = cardFactory;
    }

    private void Start() => disposable = playerStreams.CountStream.Subscribe(SyncCardList);

    private void OnDestroy() => disposable.Dispose();

    private void SyncCardList(List<Player> players) {
      foreach (var player in players.Skip(cards.Count)) {
        var characterHudCard = cardFactory.Create(player);
        characterHudCard.gameObject.transform.SetParent(bottomHud.transform);
        cards.Add(characterHudCard);
      }
    }
  }
}