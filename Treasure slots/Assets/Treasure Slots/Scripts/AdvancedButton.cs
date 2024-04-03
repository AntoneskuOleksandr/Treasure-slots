using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonConfig))]
[RequireComponent(typeof(CanvasGroup))]
public class AdvancedButton : Button
{
    private Button button;
    private ButtonConfig config;
    private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        button = GetComponent<Button>();
        config = GetComponent<ButtonConfig>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    protected override void Start()
    {
        if (AudioManager.Instance != null)
        {
            button.onClick.AddListener(AudioManager.Instance.PlayButtonClickSound);
        }
    }

    protected override void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    /*    public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            StopAllCoroutines();
            StartCoroutine(Utils.FadeOut(canvasGroup, config.onHoverAlpha, config.fadeTime));
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            StopAllCoroutines();
            StartCoroutine(Utils.FadeIn(canvasGroup, 1.0f, config.fadeTime));
        }*/

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (button.interactable == true)
        {
            base.OnPointerDown(eventData);
            canvasGroup.alpha = config.onClickAlpha;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        canvasGroup.alpha = 1.0f;
    }
}
