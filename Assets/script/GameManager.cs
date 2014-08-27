using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public enum GAME_STATUS
	{
		WAIT = 0,
		RUNNING = 1,
		END = 2,
		WIN = 3
	}
	protected GAME_STATUS game_status = GAME_STATUS.RUNNING;
	
    public void Awake()
    {
        if(this != Instance)
        {
            Destroy(this);
            return;
        }
 
        DontDestroyOnLoad(this.gameObject);
    }
	
	void FixedUpdate()
	{
		Debug.Log("game_status: " + this.game_status);
	}

	public void startGame()
	{
		this.game_status = GAME_STATUS.RUNNING;
	}
	public void loseGame()
	{
		this.game_status = GAME_STATUS.END;
	}
	public void winGame()
	{
		this.game_status = GAME_STATUS.WIN;
	}
	public void restartGame()
	{
		this.game_status = GAME_STATUS.RUNNING;
	}
	
	public GAME_STATUS getGameStatus()
	{
		return this.game_status;
	}
}
