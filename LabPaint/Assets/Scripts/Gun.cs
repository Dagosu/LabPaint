using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Parameters
    [SerializeField] float firingDelay = 0.5f;
    [SerializeField] float cleaningDelay = 0.2f;

    //References
    //[SerializeField] GameObject[] paintPrefabsArray = default;
    [SerializeField] GameObject paintPrefab = default;
    [SerializeField] GameObject cleaningPaintPrefab = default;
    [SerializeField] Color32[] paints = default;

    //Variables
    Coroutine firingCoroutine;
    Coroutine cleaningCoroutine;
    int selectedPaintIndex = 0;
    bool firingActive = false;
    bool cleaningActive = false;

    void Start()
    {

    }

    void Update()
    {
        PaintSelection();
        PlayerShooting();
    }

    private void PaintSelection()
    {
        if (Input.GetButtonDown("Paint1"))
            selectedPaintIndex = 0;
        if (Input.GetButtonDown("Paint2"))
            selectedPaintIndex = 1;
        if (Input.GetButtonDown("Paint3"))
            selectedPaintIndex = 2;
    }

    private void PlayerShooting()
    {
        if (Input.GetButtonDown("Fire1") && cleaningActive == false)
        {
            firingCoroutine = StartCoroutine(FireContinously());
            firingActive = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            firingActive = false;
        }

        if (Input.GetButtonDown("Fire2") && firingActive == false)
        {
            cleaningCoroutine = StartCoroutine(CleaningContinously());
            cleaningActive = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine(cleaningCoroutine);
            cleaningActive = false;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject bullet = Instantiate(paintPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<SpriteRenderer>().color = paints[selectedPaintIndex];

            yield return new WaitForSeconds(firingDelay);
        }
    }

    IEnumerator CleaningContinously()
    {
        while (true)
        {
            GameObject bullet = Instantiate(cleaningPaintPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(cleaningDelay);
        }
    }
}
