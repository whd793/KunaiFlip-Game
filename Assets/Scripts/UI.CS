using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class UI : BaseSceneManager<UI>
{
 
    public AudioSource clickAudio;//音效：点击
   
    public ParticleSystem psGrow;
    ParticleSystem.MainModule psGrowMM;

    public GameObject fortuneSpinButtonRounded;

    public Color colorEnd;//结束的颜色
    public Color colorStart;//开始的颜色
    public Transform pointEnd;//坐标：结束的点
    public Transform pointStart;
    public GameObject mainMenu;//主菜单
   
    public GameObject soundButton;//按钮：声音打开关闭
  
  
    //转圈
    public GameObject SpinWheel;
    public GameObject spinWheelRounded;//圆形的SpinWheel

	public GameObject afterPbutton;

	public GameObject soundOnBtn;
	public GameObject soundOffBtn;


	public void StartPlaying() {
		BaseSceneManager<mc>.Instance.isFirstStarted = true;
		afterPbutton.SetActive (false);
		this.mainMenu.SetActive(true);
		this.clickAudio.Play();

		int playerGameStatez = PlayerPrefs.GetInt("playerGameStatez");
		if (playerGameStatez == 0) {
			GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "game");
		}
	}
    public void Play()
    {
		BaseSceneManager<mc>.Instance.isGameStarted = true;
        this.mainMenu.SetActive(false);
//		GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
    }

  

    public void SoundsClicked()
    {
        bool flag = !PlayerPrefs.HasKey("soundsOn") || (PlayerPrefs.GetInt("soundsOn") == 1);
        flag = !flag;
        PlayerPrefs.SetInt("soundsOn", !flag ? 0 : 1);
        AudioListener.volume = !flag ? 0f : 1f;
        this.clickAudio.Play();
    }
		
	void UpdateMuteButtons()
	{
		
		if (!PlayerPrefs.HasKey("soundsOn") || (PlayerPrefs.GetInt("soundsOn") == 1))

		{
			soundOnBtn.gameObject.SetActive(true);
			soundOffBtn.gameObject.SetActive(false);


		}
		else
		{
			soundOnBtn.gameObject.SetActive(false);
			soundOffBtn.gameObject.SetActive(true);
		}
	}

    private void Start()
    {
        psGrowMM = this.psGrow.main;
        AudioListener.volume = (PlayerPrefs.HasKey("soundsOn") && (PlayerPrefs.GetInt("soundsOn") != 1)) ? 0f : 1f;
		GameAnalytics.Initialize ();
	}

   
	public void Ratez(){
		Application.OpenURL ("https://itunes.apple.com/us/app/kunai-flip/id1354399665?ls=1&mt=8");
		this.clickAudio.Play();
	}
    private void Update()
    {

		UpdateMuteButtons ();
        float t = (BaseSceneManager<mc>.Instance.currentPlayformId * 1f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1));
        BaseSceneManager<mc>.Instance.progression.fillAmount = t;
        BaseSceneManager<mc>.Instance.progression.color = (Color.Lerp(this.colorStart, this.colorEnd, t));
        this.pointStart.GetComponent<Image>().color = (Color.Lerp(this.colorStart, this.colorEnd, t));
        this.psGrowMM.startColor = Color.Lerp(this.colorStart, this.colorEnd, t);
        this.psGrow.transform.position = Vector3.Lerp(Camera.main.ScreenToWorldPoint(this.pointStart.position + new Vector3(0f, 0f, 1f)), Camera.main.ScreenToWorldPoint(this.pointEnd.position + new Vector3(0f, 0f, 1f)), t + 0.01f);
       
    }

   
   
}
