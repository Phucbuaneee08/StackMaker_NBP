using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStack : MonoBehaviour
{
    [SerializeField] private GameObject childStack;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Camera.main.GetComponent<CameraFL>().ChangeState(CameraState.Finish);
            collision.GetComponent<Player>().RemoveStack();
            childStack.SetActive(true);
        }
    }
}
