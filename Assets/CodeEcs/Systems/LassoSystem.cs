using CodeEcs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeEcs.Systems {
  public class LassoSystem : IEcsRunSystem {
    private readonly EcsPoolInject<Lasso> moveForwardPool;
    private readonly EcsPoolInject<Lasso> lassoPool;
    private readonly EcsFilterInject<Inc<Lasso>> lassos;

    public void Run(EcsSystems systems) {
      foreach (var entity in lassos.Value) {
        ref var lasso = ref lassoPool.Value.Get(entity);
        lasso.lineRenderer.SetPositions(new[] { lasso.ends.start.position, lasso.ends.end.position });
      }
    }
  }
}