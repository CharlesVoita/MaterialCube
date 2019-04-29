using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
    public float MoveSpeed = 10f; // How quickly the camera should move from point A to B.
    public float SnapDistance = 0.25f; // How far from the new position we should be before snapping to it.
    public Transform MainAxis; // Axis that moves the camera
    public Transform ShakeAxis; // Axis that shakes the camera

    // For moving camera
    public bool IsMoving { get; private set; }
    private Vector3 _newPosition;
    private float _currentMoveSpeed;

    // For shaking camera
    private bool _isShaking = false;
    private bool _isIntroShake = false;
    private int _shakeCount;
    private float _shakeIntensity, _shakeSpeed, _baseX, _baseY;
    private Vector3 _nextShakePosition;

	void Start () 
    {
        //enabled = false;

        // Set up base positions, these are used for shaking to determine where to return to after a shake.
        _baseX = ShakeAxis.localPosition.x;
        _baseY = ShakeAxis.localPosition.y;
	}

    public bool isShaking()
    {
        if (_isIntroShake) {
            return false;
        }
        return _isShaking;
    }

    public void setIntroShake(bool isIntroShake)
    {
        _isIntroShake = isIntroShake;
    }
	
	
	void Update () 
    {
        // Are we moving?
        if (IsMoving)
        {
            // Move us toward the new position
            Debug.Log("MOVING!");
            MainAxis.position = Vector3.MoveTowards(MainAxis.position, _newPosition, Time.deltaTime * _currentMoveSpeed);

            // Determine if we are there or not (within snap distance)
            if (Vector2.Distance(MainAxis.position, _newPosition) < SnapDistance)
            {
                MainAxis.position = _newPosition;
                IsMoving = false;
                if(!_isShaking) enabled = false;
            }
        }
        // ...or are we shaking? (Could be both)
        if (_isShaking)
        {
            // Move toward the previously determined next shake position
            ShakeAxis.localPosition = Vector3.MoveTowards(ShakeAxis.localPosition, _nextShakePosition, Time.deltaTime * _shakeSpeed);

            // Determine if we are there or not
            if (Vector2.Distance(ShakeAxis.localPosition, _nextShakePosition) < _shakeIntensity / 5f)
            {
                //Decrement shake counter
                _shakeCount--;

                // If we are done shaking, turn this off if we're not longer moving
                if (_shakeCount <= 0)
                {
                    _isShaking = false;
                    ShakeAxis.localPosition = new Vector3(_baseX, _baseY, ShakeAxis.localPosition.z);
                    if (!IsMoving) enabled = false;
                }
                // If there is only 1 shake left, return back to base
                else if(_shakeCount <= 1)
                {
                    _nextShakePosition = new Vector3(_baseX, _baseY, ShakeAxis.localPosition.z);
                }
                // If we are not done or nearing done, determine the next position to travel to
                else
                {
                    DetermineNextShakePosition();
                }
            }
        }
	}

    public void Move(float x, float y, float speed = 0)
    {
        Debug.Log("MOVE..");
        // If a speed is passed in, use that. Otherwise use the default.
        if (speed > 0) _currentMoveSpeed = speed;
        else _currentMoveSpeed = MoveSpeed;

        // Set us up to move
        _newPosition = new Vector2(transform.position.x + x, transform.position.y + y);
        IsMoving = true;
        enabled = true;
    }


    public void SetPosition(Vector2 position)
    {
        Vector3 newPosition = new Vector3(position.x, position.y, MainAxis.position.z);
        MainAxis.position = newPosition;
    }

    public void Shake(float intensity, int shakes, float speed)
    {
        enabled = true;
        _isShaking = true;
        _shakeCount = shakes;
        _shakeIntensity = intensity;
        _shakeSpeed = speed;

        DetermineNextShakePosition();
    }


    private void DetermineNextShakePosition()
    {
        _nextShakePosition = new Vector3(Random.Range(-_shakeIntensity, _shakeIntensity),
            Random.Range(-_shakeIntensity, _shakeIntensity),
            ShakeAxis.localPosition.z);
    }
}