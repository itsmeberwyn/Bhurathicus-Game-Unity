using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
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
            
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.transform.position, new Vector3(0.18f, 0.4f, 0), enemy);

            for (int i = 0; i < enemiesToDamage.Length-2; i++)
            {
                Debug.Log(enemiesToDamage[i].name);
                if(enemiesToDamage[i].name == "Enemy")
                {
                    enemiesToDamage[i]?.GetComponent<Enemy>()?.TakeDamage(attackDamage);
                }
            }
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
}
