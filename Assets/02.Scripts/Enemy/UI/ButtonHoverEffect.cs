using UnityEngine;
using UnityEngine.EventSystems;

// IPointerEnterHandler: ���콺�� �ö��� ��
// IPointerExitHandler: ���콺�� ������ ��
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToShow; // ������ ������Ʈ

    // ���콺�� ��ư ���� �ö��� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }

    // ���콺�� ��ư���� ������ ��
    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }
}