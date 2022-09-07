using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;


public class mc : BaseSceneManager<mc>
{
	int countzz = 1;

//	public GameType gameId;
	public int currentObjectLevel;
	public bool isActive = true;
	public bool isGameStarted;
	public bool isFirstStarted;
	private int sessionsCount;//会话次数（用户弹出评分框）
	public int currentPlayformId;//当前平台ID
	public Transform currentPlatform;//当前平台坐标


//	private int score = 0;//分数
	private int scorez = 0;//分数
//	public int scoreInRow = 1;//当前这行的分数
	public int scorezInRow = 2;//当前这行的分数
	public int scoreNow = 3;//当前的分数

	//添加金币重新启动
	public GameObject AddMoneyRestart;
	public Text AddMoneyRestartText;
	//
	public Animator anim;//动画，球面发生碰撞扭曲
	public GameObject AnimatedButton;//
	public Text best;//Text：最好成绩
	public GameObject bestUI;//最好成绩的UI（没啥用）
	//
	public GameObject canvas;//画布
	//
	public int currency;//金币
	public List<int> currencyList;//货币列表
	public Text currencyText;//文本：当前文本
	public GameObject currencyUI;//UI：当前金币

	[Header("配置")]
	public List<GameObject> objects;//游戏块
	public float startDrag;//开始时候的阻力：
	public float finalDrag;//结束时候的阻力：
	public PhysicMaterial mat;//物理反弹力
	public Material mcMat;
	public Material mcMatTwo;//被快速击破时候平台材质



	[Tooltip("球落地痕迹")]
	public List<GameObject> decal;//球落地痕迹



	public Text newRecord;
	public Text scoreNowText;
//	public Text scoreText;
	public Text scorezText;
	public Text levelFrom;
	public Text toplevelFrom;
	public GameObject toplevelFromz;
	public Text levelTo;
	public Image progression;
	public GameObject restartMenu;
	public Text restartPercentage;

	public GameObject finishPrefab;
	public GameObject winMenu;
	public Text levelText;


	Rigidbody rb;
	int createMoreCounter;
	public static bool createMore;

	public GameObject plusScorezz;

	[Header("音效")]
	public AudioSource AddMoneyRestartSound;
	public AudioSource deathAudio;//音效：死亡
	public AudioSource jumpAudio;//音效：通过平台
	public AudioSource hitAudio;//音效：球落地水花溅开

	int playerGameStatez;

	private void Start()
	{
		

		createMore = false;
		createMoreCounter = 0;
		this.restartMenu.SetActive(false);
		this.currentObjectLevel = PlayerPrefs.GetInt("currentObjectLevel");
		this.currentPlatform = null;
		playerGameStatez = PlayerPrefs.GetInt("playerGameStatez");
		if (playerGameStatez == 0) {
			
			this.best.text = "best: " + PlayerPrefs.GetInt("bestScorez");
			this.restartPercentage.gameObject.SetActive(false);
//			this.scoreText.gameObject.SetActive (false);
			this.scorezText.gameObject.SetActive (true);
			toplevelFromz.SetActive (false);
		} else if (playerGameStatez == 1) {
			this.best.gameObject.SetActive (false);
			this.restartPercentage.gameObject.SetActive(true);
//			this.scoreText.gameObject.SetActive (true);
			this.scorezText.gameObject.SetActive (false);
			toplevelFromz.SetActive (true);
		} else {
			this.best.gameObject.SetActive (false);

			this.restartPercentage.gameObject.SetActive(true);

			this.scorezText.gameObject.SetActive (false);
			toplevelFromz.SetActive (true);
		}

		this.levelFrom.text = (Base.currentLevel + 1).ToString();
		this.toplevelFrom.text = "Level " + (Base.currentLevel + 1).ToString();
		this.levelTo.text = (Base.currentLevel + 2).ToString();

		base.GetComponent<SphereCollider> ().isTrigger = false;

		rb = GetComponent<Rigidbody>();
	}

	//	void FixedUpdate()
	//	{
	//		Vector2 velocity = rb.velocity;
	//		velocity.x = 0f;
	//		rb.velocity = velocity;
	//	}


	void Update()
	{


		if (base.GetComponent<Rigidbody> ().velocity.y > 0.3f) {
			base.GetComponent<SphereCollider> ().isTrigger = true;
		} else if (base.GetComponent<Rigidbody> ().velocity.y < 0f) {
			base.GetComponent<SphereCollider> ().isTrigger = false;
		} else {
			base.GetComponent<SphereCollider> ().isTrigger = false;
		}




	
		this.scorezText.text = scorez.ToString();
		if (base.transform.position.y > 3f) {
			BaseSceneManager<UI>.Instance.Play();//游戏开始
//			toplevelFrom.enabled = false;
			toplevelFromz.SetActive (false);


		}


			if (base.transform.position.y > BaseSceneManager<Base>.Instance.platforms [this.currentPlayformId].transform.position.y) {


				if ((this.currentPlatform == null) || (this.currentPlatform.transform.parent != BaseSceneManager<Base>.Instance.platforms [this.currentPlayformId].transform)) {

					this.currentPlayformId++;
					int indexxx;
					indexxx = this.currentPlayformId - 3;
					if (indexxx > 1) {
						UnityEngine.Object.Destroy (BaseSceneManager<Base>.Instance.platforms [indexxx]);

					}

				}

				this.currentPlatform = null;
			}


	}



