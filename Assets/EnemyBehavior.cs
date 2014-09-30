using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyBehavior : MonoBehaviour, IComparable<EnemyBehavior> {
	protected float speed = 0.03f;
	public float health = 10;
	private GameObject target;
	private float counter = 1;
	private DateTime TimeAdded;

	public Transform thisTransform;
	public int sort;
	public int CompareTo(EnemyBehavior other)
	{
		if(other.sort == this.sort)
		{
			return this.TimeAdded.CompareTo(other.TimeAdded);
		}
		return other.sort.CompareTo(this.sort);
	}

	protected virtual void Start () {
		target = GameObject.Find ("Waypoint-1");
		thisTransform = this.transform;
		TimeAdded = DateTime.Now;
	}
	void Update () {
		if(target)
		{
			transform.LookAt(target.transform);
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
			if(Vector3.Distance (this.transform.position, target.transform.position) < 0.5f)
			{
				counter++;
				var newWaypointName = "Waypoint-" + counter;
				GameObject newWaypoint = GameObject.Find(newWaypointName);
				target = newWaypoint;
			}
		}
	}
	public void getDmg(float dmg)
	{
		health -= dmg;
		if(health <= 0)
		{
			GameObject[] towers = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject tower in towers)
			{
				tower.GetComponent<TowerController>().removeTarget(this.gameObject);
			}
			Destroy(this.gameObject);
		}
	}
}
