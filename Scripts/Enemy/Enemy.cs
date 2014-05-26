using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float curHealth = 20;
	public float maxHealth = 20;
	
	private void Update(){
		if(curHealth <= 0){
			Destroy(this.gameObject);
		}
		if(curHealth > maxHealth){
			curHealth = maxHealth;	
		}
	}
	public void ApplyDamage(float amount){
		curHealth -= amount;
	}
	
	private IEnumerable KillSelf(int timer){
		yield return new WaitForSeconds(timer);
		Destroy(this.gameObject);
	}
}
