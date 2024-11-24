using Hub.Objects;
using TMPro;
using UnityEngine;

namespace Hub.Handlers
{
    public class OptimizationsSelectorHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown optimizationsDropdown;
        [SerializeField] private TextMeshProUGUI optimizationsDescription;
        
        [SerializeField] private OptimizationInfo[] optimizationsInfos;

        private void Start()
        {
            optimizationsDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

            optimizationsDropdown.ClearOptions();
            foreach (var optimizationsInfo in optimizationsInfos)
                optimizationsDropdown.options.Add(new TMP_Dropdown.OptionData(optimizationsInfo.name));

            optimizationsDropdown.value = 2;
            OnDropdownValueChanged(2);
        }

        private void OnDropdownValueChanged(int newValue)
        {
            var optimizationsInfo = GetOptimizationSelected();
            optimizationsDescription.text = optimizationsInfo.description;
        }

        public OptimizationInfo GetOptimizationSelected() => optimizationsInfos[optimizationsDropdown.value];
    }
}