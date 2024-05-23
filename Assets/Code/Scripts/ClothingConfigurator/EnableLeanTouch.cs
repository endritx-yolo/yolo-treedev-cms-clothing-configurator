using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLeanTouch : MonoBehaviour
{
   [SerializeField] private GameObject leanTouch;

   private void Start()
   {
      StartCoroutine(EnableLeanTouchStart());
   }

   private IEnumerator EnableLeanTouchStart()
   {
      yield return new WaitForSeconds(3.3f);
      leanTouch.SetActive(true); 
   }
   
}
