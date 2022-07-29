using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitBehaviorTree.Utilities
{
    public static class KitMoveFunctions
    {
	    public static Vector3 SnapToGround(Transform transformToSnap, Vector3 destinationPos, float variance = 10f, bool checkFromBelowToo = true, string layerName = "Terrain")
        {
	        float varIn = System.Math.Max(variance, 10f);

	        Vector3 result = SnapToGroundInDirection(transformToSnap, destinationPos, varIn, layerName, Vector3.down);

            if (checkFromBelowToo && destinationPos == result)
            {
                // we were also asked to check from below if we never found any location on the navmesh trying from below
	            result = SnapToGroundInDirection(transformToSnap, destinationPos, varIn, layerName, Vector3.up);
            }

            return result;
        }

        public static Vector3 SnapToGroundInDirection(Transform transformToSnap, Vector3 startPos, float variance, string layer, Vector3 direction)
        {
            Vector3 result = startPos;
            RaycastHit[] hits;
            if (direction == Vector3.down)
                hits = Physics.RaycastAll(startPos + (Vector3.up * variance), Vector3.down, variance * 3f);
            else
                hits = Physics.RaycastAll(startPos + (Vector3.down * variance), Vector3.up, variance * 3f);

            foreach (var hit in hits)
            {
                if (!hit.collider.CompareTag(layer) || !hit.transform || hit.collider.gameObject == transformToSnap.gameObject)
                    continue;

                result = hit.point;
                break;
            }
            return result;
        }

        /// <summary>
        /// Returns the range to the target
        /// </summary>
        /// <param name="pointA">First vector3</param>
        /// <param name="pointB">Second vector3</param>
        /// <param name="range">Range we want to be within for a valid return</param>
        /// <returns>The distance between the two Vectors *if* that distance is less then the range</returns>
        public static float rangeToTarget(Transform pointA, Transform pointB, float range, bool mustBeInRange = true)
        {
            if (!mustBeInRange) return 1f; // We don't care if they are in range or not, so always positive value

            float dist = Vector3.Distance(pointA.position, pointB.position);
            if (dist <= range) return dist;
            return -1f;
        }

        /// <summary>
        /// Returns the range to the target within a sight cone extending from the front of the first transform
        /// </summary>
        /// <param name="viewer">Vector3 of the viewer LOOKING for the target</param>
        /// <param name="target">Second vector3 of the target itself</param>
        /// <param name="range">Range we want to be within for a valid return</param>
        /// <param name="degrees">Cone of sight in degrees</param>
        /// <returns>The distance between the two Vectors *if* that distance is less then the range</returns>
        public static float rangeToTargetInCone(Transform viewer, Transform target, float range, float degrees, bool mustBeInRange)
        {
            float dist = Vector3.Distance(viewer.position, target.position);

	        // if we do not need to be in range we can ignore range and check cone *OR* if we ARE in range then we check cone next
	        if (mustBeInRange == false || dist <= range)
            {
                float cone = Mathf.Cos(degrees * Mathf.Deg2Rad);
                var heading = (target.position - viewer.position).normalized;

                if (Vector3.Dot(viewer.forward, heading) > cone)
                {
                    // Target is within the cone of sight AND in range (or we don't care about range)
                    return dist;
                }
            }

            return -1f;
        }

        /// <summary>
        /// Returns true if the target within a sight cone extending from the front of the first transform
        /// </summary>
        /// <param name="viewer">Vector3 of the viewer LOOKING for the target</param>
        /// <param name="target">Second vector3 of the target itself</param>
        /// <param name="degrees">Cone of sight in degrees</param>
        /// <returns>TRUE if the target is within the degrees sight cone of the viewer</returns>
        public static bool isTargetInCone(Transform viewer, Transform target, float degrees)
        {
            float cone = Mathf.Cos(degrees * Mathf.Deg2Rad);
            var heading = (target.position - viewer.position).normalized;

            if (Vector3.Dot(viewer.forward, heading) > cone)
            {
                // Target is within the cone of sight AND in range
                return true;
            }

            return false;
        }
    }
}
