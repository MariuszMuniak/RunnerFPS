using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image timerImage;
    [Tooltip("Level time in seconds.")]
    [SerializeField] float levelTime = 60f;

    Image backgroundImage;
    
    float currentTime = 0f;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        timerImage.fillAmount = Mathf.Max(LevelTimeFraction(), 0f);
    }

    private float LevelTimeFraction()
    {
        return (levelTime - currentTime) / levelTime;
    }
}
