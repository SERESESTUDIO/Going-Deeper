using UnityEngine;
namespace SERES.Systems.PhaseSystem
{
    public class PhaseManager : MonoBehaviour
    {
        private static PhaseMode _phaseMode;
        public enum PhaseMode { GamePlay, Pause }
        private void Update()
        {
            switch (_phaseMode)
            {
                case PhaseMode.GamePlay:
                    OnGamePlay();
                    break;
                case PhaseMode.Pause:
                    OnPause();
                    break;
            }
        }

        private void FixedUpdate()
        {
            switch (_phaseMode)
            {
                case PhaseMode.GamePlay:
                    OnFixedGamePlay();
                    break;
            }
        }

        public void ChangePhaseMode(PhaseMode phaseMode)
        {
            _phaseMode = phaseMode;
        }

        public virtual void OnGamePlay() {}
        public virtual void OnFixedGamePlay() {}
        public virtual void OnPause() {}
    }
}