using UnityEngine;
using System.Collections;

public class UnitManager {
	public static Unit player1;
	
	static UnitManager(){
		player1 = new Unit ();
	}
}
