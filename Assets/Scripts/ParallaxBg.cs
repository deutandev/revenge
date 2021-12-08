using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
  private float length, xStartPos, yStartPos;
  public GameObject cam;
  public float xParallaxEffect, yParallaxEffect;

  void Start()
  {
      xStartPos = transform.position.x;
      yStartPos = transform.position.y;
      length = GetComponent<SpriteRenderer>().bounds.size.x;
  }

  void Update()
  {
      float xDist = (cam.transform.position.x * xParallaxEffect);
      float yDist = (cam.transform.position.y * yParallaxEffect);

      transform.position = new Vector3(xStartPos + xDist, yStartPos + yDist, transform.position.z);
  }
}
