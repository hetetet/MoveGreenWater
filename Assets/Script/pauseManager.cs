using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    public bool isPaused = false;
    public Image pauseModal;
    // Start is called before the first frame update
    public void quitgame()
    {
        Application.Quit();
    }
    public void gointro()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("intro");
    }
    private void Start()
    {
        pauseModal.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp("escape")) // esc 눌렀을때 일시정지 모달창 띄우기
        {
            if (!isPaused) //일시정지 안됐으면
            {
                isPaused = true;
                Time.timeScale = 0;
                pauseModal.gameObject.SetActive(true);
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
                pauseModal.gameObject.SetActive(false);
            }
        }
    }
}
