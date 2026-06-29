using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private ParallaxLayer[] layers;

    private Vector3 lastCameraPosition;

    [System.Serializable]
    public class ParallaxLayer
    {
        public SpriteRenderer image;
        public float speedX = 0.5f;
        public float speedY = 0.3f;
    }

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPosition;

        foreach (ParallaxLayer layer in layers)
        {
            if (layer.image == null)
                continue;

            layer.image.transform.position += new Vector3(delta.x * layer.speedX, delta.y * layer.speedY, 0);
        }

        lastCameraPosition = cameraTransform.position;
    }
}