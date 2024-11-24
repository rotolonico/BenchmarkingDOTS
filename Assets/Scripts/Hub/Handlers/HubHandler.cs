using Hub.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hub.Handlers
{
    [RequireComponent(typeof(OptimizationsSelectorHandler))]
    public class HubHandler : MonoBehaviour
    {
        [SerializeField] private GameObject benchmarkPanel;
        [SerializeField] private GameObject settingsPanel;
        
        [SerializeField] private Button benchmarkButton;
        [SerializeField] private Button settingsButton;
        
        [SerializeField] private GameObject scenarioPrefab;
        [SerializeField] private GameObject scenariosContainer;

        [SerializeField] private TextMeshProUGUI scenarioDescription;
        [SerializeField] private ScenarioInfo[] scenarioInfos;

        [SerializeField] private Button quitButton;
        
        private bool _isBenchmarkMode;

        private void Start()
        {
            var optimizationsSelectorHandler = GetComponent<OptimizationsSelectorHandler>();
            foreach (var scenarioInfo in scenarioInfos)
            {
                var scenario = Instantiate(scenarioPrefab, scenariosContainer.transform);
                scenario.GetComponent<ScenarioPrefabHandler>()
                    .Initialize(this, scenarioInfo, scenarioDescription, optimizationsSelectorHandler);
            }

            quitButton.onClick.AddListener(OnQuitButtonClick);
            benchmarkButton.onClick.AddListener(EnableBenchmarkMode);
            settingsButton.onClick.AddListener(DisableBenchmarkMode);
        }

        private void OnQuitButtonClick() => Application.Quit();

        private void EnableBenchmarkMode()
        {
            _isBenchmarkMode = true;
            
            benchmarkPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }

        private void DisableBenchmarkMode()
        {
            _isBenchmarkMode = false;
            
            benchmarkPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }

        public bool IsBenchmarkMode() => _isBenchmarkMode;
    }
}