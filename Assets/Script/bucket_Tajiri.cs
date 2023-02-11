using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bucket_Tajiri : MonoBehaviour
{
    Animator anim;
    public GameManager gameManager;
    void Start()
    {
        anim= GetComponent<Animator>();
        Debug.Log("tajiribuck script here");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("tajiri bucket collide, tajiricarry: "+ gameManager.tajiriCarry.ToString());
        if (collision.name == "Tajiri" && gameManager.tajiriCarry != 0)
        {
            anim.SetTrigger("put");
            Debug.Log("anim.SetTrigger put");
        }
    }
}
