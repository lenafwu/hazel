using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    Transform player;
    public Rigidbody2D rb;
    public float speed = 2.5f;
    public float attackRange = 3f;
    Boss boss;


    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponentInParent<Rigidbody2D>();
       boss = animator.GetComponentInParent<Boss>();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       boss.LookAtPlayer();
       Vector2 target = new Vector2(player.position.x, rb.position.y);
       Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
       rb.MovePosition(newPos);

       // Manually calculate the velocity
      Vector2 velocity = (newPos - rb.position) / Time.fixedDeltaTime;

       animator.SetFloat("xVelocity", Mathf.Abs(velocity.x));
       
       if(Vector2.Distance(player.position, rb.position) <= attackRange){
           animator.SetTrigger("Attack");
       }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");

    }

}
