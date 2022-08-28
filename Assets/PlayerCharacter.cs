using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacter : MonoBehaviour
{
    #region Variables
    private CharacterController controller;
    [SerializeField]
    private LayerMask groundLayerMask;

    private NavMeshAgent agent;
    private Camera camera;

    [SerializeField]
    public Animator animator;

    readonly int moveHash = Animator.StringToHash("Move");
    readonly int fallingHash = Animator.StringToHash("Falling");
    #endregion Variables
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Process mouse left button input
        if(Input.GetMouseButtonDown(0))
        {
            // Make ray from screen to world
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            // Check hit from ray
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, groundLayerMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Move our character to what we hit
                agent.SetDestination(hit.point);
                Debug.Log(agent.remainingDistance);
            }
        }

        if(agent.remainingDistance > agent.stoppingDistance)
        {
            controller.Move(agent.velocity * Time.deltaTime);
            animator.SetBool(moveHash, true);
        }
        else
        {
            controller.Move(Vector3.zero);
            animator.SetBool(moveHash, false);
        }
    }
}
