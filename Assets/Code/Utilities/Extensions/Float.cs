namespace Code.Utilities.Extensions {
  public static class Float {
    public static float Clamp(this float f, float min, float max) => f > max ? max : f < min ? min : f;
  }
}