using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFL : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerChild;

    private float speedX;
    private float speedY;
    private CameraState state;
    //offset of camera for following player from behind 
    private Vector3 cameraFollowBehind;
    //offset of camera for following player in the right of Player
    private Vector3 cameraFollowRight;
    private void Start()
    {
        speedX = 2f;
        speedY = 4f;
        state = CameraState.Start;
    }
    private void LateUpdate()
    { 
        cameraFollowBehind = new Vector3(0, 10 + playerChild.position.y / 2, -10 - playerChild.position.y / 5);
        cameraFollowRight =  new Vector3(4f + playerChild.position.y, 4f + playerChild.position.y, -4f - playerChild.position.y);


        switch (state)
        {
            case CameraState.Start:
                { 
                    transform.position = Vector3.Lerp(transform.position, player.position + cameraFollowBehind, speedX * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(35, 0, 0);
                }
                break;
            case CameraState.Finish:
                {
                    transform.position = Vector3.Lerp(transform.position, player.position + cameraFollowRight, speedY * Time.deltaTime);
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
