using UnityEngine;

public class HighlightSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static HighlightSystem Instance { get; private set;}

    public Material highlightMaterial;
    private Renderer currentHighlighted;
    private Material originalMaterial;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Highlight(Renderer renderer)
    {
        ClearHighlight();

        if (renderer == null || highlightMaterial == null) return;

        originalMaterial = renderer.sharedMaterial;
        currentHighlighted = renderer;
        renderer.sharedMaterial = highlightMaterial;

        Invoke(nameof(ClearHighlight), 0.5f);
    }

    public void ClearHighlight()
    {
        if (currentHighlighted != null && originalMaterial != null)
        {
            currentHighlighted.sharedMaterial = originalMaterial;
        }
        currentHighlighted = null;
        originalMaterial = null;
        CancelInvoke(nameof(ClearHighlight));
    }



}

