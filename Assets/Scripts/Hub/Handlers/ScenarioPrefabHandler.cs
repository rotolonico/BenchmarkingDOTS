using System.Linq;
using Hub.Objects;
using Scenarios.APIs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hub.Handlers
{
    [RequireComponent(typeof(Button))]
    public class ScenarioPrefabHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI scenarioName;

        private HubHandler _hubHandler;
        private ScenarioInfo _scenarioInfo;
        private TextMeshProUGUI _scenarioDescription;
        private OptimizationsSelectorHandler _optimizationsSelectorHandler;

        public void Initialize(HubHandler hubHandler, ScenarioInfo scenarioInfo, TextMeshProUGUI scenarioDescription,
            OptimizationsSelectorHandler optimizationsSelectorHandler)
        {
            _hubHandler = hubHandler;
            _scenarioInfo = scenarioInfo;
            _scenarioDescription = scenarioDescription;
            _optimizationsSelectorHandler = optimizationsSelectorHandler;

            scenarioName.text = scenarioInfo.name;
            GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            var optimizationScene = _scenarioInfo.optimizationScenes.FirstOrDefault(x =>
                x.optimizationId == _optimizationsSelectorHandler.GetOptimizationSelected().id);

            if (optimizationScene == null) return;

            if (_hubHandler.IsBenchmarkMode())
                ScenarioSettingsAPIs.InitializeSettingsWithBenchmark();
            else
                ScenarioSettingsAPIs.InitializeSettings(optimizationScene.defaultScenarioSettings);
            
            SceneManager.LoadScene(optimizationScene.sceneIndex);
        }


        public void OnPointerEnter(PointerEventData eventData) => _scenarioDescription.text = _scenarioInfo.description;

        public void OnPointerExit(PointerEventData eventData) =>
            _scenarioDescription.text = "Hover over a scenario button to see its description.";
    }
}