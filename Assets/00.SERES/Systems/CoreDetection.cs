using UnityEngine;

namespace SERES.Systems
{
    public class DetectCore: MonoBehaviour
    {
        private static DetectCore detectCore;

        private void Awake()
        {
            if(!detectCore || detectCore == null) detectCore = this;
            else
            {
                if(detectCore != this)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}