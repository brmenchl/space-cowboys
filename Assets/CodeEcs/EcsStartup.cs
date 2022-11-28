using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace CodeEcs {
  public class EcsStartup : MonoBehaviour {
    private EcsWorld ecsWorld;
    private EcsSystems systems;
    [Inject] private IEnumerable<IEcsSystem> registeredSystems;

    private void Start() {
      ecsWorld = new EcsWorld();

      systems = new EcsSystems(ecsWorld);

      foreach (var system in registeredSystems) {
        systems.Add(system);
      }

      systems
#if UNITY_EDITOR
          // add debug systems for custom worlds here, for example:
          .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
          .Inject()
          .Init();
    }

    private void Update() {
      systems.Run();
    }

    private void OnDestroy() {
      systems.Destroy();
      ecsWorld.Destroy();
    }
  }
}