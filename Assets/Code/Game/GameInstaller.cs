using Code.Bullets;
using Code.Cowboy;
using Code.Input;
using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameInstaller : MonoInstaller {
#pragma warning disable 0649
    [SerializeField] private PrefabRegistry prefabRegistry;
#pragma warning restore 0649

    public override void InstallBindings() {
      Container.BindInterfacesTo<GameRunner>().AsSingle();
      Container.Bind<PrefabRegistry>().FromInstance(prefabRegistry);
      BulletInstaller.Install(Container);

      Container.BindFactory<ControlScheme, IControllable, PlayerFacade, PlayerFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<PlayerInstaller>(prefabRegistry.pawnPrefab)
        .UnderTransformGroup("Players");
      Container.Bind<PlayerRegistry>().AsSingle();

      Container.BindFactory<Vector3, Quaternion, ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<ShipInstaller>(prefabRegistry.shipPrefab);

      Container.BindFactory<Vector3, Quaternion, CowboyFacade, CowboyFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<CowboyInstaller>(prefabRegistry.cowboyPrefab);
    }
  }
}