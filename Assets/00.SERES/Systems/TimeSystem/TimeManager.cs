using SERES.Systems.PhaseSystem;
using SERES.Utils;
using UnityEngine;

namespace SERES.Systems.TimeSystem
{
    public class TimeManager : PhaseManager
    {
        private float _multiplier = 10f;
        [SerializeField]
        private float _timer;
        [SerializeField]
        private string _date;
        [SerializeField]
        private TimeScale _timeScale;

        public enum TimeScale { x1, x2, x3, x4 }
        public override void OnGamePlay()
        {
            base .OnGamePlay();
            switch (_timeScale)
            {
                case TimeScale.x1:
                    _multiplier = 60f;
                    break;
                case TimeScale.x2:
                    _multiplier = 3600f;
                    break;
                case TimeScale.x3:
                    _multiplier = 86400f;
                    break;
                case TimeScale.x4:
                    _multiplier = 2678400f;
                    break;
            }
            _timer += Time.deltaTime * _multiplier;
            _date = DateConversion.FloatToDate(_timer);
        }
        public TimeScale GetTimeScale()
        {
            return _timeScale;
        }
    }
}