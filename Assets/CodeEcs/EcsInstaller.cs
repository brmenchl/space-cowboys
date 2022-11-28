using Code.Game;
using Code.Input;
using CodeEcs.Archetypes;
using CodeEcs.Systems;
using UnityEngine;
using Zenject;

namespace CodeEcs {
  public class EcsInstaller : MonoInstaller {
    [SerializeField] private PrefabRegistry prefabRegistry;

    public override void InstallBindings() {
      Container.Bind<PrefabRegistry>().FromInstance(prefabRegistry).AsSingle();
      InputInstaller.Install(Container, prefabRegistry.controllerPrefab);

      Container.Bind<ShipArchetypeFactory>().AsSingle();
      Container.Bind<CowboyArchetypeFactory>().AsSingle();
      Container.Bind<BulletArchetypeFactory>().AsSingle();

      // Systems
      Container.BindInterfacesTo<PlayerInitSystem>().AsSingle();
      Container.BindInterfacesTo<MoveForwardSystem>().AsSingle();
      Container.BindInterfacesTo<ForceMovementSystem>().AsSingle();
      Container.BindInterfacesTo<FiringSystem>().AsSingle();
      Container.BindInterfacesTo<CleanUpFiringSystem>().AsSingle();
      Container.BindInterfacesTo<EjectSystem>().AsSingle();
      Container.BindInterfacesTo<DestroyAfterTimeSystem>().AsSingle();
    }
  }
}