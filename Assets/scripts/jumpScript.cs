/*
It is  a script which stores state whether a button is pressed or not.We have to drag and drop this script
in a button and we will use events to store the state and we can access this script variable anywhere.
The button has to be static.It is used for tracking whether user pressed jump button or not
*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class jumpScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public static bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}