using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningBullet : MonoBehaviour
{
    //Parameters
    [SerializeField] float bulletSpeed = 12f;
    [SerializeField] float cleaningBulletScale = 3f;

    [SerializeField] Color32 tileColor = default;

    //References

    //Variables
    Vector3 targetPos;

    void Start()
    {
        gameObject.transform.localScale = new Vector3(cleaningBulletScale, cleaningBulletScale, 1);

        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
    }

    void Update()
    {
        MoveBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Tile"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = tileColor;
            Destroy(gameObject);
        }
    }

    private void MoveBullet()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, bulletSpeed * Time.deltaTime);

        if (transform.position == targetPos)
            gameObject.layer = LayerMask.NameToLayer("Tiles");
    }
}
