using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.enabled = true;
    }
}
