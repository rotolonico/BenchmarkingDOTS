using Scenarios.APIs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hub.Handlers
{
    public class BenchmarkPanelHandler : MonoBehaviour
    {
        [SerializeField] private SliderWithLabel benchmarkIncrementSlider;
        [SerializeField] private SliderWithLabel benchmarkNumSnapshotsSlider;
        [SerializeField] private SliderWithLabel benchmarkBaseNumEntitiesSlider;
        [SerializeField] private SliderWithLabel benchmarkSnapshotDurationSlider;
        [SerializeField] private TextMeshProUGUI benchmarkModeText;

        private void Awake()
        {
            var settings = BenchmarkSettingsAPIs.GetSettings();

            benchmarkIncrementSlider.SetValue(settings.benchmarkIncrement);
            benchmarkNumSnapshotsSlider.SetValue(settings.benchmarkNumSnapshots);
            benchmarkBaseNumEntitiesSlider.SetValue(settings.benchmarkNumEntities);
            benchmarkSnapshotDurationSlider.SetValue(settings.benchmarkSnapshotDuration);

            benchmarkIncrementSlider.AddOnValueChangedListener(OnBenchmarkIncrementChanged);
            benchmarkNumSnapshotsSlider.AddOnValueChangedListener(OnBenchmarkNumSnapshotsChanged);
            benchmarkBaseNumEntitiesSlider.AddOnValueChangedListener(OnBenchmarkBaseSpawnRadiusChanged);
            benchmarkSnapshotDurationSlider.AddOnValueChangedListener(OnBenchmarkSnapshotDurationChanged);
            
            UpdateBenchmarkModeText();
            BenchmarkSettingsAPIs.SaveSettings(settings);
        }

        private void OnBenchmarkIncrementChanged(float arg0)
        {
            var settings = BenchmarkSettingsAPIs.GetSettings();
            settings.benchmarkIncrement = (int)arg0;
            BenchmarkSettingsAPIs.SaveSettings(settings);
            
            UpdateBenchmarkModeText();
        }

        private void OnBenchmarkNumSnapshotsChanged(float arg0)
        {
            var settings = BenchmarkSettingsAPIs.GetSettings();
            settings.benchmarkNumSnapshots = (int)arg0;
            BenchmarkSettingsAPIs.SaveSettings(settings);
            
            UpdateBenchmarkModeText();
        }

        private void OnBenchmarkBaseSpawnRadiusChanged(float arg0)
        {
            var settings = BenchmarkSettingsAPIs.GetSettings();
            settings.benchmarkNumEntities = (int)arg0;
            BenchmarkSettingsAPIs.SaveSettings(settings);
            
            UpdateBenchmarkModeText();
        }

        private void OnBenchmarkSnapshotDurationChanged(float arg0)
        {
            var settings = BenchmarkSettingsAPIs.GetSettings();
            settings.benchmarkSnapshotDuration = (int)arg0;
            BenchmarkSettingsAPIs.SaveSettings(settings);
            
            UpdateBenchmarkModeText();
        }

        private void UpdateBenchmarkModeText()
        {
            var time = BenchmarkSettingsAPIs.CalculateBenchmarkCompleteDuration();
            benchmarkModeText.text =
                "Benchmark mode will take measurements of the chosen scenario. Frustum culling and camera movement will be disabled. The benchmark will take " +
                time + " to complete.";
        }
    }
}