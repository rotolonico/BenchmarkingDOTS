using System;
using UnityEngine.Serialization;

namespace Scenarios.Objects
{
    [Serializable]
    public class ScenarioSettings
    {
        public bool benchmarkMode;
        public int numEntities = 30000;
        public int spawnRadius = 300;
    }
}