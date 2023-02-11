using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class naga : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Transform trans;
    public TextMeshProUGUI nagaBtn;
    private AudioSource theAudio;
    [SerializeField] private AudioClip nagasound;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();  
        trans=gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        transform.localScale = new Vector2(0.7f, 0.7f);
        nagaBtn.fontSize = 120;
        theAudio.clip = nagasound;
        theAudio.Play();
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector2(0.6f, 0.6f);
        nagaBtn.fontSize = 100;
    }

    private void OnMouseUp()
    {
        if (!introKane.isHowtoOpen)
            Application.Quit();
    }
}
