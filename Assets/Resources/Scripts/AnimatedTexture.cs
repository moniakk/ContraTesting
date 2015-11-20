using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour {

    public int AnimationTileX = 1;
    public int AnimationTileY = 1;
    public float framesPerSecond = 1f;
    public OffsetTiling typeFrame;
    Vector2 Frame;
    Renderer render;
    void Start() {
        Frame = new Vector2(1f / AnimationTileX, 1f / AnimationTileY);
        render = GetComponent<Renderer>();
        render.material.mainTextureScale = Frame;
    }


    void Update() {
        var index = (int)(Time.time * framesPerSecond);
        index = index % (AnimationTileX * AnimationTileY);

        Vector2 size = new Vector2(1.0f / AnimationTileX, 1.0f / AnimationTileY);


        float uIndex = index % AnimationTileX;
        float vIndex = index / AnimationTileX;
        Vector2 offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);
        render.material.mainTextureOffset = offset;
    }
}
public enum OffsetTiling {
    leftToRight = 0,
    UpToDown = 1
}
