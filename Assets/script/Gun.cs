using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{	
	public const float BULLET_SHOOT_INTERVAL = 0.2f;
	public const float BULLET_CHARGE_INTERVAL = 5.0f;
	public const float BULLET_VELOCITY = 10.0f;
	public const int   BULLET_MAX_COUNT = 6;
	
	public GameObject bulletPrefab;
	public GameObject targetObject;
	public Vector3 shootVector3;
	
	protected bool bulletEnable = true;
	protected float bulletTime = 0.0f;
	protected int bulletCount = BULLET_MAX_COUNT;
	protected float bulletChargeTime = 0.0f;
	
	void Update () 
	{
		if (this.bulletEnable && this.bulletCount > 0) {
			this.Shoot();
			this.bulletCount--;
			this.bulletEnable = false;
		}
		
	    this.bulletTime += Time.deltaTime;
	    if (this.bulletTime > BULLET_SHOOT_INTERVAL) {
	        this.bulletTime = 0.0f;
	        this.bulletEnable = true;
	    }
		
		this.bulletChargeTime += Time.deltaTime;
		if (this.bulletChargeTime > BULLET_CHARGE_INTERVAL) {
			this.bulletChargeTime = 0.0f;
			this.Reload();
		}
		
		
		
		/*
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
		GameObject bullet = (GameObject)Instantiate(this.bulletPrefab, shootPos, transform.rotation);
		Vector3 direction = this.GetNormalizedDirection(transform.position, this.targetObject.transform.position);
		
		bullet.rigidbody.AddForce(direction * BULLET_VELOCITY, ForceMode.VelocityChange);
	}
	
	Vector3 GetNormalizedDirection(Vector3 from_pos, Vector3 to_pos) {
		Vector3 direction = new Vector3(to_pos.x - from_pos.x, to_pos.y - from_pos.y, to_pos.z - from_pos.z);
		return direction.normalized;
	}
	
	//
	// 弾を装填する
	//
	void Reload()
	{
		this.bulletCount = BULLET_MAX_COUNT;
	}
}
