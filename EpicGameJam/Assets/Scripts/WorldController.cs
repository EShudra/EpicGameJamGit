using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	public bool bulletPiercing = false;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Enemy"), !bulletPiercing);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Enemy"), LayerMask.NameToLayer ("Enemy"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Bullet"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Player"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bomb"), LayerMask.NameToLayer ("Player"), true);
		}
	
	// Update is called once per frame
	void Update () {
	
	}
}
