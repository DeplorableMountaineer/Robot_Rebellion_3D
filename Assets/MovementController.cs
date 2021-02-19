using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Control the motion of the character taking input from keyboard and applying
/// motion through the Character Controller component
/// </summary>
public class MovementController : MonoBehaviour {
    //We adopt the usual convention that one Unity unit corresponds to one real-world meter.
    //The physics engine assumes that anyway.
    [Tooltip("speed in meters per second")] [SerializeField]
    private float walkSpeed = 6;

    /// <summary>
    /// The Character Controller component through which we apply motion
    /// </summary>
    private CharacterController _characterController;

    private void Awake(){
        //Get the Character Controller component on this game object, and
        //make sure there is one
        _characterController = GetComponent<CharacterController>();
        Assert.IsNotNull(_characterController, "Missing Character Controller component");
    }

    private void FixedUpdate(){
        Vector3 movementInput = GetMovementInput();
        //Use walkSpeed parameter to convert intended motion to a velocity
        //we normalize the movement input to prevent diagonal motion
        //from being sqrt(2) times the speed of forward/sideways motion
        //Some early video games had that bug!!!
        Vector3 velocity = walkSpeed*movementInput.normalized;
        //Apply the velocity.  SimpleMove also automatically applies gravity.
        _characterController.SimpleMove(velocity);
    }

    /// <summary>
    /// Convert WASD keypresses to intended motion
    /// </summary>
    /// <returns>The intended motion vector</returns>
    private Vector3 GetMovementInput(){
        Vector3 result = Vector3.zero;
        if(Input.GetKey(KeyCode.W)) result.z += 1; //forward
        if(Input.GetKey(KeyCode.S)) result.z -= 1; //backward
        if(Input.GetKey(KeyCode.A)) result.x -= 1; //left
        if(Input.GetKey(KeyCode.D)) result.x += 1; //right
        return result;
    }
}