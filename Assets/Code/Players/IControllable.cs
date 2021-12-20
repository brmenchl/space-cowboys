using Code.Input;
using Cysharp.Threading.Tasks;
using External.Option;
using UnityEngine;

namespace Code.Players {
  public interface IControllable {
    ControllableType Type { get; }
    Vector2 Position { get; }
    Sprite Sprite { get; }
    Option<ReadOnlyAsyncReactiveProperty<float>> health { get; }
    void UpdateController(Color playerTheme, IUniTaskAsyncEnumerable<ControllerInputState> inputStream);
    void ClearController();
    void Destroy();
  }

  public enum ControllableType {
    Cowboy,
    Vehicle
  }
}