using Code.Players;
using UnityEngine;
using Zenject;

namespace Code.Hud {
  public class HudInstaller : MonoInstaller<HudInstaller> {
    [SerializeField] private GameObject characterHudCard;

    public override void InstallBindings() =>
      Container.BindFactory<Player, CharacterHudCard, CharacterHudCard.Factory>()
        .FromComponentInNewPrefab(characterHudCard);
  }
}