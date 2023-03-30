using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float strength = 1;

    private Rigidbody2D rb;

    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(new Vector2(0, strength), ForceMode2D.Impulse);
            rb.velocity = Vector2.up * strength;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Debug.Log("cos");
            collision.GetComponent<Coin>().PickUpCoin();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Coin"))
        {
            GameManager.Instance.OnGameOver();
        }
        
    }
}
