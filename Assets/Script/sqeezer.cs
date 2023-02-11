using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sqeezer : MonoBehaviour
{
    Animator anim;
    bool meetKane = false;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (meetKane && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("pump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Kane")
        {
            meetKane = true;
            spriteRenderer.color = new Color(0, 0.7f, 0, 1);
        }

        else if (collision.name == "Tajiri")
            anim.SetTrigger("pump");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Kane") {
            meetKane = false;
            spriteRenderer.color = new Color(1,1,1,1);
        }

    }
}
