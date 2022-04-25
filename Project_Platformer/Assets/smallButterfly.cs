using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallButterfly : MonoBehaviour
{
	public static smallButterfly instance;

	Vector3 mousePosition;
	public float moveSpeed = 0.1f;
	Rigidbody2D rb;
	Vector2 position = new Vector2(0f, 0f);

	public GameObject burst;

	public bool follow;

	public Animator anim;

	private void Start()
	{
		instance = this;



		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

		if (PlayerController.instance.doorUnlocked == true)
        {
            if (follow)
            {
				follow = false;

				anim.SetTrigger("up");
				Invoke("destroy", 3f);
			}
			
        }
	}

	private void FixedUpdate()
	{

        if (follow)
        {
			rb.MovePosition(position);
		}
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
			
			if(follow == false)
            {
				burst.SetActive(true);
				Invoke("Disablee", 3f);
				PlayerController.instance.keys++;

			}

			follow = true;
			
        }
    }

	void Disablee()
    {
		burst.SetActive(false);
    }

	void destroy()
    {
		Destroy(gameObject);	
    }
}
