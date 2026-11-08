using UnityEngine;
using SERES.Systems.TimeSystem;
using SERES.Systems.StatsSystem;
namespace SERES.UI
{
    public class ExternalDataCore : MonoBehaviour
    {
        private TimeManager _timeManager;
        private StatsManager _statsManager;
        private void Update()
        {
            if (!_timeManager && FindAnyObjectByType<TimeManager>()) _timeManager = FindAnyObjectByType<TimeManager>();
            if (!_statsManager && FindAnyObjectByType<StatsManager>()) _statsManager = FindAnyObjectByType<StatsManager>();
        }

        public StatsManager GetStatsManager()
        {
            if (_statsManager) return _statsManager;
            return null;
        }

        public TimeManager GetTimeManager()
        {
            if (_timeManager) return _timeManager;
            return null;
        }
    }
}