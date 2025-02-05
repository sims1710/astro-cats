using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furball : MonoBehaviour
{
    public float speed = 10f;  
    private Rigidbody2D rb;   
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject cat = GameObject.FindWithTag("Cat");
        Collider2D catCollider = cat.GetComponent<Collider2D>();
        Collider2D furballCollider = GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(catCollider, furballCollider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Reflective"))
        {
            Vector2 reflection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            rb.velocity = reflection * speed; 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("FishBone"))
        {
            Destroy(collision.gameObject);
        }
    }

    
}
