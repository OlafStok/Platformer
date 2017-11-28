using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerChecker : MonoBehaviour
{

    public static Renderer playerrenderer = new Renderer();
    public BoxCollider2D boxCollider;
    public LayerMask switchblock;
    public float range;
    public int horizontalRayCount = 4;
    float horizontalRaySpacing;
    public RaycastOrigins raycastOrigins;
    private GameObject hitObject;
    private Color MainColor;

    //raycast
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, topMiddle;
        public Vector2 botLeft, botRight, botMiddle;
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
        raycastOrigins.botLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.botRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.botMiddle = new Vector2(bounds.center.x, bounds.min.y);

        //right TODO
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.topMiddle = new Vector2(bounds.center.x, bounds.max.y);

        //left TODO
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.topMiddle = new Vector2(bounds.center.x, bounds.max.y);


        Vector3 up = RayCastObject.transform.TransformDirection(Vector3.up);
        Vector3 dwn = RayCastObject.transform.TransformDirection(Vector3.down);
        Debug.DrawRay(raycastOrigins.topMiddle, up * range, Color.green);
        Debug.DrawRay(raycastOrigins.topLeft, up * range, Color.green);
        Debug.DrawRay(raycastOrigins.topRight, up * range, Color.green);
        RaycastHit2D hit = (Physics2D.Raycast(raycastOrigins.topMiddle, up, range, switchblock));
        RaycastHit2D hit2 = (Physics2D.Raycast(raycastOrigins.topLeft, up, range, switchblock));
        RaycastHit2D hit3 = (Physics2D.Raycast(raycastOrigins.topRight, up, range, switchblock));
        RaycastHit2D hitb = (Physics2D.Raycast(raycastOrigins.botMiddle, dwn, range, switchblock));
        RaycastHit2D hitb2 = (Physics2D.Raycast(raycastOrigins.botLeft, dwn, range, switchblock));
        RaycastHit2D hitb3 = (Physics2D.Raycast(raycastOrigins.botRight, dwn, range, switchblock));

        //als je raakt verandert kleur
        if (hit || hit2 || hit3 || hitb || hitb2 || hitb3)
        {
            print("hit danger");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
