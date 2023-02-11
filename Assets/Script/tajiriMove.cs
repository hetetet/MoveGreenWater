using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tajiriMove : MonoBehaviour
{
    public float speed=3;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public GameManager gameManager;
    Transform target;
    public GameObject sqeezer;
    public GameObject bucket;
    public GameObject Kane;
    public TextMeshProUGUI score;
    public spillGenerator spillGenerator;

    private AudioSource theAudio;
    [SerializeField] private AudioClip bbollong;
    [SerializeField] private AudioClip splash;

    bool isAttacked = false;
    float kaneDist;
    float sqeezerDist;
    public int maxml = 200;
    void Start()
    {        
        target = sqeezer.transform;
        theAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int tajiriRand=Random.Range(0,maxml);
        Think();

        Vector3 dir = target.position - transform.position;
        if (!isAttacked && gameManager.isStart)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime);
            if (dir.x < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }

    }

    void Think()
    {
        kaneDist = Vector2.Distance(Kane.transform.position, gameObject.transform.position);
        sqeezerDist = Vector2.Distance(sqeezer.transform.position, gameObject.transform.position);
        if(kaneDist<3f && kaneDist<sqeezerDist && gameManager.kaneCarry!=0 &&gameManager.tajiriCarry==0)
        {
            target = Kane.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttacked && collision.name == "squeezer")
        {
            gameManager.tajiriCarry = maxml;
            target = bucket.transform;
            anim.SetBool("isFull", true);
            theAudio.clip = bbollong;
            theAudio.Play();
        }

        else if (!isAttacked && collision.name == "bucket_tajiri")
        {
            target = sqeezer.transform;
            gameManager.tajiribuck += gameManager.tajiriCarry;
            score.text = gameManager.tajiribuck.ToString()+"ml";
            gameManager.tajiriCarry = 0;
            anim.SetBool("isFull", false);
            theAudio.clip = splash;
            theAudio.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name ==  "squeezer" )
        {
            gameManager.tajiriCarry = maxml;
            target = bucket.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Kane")
            OnBumped(collision.transform.position);
    }

    void OnBumped(Vector2 targetPos)
    {
        isAttacked= true;
        rigid.AddForce(new Vector2(transform.position.x - targetPos.x, transform.position.y - targetPos.y) * 10, ForceMode2D.Impulse);
        //Debug.Log("targetPos.x- transform.position.x: " + (targetPos.x - transform.position.x) + "targetPos.y- transform.position.y: " + (targetPos.y - transform.position.y));
        if (gameManager.tajiriCarry > 0)
        {
            StartCoroutine(spillGenerator.Spill(rigid.transform));
            gameManager.tajiriCarry = 0;
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
        target = sqeezer.transform;
    }
}
