using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject shadow;

    // I know this is bad practice but this is for a gamejam...
    // If you are reading this, this code is awful, just ask me questions
    // and for the love of god, this code is not worth referencing.
    // @CharlesVoita
    
    public bool isCutScene, canAct, isFixedCamera;
    protected float moveJourneyTime;
    protected float horizontal, vertical;

    protected Vector2 targetVector;
    public AnimationManager animationManager;
    public Animator animator;
    public Rigidbody2D Rigidbody2D;
    public GameObject spawnLanding;
    public GameObject introLanding;
    public GameManager GameManager;
    
    
    public float MAX_SPEED = 15f;
    public const float MAX_JUMP_SPEED = 20f;
    public const float MAX_DASH_SPEED = 30f;
    public const float COOLDOWN_SPEED = .28f;
    public const float TIME_TO_MAX_SPEED = .3f;
    public const float HALFWAY_TIME = .2f;
    public const float COOLDOWN_TIME = .1f;
    
    public const float DASH_TIME = .55f;
    public const float JUMP_TIME = .3f;
    public const float yVector = 1f;
    public const float descentDuration = .3f;
    public const float negativeVector = -.65f;
    
    public Vector2 shadowDefaultVector = new Vector2(0, -.5f);

    public ParticleSystem jumpParticle1, jumpParticle2, jumpParticle3;
    

    protected bool isCoolingDown;
    public bool isJumping;
    
    public AnimationCurve animCurve = new AnimationCurve(
        new Keyframe(1f, 1f),
        //new Keyframe(.4f, .6f),
        //new Keyframe(.6f, .8f),
        //new Keyframe(.8f, .8f),
        new Keyframe(1, 1)
    );
    
    public AnimationCurve jumpCurve = new AnimationCurve(
        new Keyframe(1f, 1f),
        //new Keyframe(.4f, .6f),
        //new Keyframe(.6f, .8f),
        //new Keyframe(.8f, .8f),
        new Keyframe(1, 1)
    );
    
    public AnimationCurve jumpArcCurve = new AnimationCurve(
        new Keyframe(1f, 1f),
        //new Keyframe(.4f, .6f),
        //new Keyframe(.6f, .8f),
        //new Keyframe(.8f, .8f),
        new Keyframe(1, 1)
    );

    private void Start()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Start Game [ ] <- a cube head");
        //canAct = true;
        isCutScene = true;
        isFixedCamera = true;
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", -1);
        StartCoroutine(hideIntroText());
        StartCoroutine(doIntro());
        StartCoroutine(doIntroSpin());
    }

    private IEnumerator hideIntroText()
    {
        yield return new WaitForSeconds(3f);
        GameManager.hideTitle();
    }

    private IEnumerator doIntroSpin()
    {
        
        yield return new WaitForSeconds(.3f);
        
        animator.SetFloat("inputX", 1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.1f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", 1);
        
        yield return new WaitForSeconds(.1f);
        
        animator.SetFloat("inputX", -1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.1f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", -1);
        
        yield return new WaitForSeconds(.1f);
        
        animator.SetFloat("inputX", 1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.1f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", 1);
        
        yield return new WaitForSeconds(.1f);
        
        animator.SetFloat("inputX", -1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.1f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", -1);
        
        
        
        
        
        yield return new WaitForSeconds(.13f);
        
        animator.SetFloat("inputX", 1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.13f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", 1);
        
        yield return new WaitForSeconds(.13f);
        
        animator.SetFloat("inputX", -1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.14f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", -1);
        
        yield return new WaitForSeconds(.15f);
        
        animator.SetFloat("inputX", 1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.17f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", 1);
        
        yield return new WaitForSeconds(.20f);
        
        animator.SetFloat("inputX", -1);
        animator.SetFloat("inputY", 0);
        
        yield return new WaitForSeconds(.25f);
        
        animator.SetFloat("inputX", 0);
        animator.SetFloat("inputY", -1);
    }

    private IEnumerator doIntro()
    {
        startEmission();
        float journey = 0f;
        bool isAtLocation = false;
        CameraContainer CameraContainer = GameObject.Find("Camera Container").GetComponent<CameraContainer>();
        while (!isAtLocation && journey <= 15f) {
            var speed = 10;
            journey = journey + Time.deltaTime;
            var currentVector = new Vector2(transform.position.x, transform.position.y);
            var introVectorPos = (Vector2) introLanding.transform.position;
            var targetVectorPos = (Vector2) spawnLanding.transform.position;
            
            transform.position = Vector2.MoveTowards(transform.position, targetVectorPos, speed * Time.deltaTime);
            
            if ((currentVector- introVectorPos).magnitude < .1) {
                Debug.Log("REACHED INTRO!");
                isFixedCamera = false;
            }
//            
//            if ((currentVector - targetVectorPos).magnitude < .1) {
//                CameraContainer.ResetOffset();
//                Debug.Log("RESET!");
//            }
            
            if ((currentVector - targetVectorPos).magnitude < .1) {
                Debug.Log("REACHED LOCATION!");
                isAtLocation = true;
            }
            yield return null;
        }
        
        
        transform.position = spawnLanding.transform.position;
        endEmission();
        animationManager.MediumShake();
        CameraContainer.ResetOffset();
        yield return new WaitForSeconds(.3f);
        isCutScene = false;
        canAct = true;
    }

    void Update()
    {
        movePlayer();
    }

    protected void movePlayer()
    {
        if (!canAct || isJumping) return;
           
        moveActingPlayer();
        handleJump();
    }
    
    protected void moveActingPlayer()
    {
        if (isJumping) return;

        // TODO Consider adding crawl to stop/slide
        if ((Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f)) {
            horizontal = 0;
            vertical = 0;
            moveJourneyTime = 0f;
            //StartCoroutine(StartCooldown());
            shadow.transform.position = (Vector2) transform.position + shadowDefaultVector;
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") != horizontal && Input.GetAxisRaw("Vertical") != vertical) {
            //StartCoroutine(StartCooldown());
        }

        moveJourneyTime+= Time.deltaTime;
        // TODO TARGET
        Vector2 targetVectorPos = Vector2.ClampMagnitude(new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            ), 1);
        targetVector = targetVectorPos;
        
        setAnimation(targetVectorPos);
        
        var ourVector = new Vector2(transform.position.x, transform.position.y);
        float percent = Mathf.Clamp01(moveJourneyTime / TIME_TO_MAX_SPEED);
        
        
        float curvePercent = animCurve.Evaluate(percent);
        //transform.position = Vector2.LerpUnclamped(transform.position, targetVectorPos, percent * (MAX_SPEED/100));

        var moveSpeed = MAX_SPEED;

        if (isCoolingDown) {
            moveSpeed = COOLDOWN_SPEED;
        }
        
        
//        transform.position = Vector2.MoveTowards(transform.position, targetVectorPos + ourVector, percent * (moveSpeed/2) * Time.deltaTime);
//        shadow.transform.position = Vector2.MoveTowards(shadow.transform.position, targetVectorPos + ourVector + shadowDefaultVector, percent* (moveSpeed/2) * Time.deltaTime);

        
        //Rigidbody2D.MovePosition(targetVectorPos + ourVector * curvePercent);
        //transform.position = Vector2.LerpUnclamped(transform.position, targetVectorPos + ourVector, curvePercent * (moveSpeed/100));   
        Rigidbody2D.MovePosition(Vector2.LerpUnclamped(transform.position, targetVectorPos + ourVector, curvePercent * (moveSpeed/100)));
        shadow.transform.position = (Vector2) transform.position + shadowDefaultVector;
        //shadow.transform.position = Vector2.LerpUnclamped(shadow.transform.position, targetVectorPos + ourVector + shadowDefaultVector, curvePercent* (moveSpeed/100));

    }

    protected void handleJump()
    {
        if (PressedJump()) {
            StartCoroutine(AnimateJump(JUMP_TIME));
            StartCoroutine(AnimateDash(DASH_TIME));
        }
    }
    
    public bool PressedJump()
    {
        return Input.GetKeyDown(KeyCode.Z) ||
               Input.GetButtonDown("A Button") ||
               Input.GetKeyDown(KeyCode.Space);
    }
    
    public bool PressedInteract()
    {
        return Input.GetKeyDown(KeyCode.X) ||
               Input.GetButtonDown("X Button") ||
               Input.GetKeyDown(KeyCode.KeypadEnter) ||
               Input.GetKeyDown(KeyCode.Return);
    }
    
    
    protected IEnumerator StartCooldown()
    {
        float coolingDown = 0f;
        isCoolingDown = true;
        while (coolingDown <= COOLDOWN_TIME)
        {
            coolingDown = coolingDown + Time.deltaTime;
            yield return null;
        }

        isCoolingDown = false;
    }
    
    protected IEnumerator AnimateDash(float duration)
    {
        float journey = 0f;
        isJumping = true;
        Vector2 origin = transform.position;
        var animDuration = duration;
        
        Debug.Log("JUMP!!");
        
        while (journey <= animDuration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            var ourVector = new Vector2(transform.position.x, transform.position.y);
            var withoutY = new Vector2(transform.position.x, shadow.transform.position.y);
            
            float curvePercent = jumpCurve.Evaluate(percent);
            
            transform.position = Vector2.LerpUnclamped(transform.position, targetVector+ourVector, curvePercent * (MAX_DASH_SPEED/100));
            shadow.transform.position = Vector2.LerpUnclamped(shadow.transform.position, targetVector + withoutY, curvePercent* (MAX_DASH_SPEED/100));
    
            yield return null;
        }

    }

    public void startEmission()
    {
        
        var emission1 = jumpParticle1.emission;
        emission1.enabled = true;
        
        var emission2 = jumpParticle2.emission;
        emission2.enabled = true;
        
        var emission3 = jumpParticle3.emission;
        emission3.enabled = true;

    }
    
    public void endEmission()
    {
        
        var emission1 = jumpParticle1.emission;
        emission1.enabled = false;
        
        var emission2 = jumpParticle2.emission;
        emission2.enabled = false;
        
        var emission3 = jumpParticle3.emission;
        emission3.enabled = false;

    }
    
    
    protected IEnumerator AnimateJump(float duration)
    {
        startEmission();
        
        float journey = 0f;
        isJumping = true;
        var animDuration = duration;
        
        Debug.Log("JUMP!!");
        var jumpVector = new Vector2(0, yVector);
        
        while (journey <= animDuration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            
            Vector2 movingPosition = transform.position;
            float curvePercent = jumpCurve.Evaluate(percent);
                        
            transform.position = Vector2.LerpUnclamped(transform.position, jumpVector+movingPosition, curvePercent* (MAX_JUMP_SPEED/100));  
            yield return null;
        }
        StartCoroutine(AnimateDescent(descentDuration));
    }
    
    protected IEnumerator AnimateDescent(float duration)
    {
        
        float journey = 0f;
        isJumping = true;
        var animDuration = duration;
        
        Debug.Log("Descent!!");
        //rigidBody2D.simulated = true;
        var xVal = new Vector2(targetVector.x * .66f, 0);
        var jumpVector = new Vector2(xVal.x, negativeVector);
        
        while (journey <= animDuration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            
            Vector2 movingPosition = transform.position;
            float curvePercent = jumpArcCurve.Evaluate(percent);
            
            var withoutY = new Vector2(transform.position.x, shadow.transform.position.y);
                       
            transform.position = Vector2.LerpUnclamped(transform.position, jumpVector+movingPosition, curvePercent* (MAX_JUMP_SPEED/100));
            shadow.transform.position = Vector2.LerpUnclamped(shadow.transform.position, xVal + withoutY, curvePercent* (MAX_JUMP_SPEED/100));

            yield return null;
        }

        shadow.transform.position = (Vector2) transform.position + shadowDefaultVector;
        isJumping = false;
        animationManager.MiniShake();
        endEmission();

        // TODO Screen shake!
    }
    
    protected float SignNumber(float number) {
        return number < 0 ? -1 : (number > 0 ? 1 : 0);;
    }
    
    protected void setAnimation(Vector2 movementVector)
    {
        if (movementVector != Vector2.zero)
        {
            //animator.SetBool("isWalking", true);
            float x = SignNumber(movementVector.x);
            float y = SignNumber(movementVector.y);
            animator.SetFloat("inputX", x);
            animator.SetFloat("inputY", y);
        }
        else
        {
            //animator.SetBool("isWalking", false);
        }
    }
}
