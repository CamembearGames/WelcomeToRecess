using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake() {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Start() {
        SetupText("Hey come here");
    }

    public void SetupText(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(2f,2f);

        backgroundSpriteRenderer.size = textSize + padding;

    }
}
