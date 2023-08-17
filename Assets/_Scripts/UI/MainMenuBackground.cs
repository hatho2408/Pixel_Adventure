using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
   [SerializeField] private MeshRenderer meshRenderer;
   [SerializeField] private Vector2 backgroundSpeed;

   /// <summary>
   /// Update is called every frame, if the MonoBehaviour is enabled.
   /// </summary>
   private void Update()
   {
    meshRenderer.material.mainTextureOffset+=backgroundSpeed*Time.deltaTime;
   }

}
