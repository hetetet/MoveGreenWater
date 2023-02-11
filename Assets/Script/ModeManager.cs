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
                    warning.text = "��!\n �ּ� 30�ʺ��� \n�����ϴܴ�!";
                else if (time > 600)
                    warning.text = "��!\n �ִ� 10�б��� \n�����ϴܴ�!";
                else
                {
                    GameManager.timeOrMl = time;
                    SceneManager.LoadScene("mainGame");
                }
            }
            else
            {
                warning.text = "��! ���ڸ� \n�Է��� �� \n�ִܴ�!";
            }
        }
        else
        {
            if (int.TryParse(ml.text, out howMuch))
            {
                if (howMuch < 300)
                    warning.text = "��! �ּ� �뷮��\n300ml����!";
                else if(howMuch > 99999)
                    warning.text = "��! �ִ� �뷮��\n99999ml����!";
                else
                {
                    GameManager.timeOrMl = howMuch;
                    SceneManager.LoadScene("mainGame");
                }
            }
            else
            {
                if(ml.text.Equals("��") || ml.text.Equals("nose") || ml.text.Equals("NOSE"))
                {
                    GameManager.timeOrMl = 3000;
                    SceneManager.LoadScene("mainGame");
                }
                else
                {
                    warning.text = "��! ���ڸ� \n�Է��� �� \n�ִܴ�!";
                }                    
            }
        }       
    }
}
