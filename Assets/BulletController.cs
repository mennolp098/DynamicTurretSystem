﻿using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float destroyTime;
	public float speed;
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, destroyTime);
	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.transform.tag == "Enemy")
		{
			float _dmg = GameObject.FindGameObjectWithTag("Player").GetComponent<TowerController>().attackDamage;
			other.gameObject.GetComponent<EnemyBehavior>().getDmg(_dmg);
			Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}
}
