using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 100;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StatsSystem playerStats = GameObject.FindWithTag("Player").GetComponent<StatsSystem>();
        slider.value = playerStats.getHealthPercent() * 100;
    }
}
