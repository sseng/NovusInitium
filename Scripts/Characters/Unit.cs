using UnityEngine;
using System.Collections;

public class Unit {
	public float hp, maxHp, mp, maxMp;
	public Transform unitTransform;
    public Vector3 destination;
	public bool isMoving;
    public bool canMove;
	public float moveSpeed = 5.0f;
	
	public Unit(){
		this.hp = 100;
		this.maxHp = 100;
		this.mp = 20;
		this.maxMp = 20;
	}

}
