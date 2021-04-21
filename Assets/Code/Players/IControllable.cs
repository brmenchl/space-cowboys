using Code.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Players {
  public interface IControllable {
    ControllableType Type { get; }
    Vector2 Position { get; }
    Sprite Sprite { get; }
    void UpdateController(Color playerTheme, IUniTaskAsyncEnumerable<ControllerInputState> inputStream);
    void ClearController();
    void Destroy();
    void Damage(float value);
  }

  public enum ControllableType {
    Cowboy,
    Vehicle
  }
}