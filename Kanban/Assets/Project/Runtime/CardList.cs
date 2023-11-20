using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardList : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler
{
    [SerializeField] private Transform _placeSelector;
    [SerializeField] private Transform _cardsContainer;

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"list: end drag");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"list: pointer enter");
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void ShowPlaceSelector(int siblingIndex)
    {
        SetPlaceSelectorPosition(siblingIndex);
        _placeSelector.gameObject.SetActive(true);
    }

    private void HidePlaceSelector()
    {
        _placeSelector.gameObject.SetActive(false);
    }

    private void SetPlaceSelectorPosition(int siblingIndex)
    {
        _placeSelector.SetSiblingIndex(siblingIndex);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Debug.Log($"list: pointer position: {eventData.position}, [0] item position: {_cardsContainer.GetChild(0).position}");

        int desiredSiblingIndex = ComputeDesiredSiblingIndex(eventData.position);
        SetPlaceSelectorPosition(desiredSiblingIndex);
    }

    private int ComputeDesiredSiblingIndex(Vector2 pointerPosition)
    {
        //for(int childIndex = 0; childIndex < _cardsContainer.childCount; childIndex++)
        //{
        //    if(pointerPosition.y > _cardsContainer.GetChild(childIndex).position.y - _cardsContainer.GetChild(childIndex).GetComponent<RectTransform>().rect.height/2)
        //        return childIndex;
        //}

        //return _cardsContainer.childCount;
        var rectTransform = _cardsContainer.GetComponent<RectTransform>();
        return Mathf.FloorToInt((pointerPosition.y + rectTransform.rect.position.y - rectTransform.rect.yMin) / rectTransform.rect.height);
    }
}
