using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bucket : MonoBehaviour
{
    Animator anim;
    public GameManager gameManager;
    SpriteRenderer spriteRenderer;
    private AudioSource theAudio;

    [SerializeField] private AudioClip splash;
    bool meetKane = false;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (meetKane && gameManager.kaneCarry != 0 && Input.GetButtonDown("Jump"))//
        {
            anim.SetTrigger("put");
            theAudio.clip = splash;
            theAudio.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Kane")
        {
            spriteRenderer.color = new Color(0, 0.7f, 0, 1);
            meetKane = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Kane")
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            meetKane = false;
        }
    }
}
