using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStack : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().RemoveAllStack();
            collision.GetComponent<Control>().OnInit();
            GameManager.Instance.ChangeState(GameState.Finish);

        }
    }
}
