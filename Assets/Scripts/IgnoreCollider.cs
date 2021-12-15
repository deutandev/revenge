using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {   
        
		if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Spider")
		{
			Physics.IgnoreCollision(GetComponent<MeshCollider>(), collision.gameObject.GetComponent<CapsuleCollider>());
		}
		
		if (collision.gameObject.tag == "Fracture")
		{
			Physics.IgnoreCollision(GetComponent<MeshCollider>(), collision.gameObject.GetComponent<MeshCollider>());
		}
		
		if (collision.gameObject.tag == "Player")
		{
			Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<MeshCollider>(), true);
		}
    }
    
    void OnCollisionStay(Collision collision)
    {   
        
		if (collision.gameObject.tag == "Bat" || collision.gameObject.tag == "Spider")
		{
			Physics.IgnoreCollision(GetComponent<MeshCollider>(), collision.gameObject.GetComponent<CapsuleCollider>());
		}
		
		if (collision.gameObject.tag == "Fracture")
		{
			Physics.IgnoreCollision(GetComponent<MeshCollider>(), collision.gameObject.GetComponent<MeshCollider>());
		}
		
		if (collision.gameObject.tag == "Player")
		{
			Physics.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider>(), GetComponent<MeshCollider>(), true);
		}
    }
}
