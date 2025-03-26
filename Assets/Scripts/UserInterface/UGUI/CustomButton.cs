using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] UnityEvent OnClick = new UnityEvent();
    [SerializeField] UnityEvent OnCursorEnter = new UnityEvent();
    [SerializeField] UnityEvent OnCursorExit = new UnityEvent();

    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
        Debug.Log($"OnPointerClick: {eventData}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnCursorEnter?.Invoke();
        Debug.Log($"OnPointerEnter: {eventData}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnCursorExit?.Invoke();
        Debug.Log($"OnPointerExit: {eventData}");
    }

    public void ChangeScene(string str)
    {
        SceneLoader.LoadSceneAsync(str);
    }
}
