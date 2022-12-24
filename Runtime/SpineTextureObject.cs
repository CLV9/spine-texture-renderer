using Spine.Unity;
using UnityEngine;

namespace CLVP.Tools.SpineTextureRenderer
{
    public class SpineTextureObject : MonoBehaviour, ISpineTextureObject
    {
        private SpineRendererCamera _spineCamera;
        private bool _inited;
        private Vector4 _padding;

        public RenderTexture Texture { get; private set; }
        public SkeletonAnimation SpineAnimationInstance { get; private set; }
        
        ~SpineTextureObject()
        {
            Dispose();
        }

        private void Update()
        {
            if (!_inited)
            {
                return;
            }
            
            // resize camera dynamically with bounds
            var bounds = GetSkeletonBounds();
            CenterSpine(bounds);
            ResizeCamera(bounds);
        }

        public void Dispose()
        {
            Destroy(Texture);
            Destroy(gameObject);
        }

        public void Init(SkeletonAnimation spinePrefab, Vector2Int resolution, Vector4 padding)
        {
            if (_inited)
            {
                return;
            }
            
            SpineAnimationInstance = Instantiate(spinePrefab, transform);
            Texture = new RenderTexture(resolution.x, resolution.y, 0);

            _padding = padding;
            
            CreateCamera();
            
            var bounds = GetSkeletonBounds();
            ResizeCamera(bounds);
            CenterSpine(bounds);

            _inited = true;
        }
        
        private void CreateCamera()
        {
            _spineCamera = new GameObject("ObjectCamera").AddComponent<SpineRendererCamera>();
            _spineCamera.Init(Texture, SpineAnimationInstance);
            _spineCamera.transform.SetParent(transform);
            _spineCamera.transform.position = Vector3.zero;
            _spineCamera.transform.rotation = Quaternion.identity;
        }

        private Vector4 GetSkeletonBounds()
        {
            float[] verts = null;
            SpineAnimationInstance.Skeleton.UpdateWorldTransform();
            SpineAnimationInstance.Skeleton.GetBounds(out var x, out var y, out var width, out var height, ref verts);
            return new Vector4(x, y, width, height);
        }

        private void CenterSpine(Vector4 spineBounds)
        {
            var cameraPos = _spineCamera.transform.position;
            SpineAnimationInstance.transform.position = new Vector3(
                cameraPos.x - spineBounds.x - spineBounds.z / 2 - _padding.z / 2 + _padding.x / 2, 
                cameraPos.y - spineBounds.y - spineBounds.w / 2 - _padding.y / 2 + _padding.w / 2, 
                cameraPos.z + 1
            );
        }

        private void ResizeCamera(Vector4 spineBounds)
        {
            var paddingAdditionalSize = new Vector2(_padding.x + _padding.z, _padding.y + _padding.w);
            var cameraSize = new Vector2(spineBounds.z, spineBounds.w) + paddingAdditionalSize;
            _spineCamera.CreatedCamera.orthographicSize = cameraSize.y / 2;
            _spineCamera.CreatedCamera.aspect = cameraSize.x / cameraSize.y;
        }
    }
}