using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Parameters
    [SerializeField] float bulletSpeed = 10f;

    //References

    //Variables
    Vector3 targetPos;

    void Start()
    {
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
            collision.gameObject.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
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
