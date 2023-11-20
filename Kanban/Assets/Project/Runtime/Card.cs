using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnClicked()
    {
        Debug.Log($"card: clicked");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // grab card
        Debug.Log($"Drag started");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // draw card at cursor position
        //Debug.Log($"Draging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // leave card
        Debug.Log($"Drag ended");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Highlight
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Dim
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }
}
