using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class PlayerController : Utilities {
	public float moveSpeed = 5.0f;
	public float minMoveDistance = 0.5f;
	
	private Transform playerTransform;
	private float destinationDistance;
	private Vector3 destinationPosition;

	private bool isMoving, isAttacking;
    private bool canMove;
	
	void Start () {
		UnitManager.player1.unitTransform = transform;
        UnitManager.player1.destination = destinationPosition;
        UnitManager.player1.canMove = true;
	}
	
	void Update(){
		//get values from static object
		playerTransform = UnitManager.player1.unitTransform;
		isMoving = UnitManager.player1.isMoving;
        destinationPosition = UnitManager.player1.destination;
        canMove = UnitManager.player1.canMove;
		moveSpeed = UnitManager.player1.moveSpeed;
		
        //Movement
        if (Input.GetMouseButton (0) && canMove){
			destinationPosition = hitPoint();
		}
		
		if(destinationPosition != invalidPosition){
            isMoving = true;
			MoveUnit(destinationPosition);
			RotateUnit(destinationPosition);
        }else{
            isMoving = false;
        }
		if (destinationDistance <= 1) {
			moveSpeed = 5.0f;
		}
		//update values onto the static object 
		UnitManager.player1.unitTransform = playerTransform;
		UnitManager.player1.isMoving = isMoving;
        UnitManager.player1.destination = destinationPosition;
        UnitManager.player1.canMove = canMove;
		UnitManager.player1.moveSpeed = moveSpeed;
	}

	void RotateUnit(Vector3 destination){
		destination.y = playerTransform.position.y;
		if (destination != Vector3.zero) {
			Quaternion targetRotation = Quaternion.LookRotation (destination - transform.position);
			playerTransform.rotation = targetRotation;
		}
	}
	void MoveUnit(Vector3 destination){
		destinationDistance = Vector3.Distance (destinationPosition, playerTransform.position);
		if (destinationDistance >= minMoveDistance) {
			isMoving = true;
			transform.position = Vector3.MoveTowards (playerTransform.position, destination, moveSpeed * Time.deltaTime);
		}else{ 
			isMoving = false;
		}
	}
}