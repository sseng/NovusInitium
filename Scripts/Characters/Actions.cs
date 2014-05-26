using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Keymapping{
	public KeyCode combo;
	public KeyCode attack1;
	public KeyCode flameAttack;
	public KeyCode kick;
	public KeyCode upperCut;
	public KeyCode dash;

}

[System.Serializable]
public class VFX{
	public GameObject particle1;
	public Transform spawnPoint1;
}

public class Actions : Utilities {
	public Keymapping keymapping;
	public VFX effects;
	public float dashSpeed;
	//public GameObject hitBox;
	//public Transform lefthand, righthand;
	private AnimatorStateInfo currentBaseState;
	private Animator anim;
    private int currentState;
    private bool isMoving;
    private bool isAttacking;
    private bool canMove;
    private static int  idleState;
    private static int combo1State;
    private static int combo2State;
	private static int attackState, kickState;
    private static int moveState;
    private static int walkState;
    private float destinationDistance;
	private List<GameObject> hitboxes =  new List<GameObject>();

	void Start () {
		anim = GetComponent<Animator> ();
        combo1State = Animator.StringToHash("Base Layer.Combo1");
        combo2State = Animator.StringToHash("Base Layer.Combo2");
		kickState = Animator.StringToHash ("Attacks.Kick");
	}

	void Update () {
		//assign values to local variables 
		isMoving = UnitManager.player1.isMoving;
        canMove = UnitManager.player1.canMove;
		//animator states
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        currentState = currentBaseState.nameHash;
        //get position/destination distance
		Vector3 destinationPosition = UnitManager.player1.destination;
		Vector3 playerPos = UnitManager.player1.unitTransform.position;
		destinationDistance = Vector3.Distance (destinationPosition, playerPos);

        runAnimation(); 
        //HitboxControl();   

		if (Input.GetKey(keymapping.combo)) {
			ComboAttack ();
		}
		if (Input.GetKey (keymapping.dash)) {
			StartCoroutine(PlayAttack("Dash"));
			Dash(dashSpeed);
		}
		if (Input.GetKey (keymapping.attack1)) {
			StartCoroutine(PlayAttack("Attack1"));
		}
		if (Input.GetKey (keymapping.flameAttack)) {
			StartCoroutine(PlayAttack("FlameAttack"));
		}
		if(Input.GetKey(keymapping.kick)){
			Dash(10.0f);
			StartCoroutine(PlayAttack("Kick"));
		}
		if (Input.GetKey (keymapping.upperCut)) {
			StartCoroutine(PlayAttack("UpperCut"));
		}
		
		DisableMoveDuringAttack();
        UnitManager.player1.canMove = canMove;
	}

	private void spawnFlame(float t){
		GameObject fx = Instantiate(effects.particle1, effects.spawnPoint1.position, effects.spawnPoint1.transform.rotation) as GameObject;
		Destroy (fx, t);
	}
	private void HitboxControl(){		
		if( currentState == combo1State){
		}
		else{
		}		
	}
	private void DisableMoveDuringAttack(){
		if(currentState == combo1State ||currentState == combo2State || currentState == kickState){
			UnitManager.player1.destination = invalidPosition;
			canMove = false;
		}else{
			canMove = true;
		}
	}
	private void runAnimation(){
		if (isMoving) {
            anim.SetBool ("Moving", true);
        } else {
            anim.SetBool("Moving", false);
        }
    }
    private void ComboAttack(){
        StartCoroutine(PlayAttack("Combo1"));
        if (currentBaseState.nameHash == combo1State ) {
            StartCoroutine(PlayAttack("Combo2"));
        }
    }
    private void Dash(float dashSpeed){
		UnitManager.player1.moveSpeed = dashSpeed;
		UnitManager.player1.destination = hitPoint ();
    }
	private IEnumerator PlayAttack(string animationName){
		anim.SetBool (animationName, true);
		yield return null;
		anim.SetBool (animationName, false);
	}
	private IEnumerator spawnHitBox(Transform p, GameObject hitbox){
		Vector3 pos = p.position;
		Quaternion rot = p.rotation;
		GameObject hb = Instantiate(hitbox, pos, rot ) as GameObject;
		hitboxes.Add(hb);
		yield return null; 
	}
}