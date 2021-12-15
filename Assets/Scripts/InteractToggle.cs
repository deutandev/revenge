using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractToggle : MonoBehaviour
{
    public GameObject InteractButton;
    [SerializeField] private Animator Lever = null;
    [SerializeField] private Animator[] Platform;
    public List<GameObject> Platforms;
    [SerializeField] private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractButton.SetActive(true);
            // if (!opened)
            // {
            //     Lever.Play("handle|rotateL", 0, 0.0f);
            //     opened = true;
            // }
            // else if (opened)
            // {
            //     Lever.Play("handle|rotateR", 0, 0.0f);
            //     opened = false;
            // }
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
            Lever.Play("handle|rotateL", 0, 0.0f);
            
            foreach (GameObject plat in Platforms)
            { plat.GetComponent<Animator>().Play("Plate|Open", 0, 0.0f); }
            
            opened = true;
        }
        else if (opened)
        {
            Lever.Play("handle|rotateR", 0, 0.0f);
            
            foreach (GameObject plat in Platforms)
            { plat.GetComponent<Animator>().Play("Plate|Close", 0, 0.0f); }
            
            opened = false;
        }
    }
}
