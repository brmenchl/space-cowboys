using System;
using System.Collections.Generic;
using System.Linq;
using Code.Players;
using Code.Utilities.Extensions;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using Zenject;

namespace Code.Hud {
  public class HudManager : MonoBehaviour {
    [SerializeField] private GameObject bottomHud;
    private CharacterHudCard.Factory cardFactory;
    private PlayerStreams playerStreams;
    private readonly List<CharacterHudCard> cards = new List<CharacterHudCard>();

    [Inject]
    public void Inject(PlayerStreams playerStreams, CharacterHudCard.Factory cardFactory) {
      this.playerStreams = playerStreams;
      this.cardFactory = cardFactory;
    }

    private void Start() => playerStreams.countStream.Subscribe(SyncCardList, this.GetCancellationTokenOnDestroy());

    private void SyncCardList(IEnumerable<Guid> playerIds) =>
      playerIds
        .Skip(cards.Count)
        .ForEach(playerId => {
          var characterHudCard = cardFactory.Create(playerId);
          characterHudCard.gameObject.transform.SetParent(bottomHud.transform);
          cards.Add(characterHudCard);
        });
  }
}