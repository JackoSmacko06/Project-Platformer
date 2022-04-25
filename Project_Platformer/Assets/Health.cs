using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public static Health instance;

    private Rigidbody2D rb;

    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool canTakeDamage = true;

    public float knockbackForceUp;
    public float knockbackForceSide;

    public bool collided;

    public Vector2 checkPointPos;


    private void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
        
        if(health <= 0)
        {
            //Death();
            transform.position = checkPointPos;
            health = 5;
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;  
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;

            cameraShake.instance.Shake(5, 4, .2f);

            collided = true;
            StartCoroutine(NoLongerColliding(.1f));

            StartCoroutine(IFrames(1.5f));
        }
        

        

    }

    public void TakeDamageSpikes(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;

            cameraShake.instance.Shake(5, 4, .2f);

            transform.position = checkPointPos;

            StartCoroutine(IFrames(1.5f));
        }


        

    }

    private void FixedUpdate()
    {
        HandleKnockBack();
    }

    public void HandleKnockBack()
    {
        if (collided)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * knockbackForceUp, ForceMode2D.Impulse);
            if(PlayerController.instance.facingright == true)
            {
                rb.AddForce(Vector2.right * -knockbackForceSide, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * knockbackForceSide, ForceMode2D.Impulse);
            }
        }
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator IFrames(float time)
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(time);

        canTakeDamage = true;
    }

    IEnumerator NoLongerColliding(float time)
    {
        yield return new WaitForSeconds(time);
        collided = false;
    }

}
