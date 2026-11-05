using UnityEngine;
using TMPro;
using SERES.Systems.TimeSystem;
namespace SERES.UI
{
    class UIDate : MonoBehaviour
    {
        public TMP_Text dateText;
        public TimeManager TimeManager;

        private void Update()
        {
            dateText.text = TimeManager.GetDate();
        }
    }
}