using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public int attackDamage;
    public int kill = 0;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetFloat("XInput") > 0.8)
        {
            attackPos.transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, 0);
        }
        else if (animator.GetFloat("XInput") < -0.8)
        {
            attackPos.transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, 0);
        }
        else if (animator.GetFloat("YInput") > 0.8)
        {
            attackPos.transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, 0);
        }
        else
        {
            attackPos.transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            float knockbackforce = 2f;
            animator.SetBool("isAttacking", true);
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.transform.position, new Vector3(0.18f, 0.3f, 0),  enemy);
   
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                //Debug.Log(enemiesToDamage[i].tag);
                //Debug.Log(enemiesToDamage[i].GetType() == typeof(BoxCollider2D));
                
                if (enemiesToDamage[i].tag == "Enemy" && enemiesToDamage[i].GetType() == typeof(BoxCollider2D))
                {
                    Vector2 direction = (enemiesToDamage[i].transform.position - transform.position).normalized;
                    Vector2 knockback = direction * knockbackforce;
                    enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
                    enemiesToDamage[i].GetComponent<FlashBlink>().Flash();


                    StartCoroutine(Reset(enemiesToDamage[i]?.GetComponent<Rigidbody2D>()));

                    int kill = (int)(enemiesToDamage[i]?.GetComponent<Enemy>()?.TakeDamage(attackDamage));
                    this.kill += kill;
                }
            }


        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetBool("isAttacking", false);
        }

        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {

            timeBtwAttack -= Time.deltaTime;
        }
    }

    private IEnumerator Reset(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(0.15f);
        try
        {
            enemy.velocity = Vector2.zero;
        }catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.transform.position, new Vector3(0.18f, 0.3f, 0));

        //Gizmos.DrawWireCube(attackPos.transform.position, new Vector3(0.18f,0.2f,0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.transform.position, new Vector3(0.18f, 0.3f, 0));
    }
}
