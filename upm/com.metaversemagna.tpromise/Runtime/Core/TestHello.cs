using UnityEngine;
using UnityEngine.UI;

namespace TPromise
{
    public class TestHello : MonoBehaviour
    {
        public void Run()
        {
            Camera.main.backgroundColor = Color.cyan;
            
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasGO = new GameObject("Canvas");
                canvas = canvasGO.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasGO.AddComponent<GraphicRaycaster>();
            }

            CreateUIText(canvas.transform, "Hello World!", new Vector2(0, 50));
            CreateUIText(canvas.transform, "Nice work Promise.", new Vector2(0, -10));
        }

        private void CreateUIText(Transform parent, string text, Vector2 anchoredPosition)
        {
            GameObject textGO = new GameObject("UIText");
            textGO.transform.SetParent(parent);

            Text uiText = textGO.AddComponent<Text>();
            uiText.text = text;
            uiText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            uiText.fontSize = 40;
            uiText.alignment = TextAnchor.MiddleCenter;
            uiText.color = Color.black;

            RectTransform rectTransform = uiText.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(600, 100);
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.localScale = Vector3.one;
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}
