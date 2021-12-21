using UnityEngine;

namespace Code.Utilities.Extensions {
  public static class Camera {
    public static Vector2 ScreenBounds(this UnityEngine.Camera camera) {
      var screenBottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
      var screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

      return new Vector2 { x = screenTopRight.x - screenBottomLeft.x, y = screenTopRight.y - screenBottomLeft.y };
    }
  }
}