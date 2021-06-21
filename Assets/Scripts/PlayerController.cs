using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    Rigidbody2D MyRB;

    [SerializeField]
    bool Grounded;

    public bool Interacting;


    //---- Player Switch -----//

    public bool Active;
    float ActiveCoolDown;
    float ActiveCoolDownTime = 0.2f;

    [SerializeField]
    PlayerController OtherPlayer;
   
    //---- Color -----//
    [SerializeField]
    public GameColorTypes MyColorType;
    GameColor MyColor;


    //---- Horizontal Movement -----//
    [SerializeField]
    float Speed;                                    //Speed Factor by which the Force at which the Player moves Left and Right will be multiplied
    [SerializeField]
    float MaxVelocityX;                             //Maximum Speed for the left and right movement of the Player


    //---- Jump -----//

    [SerializeField]
    float JumpForce;                                
    bool SpaceKeyDown;


    //---- Layer Masks -----//

    [SerializeField]
    LayerMask Level;                                //Layermask of all Layers the Player collides with when growing 
    [SerializeField]
    LayerMask OnGroundLayer;                        //Layermask of all Objects the Player recongnises as Ground






    //----------------------------------------------------------------------------------------------------------------------------------------------------------//





    private void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();
        if (MyRB == null)
            Debug.LogError("no Rigidbody on Player found in PlayerController1");

        MyColor = new GameColor(MyColorType);

        this.gameObject.layer = LayerMask.NameToLayer(MyColor.Name);
    }


    private void FixedUpdate()
    {
        if (Active)
        {
            SwitchActive();

            CheckGrounded();

            Move();
            Jump();
        }


        //dont fall endlessly  - TODO take out
        if (transform.position.y < -10)
            transform.position = Vector3.zero;
    }





    //------------------------------------------------------------------------- Player Switch ---------------------------------------------------------------------------------//






    void SwitchActive()
    {
        //cooldown time after which another will be input accepted
        if (ActiveCoolDown > 0)
        {
            ActiveCoolDown -= Time.deltaTime;
            return;
        }


        if (Active && Input.GetKey(KeyCode.Tab))
        {
            //Debug.Log(this.name + " switched off");
            Active = false;
            OtherPlayer.SetPlayerActive();
        }
    }

    
    public void SetPlayerActive()
    {
        Active = true;
        ActiveCoolDown = ActiveCoolDownTime;
    }






    //------------------------------------------------------------------------ Basic Movements ----------------------------------------------------------------------------------//





    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        float accelleration;


        //when moving and applying movement calculate accelleration
        if (xInput != 0 && Mathf.Abs(MyRB.velocity.x) <= MaxVelocityX)
        {
            accelleration = Mathf.Sign(xInput) * (1 - (Mathf.Abs(MyRB.velocity.x) / MaxVelocityX));
        }
        //don't accellerate otherwhise
        else
            accelleration = 0;


        float xForce = (xInput + accelleration) * Speed;


        if (Grounded)
        {
            //Apply force at ca 64° down to deny player the ability to climb slopes
            MyRB.AddForce(new Vector2(xForce, -Mathf.Abs(xForce * 1.5f)));
        }
        else
        {
            //move horizontally at lower speed when in the air
            MyRB.AddForce(new Vector2((xForce) / 5, 0));
        }

        //Debug.Log("detected Input " + xInput);
        //Debug.Log("added Force " + xForce);
        //Debug.Log("moving at " + MyRB.velocity);
    }




    private void Jump()
    {
        if (Grounded && Input.GetKey(KeyCode.Space) && !SpaceKeyDown)
        {
            Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position - new Vector3(0, 0.05f + transform.localScale.y / 2, 0), new Vector3(transform.localScale.x - 0.2f, 0.055f, 0), 0, OnGroundLayer);

            float gradient = 0;

            foreach (Collider2D collider in collisions)
            {
                //Debug.Log("Colliding with: " + collider.gameObject.name);

                // ignore own collider
                if (collider.gameObject == this.gameObject)
                {
                    continue;
                }
                else if (collider.tag.Equals("Edge"))
                {
                    //gradient = Mathf.Tan(collider.GetComponent<PlatformEffector2D>().rotationalOffset * Mathf.Deg2Rad);
                    //Vector2 gradientTranslation = new Vector2((-gradient / 9f) * 16, (1f / 16f) * 9);

                    //Debug.Log("Gradient: " + gradient);
                    //Debug.Log("GradientTranslation: " + gradientTranslation.y/gradientTranslation.x);
                    //Debug.Log("GradientTranslation (inverse): " + gradientTranslation.x/gradientTranslation.y);

                    
                    Vector2[] points = collider.GetComponent<EdgeCollider2D>().points;

                    if (points.Length > 2)
                        Debug.LogError("to many points on collider " + collider.name);

                    Vector2 delta = collider.transform.TransformPoint(points[0]) - collider.transform.TransformPoint(points[1]);

                    if (delta.x < 0)
                        delta *= -1;

                    gradient = delta.y / delta.x;

                    Debug.Log("grd: " + delta.y/delta.x);

                    break;
                }
            }


            Vector2 ForceDirection;

            if (gradient != 0)
            {
                ForceDirection = new Vector2(1, -1 / gradient).normalized;

                if (ForceDirection.y < 0)
                    ForceDirection *= -1;
            }
            else
                ForceDirection = Vector2.up;

            Debug.Log("ForceDirection: " + ForceDirection);


            MyRB.AddForce(ForceDirection * JumpForce, ForceMode2D.Impulse);
            SpaceKeyDown = true;

            //Debug.Log("Jumped");
        }

        if (SpaceKeyDown && !Input.GetKey(KeyCode.Space))
            SpaceKeyDown = false;

        //Debug.Log("Current velocity = " + MyRB.velocity);
    }

    //-------------------------------------------------------------------- Change Color --------------------------------------------------------------------------------------//





    public void ChangeColor(GameColorTypes newColorType)
    {
        MyColor = new GameColor(newColorType);
        MyColorType = newColorType;
        GetComponent<SpriteRenderer>().color = MyColor.Color;
        this.gameObject.layer = LayerMask.NameToLayer(MyColor.Name);

        //Debug.Log(this.gameObject.name + " changed to Color " + MyColor.Name);

        if(OtherPlayer.MyColorType != ColorMaster.Instance.GetRespectiveColor(MyColorType))
            OtherPlayer.ChangeColor(ColorMaster.Instance.GetRespectiveColor(MyColorType));
    }






    //-------------------------------------------------------------------- State Check --------------------------------------------------------------------------------------//





    void CheckGrounded()
    {
        //Debug.Log("CheckGrounded:");

        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position - new Vector3(0, 0.05f + transform.localScale.y/2, 0), new Vector3(transform.localScale.x - 0.1f, 0.055f, 0), 0, OnGroundLayer);

        bool GroundCheck = false;

        foreach (Collider2D collider in collisions)
        {
            //Debug.Log("Colliding with: " + collider.gameObject.name);

            // ignore own collider
            if (collider.gameObject == this.gameObject)
            {
                continue;
            }
            else
            {
                GroundCheck = true;
            }

        }
        Grounded = GroundCheck;
    }
}
