using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public Animator bucketAnim;
    public GameManager gameManager;
    public TextMeshProUGUI score;
    public SpriteRenderer bucketSprite;
    public spillGenerator spillGenerator;

    private AudioSource theAudio;
    [SerializeField] private AudioSource splashSource;
    [SerializeField] private AudioClip igonan;
    [SerializeField] private AudioClip bbollong;


    bool canfill = false;
    bool canpour = false;
    bool isAttacked= false;
    public int maxml = 200;
    
    Vector2 vector;
    // Start is called before the first frame update
    void Awake()
    {
        theAudio = GetComponent<AudioSource>();
        rigid= GetComponent<Rigidbody2D>();
        spriteRenderer =GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        if (Time.timeScale!=0 && !isAttacked && gameManager.isStart)
        {
            vector.x = Input.GetAxisRaw("Horizontal");
            vector.y = Input.GetAxisRaw("Vertical");
            rigid.velocity = vector.normalized * speed;
            if (rigid.velocity.x<0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }

        if (vector.x != 0 || vector.y != 0)
            anim.SetBool("isWalk", true);
        else
            anim.SetBool("isWalk", false);

        //fill and pour
        if (canfill && Input.GetButtonDown("Jump"))
        {
            gameManager.kaneCarry = maxml;
            anim.SetBool("isFull", true);
            theAudio.clip = bbollong;
            theAudio.volume = 0.7f;
            theAudio.Play();
        }

        if (canpour && Input.GetButtonDown("Jump"))
        {           
            gameManager.kanebuck += gameManager.kaneCarry;
            score.text = gameManager.kanebuck.ToString() + "ml";
            if (gameManager.kaneCarry != 0)
                bucketAnim.SetTrigger("put");
            gameManager.kaneCarry = 0;
            anim.SetBool("isFull", false);
            splashSource.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "squeezer")
            canfill = true;
        else if(collision.name == "bucket_kane")
        {
            canpour = true;
            bucketSprite.color= new Color(0, 0.7f, 0, 1);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "squeezer")
            canfill = false;
        else if (collision.name == "bucket_kane")
        {
            canpour = false;
            bucketSprite.color = new Color(1, 1, 1, 1);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tajiri")
        {
            OnBumped(collision.transform.position);
            theAudio.clip = igonan;
            theAudio.volume = 0.2f;
            theAudio.Play();
        }
    }

    void OnBumped(Vector2 targetPos)
    {
        isAttacked = true;
        rigid.AddForce(new Vector2(transform.position.x - targetPos.x, transform.position.y - targetPos.y) * 10, ForceMode2D.Impulse);
        //Debug.Log("targetPos.x- transform.position.x: " + (targetPos.x - transform.position.x) + "targetPos.y- transform.position.y: " + (targetPos.y - transform.position.y));
        if (gameManager.kaneCarry > 0)
        {
            gameManager.kaneCarry = 0;
            StartCoroutine(spillGenerator.Spill(rigid.transform));

        }
        anim.SetBool("isFull", false);
        anim.SetTrigger("bump");
        Invoke("OffBumped", 1.7f);
    }

    void OffBumped()
    {
        anim.SetTrigger("getup");
        Invoke("canWalkAgain", 0.3f);
    }

    void canWalkAgain()
    {
        isAttacked = false;
    }
}
