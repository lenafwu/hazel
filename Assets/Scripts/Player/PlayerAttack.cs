using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool _isAttacking = false;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject hitzone;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        hitzone.SetActive(false);
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogueManager.isDialogueOpen) {
            return;
        }
        if(Input.GetButtonDown("Fire1") && !_isAttacking && playerController.isGrounded)
        {
            _isAttacking = true;
      //      hitzone.SetActive(true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayAttackAnimation();
        }

        if(!_isAttacking){
            SetAttackZoneInactive();
        }

       // _isAttacking = false;
    }

    void PlayAttackAnimation()
    {
        _anim.SetTrigger("attacked");
        playerController.PlayAttackSound();
    }

    // add to the animation event BW_attack
    public void FinishAttack()
    {
        _isAttacking = false;
        SetAttackZoneInactive();
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }

    public void SetAttackZoneActive(){
        hitzone.SetActive(true);
    }

    public void SetAttackZoneInactive(){
        hitzone.SetActive(false);
    }
}
