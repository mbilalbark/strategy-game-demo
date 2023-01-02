using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfinitiveScrolView : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    [SerializeField] ScrollRect scrollRect;

    [SerializeField]
    private float outOfBoundsThreshold;

    [SerializeField] GridLayoutGroup gridLayout;

    private RectTransform rect;
    private Vector2 lastDragPosition;
    private bool positiveDrag;
    private float height;
   
    private void Start()
    {
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        scrollRect.onValueChanged.AddListener(OnViewScroll);
        CalculateHeight();
    }
    private void CalculateHeight()
    {
        rect = GetComponent<RectTransform>();
        height = rect.rect.height;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        positiveDrag = eventData.position.y > lastDragPosition.y;
        lastDragPosition = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        positiveDrag = eventData.scrollDelta.y > 0;
    }

    public void OnViewScroll(Vector2 _)
    {
        int leftItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var lItem = scrollRect.content.GetChild(leftItemIndex);

        int rItemIndex = positiveDrag ? scrollRect.content.childCount - 2 : 1;
        var rItem = scrollRect.content.GetChild(rItemIndex);

        if (!ReachedThreshold(rItem) && !ReachedThreshold(lItem))
        {
            return;
        }
        Vector2 newPos = scrollRect.content.anchoredPosition;

        if (positiveDrag)
        {
            newPos.y = newPos.y - ( gridLayout.spacing.y + gridLayout.cellSize.x );
        }
        else
        {
            newPos.y = newPos.y + (gridLayout.spacing.y + gridLayout.cellSize.x);
        }

        int nIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;

        rItem.SetSiblingIndex(nIndex);
        lItem.SetSiblingIndex(positiveDrag ? nIndex + 1 : nIndex - 1);
        scrollRect.content.anchoredPosition = newPos;
    }

    private bool ReachedThreshold(Transform item)
    {
        float posYThreshold = transform.position.y - (height * 0.5f);
        float negYThreshold = transform.position.y + (height * 0.5f);

        return positiveDrag ? item.position.y - gridLayout.cellSize.y > posYThreshold :
            item.position.y + gridLayout.cellSize.y < negYThreshold;
    }
}
