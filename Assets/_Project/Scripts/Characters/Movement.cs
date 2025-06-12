/* Title: Movement.cs
 * Author: Breno Aguirres
 * Date: 2025-11-06
 * Description: Contains useful functions for creating linear 2D movement behaviour for characters or non-static objects.
*/

using TMPro;
using UnityEngine;

namespace Characters
{
    public class Movement : MonoBehaviour
    {
        #region FIELDS
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 5f;

        [Space(1)]
        [Header("Rotation Settings")]
        [SerializeField] private float _rotationSpeed = 540f;
        #endregion

        #region MOVEMENT METHODS
        public void MoveTowards(Rigidbody rb, Vector2 input)
        {
            Vector3 velocity = new Vector3(
                input.x * _moveSpeed,
                rb.linearVelocity.y,
                input.y * _moveSpeed
            );
            rb.linearVelocity = velocity;
        }

        public void SuddenStop(Rigidbody rb)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
        #endregion

        #region ROTATION METHODS
        public void LookTowards(Rigidbody rb, Vector2 input)
        {
            if (input.magnitude <= 0.001f) return;

            Vector3 lookDirection = new Vector3(input.x, 0f, input.y);

            if (lookDirection.sqrMagnitude < 0.001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            Quaternion newRotation = Quaternion.RotateTowards(
                rb.rotation,
                targetRotation,
                _rotationSpeed * Time.fixedDeltaTime
            );

            rb.MoveRotation(newRotation);
        }
        #endregion
    }
}