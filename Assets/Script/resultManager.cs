using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultManager : MonoBehaviour
{
    private AudioSource theAudio;
    [SerializeField] private AudioClip endsound;
    public void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theAudio.clip = endsound;
        theAudio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void gotoIntro()
    {
        SceneManager.LoadScene("intro");
    }
}
