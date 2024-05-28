using System.Collections;
using UnityEngine;

public class Anim_Test_Bow : MonoBehaviour
{
    public Animator animator;

    float walkSpeed = 40f;
    float horizMove = 0f;
    bool isDragging = false;
    bool hasReleased = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizMove));

        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnEndDrag();
        }
    }

    private void OnPointerDown()
    {
        isDragging = true;
        hasReleased = false;
        animator.SetBool("isAttack1", true);
        Debug.Log("OnPointerDown");
    }

    private void OnDrag()
    {
        if (!isDragging)
        {
           
            animator.SetBool("isAttack1", true);
            isDragging = true;
        }
        Debug.Log("OnDrag");
    }

    private void OnEndDrag()
    {
        if (!hasReleased)
        {
            animator.SetBool("isAttack1", false);
            animator.SetBool("isAttack2", true);
            hasReleased = true;
            StartCoroutine (onEndDrag());

        }
       
        Debug.Log("OnEndDrag");
    }

    IEnumerator onEndDrag()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isAttack2", false);
    }

    IEnumerator onEndDrag1()
    {
        yield return new WaitForSeconds(0.1f);
       
    }
}
