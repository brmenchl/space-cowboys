using Code.Game;
using Code.Input;
using CodeEcs.Archetypes;
using CodeEcs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Zenject;

namespace CodeEcs.Systems {
  public class PlayerInitSystem : IEcsInitSystem {
    [Inject] private PrefabRegistry prefabRegistry;
    [Inject] private InputService inputService;
    [Inject] private GameConfig gameConfig;
    [Inject] private ShipArchetypeFactory shipArchetypeFactory;

    private readonly EcsWorldInject world;
    private readonly EcsPoolInject<Player> playerPool;
    private readonly EcsPoolInject<Controlled> controlledPool;

    public void Init(EcsSystems ecsSystems) {
      gameConfig.players.ForEach(controlScheme => {
        inputService.AddController(controlScheme);

        var playerEntity = world.Value.NewEntity();

        ref var player = ref playerPool.Value.Add(playerEntity);
        player.controlScheme = controlScheme;

        var spawnPointIndex = Random.Range(0, gameConfig.players.Count);
        var shipEntity = shipArchetypeFactory.Create(
          world.Value,
          gameConfig.spawnPoints[spawnPointIndex],
          Quaternion.Euler(0, 0, Random.Range(0, 360f))
        );

        ref var controlled = ref controlledPool.Value.Add(shipEntity);
        controlled.controlScheme = controlScheme;
      });
    }
  }
}