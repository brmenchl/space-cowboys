using UnityEngine;
using Zenject;

namespace Code.Lasso {
  public class LassoRenderer : ITickable {
    private const float lineWidth = 0.1f;
    private readonly LassoEnds lassoEnds;
    private readonly LineRenderer lineRenderer;

    public LassoRenderer(LineRenderer lineRenderer, LassoEnds lassoEnds) {
      this.lineRenderer = lineRenderer;
      this.lassoEnds = lassoEnds;
      lineRenderer.startWidth = lineWidth;
      lineRenderer.endWidth = lineWidth;
      lineRenderer.positionCount = 2;
    }

    public void Tick() => DrawRope();

    private void DrawRope() =>
      lineRenderer.SetPositions(new[] { lassoEnds.start.position, lassoEnds.end.position });
  }
}