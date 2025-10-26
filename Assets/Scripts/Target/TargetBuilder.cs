using UnityEngine;

public class TargetBuilder : MonoBehaviour
{
    public float Speed { get; private set; }
    public int PointValue { get; private set; }
    public Vector3 Size { get; private set; }
    public Color Color { get; private set; }

    private TargetBuilder()
    {
    }

    public void SetSpeed(float speed) { Speed = speed; }
    public void SetPointValue(int points) { PointValue = points; }
    public void SetSize(Vector3 size) { Size = size; }
    public void SetColor(Color color) { Color = color; }

    public class Builder
    {
        private float speed = 1f;
        private int pointValue = 10;
        private Vector3 size = Vector3.one;
        private Color color = Color.white;
        private GameObject prefab;

        public Builder(GameObject targetPrefab)
        {
            prefab = targetPrefab;
        }

        public Builder WithSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }

        public Builder WithPointValue(int pointValue)
        {
            this.pointValue = pointValue;
            return this;
        }

        public Builder WithSize(Vector3 size)
        {
            this.size = size;
            return this;
        }

        public Builder WithColor(Color color)
        {
            this.color = color;
            return this;
        }

        public TargetBuilder Build()
        {
            GameObject targetObject = Object.Instantiate(prefab);
            TargetBuilder target = targetObject.GetComponent<TargetBuilder>();
            
            target.Speed = speed;
            target.PointValue = pointValue;
            target.Size = size;
            target.Color = color;

            targetObject.transform.localScale = size;
            Renderer renderer = targetObject.GetComponentInChildren<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }

            return target;
        }
    }
}