	//增加金币
	public void AddMoney(int money)
	{
		this.currency += money;
		PlayerPrefs.SetInt("currency", this.currency);
	}


	//失败
	public void Fail()
	{
		
//		this.scoreInRow = 0;
		this.scorezInRow = 0;

		this.isActive = false;


		base.GetComponent<Rigidbody>().isKinematic = true;//让球不再运动
		this.restartMenu.SetActive(true);
		        this.deathAudio.Play();

		this.restartPercentage.text = ((int)((BaseSceneManager<mc>.Instance.currentPlayformId * 100f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count + 1)))).ToString("D") + "% Completed!";

//		this.ReportScore((float)score);

		if (playerGameStatez == 0) {

			GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", scorez);

			if (PlayerPrefs.GetInt("bestScorez") < scorez)
			{
				this.newRecord.gameObject.SetActive(true);
				PlayerPrefs.SetInt("bestScorez", scorez);
				this.newRecord.text = "NEW RECORD!\n" + scorez.ToString();

			}
		} 


	}




	private void LateUpdate(){
		if (isFirstStarted == true) {
			if (countzz == 1) {
				if (Input.GetMouseButtonDown (0)) {
					
					Vector2 velocity = rb.velocity;
					velocity.y = 72.5f;
					rb.velocity = velocity;
					this.countzz = 0;
					this.jumpAudio.Play();
				}
			}
		}
	}

	//    private void LateUpdate()
	//    {
	////        if (this.setUpvelocity)
	////        {
	////            base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 32f, 0f);
	////			//normal is 30
	////            this.setUpvelocity = false;
	//
	////        }
	//
	//
	//    }

	//等级提升
	public void LevelUp()
	{
		if (this.currency >= this.currencyList[this.currentObjectLevel])
		{
			
				this.AddMoney(-this.currencyList[this.currentObjectLevel]);
				this.currentObjectLevel++;
				PlayerPrefs.SetInt("currentObjectLevel", this.currentObjectLevel);
				this.UpdateObjects();
				if (this.objects[this.currentObjectLevel - 1].GetComponent<Animator>() != null)
				{
					this.objects[this.currentObjectLevel - 1].GetComponent<Animator>().Play("Base Layer.appear", 0, 0f);
				}

		}
	}

	//发生碰撞
	private void OnCollisionEnter(Collision other)
	{
		//        UnityEngine.Debug.Log("OnCollisionEnter");
		if (other.collider.tag == "Fail")
		{
			base.StartCoroutine (this.Failzzz ());

				//                UnityEngine.Debug.Log("失败");
				this.Fail();
				CameraFollow.Shake (0.1f, 0.17f);

		}

		if (other.collider.tag == "Normal") {
			base.StartCoroutine (this.destroyLayerCoroutine (BaseSceneManager<Base>.Instance.platforms [this.currentPlayformId-1]));
			//				base.GetComponent<Rigidbody>().isKinematic = false;

			//				base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);

			CameraFollow.Shake (0.1f, 0.17f);
			this.hitAudio.Play();
		}


		//		}
		if (other.collider.tag == "groundz")
		{
			base.StartCoroutine (this.destroyLayerCoroutine (BaseSceneManager<Base>.Instance.platforms [this.currentPlayformId-1]));

//			UnityEngine.Debug.Log("Score!!");
			CameraFollow.Shake (0.1f, 0.15f);

			createMoreCounter++;
			if (createMoreCounter == 1) {
				createMore = true;
				createMoreCounter = 0;
			}

			int indexxxxxx = this.currentPlayformId - 3;
			if (indexxxxxx > 1) {
				UnityEngine.Object.Destroy (BaseSceneManager<Base>.Instance.platforms [indexxxxxx]);
			}
			this.hitAudio.Play();
		}


		if (other.collider.tag == "Finish")
		{

			//赢了
			//            UnityEngine.Debug.Log("Finish , you are win");
			this.Win();
//			this.scoreInRow = 0;
			this.hitAudio.Play();
		}


	}





	public void Restart()
	{
		base.StartCoroutine(this.AddMoneyRestartCoroutine());
		UI.Instance.clickAudio.Play ();
//		BaseSceneManager<UI>.Instance.Restartzz();
	}








	public void UpdateObjects()
	{
		for (int i = 0; i < this.objects.Count; i++)
		{
			if (i < this.currentObjectLevel)
			{
				this.objects[i].SetActive(true);
			}
		}
		if (this.currency >= this.currencyList[this.currentObjectLevel])
		{
			this.AnimatedButton.SetActive(true);
		}
		else
		{
			this.AnimatedButton.SetActive(false);
		}
	}

	//获胜
	public void Win()
	{

		this.isActive = false;
		base.GetComponent<Rigidbody>().isKinematic = true;
	
		this.levelText.text = "Level " + ((Base.currentLevel + 1)).ToString() + " passed";
		base.StartCoroutine(this.NextLevel());
	}

	float time = 0;
	float speed = 0;
	private IEnumerator Failzzz(){
		while (time < 0.5f)
			        {
			////            UnityEngine.Debug.Log("time ： " + time);
			            time += Time.deltaTime;

			speed += Time.deltaTime * Physics.gravity.y;
			//            for (int m = 0; m < objectList.Count; m++)
			//            {
			//                Transform objectItem = objectList[m];
			////				objectList[m].GetComponent<MeshRenderer>().material = this.mcMat;
			//
			Vector3 vector2 = (Vector3)((this.transform.forward * Time.deltaTime) * 22f);
			this.transform.position += new Vector3(-4f * Time.deltaTime, speed * Time.deltaTime, 0);
			this.transform.Rotate((float)(0f * Time.deltaTime), 0f * Time.deltaTime, (float)(220f * Time.deltaTime));
			////              
//			this.transform.rotation
//			gameObject.GetComponent<Animator> ().Play ("flip");               

			            yield return null; //下一帧调用, 什么都不做
			        }
	}
	//添加金币重启
	private IEnumerator AddMoneyRestartCoroutine()
	{
//		int scoreAdd = score / 50;//添加的分数

		this.newRecord.gameObject.SetActive(false);
		this.restartPercentage.gameObject.SetActive(false);

		yield return new WaitForSeconds(0.5f);

//		this.AddMoney(scoreAdd);
		yield return new WaitForSeconds(0.5f);


		if (playerGameStatez == 0) {
			scorez = 0;

		} 


//		mc.score = 0;
//		mc.scorez = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}

	//摧毁当前平台
	private IEnumerator destroyLayerCoroutine(GameObject layer)
	{
		
		this.countzz = 1;
//		this.scoreInRow = Base.currentLevel + 1;
		if (transform.position.y < 150) {
			this.scorezInRow = 2;
		} else if (transform.position.y < 300) {
			this.scorezInRow = 3;


		} else if (transform.position.y < 430) {
			this.scorezInRow = 4;

		} else {
			this.scorezInRow = 5;
		}



		if (playerGameStatez == 0) {
			scorez += this.scorezInRow;
			base.StartCoroutine(this.plusCoroutine());
		} 
//		else {
//			score += this.scoreInRow;
//		
//		}

	
		yield return null;
	}

	private IEnumerator NextLevel()
	{
		int i = 0;
		Dictionary<string, object> properties = new Dictionary<string, object>();
		float ypos = this.transform.position.y;
		properties["Level"] = Base.currentLevel;

		Base.currentLevel++;
		PlayerPrefs.SetInt("currentLevel", Base.currentLevel);
//		if (this.gameId != GameType.GAME_ANIM)
//		{
			this.winMenu.SetActive(true);
			yield return new WaitForSeconds(1.5f);
//		}
		while (i < 10)
		{
			i++;
			UnityEngine.Object.Instantiate<GameObject>(this.finishPrefab, new Vector3(0f, ypos + (5 * i), 0f), Quaternion.identity);
			yield return new WaitForSeconds(0.1f);
		}

//		if (this.gameId != GameType.GAME_WORLD)
//		{

			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//		}

	}

	private IEnumerator plusCoroutine()
	{

		GameObject plusScorezz = null;

			plusScorezz = UnityEngine.Object.Instantiate<GameObject>(this.plusScorezz, this.canvas.transform);
			plusScorezz.GetComponent<Animator>().Play("Base Layer.score", 0, 0f);

		if (transform.position.y < 150) {
			plusScorezz.gameObject.GetComponent<Text>().text = "+2".ToString();
		} else if (transform.position.y < 300) {
			plusScorezz.gameObject.GetComponent<Text>().text = "+3".ToString();


		} else if (transform.position.y < 430) {
			plusScorezz.gameObject.GetComponent<Text>().text = "+4".ToString();


		} else {
			plusScorezz.gameObject.GetComponent<Text>().text = "+5".ToString();

		}
			
		yield return new WaitForSeconds(0.6f);
		UnityEngine.Object.Destroy(plusScorezz);

	}
}