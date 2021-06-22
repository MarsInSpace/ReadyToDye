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
        float xInput = OrientationMaster.Instance.GetHorizontalAxis();

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
            MyRB.AddForce(new Vector2(xForce, 0));
        else
            //move horizontally at lower speed when in the air
            MyRB.AddForce(new Vector2((xForce) / 5, 0));

        //Debug.Log("detected Input " + xInput);
        //Debug.Log("added Force " + xForce);
        //Debug.Log("moving at " + MyRB.velocity);
    }




    private void Jump()
    {
        if (Grounded && Input.GetKey(KeyCode.Space) && !SpaceKeyDown)
        {            
            MyRB.AddForce(OrientationMaster.Instance.Up() * JumpForce, ForceMode2D.Impulse);
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
