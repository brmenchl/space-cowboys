using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Input {
  public interface IControllable {
    ControllableType Type { get; }
    Vector2 Position { get; }
    Sprite Sprite { get; }
    void UpdateController(IUniTaskAsyncEnumerable<ControllerInputState> inputStream);
    void ClearController();
    void Destroy();
  }

  public enum ControllableType {
    Cowboy,
    Vehicle
  }
}