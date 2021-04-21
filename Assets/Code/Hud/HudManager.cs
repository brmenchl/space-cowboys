using System;
using System.Collections.Generic;
using System.Linq;
using Code.Players;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Zenject;

namespace Code.Hud {
  public class HudManager : MonoBehaviour {
    [SerializeField] private GameObject bottomHud;
    private readonly List<CharacterHudCard> cards = new List<CharacterHudCard>();
    [Inject] private CharacterHudCard.Factory cardFactory;
    [Inject] private PlayerStreams playerStreams;
    private IDisposable disposable;

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