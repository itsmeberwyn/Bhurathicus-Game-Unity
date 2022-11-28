using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BTNclick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;

    public void OnPointerDown(PointerEventData eventData){
        _img.sprite = _pressed;
        SceneManager.LoadScene("Main");
    }
    public void OnPointerUp(PointerEventData eventData){
        _img.sprite = _default;
    }
}
