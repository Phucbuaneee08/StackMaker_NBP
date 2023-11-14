using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFL : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerChild;
    private float speed;
    private CameraState state;
    private void Start()
    {
        speed = 2f;
        state = CameraState.Start;
     
    }
    private void LateUpdate()
    {
        switch (state)
        {
            case CameraState.Start:
                { 
                    transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 10 + playerChild.position.y / 2, -10 - playerChild.position.y / 5), speed * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(35, 0, 0);
                }
                break;
            case CameraState.Finish:
                {
                    transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(7.5f + playerChild.position.y, 6.5f + playerChild.position.y , -6.5f- playerChild.position.y), 4 * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(20, -50, 5);
                }
                break;
        }      
            
       
    }
    public void ChangeState(CameraState state)
    {
        this.state = state;
    }


}
