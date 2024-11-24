using Scenarios.APIs;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenarios.Handlers
{
    public class ScenarioUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject escapeFocusHintText;
        [SerializeField] private GameObject escapeHintText;
        [SerializeField] private GameObject mainPanel;

        [SerializeField] private GameObject benchmarkPanel;
        [SerializeField] private GameObject settingsPanel;

        [SerializeField] private Button leaveButton;

        private void Start()
        {
            var benchmarkMode = ScenarioSettingsAPIs.IsBenchmarkMode();
            benchmarkPanel.SetActive(benchmarkMode);
            settingsPanel.SetActive(!benchmarkMode);

            leaveButton.onClick.AddListener(OnBackButtonClick);
            
            escapeFocusHintText.SetActive(!benchmarkMode);
        }


        private void Update()
        {
            if (ScenarioSettingsAPIs.IsBenchmarkMode()) return;
            var isCursorLocked = Cursor.lockState == CursorLockMode.Locked;
            escapeHintText.SetActive(isCursorLocked);
            escapeFocusHintText.SetActive(!isCursorLocked);
            mainPanel.SetActive(!isCursorLocked);
        }

        private void OnBackButtonClick()
        {
            ScenarioBenchmarkHandler.ResetProgress();
            SceneManager.LoadScene("Hub");
        }
    }
}