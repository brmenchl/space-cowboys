using System;
using System.Collections.Generic;
using Code.Input;
using UnityEngine;

namespace CodeEcs {
  [Serializable]
  public class GameConfig {
    public List<ControlScheme> players;
    public List<Vector2> spawnPoints;
  }
}
