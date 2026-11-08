using UnityEngine;
using UnityEngine.Events;

namespace SERES.UI
{
    public class UIOpenCloseTrigger : MonoBehaviour
    {
        public UnityEvent openEvent;
        public UnityEvent closeEvent;
        
        private bool _isActive = false;
        private void Update()
        {
            if(_isActive)
            {
                openEvent.Invoke();
            }
            else
            {
                closeEvent.Invoke();
            }
        }
        public void Active()
        {
            _isActive = !_isActive;
        }
    }
}