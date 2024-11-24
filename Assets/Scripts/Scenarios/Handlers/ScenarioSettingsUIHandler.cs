using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenarios.Handlers
{
    public class ScenarioSettingsUIHandler : MonoBehaviour
    {
        [SerializeField] private SliderWithLabel numEntitiesSlider;
        [SerializeField] private SliderWithLabel spawnRadiusSlider;
        
        [SerializeField] private GameObject resetHintText;
        [SerializeField] private Button resetScenarioButton;
        
        private void Awake()
        {
            numEntitiesSlider.SetValue(APIs.ScenarioSettingsAPIs.GetNumEntities());
            spawnRadiusSlider.SetValue(APIs.ScenarioSettingsAPIs.GetSpawnRadius());
            
            numEntitiesSlider.AddOnValueChangedListener(OnNumEntitiesChanged);
            spawnRadiusSlider.AddOnValueChangedListener(OnSpawnRadiusChanged);
            resetScenarioButton.onClick.AddListener(OnResetScenarioButtonClick);
            
            resetHintText.SetActive(false);
        }

        private void OnNumEntitiesChanged(float value)
        {
            APIs.ScenarioSettingsAPIs.SetNumEntities((int)value);
            resetHintText.SetActive(true);
        }

        private void OnSpawnRadiusChanged(float value)
        {
            APIs.ScenarioSettingsAPIs.SetSpawnRadius((int)value);
            resetHintText.SetActive(true);
        }

        private void OnResetScenarioButtonClick() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}