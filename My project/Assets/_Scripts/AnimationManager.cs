using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CamAnim();
    }

    public void CamAnim()
    {
        animator.SetBool("isWalking", InputController.Instance.movementVector.magnitude > 0.1);
    }
}
