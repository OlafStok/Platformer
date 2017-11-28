using UnityEngine;

//looks for trigger objects, and if hit, activates gothit() from the hit object if its a trigger

public class TriggerChecker : MonoBehaviour
{

    public static Renderer playerrenderer = new Renderer();
    public BoxCollider2D boxCollider;
    public float range;
    public int horizontalRayCount = 4;
    float horizontalRaySpacing;
    public RaycastOrigins raycastOrigins;
    private GameObject hitObject;
    private Color MainColor;
    public LayerMask triggerBlock;

    //raycast
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, topMiddle;
    }

    //raycast
    public GameObject RayCastObject;
    public void CheckForHit()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(0.015f * -2);

        //top
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.topMiddle = new Vector2(bounds.center.x, bounds.max.y);

        //bottom TODO
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.topMiddle = new Vector2(bounds.center.x, bounds.max.y);

        //right TODO
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.topMiddle = new Vector2(bounds.center.x, bounds.max.y);

        //left TODO
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.topMiddle = new Vector2(bounds.center.x, bounds.max.y);


        Vector3 fwd = RayCastObject.transform.TransformDirection(Vector3.up);
        Debug.DrawRay(raycastOrigins.topMiddle, fwd * range, Color.green);
        Debug.DrawRay(raycastOrigins.topLeft, fwd * range, Color.green);
        Debug.DrawRay(raycastOrigins.topRight, fwd * range, Color.green);
        RaycastHit2D hit = (Physics2D.Raycast(raycastOrigins.topMiddle, fwd, range, triggerBlock));
        RaycastHit2D hit2 = (Physics2D.Raycast(raycastOrigins.topLeft, fwd, range, triggerBlock));
        RaycastHit2D hit3 = (Physics2D.Raycast(raycastOrigins.topRight, fwd, range, triggerBlock));

        //als je raakt verandert kleur
        if (hit || hit2 || hit3)
        {
            print("hit something");
            if (hit)
            {
                hitObject = hit.transform.gameObject;
            }
            else if (hit2)
            {
                hitObject = hit2.transform.gameObject;
            }
            else
            {
                hitObject = hit3.transform.gameObject;
            }
            if (hitObject.GetComponent<Trigger>() != null)
            {
                print("hit trigger");
                hitObject.GetComponent<Trigger>().GotHit();
            }

        }

    }

    void Start()
    {
        playerrenderer = GetComponent<Renderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        CheckForHit();
    }



}
