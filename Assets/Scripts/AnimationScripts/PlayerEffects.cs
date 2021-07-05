using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    [SerializeField] SpriteRenderer ColorChangeSprite;
    [SerializeField] SpriteRenderer ActiveHalo;


    bool Glowing;
    bool Dying;

    [SerializeField] float ColorChangeGlowFallOff = 0.07f;

    [SerializeField] float DyingSpeed = 0.01f;
    float NewDeathTimer;
    float OldDeathTimer;


    private void FixedUpdate()
    {
        if (Glowing)
        {
            ColorChangeSprite.color = new Color(ColorChangeSprite.color.r, ColorChangeSprite.color.g, ColorChangeSprite.color.b, ColorChangeSprite.color.a - ColorChangeGlowFallOff);

            if(ColorChangeSprite.color.a <= 0)
            {
                ColorChangeSprite.color = new Color(ColorChangeSprite.color.r, ColorChangeSprite.color.g, ColorChangeSprite.color.b, 0);
                Glowing = false;
            }
            
        }

        if (Dying)
        {
            Debug.Log("LastTime = " + OldDeathTimer);
            Debug.Log("NewTime" + NewDeathTimer);

            if (NewDeathTimer == OldDeathTimer)
            {
                Debug.Log("no longer dying");

                Dying = false;

                ColorChangeSprite.color = Color.white;
                SetActiveHalo(GetComponent<PlayerController>().Active);

                OldDeathTimer = 0;
                NewDeathTimer = 0;
            }

            OldDeathTimer = NewDeathTimer;
        }
    }

    public void Glow()
    {
        Glowing = true;

        ColorChangeSprite.color = new Color(ColorChangeSprite.color.r, ColorChangeSprite.color.g, ColorChangeSprite.color.b, 1);        
    }

    public void Die(float timer)
    {
        if (!Dying)
        {
            Dying = true;
            ActiveHalo.color = new Color(1f, 0.15f, 0.15f, 1f);
            SetActiveHalo(true);
        }

        NewDeathTimer = timer;

        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0), DyingSpeed);
    }

    public void SetActiveHalo(bool active)
    {
        if (Dying && !active) return;

        ActiveHalo.enabled = active;
    }
}
