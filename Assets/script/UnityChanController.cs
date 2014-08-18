using UnityEngine;
using System.Collections;

public class UnityChanController : MonoBehaviour {
	
	public const int MAX_LIFE = 10;
	public const int GAME_STATUS_WAIT = 0;
	public const int GAME_STATUS_RUNNING = 1;
	public const int GAME_STATUS_END = 2;
	public const int GAME_STATUS_WIN = 3;
	
	public GameObject enemyGameObject;

	private Animator animator;
	protected int life = MAX_LIFE;
	protected int game_status = GAME_STATUS_RUNNING;

	void Start () {
		animator = GetComponent<Animator>();
	}
	
	void Update ()
	{
		switch (this.game_status) {
		case GAME_STATUS_WAIT:
			break;
		case GAME_STATUS_RUNNING:
			this.runningControl();
			break;
		case GAME_STATUS_END:
			break;
		case GAME_STATUS_WIN:
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
		
		if (Input.GetKey ("up") || Input.GetKey (KeyCode.W)) {
			transform.position += transform.forward * 0.08f;
			animator.SetBool ("is_running", true);
		}
		
		if (Input.GetKey (KeyCode.Space)) {
			animator.SetBool ("is_jump", true);
		} else if (Input.GetKey ("right") || Input.GetKey (KeyCode.D)) {
			animator.SetBool ("is_jump", true);
			animator.SetFloat("moving",  0.08f);
		} else if (Input.GetKey ("left") || Input.GetKey (KeyCode.A)) {
			animator.SetBool ("is_jump", true);
			animator.SetFloat("moving", -0.08f);
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
			this.loseGame();
		}
	}
	private void loseGame() {
		this.game_status = GAME_STATUS_END;
	}
	private void Goal(Collider collider) {
		if (this.enemyGameObject.name == collider.name) {
			this.game_status = GAME_STATUS_WIN;
			animator.SetBool("is_winning", true);
		}
	}
	
	//
	// 描画処理
	//
	void OnGUI() {
		switch (this.game_status) {
		case GAME_STATUS_WAIT:
			break;
		case GAME_STATUS_RUNNING:
			GUI.Box (new Rect (0,0,100,50), "Life: " + this.life);		
			break;
		case GAME_STATUS_END:
			GUI.Box (new Rect (Screen.width/2-50,Screen.height/2-25,100,50), "You lose");
			if (GUI.Button(new Rect(Screen.width/2-40,Screen.height/2,80,20), "Restart")) {
				Application.LoadLevel("Sample");
			}
			break;
		case GAME_STATUS_WIN:
			GUI.Box (new Rect (Screen.width/2-50,Screen.height/2-25,100,50), "You win!");
			if (GUI.Button(new Rect(Screen.width/2-40,Screen.height/2,80,20), "Restart")) {
				Application.LoadLevel("Sample");
			}
			break;
		}
	}
}
