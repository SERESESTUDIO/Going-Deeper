using UnityEngine;
namespace SERES.Systems.PhaseSystem
{
    public class PhaseController : MonoBehaviour
    {
        public PhaseManager phaseManager;

        private PhaseManager.PhaseMode _savedPhaseMode;

        [SerializeField]
        private PhaseManager.PhaseMode _phaseMode;

        private void Update()
        {
            if (_savedPhaseMode != _phaseMode)
            {
                ChangeMode(_phaseMode);
                _savedPhaseMode = _phaseMode;
            }
        }

        public void ChangePhaseMode(PhaseManager.PhaseMode phaseMode)
        {
            _phaseMode = phaseMode;
        }

        public void ChangePhaseModeByString(string phaseMode)
        {
            if (System.Enum.TryParse(phaseMode, out PhaseManager.PhaseMode parsedPhaseMode))
            {
                _phaseMode = parsedPhaseMode;
            }
            else
            {
                Debug.LogWarning($"Invalid phase mode: {phaseMode}");
            }
        }

        private void ChangeMode(PhaseManager.PhaseMode phaseMode)
        {
            phaseManager.ChangePhaseMode(phaseMode);
        }
    }
}