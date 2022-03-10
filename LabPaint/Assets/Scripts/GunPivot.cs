using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{
    //References

    //Variables
    float angle;

    private void FixedUpdate()
    {

        CalculateAngle();
        RotateAndFlip();     
    }

    private void CalculateAngle()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePos - transform.position;
        difference.Normalize();

        angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    }

    private void RotateAndFlip()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (angle < -90 || angle > 90)
            transform.rotation = Quaternion.Euler(180f, 0f, -angle);
    }
}
