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
            animator.SetBool("isAttacking", true);
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.transform.position, new Vector3(0.18f, 0.3f, 0),  enemy);
   
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                //Debug.Log(enemiesToDamage[i].tag);
                //Debug.Log(enemiesToDamage[i].GetType() == typeof(BoxCollider2D));
                
                if (enemiesToDamage[i].tag == "Enemy" && enemiesToDamage[i].GetType() == typeof(BoxCollider2D))
                {
                    enemiesToDamage[i]?.GetComponent<Enemy>()?.TakeDamage(attackDamage);
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
