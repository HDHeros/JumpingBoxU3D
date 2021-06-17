using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnCollisionEnter(Collision collision)
    {

            _animator.SetTrigger("collision");
        
    }
}
