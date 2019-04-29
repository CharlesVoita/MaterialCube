using System.Collections;
using UnityEngine;


public class CubeBoyManager : MonoBehaviour
{
    public GameObject CubeBoy;
    protected GameManager GameManager;
    
    protected GameObject playerObj;
    protected Player playerComp;
    protected AnimationManager cubeAnimationManager;
    protected SpriteRenderer SpriteRenderer;
    private AudioSource source;
    public AudioClip cubeSound;

    protected const string PLAYER_BODY = "Player";

    protected BoxCollider2D collider2D;

    protected bool isActive;

    protected Vector2 targetVector;
    
    private void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        cubeAnimationManager = CubeBoy.GetComponent<AnimationManager>();
        SpriteRenderer = CubeBoy.GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        isActive = true;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        //checkPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (!isActive || PLAYER_BODY != other.gameObject.name) return;
        playerObj = other.gameObject;
        playerComp = playerObj.GetComponent<Player>();
        if (playerComp.isJumping) return;
        isActive = false;
        StartCoroutine(AnimateStuff());
        StartCoroutine(BrainShaker());
        
    }

    private IEnumerator BrainShaker()
    {
        yield return new WaitForSeconds(1.8f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(2.9f);
        
        
        playerComp.animationManager.MiniShake();
    }
    
    private IEnumerator DeathShaker()
    {
        playerComp.animationManager.MiniShake();
        yield return new WaitForSeconds(.1f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.3f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.1f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.1f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.1f);
        playerComp.animationManager.BrainShake();
        
        
        
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        yield return new WaitForSeconds(.2f);
        playerComp.animationManager.BrainShake();
        GameManager.endGame();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }

    private IEnumerator AnimateStuff()
    {
        source.PlayOneShot(cubeSound);
        playerComp.canAct = false;
        var vectorOffset = new Vector2(0, .5f);
        var pos = (Vector2) transform.position;
        playerObj.transform.position = pos  + vectorOffset;
        playerComp.shadow.transform.position = pos+ new Vector2(0, 0f);
        yield return new WaitForSeconds(1f);
        playerObj.transform.position = pos  + vectorOffset;
        playerComp.shadow.transform.position = pos+ new Vector2(0, 0f);
        playerComp.animator.SetFloat("inputX", 1);
        playerComp.animator.SetFloat("inputY", 0);
        playerComp.animationManager.SetAnimationID(101);
        cubeAnimationManager.SetAnimationID(101);
        yield return new WaitForSeconds(2f);
        GameManager.addToCurrency(500);
        GameManager.addToHealth(-100);
        yield return new WaitForSeconds(1.4f);
        
        playerComp.animationManager.SetAnimationID(104);
        cubeAnimationManager.SetAnimationID(104);
        yield return new WaitForSeconds(2f);
        
        
        var isDead = GameManager.checkForTrueDeath();

        if (!isDead) {
            playerComp.canAct = true;
        } else {
            StartCoroutine(DeathShaker());
            playerComp.animationManager.SetAnimationID(201);
        }
        
        
        isActive = false;
        
        float journey = 0f;
        while (journey <= 10f) {
            var speed = 2.545f;
            journey = journey + Time.deltaTime;
            if (journey < .4f) {
                speed = 1.618f;
            }
            var currentVector = new Vector2(CubeBoy.transform.position.x, CubeBoy.transform.position.y);
            var targetVectorPos = new Vector2(0, 1);
            
            CubeBoy.transform.position = Vector2.MoveTowards(CubeBoy.transform.position, targetVectorPos + currentVector, speed * Time.deltaTime);
            yield return null;
        }
        SpriteRenderer.enabled = false;
        
    }

    
}
