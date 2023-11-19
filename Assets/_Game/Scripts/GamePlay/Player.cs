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
    [SerializeField] private PointText pointTextPrefab;
    [SerializeField] private Transform pointSpawnPosition;
    [SerializeField] private Animator anim;


    public Transform destination;
    private int coin;
    private string currentAnim;
    private void Awake()
    {
        stacks = new Stack<GameObject>();
    }

    public void OnInit()
    {
        coin = 0;
        ChangeAnim(Const.ANIM_IDLE);
        RemoveAllStack();

    }
    private void AddCoin()
    {
        coin += 1;
        UIManager.Instance.SetCoin(coin);
    }
    public float GetStackHeight()
    {
        return stacks.Count * Const.STACK_HEIGHT;
    }
    public void AddStack()
    {
        AddCoin();
        GameObject newStack = Instantiate(stackPrefabs, transform.position + Vector3.up * (stacks.Count) * Const.STACK_HEIGHT, stackPrefabs.transform.rotation);
        stacks.Push(newStack);
        newStack.transform.parent = parentStack.transform;
        playerSprite.transform.position += Vector3.up * Const.STACK_HEIGHT;


        Instantiate(pointTextPrefab, pointSpawnPosition.position + pointSpawnPosition.up, Quaternion.identity).OnInit(100);
    }
    public void RemoveAllStack()
    {
        while (stacks.Count > 0)
        {
            Destroy(stacks.Peek());
            stacks.Pop();
        }
        playerSprite.transform.position = transform.position + new Vector3(0, 0, 0);
    }
    public void EndLevel()
    {
        UIManager.Instance.SetPoint(coin * 100);
        StartCoroutine(Celebration());
    }

    private IEnumerator Celebration()
    {

        transform.position = destination.position;
        ChangeAnim(Const.ANIM_DANCE);
        SoundManager.Instance.StopSound();
        SoundManager.Instance.PlaySound(Sound.Win);
        yield return new WaitForSeconds(3f);
        LevelManager.Instance.OnFinish();
    }

    public void RemoveStack()
    {
        if (stacks.Count == 0 && GameManager.Instance.IsState(GameState.GamePlay))
        {
            SoundManager.Instance.PlaySound(Sound.Win);
            UIManager.Instance.SetPoint(coin * 100);
            LevelManager.Instance.OnFinish();
        }
        else
        {
            Destroy(stacks.Peek());
            stacks.Pop();
        }

        playerSprite.transform.position -= Vector3.up * Const.STACK_HEIGHT;


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
