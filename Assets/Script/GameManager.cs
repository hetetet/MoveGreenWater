using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static bool gameType=true; //true: 시간 모드, false: N밀리내기 모드
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
        "뭉탱이로있다가유링게슝아니그냥", 
        "ESC를 누르면 메인 메뉴로 이동하거나 게임을 종료할 수 있다맨이야", 
        "타지리와 부딪혀서 타지리리님을 녹즙기로부터 멀리 떨어뜨려라 맨이야",
        "케인인님의 코는 정말 크다 맨이야",
        "부딪혀서 넘어지는 것이 이 게임의 주된 포인트, 타지리와 격렬한 몸싸움을 벌여라 맨이야.",
    };

    private void Start()
    {
        theAudio = GetComponent<AudioSource>();

        StartCoroutine(CountdownToStart());
        if (gameType)
            billboard.text= "남은 시간: " + ((int)timeOrMl/60).ToString("D2") +":"+ ((int)timeOrMl % 60).ToString("D2");
        else
            billboard.text = timeOrMl.ToString() + "ml를 먼저 모으는 사람이 이긴다 맨이야";

        StartCoroutine(showTips());
    }

    private void Update()
    {

        if (Input.GetKeyUp("escape")) // esc 눌렀을때 bgm 일시정지 및 재생
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
            billboard.text = "남은 시간: " + ((int)timeOrMl / 60).ToString("D2") + ":" + ((int)timeOrMl % 60).ToString("D2");
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
            tipindex = UnityEngine.Random.Range(0, tips.Length - 1); //랜덤 인덱스 선택
            //이전 인덱스와 중복 방지 코드
            if(prev_tipindex == tipindex)
                tipindex=(tipindex+1)%tips.Length;             
            tip.text = tips[tipindex];
            //지금 인덱스를 이전 인덱스에 저장
            prev_tipindex = tipindex;

            Debug.Log(tipindex.ToString());
            yield return new WaitForSeconds(5f);
        }
    }
}
