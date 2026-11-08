using UnityEngine;

namespace SERES.UI
{
    public class UIChangePosition: MonoBehaviour
    {
        public void SetYPosition(float newPosition)
        {
            RectTransform trans = transform.GetComponent<RectTransform>();
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(trans.anchoredPosition.x, newPosition);
        }
        public void SetXPosition(float newPosition)
        {
            RectTransform trans = transform.GetComponent<RectTransform>();
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(newPosition, trans.anchoredPosition.y);
        }
    }
}