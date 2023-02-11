using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static bool gameType=true; //true: �ð� ���, false: N�и����� ���
    public static float timeOrMl=61;
    public TextMeshProUGUI billboard;
    public int countDown;
    public TextMeshProUGUI countDownDisplay;
    public TextMeshProUGUI tip;

    public int kaneCarry=0;
    public int tajiriCarry=0;
    public int kanebuck = 0;
    public int tajiribuck = 0;
    public bool isStart=false;
    public bool isTimeTick = false;
    public bool isPaused = false;
    private int tipindex;
    private int prev_tipindex;

    private AudioSource theAudio;
    [SerializeField] private AudioClip bbam;
    [SerializeField] private AudioClip oong;
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip oong_nice;
    [SerializeField] private AudioClip crying;

    public Animator tajiri;
    private String[] tips = {
        "�����̷��ִٰ������Խ��ƴϱ׳�", 
        "ESC�� ������ ���� �޴��� �̵��ϰų� ������ ������ �� �ִٸ��̾�", 
        "Ÿ������ �ε����� Ÿ���������� �����κ��� �ָ� ����߷��� ���̾�",
        "�����δ��� �ڴ� ���� ũ�� ���̾�",
        "�ε����� �Ѿ����� ���� �� ������ �ֵ� ����Ʈ, Ÿ������ �ݷ��� ���ο��� ������ ���̾�.",
    };

    private void Start()
    {
        theAudio = GetComponent<AudioSource>();

        StartCoroutine(CountdownToStart());
        if (gameType)
            billboard.text= "���� �ð�: " + ((int)timeOrMl/60).ToString("D2") +":"+ ((int)timeOrMl % 60).ToString("D2");
        else
            billboard.text = timeOrMl.ToString() + "ml�� ���� ������ ����� �̱�� ���̾�";

        StartCoroutine(showTips());
    }

    private void Update()
    {

        if (Input.GetKeyUp("escape")) // esc �������� bgm �Ͻ����� �� ���
        {
            if (!isPaused)
            {
                theAudio.Pause();
                isPaused = true;
            }
            else
            {
                theAudio.Play();
                isPaused = false;
            }
        }
         if (isTimeTick && gameType)
        {
            timeOrMl -= Time.deltaTime;
            billboard.text = "���� �ð�: " + ((int)timeOrMl / 60).ToString("D2") + ":" + ((int)timeOrMl % 60).ToString("D2");
        }

        if(gameType && (int)timeOrMl == 0)
        {
            if (kanebuck > tajiribuck)
                SceneManager.LoadScene("win");
            else if (kanebuck == tajiribuck)
                SceneManager.LoadScene("tie");
            else
                SceneManager.LoadScene("lose");
        }

        if(!gameType && ((kanebuck>= (int)timeOrMl) || tajiribuck >= (int)timeOrMl))
        {
            if (kanebuck >= (int)timeOrMl)
            {
                SceneManager.LoadScene("win");
            }
            else if (tajiribuck >= (int)timeOrMl)
            {
                SceneManager.LoadScene("lose");
            }
            else if ((kanebuck >= (int)timeOrMl) && tajiribuck >= (int)timeOrMl)
                SceneManager.LoadScene("tie");
        }
    }
    IEnumerator CountdownToStart()
    {
        while (countDown > 0)
        {
            theAudio.clip = bbam;
            theAudio.Play();
            countDownDisplay.text =countDown.ToString();
            yield return new WaitForSeconds(1f);
            countDown--;
        }
        countDownDisplay.text = "GO!!!";
        theAudio.clip = oong;
        theAudio.Play();
        theAudio.volume = 0.4f;
        isStart = true;
        tajiri.SetTrigger("start");
        yield return new WaitForSeconds(1f);  
        
        countDownDisplay.gameObject.SetActive(false);
        theAudio.clip = bgm;
        theAudio.loop = true;
        theAudio.volume = 0.7f;
        theAudio.Play();
        isTimeTick = true;
    }
    IEnumerator showTips()
    {
        while (true)
        {
            tipindex = UnityEngine.Random.Range(0, tips.Length - 1); //���� �ε��� ����
            //���� �ε����� �ߺ� ���� �ڵ�
            if(prev_tipindex == tipindex)
                tipindex=(tipindex+1)%tips.Length;             
            tip.text = tips[tipindex];
            //���� �ε����� ���� �ε����� ����
            prev_tipindex = tipindex;

            Debug.Log(tipindex.ToString());
            yield return new WaitForSeconds(5f);
        }
    }
}
