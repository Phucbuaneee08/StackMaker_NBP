using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningWall : MonoBehaviour
{
  
    [SerializeField] private float height;
    private Player playerComponent;
    private void Start()
    {
        height = 20 * Const.STACK_HEIGHT;


    }
    private void OnTriggerEnter(Collider collision)
    {


        if (collision.CompareTag(Const.PLAYER_TAG))
        {
            playerComponent = collision.GetComponent<Player>();
            if (CompareHeight(playerComponent.GetStackHeight()))
            {
                playerComponent.RemoveAllStack();
                SoundManager.Instance.PlaySound(Sound.Lose);
            }
            else
            {
                
                StartCoroutine(PlayAgain());
            }
        }
    }
   private IEnumerator PlayAgain()
    {
        SoundManager.Instance.PlaySound(Sound.Lose);
        SoundManager.Instance.StopSound();
        UIManager.Instance.OpenLoseUI();
       
        yield return new WaitForSeconds(4f);
        LevelManager.Instance.LoadLevel();
    }
    private bool CompareHeight(float playerHeight)
    {
        return playerHeight > height;
    }
}
