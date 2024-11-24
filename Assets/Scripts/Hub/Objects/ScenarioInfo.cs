using System;
using UnityEngine.SceneManagement;

namespace Hub.Objects
{
    [Serializable]
    public class ScenarioInfo
    {
        public string name;
        public string description;
        public OptimizationScene[] optimizationScenes;
    }
}
