using UnityEngine;

namespace FPS.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] RectTransform foreground;
        [SerializeField] Health health;

        void Update()
        {
            foreground.localScale = new Vector3(health.GetFraction(), 1, 1);
        }
    }
}