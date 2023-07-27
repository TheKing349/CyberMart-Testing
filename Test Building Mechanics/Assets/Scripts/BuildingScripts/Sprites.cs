using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour
{
    [HideInInspector] public List<Sprite> spritesList;

    private void Awake()
    {
        Object[] sprites = Resources.LoadAll("Sprites", typeof(Sprite));

        spritesList = new List<Sprite>();

        foreach (var sprite in sprites)
        {
            spritesList.Add((Sprite)sprite);
        }
    }


}