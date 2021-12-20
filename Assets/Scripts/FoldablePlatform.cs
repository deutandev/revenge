using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldablePlatform : MonoBehaviour
{
	public Canvas canvas;
	public alfonso player;
	public FollowTarget camera;
    public GameObject InteractButton;
    public GameObject switchTarget;
    private Animator Lever;
    public List<GameObject> Platforms;
    [SerializeField] private bool opened = false;
    
    private void Start()
    {
		Lever = GetComponent<Animator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetActive(false);
        }
    }

    public void ToggleLever()
    {
        if (!opened)
        {
            StartCoroutine(OpenFoldablePlatform());
        }
        else if (opened)
        {
           StartCoroutine(CloseFoldablePlatform());
        }
    }
    
    IEnumerator OpenFoldablePlatform()
	{
		canvas.enabled = false;
		
		Lever.Play("handle|rotateL", 0, 0.0f);
            
        camera.target = switchTarget;
        camera.inZoomArea = true;
		camera.zoomInFOV = 80f;
		
		yield return new WaitForSeconds(1.5f);
		
		foreach (GameObject plat in Platforms)
        { plat.GetComponent<Animator>().Play("Plate|Open", 0, 0.0f); }
            
        opened = true;
        
        yield return new WaitForSeconds(1.5f);
        
        camera.target = player.gameObject;
		camera.inZoomArea = false;
		
		yield return new WaitForSeconds(0.5f);
		
		canvas.enabled = true;
	}
	
	IEnumerator CloseFoldablePlatform()
	{
		canvas.enabled = false;
		
		Lever.Play("handle|rotateR", 0, 0.0f);
            
        camera.target = switchTarget;
        camera.inZoomArea = true;
		camera.zoomInFOV = 80f;
		
		yield return new WaitForSeconds(1.5f);
		
		foreach (GameObject plat in Platforms)
        { plat.GetComponent<Animator>().Play("Plate|Close", 0, 0.0f); }
            
        opened = false;
        
        yield return new WaitForSeconds(1.5f);
        
        camera.target = player.gameObject;
		camera.inZoomArea = false;
		
		yield return new WaitForSeconds(0.5f);
		
		canvas.enabled = true;
	}
}
