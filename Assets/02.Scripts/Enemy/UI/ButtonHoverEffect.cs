using UnityEngine;
using UnityEngine.EventSystems;

// IPointerEnterHandler: 마우스가 올라갔을 때
// IPointerExitHandler: 마우스가 나갔을 때
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToShow; // 보여줄 오브젝트

    // 마우스가 버튼 위에 올라갔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }

    // 마우스가 버튼에서 나갔을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }
}