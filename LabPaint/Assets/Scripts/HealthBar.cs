using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Variables
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
