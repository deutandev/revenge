using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public GameObject player;
	public Transform reciever;
	public bool reverse = true;
	public bool right = true;

	private bool playerIsOverlapping = false;

	// Update is called once per frame
	void Update () {
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.transform.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				// Teleport him!
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				int index = 1;
				if(reverse == true)
				{
					index = -1;
					rotationDiff += 180;
				}
				else rotationDiff += 0;
				player.transform.Rotate(Vector3.up, rotationDiff);
				
				int x = 1;
				if(right == false) x = -1;
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer * x;
				player.transform.position = reciever.position + positionOffset;
				
				Rigidbody playerRb = player.GetComponent<Rigidbody>();
				playerRb.AddForce(400f * index, 0, 0);

				playerIsOverlapping = false;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
