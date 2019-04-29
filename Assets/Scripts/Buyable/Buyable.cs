using System.Collections;
using TMPro;
using UnityEngine;


public class Buyable : MonoBehaviour
{
    public GameObject targetLocation;
    public BoxCollider2D physicalCollider;
    protected GameManager GameManager;
    protected GameObject playerObj;
    protected Player playerComp;
    
    public TextMeshPro valueText;
    
    protected const string PLAYER_BODY = "Player";

    protected BoxCollider2D collider2D;

    protected bool isActive, isBuyable;

    protected Vector2 targetVector;
    
    private void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        isActive = true;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!isBuyable || !isActive) return;
        CheckBuy();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (!isActive || PLAYER_BODY != other.gameObject.name) return;
        playerObj = other.gameObject;
        playerComp = playerObj.GetComponent<Player>();
        valueText.enabled = true;
        if (playerComp.isJumping) return;
        isBuyable = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (playerComp.isJumping) {
            isBuyable = false;
        }
        else {
            isBuyable = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        valueText.enabled = false;
        isBuyable = false;
    }


    void CheckBuy()
    {
        if (playerComp.PressedInteract()) {
            Debug.Log("BUY!!!!");
            var value = int.Parse(valueText.text);
            if (value <= GameManager.currency) {
                isActive = false;
                valueText.enabled = false;
                isBuyable = false;
                GameManager.addToCurrency(-value);
                Debug.Log("Bought");
                StartCoroutine(moveToTarget());
            } else {
                StartCoroutine(flashCost());
                StartCoroutine(GameManager.flashCost());
            }
        }
    }

    IEnumerator flashCost()
    {
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        valueText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
    }

    IEnumerator moveToTarget()
    {
        
        physicalCollider.enabled = false;
        
        float journey = 0f;
        bool isAtLocation = false;
        while (!isAtLocation && journey <= 25f) {
            var speed = 2.545f;
            journey = journey + Time.deltaTime;
            if (journey < .4f) {
                speed = 1.618f;
            }
            var currentVector = new Vector2(transform.position.x, transform.position.y);
            var targetVectorPos = (Vector2) targetLocation.transform.position;
            
            transform.position = Vector2.MoveTowards(transform.position, targetVectorPos, speed * Time.deltaTime);
            if ((currentVector - targetVectorPos).magnitude < .1) {
                Debug.Log("REACHED LOCATION!");
                isAtLocation = true;
            }
            yield return null;
        }

        transform.position = targetLocation.transform.position;
        physicalCollider.enabled = true;
    }
    
    

    

    
}
