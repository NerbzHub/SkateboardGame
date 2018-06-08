using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------------------------------------------------------
// Class:
//      PlayerController: Player controller controls the player's movment and most
//                        of the interactions that the player has in the world.
//-------------------------------------------------------------------------------------
public class PlayerController : MonoBehaviour
{
    //-------------------------------------------------------------------------------------
    // GameObjects in relation to the Camera.
    //
    // Gameobject to parent the camera's position.
    public GameObject cameraHolder;
    // Gameobject for where the camera's position is when crouching.
    //public GameObject crouchCameraHolder;
    // Gameobject for where the camera's position is when standing.
    //public GameObject standCameraHolder;
    //-------------------------------------------------------------------------------------

    //-------------------------------------------------------------------------------------
    // Projectiles
    //
    // The projectile that gets shot from the revolver.
    //public GameObject m_Projectile;
    // The array that stores the 6 bullets.
    //public GameObject[] ProjectileArray;
    // Check whether the player is currently reloading.
    //public bool reloading = false;
    //-------------------------------------------------------------------------------------

    // This text shows when the player walks near the crouch bench.
    //public GameObject CrouchText;
    // The collider that gets turned into a trigger when crouching.
    //public Collider m_headCollider;
    // Collider that checks whether the player is on the ground or not.
    public Collider m_groundCollider;
    // The cloth for the target flags.
    //public Cloth targetCloth;

    // Player's rigidbody.
    public Rigidbody m_rigidBody;

    // Number of bullets available for the player to shoot on revolver.
    //[HideInInspector]
    //public int bulletCounter = 0;
    // Whether the player is under something or not.
    //private bool m_canStand = true;
    // Whether the player is on the ground or not.
    public bool m_grounded = false;
    // Whether the player is jumping or not.
    private bool m_jumping = false;
    // A useful tool for a bool to occur once.
    //private bool bOnce = true;
    // The different axes of the mouse's position.
    //public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    //// The rotational axes for mouse and y.
    //public RotationAxes axes = RotationAxes.MouseXAndY;
    //// Sensitivity of mouse movement.
    //public float sensitivityX = 15F;
    //public float sensitivityY = 15F;
    ////Clamping of mouse viewing.
    //public float minimumX = -360F;
    //public float maximumX = 360F;
    //public float minimumY = -60F;
    //public float maximumY = 60F;
    //// Rotational on the Y axis
    //float rotationY = 0F;

    // Rate that the player moves at.
    public float moveSpeed;
    // Rate that the player turns.
    public float turnSpeed;
    // The maximum movement speed the player can go at.
    public float maxMoveSpeed = 25.0f;

    // Allocating a weaponstate to current weapon.
    //private WeaponStates.enumWeaponStates currentWeapon;

    // Use this for initialization
    void Start()
    {
        // If there is a gameobject with a rigidbody, freeze its' rotation.
        //if (gameObject.GetComponent<Rigidbody>())
        //    gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        // Set the current state to revolver.
        //gameObject.GetComponent<WeaponStates>().currentState = WeaponStates.enumWeaponStates.Revolver;
        // Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        // Cursor is invisible in inspector.
        //Cursor.visible = false;
    }

