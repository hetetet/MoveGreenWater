using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class introKane : MonoBehaviour
{
    Animator animator;
    public TextMeshProUGUI txt;
    public Image howTo;
    public GameObject Tajiri;
    public GameObject sungKun;
    public static bool isHowtoOpen=false;

    private AudioSource theAudio;
    [SerializeField] private AudioClip ja;
    [SerializeField] private AudioClip bbollong;
    private void Start()
    {
        theAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)&& howTo.gameObject.activeSelf){
            howTo.gameObject.SetActive(false);
            isHowtoOpen = false;
        }
    }
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        animator.SetBool("mouseOver",true);
        txt.fontSize = 120;
        theAudio.clip = ja;
        theAudio.Play();
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        animator.SetBool("mouseOver", false);
        txt.fontSize = 100;

    }

    private void OnMouseUp()
    {
        if (!isHowtoOpen)
        {
            howTo.gameObject.SetActive(true);
            isHowtoOpen = true;
            theAudio.clip = bbollong;
            theAudio.Play();
        }
    }
}
