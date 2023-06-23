using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    private float dirX;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); // Player'ın haritadaki konumu, koordinatı. -1 sol, 1 sağ.
        if(Input.GetKeyDown("space"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    private void FixedUpdate() {
        if(dirX < 0)
        {
            player.rotation = Quaternion.Euler(0f, 180f, 0f); 
        }
        else if(dirX > 0)
        {
            player.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y); // Player'ın hızı.
    }
}
