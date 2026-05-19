using UnityEngine;
using UnityEngine.UI;

public class RollingBackground : MonoBehaviour
{
   [SerializeField] RawImage background;
   [SerializeField] float x, y;

    void Update()
    {
        background.uvRect = new Rect(background.uvRect.position + new Vector2(x, y) * Time.deltaTime, background.uvRect.size);
    }
}
