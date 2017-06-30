using UnityEngine;
using System.Collections;

public class PlatformerCamera : MonoBehaviour {

	public float smoothTimeX;
	public float smoothTimeY;
	public GameObject player;
	public float xAxisOffsetScreenPercentage = 1f;

	private Vector2 velocity;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate () {
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		//float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		if (Input.GetKey (KeyCode.D)) {
			posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x+xAxisOffsetScreenPercentage, ref velocity.x, smoothTimeX);
			posX += xAxisOffsetScreenPercentage;
		} else if (Input.GetKey (KeyCode.A)) {
			posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x-xAxisOffsetScreenPercentage, ref velocity.x, smoothTimeX);
			posX -= xAxisOffsetScreenPercentage;
		}

		transform.position = new Vector3 (posX, posY, transform.position.z);
	}
}
