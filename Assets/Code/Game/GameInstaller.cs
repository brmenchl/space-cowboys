using Code.Cowboy;
using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameInstaller : MonoInstaller {
    [SerializeField] private GameObject shipPrefab = null;
    [SerializeField] private GameObject cowboyPrefab = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject pawnPrefab = null;

    public override void InstallBindings() {
      Container.BindInterfacesTo<GameRunner>().AsSingle();

      Container.BindFactory<Vector3, Quaternion, ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<ShipInstaller>(shipPrefab);

      Container.BindFactory<Vector3, Quaternion, CowboyFacade, CowboyFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<CowboyInstaller>(cowboyPrefab);

      Container.BindInstance(bulletPrefab).WhenInjectedInto<ShipInstaller>();
      Container.BindInstance(bulletPrefab).WhenInjectedInto<CowboyInstaller>();

      Container.BindFactory<string, Pawn, Pawn.Factory>().FromComponentInNewPrefab(pawnPrefab);
    }
  }
}