using Code.Lasso;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeEcs.Components {
  public struct HasLasso { }

  public struct Lassoing {
    public EcsPackedEntity lasso;
  }

  public struct Lasso {
    public Code.Lasso.Lasso.LassoState state;
    public LassoEnds ends;
    public LineRenderer lineRenderer;
  }
}