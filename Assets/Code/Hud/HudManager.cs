using System;
using System.Collections.Generic;
using System.Linq;
using Code.Players;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Zenject;

namespace Code.Hud {
  public class HudManager : MonoBehaviour {
    [SerializeField] private GameObject bottomHud;
    [Inject] private CharacterHudCard.Factory cardFactory;
    [Inject] private PlayerStreams playerStreams;
    private readonly List<CharacterHudCard> cards = new List<CharacterHudCard>();

    private void Start() => playerStreams.CountStream.Subscribe(SyncCardList, this.GetCancellationTokenOnDestroy());

    private void SyncCardList(List<Player> players) {
      foreach (var player in players.Skip(cards.Count)) {
        var characterHudCard = cardFactory.Create(player);
        characterHudCard.gameObject.transform.SetParent(bottomHud.transform);
        cards.Add(characterHudCard);
      }
    }
  }
}