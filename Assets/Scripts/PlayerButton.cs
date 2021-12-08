using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
 {
     // Define a property so that other classes can know whether the button is pressed
     public bool IsPressed
     {
         get ;
         private set ;
     }

     public void OnPointerDown(PointerEventData eventData)
     {
         IsPressed = true;
     }

     public void OnPointerUp(PointerEventData eventData)
     {
         IsPressed = false;
     }

     public void OnPointerExit(PointerEventData eventData)
     {
         IsPressed = false;
     }
 }
