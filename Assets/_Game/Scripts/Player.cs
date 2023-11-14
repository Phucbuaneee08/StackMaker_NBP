using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject stackPrefabs;
    [SerializeField] private Stack<GameObject> stacks;
    [SerializeField] private GameObject parentStack;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator anim;

    const float STACK_HEIGHT = 0.3f;

    public Transform destination;
    private int coin;
    private string currentAnim;

    public void OnInit()
    {
        coin = 0;
        ChangeAnim("idle");
        stacks = new Stack<GameObject>();
    }
    private void AddCoin()
    {
        coin += 1;
        UIManager.Instance.SetCoin(coin);
    }

    public void AddStack()
    {
        AddCoin();
        GameObject newStack = Instantiate(stackPrefabs, transform.position + Vector3.up * (stacks.Count) * STACK_HEIGHT, stackPrefabs.transform.rotation);
        stacks.Push(newStack);
        newStack.transform.parent = parentStack.transform;
        playerSprite.transform.position += Vector3.up * STACK_HEIGHT;
    }
    public void RemoveAllStack()
    {

        UIManager.Instance.SetPoint(coin * 100);
        while (stacks.Count > 0)
        {

            Destroy(stacks.Peek());
            stacks.Pop();
        }

        StartCoroutine(Celebration());

    }

    private IEnumerator Celebration()
    {

        transform.position = destination.position;
        playerSprite.transform.position = transform.position + new Vector3(0, 0, 0);
        ChangeAnim("dancing");
        yield return new WaitForSeconds(3f);
        UIManager.Instance.OpenFinishUI();
    }

    public void RemoveStack()
    {
        Destroy(stacks.Peek());
        stacks.Pop();
        if (stacks.Count == 0)
        {
            UIManager.Instance.OpenFinishUI();
        }

        playerSprite.transform.position -= Vector3.up * STACK_HEIGHT;
      

    }
    private void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

}
