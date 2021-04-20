using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Input {
  public interface IControllable {
    void UpdateController(int playerId, IUniTaskAsyncEnumerable<ControllerInputState> inputStream);

    ControllableType Type { get; }
    Vector2 Position { get; }
    void ClearController();
    void Destroy();
  }

  public enum ControllableType {
    Cowboy,
    Vehicle
  }
}