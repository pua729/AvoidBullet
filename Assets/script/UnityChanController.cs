using UnityEngine;
using System.Collections;

public class UnityChanController : MonoBehaviour {
	
	public const int MAX_LIFE = 10;
	
	public GameObject enemyGameObject;

	private Animator animator;
	protected int life = MAX_LIFE;

	void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool ("is_running", true);
	}
	
	void Update ()
	{
		switch (GameManager.Instance.getGameStatus()) {
		case GameManager.GAME_STATUS.WAIT:
			break;
		case GameManager.GAME_STATUS.RUNNING:
			this.runningControl();
			break;
		case GameManager.GAME_STATUS.END:
			break;
		case GameManager.GAME_STATUS.WIN:
			break;
		}
	}

	//
	// ユーザー入力処理
	//
	private void runningControl() 
	{
		if (animator.GetBool("is_jump")) {
			animator.SetBool("is_jump", false);
			transform.position += transform.right * animator.GetFloat("moving");
			return;
		}
		if (animator.GetBool("is_sliding")) {
			animator.SetBool("is_sliding", false);
		}
		
		transform.position += transform.forward * 0.01f;

		if (Input.GetKey (KeyCode.Space) && this.transform.position.y <= 0) {
			animator.SetBool ("is_jump", true);
			this.rigidbody.AddForce(Vector3.up * 13000);
		} else if (Input.GetKey ("right") || Input.GetKey (KeyCode.D)) {
			animator.SetBool ("is_jump", true);
			animator.SetFloat("moving",  0.05f);
		} else if (Input.GetKey ("left") || Input.GetKey (KeyCode.A)) {
			animator.SetBool ("is_jump", true);
			animator.SetFloat("moving", -0.05f);
		} else if (Input.GetKey ("down") || Input.GetKey (KeyCode.S)) {
			animator.SetBool ("is_sliding", true);
		}
	}
	
	//
	// 衝突安定
	//
	private void OnTriggerEnter(Collider collider) {
		this.hitBullet(collider);
		this.Goal(collider);
	}
	private void hitBullet(Collider collider) {
		if (!collider.CompareTag("enemy_bullet")) {
			return;
		}
		
		this.life -= 1;

		if (this.life < 0) {
			GameManager.Instance.loseGame();
		}
	}
	private void Goal(Collider collider) {
		if (this.enemyGameObject.name == collider.name) {
			GameManager.Instance.winGame();
			animator.SetBool("is_winning", true);
		}
	}
	
	//
	// 描画処理
	//
	void OnGUI() {
		switch (GameManager.Instance.getGameStatus()) {
		case GameManager.GAME_STATUS.WAIT:
			break;
		case GameManager.GAME_STATUS.RUNNING:
			GUI.Box (new Rect (0,0,100,50), "Life: " + this.life);		
			break;
		case GameManager.GAME_STATUS.END:
			GUI.Box (new Rect (Screen.width/2-50,Screen.height/2-25,100,50), "You lose");
			if (GUI.Button(new Rect(Screen.width/2-40,Screen.height/2,80,20), "Restart")) {
				GameManager.Instance.restartGame();
				Application.LoadLevel("Sample");
			}
			break;
		case GameManager.GAME_STATUS.WIN:
			GUI.Box (new Rect (Screen.width/2-50,Screen.height/2-25,100,50), "You win!");
			if (GUI.Button(new Rect(Screen.width/2-40,Screen.height/2,80,20), "Restart")) {
				GameManager.Instance.restartGame();
				Application.LoadLevel("Sample");
			}
			break;
		}
	}
}
