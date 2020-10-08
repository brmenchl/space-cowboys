using Code.Player;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game
{
  public class GameInstaller : MonoInstaller
  {
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject pawnPrefab;

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<GameRunner>().AsSingle();

      Container.BindFactory<ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<ShipInstaller>(shipPrefab);

      Container.BindInstance(bulletPrefab).WhenInjectedInto<ShipInstaller>();

      Container.BindFactory<Pawn, Pawn.Factory>().FromComponentInNewPrefab(pawnPrefab);
    }
  }
}
