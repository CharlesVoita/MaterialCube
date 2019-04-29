using System.Collections;
using TMPro;
using UnityEngine;


public class InvisFloor : MonoBehaviour
{
    protected GameObject playerObj;
    protected Player playerComp;
    public AnimationManager AnimationManager;
    public SpriteRenderer SpriteRenderer;
    
    protected const string PLAYER_BODY = "Player";

    protected BoxCollider2D collider2D;

    protected bool isActive, isDisappearing, isActivated;

    protected Vector2 targetVector;
    
    private void Start()
    {
        isActive = true;
        collider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!isActive) return;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (!isActive || isDisappearing || PLAYER_BODY != other.gameObject.name) return;
        playerObj = other.gameObject;
        playerComp = playerObj.GetComponent<Player>();
        if (playerComp.isJumping) return;
        StartCoroutine(disappear());

    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (playerComp.isJumping) {
            isActivated = false;
        }
        else if (!isDisappearing) {
            StartCoroutine(disappear());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isActivated = false;
    }



    IEnumerator disappear()
    {
        isDisappearing = true;
        AnimationManager.SetAnimationID(101);
        yield return new WaitForSeconds(.5f);
        Debug.Log("REMOVE GROUND");
        collider2D.enabled = false;
        yield return new WaitForSeconds(.2f);
        SpriteRenderer.enabled = false;
        yield return new WaitForSeconds(2f);
        StartCoroutine(appear());
        
    }
    
    IEnumerator appear()
    {
        SpriteRenderer.enabled = true;
        isDisappearing = false;
        AnimationManager.SetAnimationID(102);
        yield return new WaitForSeconds(.2f);
        AnimationManager.SetAnimationID(0);
        Debug.Log("ADD GROUND");
        collider2D.enabled = true;
        yield return new WaitForSeconds(.5f);
    }
    

    

    
}
