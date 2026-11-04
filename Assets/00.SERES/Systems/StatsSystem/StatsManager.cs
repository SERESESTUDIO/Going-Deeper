using SERES.Systems.PhaseSystem;
using SERES.Systems.TimeSystem;
using SERES.Definitions;
using UnityEngine;
namespace SERES.Systems.StatsSystem
{
    public class StatsManager : PhaseManager
    {
        public TimeManager timeManager;
        public StatsDefinitionObject startStats;
        [SerializeField]
        private StatsDefinition _statsDefinitions;

        //dinamic stats
        private int _thirstIndex = 0;

        private void Awake()
        {
            StatsDefinitionObject statsDefinitionObject = Instantiate(startStats);
            _statsDefinitions = statsDefinitionObject.statsDefinition;
        }

        public override void OnGamePlay()
        {
            base.OnGamePlay();
            float timeMultiplier = 1f;
            switch (timeManager.GetTimeScale())
            {
                case TimeManager.TimeScale.x1:
                    timeMultiplier = 1f;
                    break;
                case TimeManager.TimeScale.x2:
                    timeMultiplier = 1.5f;
                    break;
                case TimeManager.TimeScale.x3:
                    timeMultiplier = 2f;
                    break;
                case TimeManager.TimeScale.x4:
                    timeMultiplier = 10f;
                    break;
            }
            if (_statsDefinitions.healthContainer.thirstDelay.Length > 0)
            {
                _statsDefinitions.healthContainer.thirst += Time.deltaTime * _statsDefinitions.healthContainer.thirstDelay[_thirstIndex].delay * timeMultiplier * 0.1f;
            }
        }
    }
}