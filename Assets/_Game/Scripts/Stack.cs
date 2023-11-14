using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField]private GameObject childObject;
  

    private bool isActive;
    private void Start()
    {
        isActive = true;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        
        if(collision.tag == "Player" && isActive)
        {
            isActive = false;
            childObject.SetActive(false);
            collision.GetComponent<Player>().AddStack();
        
        }
    }
}
