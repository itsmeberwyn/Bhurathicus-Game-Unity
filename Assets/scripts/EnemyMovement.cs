using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int damage = 2;
    private float moveSpeed = 0.5f;
    private Transform target;

    private BoxCollider2D boxCollider;
    //private CircleCollider2D circleCollider;
    private RaycastHit2D hit;
    private Animator animator;

    Rigidbody2D rb;
    Vector2 moveDirection;

    // for attacking
    private bool attacking = false;
    public GameObject attack;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        //circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.y);
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!attacking) {
            attack.SetActive(false);

            animator.SetFloat("XInput", moveDirection.x);
            animator.SetFloat("YInput", moveDirection.y);

            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
                new Vector2(0, moveDirection.y), Mathf.Abs(moveDirection.y * Time.deltaTime),
                LayerMask.GetMask("Player", "Tree", "Wall", "Wall2", "Wall3"));

            if (hit.collider == null && (moveDirection.x != 0 || moveDirection.y != 0)) 
            {
                animator.SetBool("Spawn", true);
                animator.SetBool("Attack", false);

                transform.Translate(0, moveDirection.y * moveSpeed * Time.deltaTime, 0);
            }

            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
                new Vector2(moveDirection.x, 0), Mathf.Abs(moveDirection.x * Time.deltaTime),
                LayerMask.GetMask("Player", "Tree", "Wall", "Wall2", "Wall3"));

            if (hit.collider == null && (moveDirection.x != 0 || moveDirection.y != 0))
            {
                animator.SetBool("Spawn", true);
                animator.SetBool("Attack", false);

                transform.Translate(moveDirection.x * moveSpeed * Time.deltaTime, 0, 0);
            }

            if (animator.GetBool("Spawn"))
            {
                animator.SetBool("isWalking", true);
            }

        } else
        {
            if (timeBtwAttack <= 0)
            {
                attack.SetActive(true);
                if (animator.GetFloat("XInput") > 0.8)
                {
                    attack.transform.position = new Vector3(transform.position.x + 0.21f, transform.position.y, 0);
                }
                else if (animator.GetFloat("XInput") < -0.8)
                {
                    attack.transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, 0);
                }
                else if (animator.GetFloat("YInput") > 0.8)
                {
                    attack.transform.position = new Vector3(transform.position.x, transform.position.y + 0.18f, 0);
                }
                else
                {
                    attack.transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, 0);
                }

                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }


        

        //if (target)
        //{
        //    rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        //}
        //Debug.Log(hit.point);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            target = go.transform;
            Debug.Log("Hit 1");
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
    }

    public int triggerAttack()
    {
        InvokeRepeating("attackAnimation", 2, Random.Range(3,5));
        attacking = !attacking;
        return damage;
    }

    private void attackAnimation()
    {
        Debug.Log("Attack!");
        animator.SetBool("isWalking", false);
        animator.SetBool("Spawn", false);
        animator.SetBool("Attack", true);

    }


}
