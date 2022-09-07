using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundMotion : MonoBehaviour {

	private float delta = 9.5f;  // Amount to move left and right from the start point
	public float speed;
	private Vector3 startPos;
	public bool movi;
	public List<float> onez;


	public Material mcMat;
	public Material mcMatTwo;//被快速击破时候平台材质


	void Start () {
//		transform.position -= new Vector3 (7f,0,0);
		startPos = transform.position;

//		this.canHitzzz = PlayerPrefs.GetInt("canHitzzz");
		movi = true;

//		List<float> onezClone = onez;

		int playerGameStatez = PlayerPrefs.GetInt("playerGameStatez");

		if (playerGameStatez == 0) {
			if (transform.position.y < 150) {
				speed = Random.Range (2.1f, 3.1f);
				} else if (transform.position.y < 300) {
				speed = Random.Range (2.6f, 3.6f);
			} else if (transform.position.y < 430) {
				speed = Random.Range (3.3f, 3.9f);
			} else {
				speed = Random.Range (2.7f, 4f);
				transform.localScale = new Vector3(transform.localScale.x * 0.8f,transform.localScale.y,transform.localScale.z);

			}
				
		} else if (playerGameStatez == 1) {
			speed = onez[Random.Range(0, onez.Count)];

		} else {
			speed = onez[Random.Range(0, onez.Count)];

		}

//		speed = onez[Random.Range(0, onez.Count)];


	}
//	private bool dirRight = true;

	void Update () {

//		if (BaseSceneManager<mc>.Instance.isActive == true) {
		if (movi == true && BaseSceneManager<mc>.Instance.isActive == true) {
				Vector3 v = startPos;
				//METHOD ONE
//				v.x += delta * Mathf.Sin (Time.time * speed);

				//METHOD TWO
//				v.x += Mathf.PingPong (Time.time * speed * 5.5f, delta * 2);

				//METHOD THREE
				v.x = Mathf.PingPong (Time.time * speed * 5.2f, delta * 2) - 9.5f;

				//METHOD FOUR
//				if (dirRight)
//					transform.Translate (Vector2.right * speed * 5.5f* Time.deltaTime);
//				else
//					transform.Translate (-Vector2.right * speed * 5.5f* Time.deltaTime);
//
//				if(transform.position.x >= 7.0f) {
//					dirRight = false;
//				}
//
//				if(transform.position.x <= -7) {
//					dirRight = true;
//				}
//
				transform.position = v;
//			}
		}


	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Player")
		{
			
			movi = false;
			StartCoroutine (hitzz ());
		}
	}


	private IEnumerator hitzz() {
//		float speed = 0f;//速度
		float time = 0f;//时间

		while (time < 0.04f)
		{
			//            UnityEngine.Debug.Log("time ： " + time);
			time += Time.deltaTime;
//			speed += Time.deltaTime * Physics.gravity.y;
			gameObject.GetComponent<MeshRenderer>().material = this.mcMat;

			yield return null; //下一帧调用, 什么都不做
		}

		gameObject.GetComponent<MeshRenderer>().material = this.mcMatTwo;


	}
}