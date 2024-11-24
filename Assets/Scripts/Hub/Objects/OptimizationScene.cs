using System;
using Scenarios.Objects;
using UnityEditor;

namespace Hub.Objects
{
    [Serializable]
    public class OptimizationScene
    {
        public string optimizationId;
        public int sceneIndex;
        public ScenarioSettings defaultScenarioSettings;
    }
}