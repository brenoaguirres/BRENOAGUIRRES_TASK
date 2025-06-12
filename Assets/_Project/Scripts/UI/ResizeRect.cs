using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResizeRect : MonoBehaviour
    {
        #region ENUM
        public enum ResizeType
        {
            Width,
            Height,
            Both
        }
        #endregion

        #region FIELDS
        [Header("Resize Settings")]
        [SerializeField] private ResizeType _resizeType = ResizeType.Height;

        private RectTransform _rectTransform;
        private GridLayoutGroup _gridLayoutGroup;

        private int _lastChildCount = 0;
        private Vector2 _defaultSize;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            SetDefaultSize();
            if (CountChildren())
                StartCoroutine(SetSizeByChildren());
        }

        private void Update()
        {
            if (CountChildren())
                StartCoroutine(SetSizeByChildren());
        }
        #endregion

        #region CUSTOM METHODS
        private void SetDefaultSize()
        {
            _defaultSize = new Vector2(_rectTransform.rect.width, _rectTransform.rect.height);
        }
        private bool CountChildren()
        {
            if (_lastChildCount != _rectTransform.childCount)
            {
                _lastChildCount = _rectTransform.childCount;
                return true;
            }

            return false;
        }
        private IEnumerator SetSizeByChildren()
        {
            yield return new WaitForEndOfFrame();

            if (_lastChildCount == 0)
            {
                SetDefaultSize();
                yield break;
            }

            Vector2 newSize = new Vector2(0, 0);
            int childPerRow = _gridLayoutGroup.constraintCount;
            float maxRowWidth = 0;
            float maxColumnHeight = 0;
            int currentColumn = 0;

            foreach (RectTransform child in _rectTransform)
            {
                if (child == _rectTransform) continue;

                maxRowWidth += child.rect.width;

                if (child.rect.height > maxColumnHeight)
                {
                    maxColumnHeight = child.rect.height;
                }

                currentColumn++;

                if (currentColumn >= childPerRow || currentColumn == _rectTransform.childCount - 1)
                {
                    if (maxRowWidth > newSize.x)
                    {
                        newSize.x = maxRowWidth;
                    }

                    newSize.y += maxColumnHeight;

                    maxRowWidth = 0;
                    maxColumnHeight = 0;
                    currentColumn = 0;
                }
            }

            switch (_resizeType)
            {
                case ResizeType.Width:
                    _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize.x);
                    break;
                case ResizeType.Height:
                    _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize.y);
                    break;
                case ResizeType.Both:
                    _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize.x);
                    _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize.y);
                    break;
            }
        }
        #endregion
    }
}
