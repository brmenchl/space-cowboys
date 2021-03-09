using Code.Bullets;
using Code.Cowboy;
using Code.Input;
using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameInstaller : MonoInstaller {
    [SerializeField] private PrefabRegistry prefabRegistry;

    public override void InstallBindings() {
      Container.BindInterfacesTo<GameRunner>().AsSingle();
      Container.Bind<PrefabRegistry>().FromInstance(prefabRegistry);
      BulletInstaller.Install(Container);

      Container.BindFactory<ControlScheme, IControllable, PlayerFacade, PlayerFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<PlayerInstaller>(prefabRegistry.pawnPrefab)
        .UnderTransformGroup("Players");

      Container.BindFactory<Vector3, Quaternion, ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<ShipInstaller>(prefabRegistry.shipPrefab);

      Container.BindFactory<Vector3, Quaternion, CowboyFacade, CowboyFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<CowboyInstaller>(prefabRegistry.cowboyPrefab);
    }
  }
}