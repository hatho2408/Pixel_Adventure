using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType 
{
    Apple,
    Banana,
    Cherry,
    Kiwi,
    Melon,
    Orange,
    Pineapple,
    Strawberry
    
}
public class Fruit_Item : MonoBehaviour
{
    public FruitType fruitType;
    [SerializeField]protected Animator anim;
    [SerializeField]protected SpriteRenderer spriteRenderer;
    [SerializeField]private Sprite[] fruitSprite;
    [SerializeField] private GameObject pickupFX;

 

    public void FruitSetup(int fruitIndex)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(fruitIndex, 1);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            PlayerManager.instance.fruits++;
            AudioManager.instance.PlaySFX(8);
            if(pickupFX!=null)
            {
                GameObject newpickupFx=Instantiate(pickupFX,transform.position,transform.rotation);
                Destroy(newpickupFx,.5f);
                
            }
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    // private void OnValidate()
    // {
    //     spriteRenderer.sprite=fruitSprite[(int)fruitType];
    // }

}
