using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    Rigidbody2D MyRB;                               //Rigidbody of the player

    [SerializeField]
    bool Grounded;                                  //true when player on solid object

    public bool Interacting;                        //is true when player interacts with certain interactive objects in the scene (teleporter, world turn etc.)


    //---- Player Switch -----//

    public bool Active;                             //is true when player is active player
    bool SwitchKeyDown;                             //input key memory

    
    public PlayerController OtherPlayer;                   // the second player
   

    //---- Color -----//

    [SerializeField]
    public GameColorTypes MyColorType;
    GameColor MyColor;
    public bool CanChangeColor = true;

    //---- Horizontal Movement -----//
    [SerializeField]
    float Speed;                                    //Speed Factor by which the Force at which the Player moves Left and Right will be multiplied
    [SerializeField]
    float MaxVelocityX;                             //Maximum Speed for the left and right movement of the Player


    //---- Jump -----//

    [SerializeField]
    float JumpForce;                                //how high                             
    bool SpaceKeyDown;                              //input key memory


    //---- Layer Masks -----//

    //[SerializeField]
    //LayerMask Level;                                //Layermask of all Layers the Player collides with when growing 
    [SerializeField]
    LayerMask OnGroundLayer;                        //Layermask of all Objects the Player recongnises as Ground






    //----------------------------------------------------------------------------------------------------------------------------------------------------------//





    private void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();

        if (MyRB == null)
            Debug.LogError("no Rigidbody on Player found in PlayerController");

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
    }





    //------------------------------------------------------------------------- Player Switch ---------------------------------------------------------------------------------//




    void SwitchActive()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !SwitchKeyDown)
        {
            SwitchKeyDown = true;

            //Debug.Log(this.name + " switched off");
            Active = false;
            transform.Find("ActiveHalo").GetComponent<SpriteRenderer>().enabled = false;

            OtherPlayer.SetPlayerActive();
        }

        if (!Input.GetKey(KeyCode.LeftShift) && SwitchKeyDown)
            SwitchKeyDown = false;

    }


    public void SetPlayerActive()
    {
        Active = true;
        SwitchKeyDown = true;

        transform.Find("ActiveHalo").GetComponent<SpriteRenderer>().enabled = true;
    }






    //------------------------------------------------------------------------ Basic Movements ----------------------------------------------------------------------------------//





    private void Move()
    {
        float xInput = OrientationMaster.Instance.GetHorizontalAxisInput();

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
        if (!CanChangeColor) return;

        //change local color memory
        MyColor = new GameColor(newColorType);
        MyColorType = newColorType;

        //update sprite color
        GetComponent<SpriteRenderer>().color = MyColor.Color;

        //update layer according to color
        this.gameObject.layer = LayerMask.NameToLayer(MyColor.Name);

        //Debug.Log(this.gameObject.name + " changed to Color " + MyColor.Name);

        //update other player to associated color
        if(OtherPlayer.MyColorType != ColorMaster.Instance.GetRespectiveColor(MyColorType))
            OtherPlayer.ChangeColor(ColorMaster.Instance.GetRespectiveColor(MyColorType));
    }






    //-------------------------------------------------------------------- State Check --------------------------------------------------------------------------------------//





    void CheckGrounded()
    {
        //Debug.Log("CheckGrounded:");

        //where dose the box go?
        Vector2 boxPosOffset = OrientationMaster.Instance.Down() * (0.05f + transform.localScale.y / 2);
        Vector3 boxPosition = transform.position + new Vector3(boxPosOffset.x, boxPosOffset.y, 0);

        //how big is the box?
        Vector3 boxScale = new Vector3(transform.localScale.x - 0.1f, 0.055f, 0);

        //TODO turn box when working with 90 degree level orientation



        Collider2D[] collisions = Physics2D.OverlapBoxAll(boxPosition, boxScale, 0, OnGroundLayer);

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
