using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {	
        Invoke("Destroy", 7f);
    }
    
    private void Destroy() {Destroy(gameObject);}
}
