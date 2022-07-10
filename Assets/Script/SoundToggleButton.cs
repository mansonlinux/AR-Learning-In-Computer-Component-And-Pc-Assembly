using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggleButton : MonoBehaviour
{
    public enum ButtonType
    {
        BackgroundMusic,
        SoundFx
    };

    public ButtonType type;

    public Sprite onSprite;
    public Sprite offSprite;

    public GameObject button;
    public Vector3 offButtonPosition;

    private Vector3 _onButtonPosition;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = onSprite;
        _onButtonPosition = button.GetComponent<RectTransform>().anchoredPosition;
        ToggleButton();
    }

    public void ToggleButton()
    {
        var muted = false;
        if(type == ButtonType.BackgroundMusic)
            muted = SoundManager.instance.IsBackgroundMusicMuted();
        else
            muted = SoundManager.instance.IsSoundFXMuted();

        if(muted)
        {
            _image.sprite = offSprite;
            button.GetComponent<RectTransform>().anchoredPosition = offButtonPosition;
        }
        else
        {
            _image.sprite = onSprite;
            button.GetComponent<RectTransform>().anchoredPosition = _onButtonPosition;
        }
    }

}
