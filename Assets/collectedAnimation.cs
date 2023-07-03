using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectedAnimation : MonoBehaviour
{

    private Animator anim;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetObjectTransparency(0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {   
            anim.SetTrigger("collected");
            SetObjectTransparency(1f);
            Invoke("DestroyObject", 0.5f);
        }
        
    }

    private void SetObjectTransparency(float alpha)
    {
        Color currentColor = spriteRenderer.color;
        currentColor.a = alpha;
        spriteRenderer.color = currentColor;
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
