using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 moveDelta;
    private RaycastHit2D hit;
    private Animator animator;

    public HealthBar healthBar;

    private float maxHealth = 100;
    public float currentHealth;

    public bool isDead = false;
    private int damageReceive = 0;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            Debug.Log(GetComponent<Timer>().timerText.text);
            PlayerPrefs.SetString("record", GetComponent<Timer>().timerText.text);
            Destroy(gameObject);
            SceneManager.LoadScene("EndScene");
        }

        Debug.Log(currentHealth);
        if (currentHealth < maxHealth)
        {
            currentHealth += 0.01f;
            healthBar.setHealth(currentHealth);
        }

        // get keybinds
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // reset moveDelta
        moveDelta = new Vector2(x, y);

        if (!moveDelta.Equals(Vector2.zero))
        {
            animator.SetFloat("XInput", x);
            animator.SetFloat("YInput", y);
        }


        // set the main player layer to Actor, and change the settings in 
        // edit > project settings > physics 2D > and uncheck queries start in colliders

        // phyics collider for Y axis using layer
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
            new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Player", "Tree", "Wall", "Wall2", "Wall3"));

        int kill = (int)(GetComponent<PlayerAttack>()?.kill);
        GameObject wall1 = GameObject.FindGameObjectWithTag("Wall");
        GameObject wall2 = GameObject.FindGameObjectWithTag("Wall2");

        if (kill == 13 && wall1 != null)
        {
            wall1.SetActive(false);
        }else if (kill == 26 && wall2 != null)
        {
            wall2.SetActive(false);
        }else if (kill == 33)
        {
            SceneManager.LoadScene("Win");
        }



        if (hit.collider == null)
        {
            // make player move
            animator.SetBool("isWalking", true);
            transform.Translate(0, moveDelta.y * 0.5f * Time.deltaTime, 0);
        }
    

        // phyics collider for X axis using layer
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,
            new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Player", "Tree", "Wall", "Wall2", "Wall3"));

        if (hit.collider == null)
        {
            // make player move
            animator.SetBool("isWalking", true);
            transform.Translate(moveDelta.x * 0.5f * Time.deltaTime, 0, 0);
        }
           
        if (x == 0 && y == 0)
        {
            animator.SetBool("isWalking", false);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && other.GetType() == typeof(BoxCollider2D))
        {

            damageReceive = other.GetComponent<EnemyMovement>().triggerAttack();
            
            InvokeRepeating("PlayerDamage", 0, 0.5f);

            //FindObjectOfType<EnemyMovement>().triggerAttack();
            //StageUtility.GetCurrentStageHandle().FindComponentOfType<EnemyMovement>().triggerAttack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && other.GetType() == typeof(BoxCollider2D))
        {
            other.GetComponent<EnemyMovement>().triggerAttack();
            CancelInvoke("PlayerDamage");

            //FindObjectOfType<EnemyMovement>().triggerAttack();
            //StageUtility.GetCurrentStageHandle().FindComponentOfType<EnemyMovement>().triggerAttack();

        }
    }

    private void PlayerDamage()
    {
        TakeDamage(damageReceive);
    }


    private void TakeDamage(int damage)
    {
        Debug.Log(currentHealth + " " + damage);
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
