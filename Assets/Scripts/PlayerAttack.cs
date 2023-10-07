using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _isAttacking = false;
    [SerializeField] private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !_isAttacking)
        {
            _isAttacking = true;
            PlayAttackAnimation();
        }

        _isAttacking = false;
    }

    void PlayAttackAnimation()
    {
        _anim.SetTrigger("attacked");
    }
}
