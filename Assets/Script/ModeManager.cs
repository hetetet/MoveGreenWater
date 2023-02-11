using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ModeManager : MonoBehaviour
{
    public Button timerMode;
    public Button nmlMode;
    public TextMeshProUGUI warning;
    public TMP_InputField min;
    public TMP_InputField sec;
    public TMP_InputField ml;
    int time=61;
    int howMuch=3000;
    int minnum, secnum;

    private AudioSource theAudio;
    [SerializeField] private AudioClip bbollong;

    // Start is called before the first frame update
    void Start()
    {
        min.text = "1";
        sec.text = "1";
        ml.text = "3000";
        theAudio = GetComponent<AudioSource>();
        theAudio.clip = bbollong;
        theAudio.Play();
    }


    public void LoadGame(bool gameType)
    {    
        GameManager.gameType=gameType;
        if (gameType)
        {
            if (int.TryParse(min.text, out minnum) && int.TryParse(sec.text, out secnum))
            {
                time = minnum * 60 + secnum;
                if (time < 30)
                    warning.text = "얘!\n 최소 30초부터 \n가능하단다!";
                else if (time > 600)
                    warning.text = "얘!\n 최대 10분까지 \n가능하단다!";
                else
                {
                    GameManager.timeOrMl = time;
                    SceneManager.LoadScene("mainGame");
                }
            }
            else
            {
                warning.text = "얘! 숫자만 \n입력할 수 \n있단다!";
            }
        }
        else
        {
            if (int.TryParse(ml.text, out howMuch))
            {
                if (howMuch < 300)
                    warning.text = "얘! 최소 용량은\n300ml란다!";
                else if(howMuch > 99999)
                    warning.text = "얘! 최대 용량은\n99999ml란다!";
                else
                {
                    GameManager.timeOrMl = howMuch;
                    SceneManager.LoadScene("mainGame");
                }
            }
            else
            {
                if(ml.text.Equals("코") || ml.text.Equals("nose") || ml.text.Equals("NOSE"))
                {
                    GameManager.timeOrMl = 3000;
                    SceneManager.LoadScene("mainGame");
                }
                else
                {
                    warning.text = "얘! 숫자만 \n입력할 수 \n있단다!";
                }                    
            }
        }       
    }
}
