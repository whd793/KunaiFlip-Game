using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class Base : BaseSceneManager<Base>
{
   
   
//	public static Base Instance { private set; get; }
    public static int currentLevel = 0;

    public float currentYOffset;//当前Y轴的偏离距离

    public List<GameObject> platforms;//平台列表


    public List<GameObject> beginModels;
    public List<GameObject> easyModels;//圆台模型：简单难度
    public List<GameObject> midModels;//圆台模型：中等难度
    public List<GameObject> hardModels;
	public List<GameObject> movingModels;//圆台模型：开始
	public List<GameObject> smModels;//圆台模型：开始

	public List<GameObject> endlessModels;
    public GameObject finish;//结束的台面
   
    public GameObject mainBranch;//中心柱体
	public int playerGameStatez;
	public int sessionCountz;

	public GameObject topMenuz;
	public GameObject playlvlz;
	public GameObject playendlessz;
    private void Awake()
    {
//		Instance = this;
//		Instance = this;
		Application.targetFrameRate = 60;
		this.playerGameStatez = PlayerPrefs.GetInt("playerGameStatez");
        this.platforms = new List<GameObject>();
		if (playerGameStatez == 1) {
			this.CreateLevel ();
			topMenuz.SetActive (true);
			playlvlz.SetActive (false);
			playendlessz.SetActive (true);
		


		} else if (playerGameStatez == 0) {
			this.CreateEndless ();
			topMenuz.SetActive (false);
			playlvlz.SetActive (true);
			playendlessz.SetActive (false);

		} else {
			this.CreateEndless ();
			topMenuz.SetActive (false);
			playlvlz.SetActive (true);
			playendlessz.SetActive (false);
		
		}
    }

	void Update() {
		if (mc.createMore == true) {
			CreateMoreEndless ();
		}
	}

	public void PlayEndless(){
		PlayerPrefs.SetInt("playerGameStatez", 0);
	
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	

	}

	public void PlayLevelzz(){
		PlayerPrefs.SetInt("playerGameStatez", 1);
	
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);


	}

	public void CreateEndless()
	{
		
		int i = 0;
		this.currentYOffset -= 5f;
		this.mainBranch.transform.position = new Vector3 (0f, currentYOffset, -5f);
		this.currentYOffset += 12.2f;
		for(i = 0; i < 5; i++)
		{
			this.platforms.Add(Object.Instantiate<GameObject>(this.endlessModels[Random.Range(0, this.endlessModels.Count)], new Vector3(0f, this.currentYOffset, -5f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), base.transform));
			//			platforms2.Add(UnityEngine.Object.Instantiate<GameObject>(this.beginModels[Random.Range(0, this.beginModels.Count)], new Vector3(0f, this.currentYOffset, -5f), Quaternion.identity));

			this.currentYOffset += 12.2f;
		}

	}

	public void CreateMoreEndless()
	{
		mc.createMore = false;

		int i = 0;

		for(i = 0; i < 1; i++)
		{
			this.platforms.Add(Object.Instantiate<GameObject>(this.endlessModels[Random.Range(0, this.endlessModels.Count)], new Vector3(0f, this.currentYOffset, -5f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), base.transform));
			//			platforms2.Add(UnityEngine.Object.Instantiate<GameObject>(this.beginModels[Random.Range(0, this.beginModels.Count)], new Vector3(0f, this.currentYOffset, -5f), Quaternion.identity));

			this.currentYOffset += 12.2f;
		}

		StartCoroutine(CoEndless());

	}

	public IEnumerator CoEndless() {
		yield return new WaitForSeconds(0.01f);
		mc.createMore = false;

	}
    /**
     * 创建对应等级游戏
     **/
    public void CreateLevel()
    {
//		PlayerPrefs.SetInt("currentLevel", 110);
		currentLevel = PlayerPrefs.GetInt("currentLevel");
//		currentLevel = 110;
        int num = 1;
//        if (BaseSceneManager<mc>.Instance.gameId == GameType.GAME_SHORT)
//        {
//            num = 3;
//        }

        //总的平台数量 = 开始难度的数量 + 简单难度的数量 + 中等难度数量 + 困难难度的数量
//		int allPlatformNum = (this.beginCountConfig[0] / num) + (this.easyCountConfig[0] / num) + (this.midCountConfig[0] / num) + (this.hardCountConfig[0] / num);
//        Debug.Log("总的层数 : " + allPlatformNum);
        //创建所有平台
		this.currentYOffset -= 5f;
		this.mainBranch.transform.position = new Vector3 (0f, currentYOffset, -5f);
		//			        this.mainBranch.transform.localScale = new Vector3(9.9f, (90f - this.currentYOffset) / 2f, 9.9f);
		this.currentYOffset += 12.2f;

		int numMovingPlatform = 2;
		for (int i2 = 0; i2 < numMovingPlatform + 1; i2++) {
			this.platforms.Add (Object.Instantiate<GameObject> (this.beginModels [Random.Range (0, this.beginModels.Count)], new Vector3 (0f, this.currentYOffset, -5f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)), base.transform));
			this.currentYOffset += 12.2f;
		}

		int numMovingPlatform2 = currentLevel;
		if (currentLevel > 3) {
			numMovingPlatform2 = 3;
		}
		for (int i2 = 0; i2 < numMovingPlatform2 + 1; i2++) {
			this.platforms.Add (Object.Instantiate<GameObject> (this.easyModels [Random.Range (0, this.easyModels.Count)], new Vector3 (0f, this.currentYOffset, -5f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)), base.transform));
			this.currentYOffset += 12.2f;
		}

	
		int numMovingPlatform3 = 1 + currentLevel / 2;
		if (currentLevel > 6) {
			numMovingPlatform3 = 4;
		}

		for (int i2 = 0; i2 < numMovingPlatform3 + 1; i2++) {
			
			this.platforms.Add (Object.Instantiate<GameObject> (this.midModels [Random.Range (0, this.midModels.Count)], new Vector3 (0f, this.currentYOffset, -5f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)), base.transform));
			this.currentYOffset += 12.2f;
		}

		int numMovingPlatform4 = currentLevel / 5;
		if (currentLevel > 30) {
			numMovingPlatform4 = 6;
		}
		for (int i2 = 0; i2 < numMovingPlatform4 + 1; i2++) {
			
			this.platforms.Add (Object.Instantiate<GameObject> (this.hardModels [Random.Range (0, this.hardModels.Count)], new Vector3 (0f, this.currentYOffset, -5f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)), base.transform));
			this.currentYOffset += 12.2f;
		}

		int numMovingPlatform5 = (currentLevel / 3);
		if (currentLevel > 12) {
			numMovingPlatform5 = 4;
		}
		for (int i2 = 0; i2 < numMovingPlatform5 + 1; i2++) {

			this.platforms.Add (Object.Instantiate<GameObject> (this.movingModels [Random.Range (0, this.movingModels.Count)], new Vector3 (0f, this.currentYOffset, -5f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)), base.transform));
			this.currentYOffset += 12.2f;
		}

		int numMovingPlatform6 = (currentLevel / 7);
		if (currentLevel > 35) {
			numMovingPlatform6 = 5;
		}
		for (int i2 = 0; i2 < numMovingPlatform6 + 1; i2++) {

			this.platforms.Add (Object.Instantiate<GameObject> (this.smModels [Random.Range (0, this.smModels.Count)], new Vector3 (0f, this.currentYOffset, -5f), Quaternion.Euler (new Vector3 (0f, 0f, 0f)), base.transform));
			this.currentYOffset += 12.2f;
		}



		this.platforms.Add(Object.Instantiate<GameObject>(this.finish, new Vector3(0f, this.currentYOffset, -5f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), base.transform));

	
	}

   

  
}
