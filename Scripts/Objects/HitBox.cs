using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour {

	public int Damage;
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<Enemy>().ApplyDamage(Damage);
		}
	}
}
