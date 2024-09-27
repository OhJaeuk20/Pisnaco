using UnityEngine;

namespace INab.WorldAlchemy
{
    public class StickyProjectile : MonoBehaviour
    {
        public AnimationCurve scaleCurve;
        public float minScale = 0.1f;
        public float maxScale = 2f;
        public float duration = 2f;

        public LayerMask mask;

        private bool isSticking = false;
        private float timer = 0f;

        private void Start()
        {
            transform.localScale = new Vector3(minScale, minScale, minScale);
        }

        void Update()
        {
            if (isSticking)
            {
                // Increment the timer based on the elapsed time.
                timer += Time.deltaTime;

                // Calculate the current scale based on the animation curve and the max scale.
                float currentScale = Mathf.Lerp(minScale, maxScale, scaleCurve.Evaluate(timer / duration));

                // Apply the calculated scale to the projectile.
                transform.localScale = new Vector3(currentScale, currentScale, currentScale);

                // Reset and disable the projectile after the duration has passed.
                if (timer >= duration)
                {
                    isSticking = false;
                    timer = 0f;
                    // Optionally, deactivate the projectile or implement pooling for optimization.
                    //gameObject.SetActive(false);
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (mask != (mask | (1 << other.gameObject.layer))) return;

            // Check if the projectile is not already sticking to prevent re-triggering on subsequent collisions.
            if (!isSticking)
            {
                isSticking = true;

                // Optional: Disable the Rigidbody to stop movement and prevent further physics interactions.
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.velocity = Vector3.zero;
                }

                var collider = GetComponent<Collider>();
                collider.isTrigger = true;
            }
        }

    }
}