using UnityEngine;
using TMPro;
using SERES.Systems.TimeSystem;
namespace SERES.UI
{
    class UIDate : MonoBehaviour
    {
        public TMP_Text dateText;
        public ExternalDataCore dataCore;

        private void Update()
        {
            if(dataCore.GetTimeManager()) dateText.text = dataCore.GetTimeManager().GetDate();
        }
    }
}