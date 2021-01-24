using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Player {
  public class PlayerInstaller : Installer<GameObject, PlayerInstaller> {
    private readonly GameObject pawnPrefab;

    public PlayerInstaller(GameObject pawnPrefab) => this.pawnPrefab = pawnPrefab;

    public override void InstallBindings() {
      Container.BindFactory<string, Pawn, Pawn.Factory>().FromComponentInNewPrefab(pawnPrefab);
      Container.Bind<InputState>().AsTransient().WhenInjectedInto<Pawn>();
    }
  }
}