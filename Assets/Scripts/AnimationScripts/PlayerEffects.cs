using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    [SerializeField] SpriteRenderer ColorChangeSprite;
    [SerializeField] float ColorChangeGlowFallOff = 0.07f;


    bool Glowing;


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
    }

    public void Glow()
    {
        Glowing = true;

        ColorChangeSprite.color = new Color(ColorChangeSprite.color.r, ColorChangeSprite.color.g, ColorChangeSprite.color.b, 1);        
    }
}
