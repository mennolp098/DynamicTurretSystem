using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TowerController : MonoBehaviour {
	public float attackCooldown = 1.0f;
	public float attackDamage = 10.0f;
	public float rotationSpeed;

	private List<EnemyBehavior> enemyScripts = new List<EnemyBehavior>();
	private float attackTime = 0f;

	public GameObject bullet;
	public Transform spawnpoint;
	void Update() 
	{
		if(enemyScripts.Count != 0)
		{
			for(int i = 0; i < enemyScripts.Count; i++)
			{
				if(enemyScripts[0].thisTransform)
				{
					Vector3 relativePos = enemyScripts[0].thisTransform.position - transform.position;
					Quaternion enemyLookAt = Quaternion.LookRotation(relativePos);
					this.transform.rotation = Quaternion.Slerp(transform.rotation, enemyLookAt, Time.deltaTime * rotationSpeed);
					if (Time.time > attackTime) 
					{
						attack ();
					}
				}
			}
		}

	}
	public void removeTarget(GameObject other)
	{
		if(enemyScripts.Contains(other.GetComponent<EnemyBehavior>()))
		{
			enemyScripts.Remove(other.GetComponent<EnemyBehavior>());
		}
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Enemy")
		{
			enemyScripts.Add(other.GetComponent<EnemyBehavior>());
			enemyScripts.Sort();
		}
	}
	void OnTriggerExit(Collider other) 
	{
		if(enemyScripts.Contains(other.GetComponent<EnemyBehavior>()))
		{
			enemyScripts.Remove(other.GetComponent<EnemyBehavior>());
			enemyScripts.Sort();
		}
	}
	void attack() 
	{
		attackTime = Time.time + attackCooldown;
		GameObject newBullet = Instantiate (bullet, spawnpoint.position, spawnpoint.rotation) as GameObject;
		newBullet.transform.parent = GameObject.FindGameObjectWithTag("Bullets").transform;
	}
}
