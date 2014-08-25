using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{	
	public const float BULLET_SHOOT_INTERVAL = 0.2f;
	public const float BULLET_CHARGE_INTERVAL = 1.0f;
	public const float BULLET_VELOCITY = 30.0f;
	public const int   BULLET_MAX_COUNT = 6;
	
	public GameObject bulletPrefab;
	public GameObject explodePrefab;
	public GameObject targetObject;
	public Vector3 shootVector3;
	
	protected bool bulletEnable = true;
	protected int bulletCount = BULLET_MAX_COUNT;
	
	void Update () 
	{
		this.SampleStrategy();

		this.DrawLine();
	}
	
	// 弾があればなくなるまで連射する
	void SampleStrategy()
	{
		if (this.bulletEnable && this.bulletCount > 0) {
			this.Shoot();
			this.bulletCount--;
			StartCoroutine("WaitForIt");		
		} 
		if (this.bulletCount <= 0){
			StartCoroutine("WaitForReload");		
		}
	}
    IEnumerator WaitForIt(){		
        this.bulletEnable = false;
        yield return new WaitForSeconds(BULLET_SHOOT_INTERVAL);
        this.bulletEnable = true;
    }
    IEnumerator WaitForReload(){		
        yield return new WaitForSeconds(BULLET_CHARGE_INTERVAL);
		this.bulletCount = BULLET_MAX_COUNT;
    }
	
	//
	// 射線を描く
	//
	void DrawLine()
	{
		/* TODO
		// ターゲットあでの線を引く
		LineRenderer line = gameObject.GetComponent<LineRenderer>();;
		//line.material.color = Color.red;
		//line.SetWidth(1, 0.1);
		line.SetVertexCount(2);
		line.SetPosition(0, transform.position);
		line.SetPosition(1, this.targetObject.transform.position);
		*/
	}
	
	//
	// 弾を発射する
	// TODO 上下左右にバラけさせる
	//
	void Shoot()
	{
		Vector3 shootPos = transform.position + this.shootVector3;

		// 爆発エフェクト
		GameObject explode = (GameObject)Instantiate(this.explodePrefab, shootPos, transform.rotation);
		
		// 弾
		GameObject bullet = (GameObject)Instantiate(this.bulletPrefab, shootPos, transform.rotation);
		Vector3 direction = this.GetNormalizedDirection(transform.position, this.targetObject.transform.position);
		
		bullet.rigidbody.AddForce(direction * BULLET_VELOCITY, ForceMode.VelocityChange);
	}
	
	Vector3 GetNormalizedDirection(Vector3 from_pos, Vector3 to_pos) {
		Vector3 direction = new Vector3(to_pos.x - from_pos.x, to_pos.y - from_pos.y, to_pos.z - from_pos.z);
		return direction.normalized;
	}
	
}
