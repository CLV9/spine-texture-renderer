using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace CLVP.Tools.SpineTextureRenderer.Samples
{
    public class RendererTest : MonoBehaviour
    {
        // Inject this
        private readonly ISpineTextureRenderer _renderer = new SpineTextureRenderer();
        
        [Header("UI")]
        [SerializeField] private RawImage _outputImage;
        [SerializeField] private Button _renderButton;

        [Header("Render Settings")]
        [SerializeField] private List<SkeletonAnimation> _prefabs;
        [SerializeField] private Vector2Int _targetResolution;
        [SerializeField] private Vector4 _targetPadding;
        
        private int _currentPrefabIndex;
        private ISpineTextureObject _currentObject;

        private void Awake()
        {
            _renderButton.onClick.AddListener(Render);
        }

        private void Render()
        {
            if (_currentPrefabIndex >= _prefabs.Count)
            {
                _currentPrefabIndex = 0;
            }
            
            if (_currentObject != null)
            {
                _currentObject.Dispose();
                _currentObject = null;
            }
                
            _currentObject = _renderer.Render(_prefabs[_currentPrefabIndex], _targetResolution, _targetPadding);
            _outputImage.texture = _currentObject.Texture;
            var animName = _currentObject.SpineAnimationInstance.AnimationState.Data.SkeletonData.Animations.First().Name;
            _currentObject.SpineAnimationInstance.AnimationState.SetAnimation(1, animName, true);
            _currentPrefabIndex++;
        }
    }
}