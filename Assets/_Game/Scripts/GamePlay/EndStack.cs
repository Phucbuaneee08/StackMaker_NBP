using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStack : MonoBehaviour
{
    private Player playerComponent;
    private Control controlComponent;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(Const.PLAYER_TAG))
        {
            if(playerComponent == null) {
                playerComponent = collision.GetComponent<Player>();
            }
            playerComponent.RemoveAllStack();
            playerComponent.EndLevel();
            if (controlComponent == null)
            {
                controlComponent = collision.GetComponent<Control>();
            }
            controlComponent.OnInit();
            GameManager.Instance.ChangeState(GameState.Finish);

        }
    }
}
