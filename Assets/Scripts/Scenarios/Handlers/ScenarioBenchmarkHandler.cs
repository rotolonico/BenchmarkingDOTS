using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenarios.APIs;
using Scenarios.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenarios.Handlers
{
    public class ScenarioBenchmarkHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private TextMeshProUGUI resultsText;
        [SerializeField] private Button copyResultsButton;

        private static readonly List<float> AverageFPS = new();

        private BenchmarkSettings _settings;
        private float _elapsedTime;
        private float _timeIntervalElapsedTime;
        private const float _timeInterval = 0.5f;
        private float lastFPS;
        private bool _isBenchmarking;

        private void Start()
        {
            _settings = BenchmarkSettingsAPIs.GetSettings();
            copyResultsButton.onClick.AddListener(OnCopyResultsButtonClick);

            if (!ScenarioSettingsAPIs.IsBenchmarkMode()) return;

            if (AverageFPS.Count >= _settings.benchmarkNumSnapshots)
            {
                ShowResults();
                ResetProgress();
                return;
            }

            StartCoroutine(Benchmark());
        }

        private void Update()
        {
            if (!_isBenchmarking) return;
            _elapsedTime += Time.unscaledDeltaTime;
            _timeIntervalElapsedTime += Time.unscaledDeltaTime;

            if (_timeIntervalElapsedTime >= _timeInterval || lastFPS == 0)
            {
                lastFPS = 1.0f / Time.unscaledDeltaTime;
                _timeIntervalElapsedTime = 0.0f;
            }
            
            progressText.text =
                $"Progress: {AverageFPS.Count}/{_settings.benchmarkNumSnapshots} snapshots" +
                $"\nNum entities: {ScenarioSettingsAPIs.GetNumEntities()}" +
                $"\nTime left: {Math.Max(0, _settings.benchmarkSnapshotDuration - _elapsedTime):F1}s" +
                $"\nFPS: {lastFPS:F1}";
        }

        private IEnumerator Benchmark()
        {
            _isBenchmarking = true;
            PerformanceProfiler.Instance.StartProfiling();
            yield return new WaitForSecondsRealtime(_settings.benchmarkSnapshotDuration);
            AverageFPS.Add(PerformanceProfiler.Instance.StopProfiling());

            if (AverageFPS.Count >= _settings.benchmarkNumSnapshots)
                ScenarioSettingsAPIs.SetNumEntities(0);
            else
                ScenarioSettingsAPIs.SetNumEntities(
                    ScenarioSettingsAPIs.GetNumEntities() + _settings.benchmarkIncrement);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void ResetProgress() => AverageFPS.Clear();

        private void ShowResults()
        {
            resultsPanel.SetActive(true);
            resultsText.text = JsonUtility.ToJson(GenerateBenchmarkResults(), true);
        }

        private BenchmarkResults GenerateBenchmarkResults() =>
            new()
            {
                scenarioName = SceneManager.GetActiveScene().name,
                settings = _settings,
                results = AverageFPS.Select((fps, index) => new BenchmarkResult
                {
                    averageFPS = Math.Round(fps, 1),
                    numEntities = _settings.benchmarkNumEntities + _settings.benchmarkIncrement * index
                }).ToArray()
            };

        private void OnCopyResultsButtonClick() => GUIUtility.systemCopyBuffer = resultsText.text;
    }
}