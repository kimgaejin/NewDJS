using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    // * 현재 조이스틱의 x,y 좌표를 GetHorizon, GetVertical을 써서 받아오면 됩니다. -1 ~ 1 사이의 값을 가집니다.


    private Image bgImg;
    private Image joystickImg;
    private Vector3 inputVector;

    // 조이스틱을 누를 때, 조금만 눌렀을 때 어느정도 이동하는지. 기본값은 2입니다.
    private float stickSensitivity = 2.0f;

    void Awake()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    // ==========[[터치시 발동]]

    // 터치하고 있을 때 발생하는 함수입니다.
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    // 터치가 끝나면, 조이스틱 이미지 및 위치정보를 초기화합니다.
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    // ===========[[외부에서 값 호출]]
    // 조이스틱의 x값, y값을 반환합니다.

    public float GetHorizon()
    {
        return inputVector.x;
    }

    public float GetVertical()
    {
        return inputVector.y;
    }

    // ==========[[클래스 내부에서 호출]]

    // 터치패드(이미지)를 누르고 있을 때 발동되는 함수
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        // 지정된 지역 사각형이 터치된다면 true를 반환합니다.
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            // 터치된 좌표값을 pos로 할당합니다. -1 < x < 0, 0 < y < 1
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            // 조작민감도는 주로 스틱이 이미지보다 2배 작으니 기본값은 2 입니다.
            // 터치하고 화면 끝까지 당길수도 있으니 단위벡터로 만듭니다.
            inputVector = new Vector3(pos.x * stickSensitivity, pos.y * stickSensitivity, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // 조이스틱 이미지를 터치한 좌표값으로 이동시킵니다.
            joystickImg.rectTransform.anchoredPosition
                = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputVector.y * (bgImg.rectTransform.sizeDelta.y) / 3);
        }
    }



}
