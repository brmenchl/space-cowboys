namespace Code.Utilities {
  public static class AppHelper {
    public static void Exit() {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.ExitPlaymode();
#else
      UnityEngine.Application.Quit();
#endif
    }
  }
}