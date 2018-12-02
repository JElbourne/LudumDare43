using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

    public GameObject target;

    EntityController m_controller;
    TraitJump m_jumpTrait;
    TraitLedgeGrab m_ledgeTrait;
    TraitCrouch m_crouchTrait;
    TraitMeleeAttack m_meleeTrait;

	// Use this for initialization
	void Start () {
        m_jumpTrait = target.GetComponent<TraitJump>();
        m_crouchTrait = target.GetComponent<TraitCrouch>();
        m_ledgeTrait = target.GetComponent<TraitLedgeGrab>();
        m_meleeTrait = target.GetComponent<TraitMeleeAttack>();
        m_controller = target.GetComponent<EntityController>();
	}

    // Update is called once per frame
    void Update() {

        // Directional moving
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_controller.SetDirectionalInput(directionalInput);

        // Jumping
        if (Input.GetButtonDown("Jump")) {
            if (m_ledgeTrait && m_controller.collisions.ledgeIsGrabbed)
            {
                m_ledgeTrait.ClimbLedge();
            } else if (m_jumpTrait) {
                m_jumpTrait.OnJumpInputDown();
            }
        }

        // Early Ending Jump
        if (Input.GetButtonUp("Jump")) {
            if (m_jumpTrait) m_jumpTrait.OnJumpInputUp();
        }

        // Crouching
        if (Input.GetButtonDown("Crouch"))
        {
            if (m_crouchTrait) m_crouchTrait.OnCrouchInputDown();
        }
        if (Input.GetButtonUp("Crouch"))
        {
            if (m_crouchTrait) m_crouchTrait.OnCrouchInputUp();
        }

        // Melee Attack
        if (Input.GetButtonDown("Melee"))
        {
            if (m_meleeTrait) m_meleeTrait.OnMeleeAttackInputDown();
        }

        if (Input.GetKeyDown("x"))
        {
            SceneManager.LoadScene("PlayGame");
        }
        if (Input.GetKeyDown("m"))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown("b"))
        {
            PlayerPrefs.DeleteAll();
        }
    }

}
