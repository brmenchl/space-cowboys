using UnityEngine;

public static class CameraExtensions
{
  public static Vector2 ScreenBounds(this Camera camera)
  {
    var screenBottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
    var screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

    return new Vector2
    {
      x = screenTopRight.x - screenBottomLeft.x,
      y = screenTopRight.y - screenBottomLeft.y
    };
  }
}
