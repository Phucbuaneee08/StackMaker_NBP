using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointText : MonoBehaviour
{
    [SerializeField] private Text pointText;
    // Start is called before the first frame update
    public void OnInit(int point)
    {
        pointText.text = "+" + point.ToString();
        StartCoroutine(OnDespawn());
    }
    private IEnumerator OnDespawn()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    } 
}
