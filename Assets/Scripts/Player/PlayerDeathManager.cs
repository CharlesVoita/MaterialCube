using System.Collections;
using UnityEngine;


public class PlayerDeathManager : MonoBehaviour
{
    public Player player;
    public GameObject playerContainer;
    
    public GameObject spawnPoint;
    
    public BoxCollider2D collider2D;

    protected bool isSafe, canSave, isStay;

    private const int GROUND = 8;
    private const int SOLID = 10;
    
    int overlaps;
    private int unsavables;
    
    protected Vector2 targetVector;
    
    private void Start()
    {
        //throw new System.NotImplementedException();
        Debug.Log("YO!!!");
        isSafe = true;
    }

    void Update()
    {
        checkPlayer();
    }

    void checkPlayer()
    {
        if (player.isJumping) {
            canSave = true;
            return;
        }

        if (isSafe) {
            if (unsavables == 0 && canSave) {
                canSave = false;
                spawnPoint.transform.position = player.transform.position;
            }
            return;
        }

        handleDeath();

    }

    public void handleDeath()
    {
        Debug.Log("DEAD!!!");
        player.transform.position = spawnPoint.transform.position;
        var ourVector = new Vector2(spawnPoint.transform.position.x, spawnPoint.transform.position.y);
        player.shadow.transform.position = ourVector + new Vector2(0, -.5f);
        StartCoroutine(deathAnimation());
    }

    protected IEnumerator deathAnimation()
    {
        player.canAct = false;
        yield return new WaitForSeconds(.4f);
        player.canAct = true;
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (GROUND == other.gameObject.layer) {
            Debug.Log("GROUND!");
            isSafe = true;
            overlaps++;
        }
        
        if (other.gameObject.CompareTag("Unsavable")) {
            Debug.Log("Unsavable!");
            unsavables++;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (8 == other.gameObject.layer) {
            isSafe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (GROUND == other.gameObject.layer) {
            Debug.Log("LEAVE GROUND!");
            overlaps--;
            if (player.isJumping) {
                overlaps = 0;
            }
            if (overlaps == 0) {
                isSafe = false;
            }
        }
        
        if (other.gameObject.CompareTag("Unsavable")) {
            unsavables--;
        }
    }

    
}