    //-------------------------------------------------------------------------------------
    // Update is called once per frame.
    //-------------------------------------------------------------------------------------
    void Update()
    {
        ////-------------------------------------------------------------------------------------
        //// Mouse look at.
        ////-------------------------------------------------------------------------------------
        //if (axes == RotationAxes.MouseXAndY)
        //{
        //    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        //    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        //}
        //else if (axes == RotationAxes.MouseX)
        //{
        //    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        //}
        //else
        //{
        //    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //    transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        //}

        // If you have more than the bullet counter then you're out of ammo.
        //if (bulletCounter > 6)
        //{
        //    for (int i = 1; i < bulletCounter; i++)
        //    {
        //        targetCloth.capsuleColliders[i] = null;
        //    }

        //    bulletCounter = 1;
        //}

        //// When R is pressed, reload the revolver to full ammo.
        //if (Input.GetKey(KeyCode.R) && !reloading)
        //{
        //    reloading = true;
        //    // Run this function in 1.5 seconds on a seperate thread.
        //    Invoke("Reload", 1.5f);

        //}

        ////-------------------------------------------------------------------------------------------
        ////                                          Mouse
        ////
        //// This occurrs when the left click is pressed.
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Ray cast at the window's centre.
        //    Ray raycast = Camera.main.ScreenPointToRay(new Vector3(Camera.main.scaledPixelWidth * 0.5f, Camera.main.scaledPixelHeight * 0.5f, 0));
        //    // Info of raycast.
        //    RaycastHit hitInfo;
        //    // Create a new layermask for ragdoll and cloth.
        //    LayerMask layerMaskRagdoll =
        //        ~(LayerMask.NameToLayer("Ragdoll"));

        //    LayerMask layerMaskCloth =
        //       // targets/ragdolls
        //       ~(LayerMask.NameToLayer("Cloth"));

        //    //-------------------------------------------------------------------------------------
        //    // Shoot
        //    //-------------------------------------------------------------------------------------
        //    if (Physics.Raycast(raycast, out hitInfo, 100.0f, layerMaskRagdoll.value))
        //    {
        //        // When the animated character is clicked on, it turns into a ragdoll.
        //        Ragdoll ragdoll = hitInfo.transform.gameObject.GetComponentInParent<Ragdoll>();
        //        if (ragdoll != null)
        //            ragdoll.RagdollOn = true;
        //    }

        //    //-------------------------------------------------------------------------------------
        //    // If the raycast hits a cloth, shoot a bullet at the cloth.
        //    //-------------------------------------------------------------------------------------
        //    if (Physics.Raycast(raycast, out hitInfo, 100.0f, layerMaskCloth.value))
        //    {
        //        Transform newTrans = gameObject.transform;
        //        Vector3 newVec3 = newTrans.position;
        //        newVec3 += transform.forward;
        //        // 
        //        ProjectileArray[bulletCounter].transform.position =
        //        gameObject.GetComponent<WeaponStates>().m_GOWeaponPos.transform.position + gameObject.GetComponent<WeaponStates>().m_GOWeaponPos.transform.forward;

        //        ProjectileArray[bulletCounter].GetComponent<Rigidbody>().isKinematic = true;
        //        ProjectileArray[bulletCounter].GetComponent<Rigidbody>().isKinematic = false;
        //        ProjectileArray[bulletCounter].GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
        //        ++bulletCounter;
        //    }

        //}
        ////-------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------
        //                                   Keyboard Input Movement
        //
        // Forwards
        if (Input.GetKey(KeyCode.W))
        {
            m_rigidBody.AddForce(new Vector3(transform.forward.x * moveSpeed, 0, transform.forward.z * moveSpeed), ForceMode.Acceleration);
        }

        // Backwards
        if (Input.GetKey(KeyCode.S))
        {
            m_rigidBody.AddForce(new Vector3(-transform.forward.x * moveSpeed, 0, -transform.forward.z * moveSpeed), ForceMode.Acceleration);
        }

        // Right
        if (Input.GetKey(KeyCode.D))
        {
            m_rigidBody.AddTorque(transform.up * turnSpeed);
            //m_rigidBody.AddForce(new Vector3(transform.right.x * moveSpeed, 0, transform.right.z * moveSpeed), ForceMode.Acceleration);
        }

        // Left
        if (Input.GetKey(KeyCode.A))
        {
            m_rigidBody.AddTorque(-transform.up * turnSpeed);

            //m_rigidBody.AddForce(new Vector3(-transform.right.x * moveSpeed, 0, -transform.right.z * moveSpeed), ForceMode.Acceleration);
        }

        // Movement Cap
        if (m_rigidBody.velocity.magnitude > maxMoveSpeed && m_grounded || m_rigidBody.velocity.magnitude > maxMoveSpeed && m_jumping)
        {
            float f = m_rigidBody.velocity.magnitude - maxMoveSpeed;
            m_rigidBody.AddForce(m_rigidBody.velocity * -f);
        }


        //-------------------------------------------------------------------------------------------
        //                                          Boost
        //
        if ((Input.GetKey(KeyCode.LeftShift)))
        {
            m_rigidBody.AddForce(new Vector3(transform.forward.x * moveSpeed, 0, transform.forward.z * moveSpeed), ForceMode.Acceleration);
        }

        //-------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------
        //                                          Jump
        //
        if ((Input.GetKeyDown(KeyCode.Space) && m_grounded && !m_jumping))
        {
            m_rigidBody.AddForce(Vector3.up * 20, ForceMode.Impulse);
            m_jumping = true;
            Invoke("Jumped", 2.0f);
        }
        //-------------------------------------------------------------------------------------------

        ////-------------------------------------------------------------------------------------------
        ////                                      Toggle Weapon
        ////
        //// Toggles through weapons when scrolling up.
        //if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        //{
        //    // This scrolls through all of the weapons with the scrollwheel up.
        //    switch (gameObject.GetComponent<WeaponStates>().currentState)
        //    {
        //        case WeaponStates.enumWeaponStates.Revolver:
        //            gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.FluidGun);
        //            // revolver model visible
        //            break;

        //        case WeaponStates.enumWeaponStates.FluidGun:
        //            //fluidgun
        //            gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.WaterJetpack);
        //            break;

        //        case WeaponStates.enumWeaponStates.WaterJetpack:
        //            //WaterJetpack
        //            gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.Revolver);
        //            break;
        //        default:
        //            break;
        //    }
        //    // Turn off bOnce
        //    //bOnce = false;
        //    //Invoke("TimedbOnce", 0.0f);
        //}

        //// Toggle through weapons by pressing U.
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    switch (gameObject.GetComponent<WeaponStates>().currentState)
        //    {
        //        case WeaponStates.enumWeaponStates.Revolver:
        //            gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.FluidGun);
        //            // revolver model visible
        //            break;

        //        case WeaponStates.enumWeaponStates.FluidGun:
        //            //fluidgun
        //            gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.WaterJetpack);
        //            break;

        //        case WeaponStates.enumWeaponStates.WaterJetpack:
        //            //WaterJetpack
        //            gameObject.GetComponent<WeaponStates>().setState(WeaponStates.enumWeaponStates.Revolver);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //    //-------------------------------------------------------------------------------------------
        //    //                                         Crouch
        //    //
        //    // When left control is pressed, set the head collider to trigger, allowing things to go 
        //    //  through it.
        //    if (Input.GetKeyDown(KeyCode.LeftControl))
        //    {
        //        m_headCollider.isTrigger = true;
        //        cameraHolder.transform.position = crouchCameraHolder.transform.position;
        //    }

        //    //-------------------------------------------------------------------------------------------
        //    // When the left ctrl is pressed, sets everything back to how it should be.
        //    if (Input.GetKeyUp(KeyCode.LeftControl))
        //    {
        //        if (m_canStand)
        //        {
        //            cameraHolder.transform.position = standCameraHolder.transform.position;
        //            m_headCollider.isTrigger = false;
        //        }
        //    }
        //    //-------------------------------------------------------------------------------------------
        //}
    }
    //-------------------------------------------------------------------------------------------
    // This occurrs when the player enters a trigger box.
    //-------------------------------------------------------------------------------------------
    

    //    // Brings up the couch tip.
    //    if (other.tag == "CrouchTip")
    //    {
    //        CrouchText.SetActive(true);
    //    }
    //}

    //-------------------------------------------------------------------------------------------
    // This occurrs when the player exits the trigger box.
    //-------------------------------------------------------------------------------------------
    

    //    // Sets the writing for the crouchTip to false.
    //    if (other.tag == "CrouchTip")
    //    {
    //        CrouchText.SetActive(false);
    //    }


    //-------------------------------------------------------------------------------------------
    // bOnce used in various places with invoke.
    //-------------------------------------------------------------------------------------------
    //private void TimedbOnce()
    //{
    //bOnce = true;
    //}
    //-------------------------------------------------------------------------------------------
    // Reload the revolver.
    //-------------------------------------------------------------------------------------------
    //private void Reload()
    //{
    //    bulletCounter = 0;
    //    reloading = false;
    //}

    //-------------------------------------------------------------------------------------------
    // Setting jumping to false after a timed amount.
    //-------------------------------------------------------------------------------------------
    private void Jumped()
    {
        m_jumping = false;
    }

}