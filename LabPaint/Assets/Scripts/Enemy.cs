using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Parameters
    [SerializeField] float enemyMoveSpeed = 3f;
    [SerializeField] float enemyHealth = 2f;
    [SerializeField] float attackDamage = 3f;
    [SerializeField] float attackCooldown = 1f;

    [SerializeField] float empowerScale = 3f;
    [SerializeField] float enemyEmpoweredMoveSpeed = 2f;
    [SerializeField] float enemyEmpoweredHealth = 5f;
    [SerializeField] Color32 empoweredColor = default;

    [SerializeField] Color32 tileColor = default;

    //References

    //Variables
    Vector3 playerPos;
    Animator animator;
    Coroutine attackCoroutine;
    bool isAttacking = false;
    float attackTime = 0;
    bool isEmpowered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyMovement();

        if (!isAttacking)
            attackTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Tile"))
        {
            if (checkColorMatch(collision))
                EnemyDamaged(collision);
            else
                if (checkEmpoweredNeeded(collision))
                EmpowerEnemy();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
           if (!isAttacking && attackTime >= attackCooldown)
                attackCoroutine = StartCoroutine(Attack());
        }
    }

    private void EnemyMovement()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        transform.position = Vector3.MoveTowards(transform.position, playerPos, enemyMoveSpeed * Time.deltaTime);

        //Handle Enemy Direction
        if (transform.position.x < playerPos.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        if (transform.position.x > playerPos.x)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private bool checkColorMatch(Collider2D tileCollider)
    {
        Color enemy = GetComponent<SpriteRenderer>().color;
        Color tile = tileCollider.gameObject.GetComponent<SpriteRenderer>().color;

        Color32 enemy32 = new Color32((byte)(enemy.r * 255), (byte)(enemy.g * 255), (byte)(enemy.b * 255), (byte)(enemy.a * 255));
        Color32 tile32 = new Color32((byte) (tile.r*255), (byte)(tile.g * 255), (byte)(tile.b * 255), (byte)(tile.a * 255));

        return enemy32.Equals(tile32);
    }

    private bool checkEmpoweredNeeded(Collider2D tileCollider)
    {
        Color tile = tileCollider.gameObject.GetComponent<SpriteRenderer>().color;
        Color32 tile32 = new Color32((byte)(tile.r * 255), (byte)(tile.g * 255), (byte)(tile.b * 255), (byte)(tile.a * 255));

        return !tile32.Equals(tileColor);
    }

    private void EnemyDamaged(Collider2D tileCollider)
    {
        enemyHealth -= 1;

        GameObject tile = tileCollider.gameObject;
        tile.GetComponent<SpriteRenderer>().color = tileColor;

        if (enemyHealth <= 0)
            EnemyDeath();
    }

    private void EmpowerEnemy()
    {
        transform.localScale = new Vector3(empowerScale, empowerScale, 1);
        GetComponent<SpriteRenderer>().color = empoweredColor;

        enemyHealth = enemyEmpoweredHealth;
        enemyMoveSpeed = enemyEmpoweredMoveSpeed;

        isEmpowered = true;
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        animator.SetTrigger("EnemyAttack");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        DamagePlayer();

        isAttacking = false;
        attackTime = 0;
    }

    private void DamagePlayer()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.DamagePlayer(attackDamage);
    }

    private void EnemyDeath()
    {
        AddScore();

        Destroy(gameObject);
    }

    private void AddScore()
    {
        Score scoreObject = GameObject.Find("Score Text").GetComponent<Score>();

        if (!isEmpowered)
            scoreObject.AddScore(5);
        else
            scoreObject.AddScore(10);
    }
}
