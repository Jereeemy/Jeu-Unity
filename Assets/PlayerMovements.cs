﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float sensitivityX = 4f;
    [SerializeField]
    private float sensitivityY = 4f;
    [SerializeField]
    private Camera playerCam;
    [SerializeField]
    private float camRotationLimit = 30f;
    [SerializeField]
    private Vector3 jumpSpeed = new Vector3(0, 5, 0);

    private float currentCamRotationX = 0f;
    private Rigidbody rb;

    /*public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    bool isGrounded;*/
    Vector3 volocity;
    CharacterController controler;
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//pour bloquer la souris
        rb = GetComponent<Rigidbody>();
        controler = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    //si player au sol
    /*bool Igrounded()
    {
        return Physics.CheckCapsule(playerCapsule.bounds.center, new Vector3(playerCapsule.bounds.center.x, playerCapsule.bounds.min.y - 0.1f, playerCapsule.bounds.center.z), 0.33572f);
    }*/

    // Update is called once per frame
    void Update()
    {
        //déplacements du joueur
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float xAxisMovement = Input.GetAxisRaw("Horizontal");//retourne une valeur entre -1(Q) et 1(D)
        float zAxisMovement = Input.GetAxisRaw("Vertical");//retourne une valeur entre -1(S) et 1(Z)

        Vector3 movementX = transform.right * xAxisMovement;//right ou left ne change rien, c'est pour indiquer l'axe
        Vector3 movementZ = transform.forward * zAxisMovement;//*-1 on recule et *1 on avance

        //on met ces 2 vercteurs dans un seul
        Vector3 playerVelocity = (movementX + movementZ).normalized * speed;//normalize = mettre vecteur à 1


        //faire se déplacer le joueur
        if (playerVelocity != Vector3.zero)//si le vecteur de déplacement n'est pas nul
        {
            rb.MovePosition(rb.position + playerVelocity * Time.deltaTime);
        }

        //rotation du joueur

        float yAxisRotation = Input.GetAxisRaw("Mouse X");//quand on bouge sur l'axe X, la rotation s'effectu sur l'axe Y
        Vector3 playerRotation = new Vector3(0, yAxisRotation, 0) * sensitivityX;
        //faire la rotation du joueur
        rb.MoveRotation(rb.rotation * Quaternion.Euler(playerRotation));//MoveRotation veux  des quaternions, on doit convertir notre Vecto3 en Quaternion

        //rottation de la caméra (on ne peux pas faire pivoter le joueur de haut en bas)

        float xAxisRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xAxisRotation * sensitivityY;

        currentCamRotationX -= cameraRotationX;
        currentCamRotationX = Mathf.Clamp(currentCamRotationX, -camRotationLimit, camRotationLimit);//clamp bloque une valeur entre un min et un max

        //faire la rotation de la camera
        playerCam.transform.localEulerAngles = new Vector3(currentCamRotationX, 0f, 0f);

        //sprinter
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetKey(KeyCode.LeftShift))
            speed = 10;
        else
            speed = 4;


        /*if (isGrounded)
            Debug.Log("oui");
        else
            Debug.Log("non");
        //sauter
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//si on appuye sur space et que le perso est au sol
        {
            playerAnimator.SetBool("jump", true);
            rb.velocity = jumpSpeed;
            //Debug.Log("oui");     
        }
        else
            playerAnimator.SetBool("jump", false);*/
        /*if (controler.isGrounded)
            Debug.Log("auSol");
        else
            Debug.Log("nonnnnn");*/
    }
}
