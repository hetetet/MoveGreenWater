using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class introTajiri : MonoBehaviour
{
    Animator animator;
    public TextMeshProUGUI txt;
    private AudioSource theAudio;
    [SerializeField] private AudioClip rul;
    private void Update()
    {
        theAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    void OnMouseEnter()
    {
        animator.SetBool("mouseOver", true);
        txt.fontSize = 120;
        theAudio.clip = rul;
        theAudio.Play();
    }

    void OnMouseExit()
    {
        animator.SetBool("mouseOver", false);
        txt.fontSize = 100;
    }

    private void OnMouseDown()
    {
        if(!introKane.isHowtoOpen)
            SceneManager.LoadScene("selectMode");
    }
}
