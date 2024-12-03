using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float moveSpeed = 2f;
    public ObstacleData obstacleData;
    public bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            HandleMouseClick();
        }
    }

    // Handles mouse click input
    private void HandleMouseClick()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the clicked object is a tile
            TileScript tile = hit.collider.GetComponent<TileScript>();
            if (tile != null)
            {
                Vector3 targetPosition = new Vector3(tile.x, 0, tile.y);

                // Check if the target tile is not blocked
                if (!IsTileBlocked(tile.x, tile.y))
                {
                    // Start pathfinding and move the player
                    List<Vector3> path = FindPath(transform.position, targetPosition);
                    if (path != null && path.Count > 0)
                    {
                        StartCoroutine(MoveAlongPath(path));
                    }
                }
            }
        }
    }

    // Check if a tile is blocked
    private bool IsTileBlocked(int x, int y)
    {
        int index = x * 10 + y;
        return obstacleData.obstacleGrid[index];
    }

    // Implement simple pathfinding (e.g., BFS or A*)
    private List<Vector3> FindPath(Vector3 start, Vector3 target)
    {
        // Convert positions to grid indices
        int startX = Mathf.RoundToInt(start.x);
        int startY = Mathf.RoundToInt(start.z);
        int targetX = Mathf.RoundToInt(target.x);
        int targetY = Mathf.RoundToInt(target.z);

        // BFS implementation for simplicity
        Queue<(int x, int y, List<Vector3> path)> queue = new Queue<(int, int, List<Vector3>)>();
        HashSet<(int x, int y)> visited = new HashSet<(int, int)>();

        queue.Enqueue((startX, startY, new List<Vector3> { start }));
        visited.Add((startX, startY));

        int[] dx = { 0, 1, 0, -1 }; // Directions (right, down, left, up)
        int[] dy = { 1, 0, -1, 0 };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int cx = current.x;
            int cy = current.y;
            List<Vector3> path = current.path;

            if (cx == targetX && cy == targetY)
            {
                return path; // Return the path if the target is reached
            }

            for (int i = 0; i < 4; i++)
            {
                int nx = cx + dx[i];
                int ny = cy + dy[i];

                // Check bounds and obstacles
                if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10 && !visited.Contains((nx, ny)) && !IsTileBlocked(nx, ny))
                {
                    visited.Add((nx, ny));
                    List<Vector3> newPath = new List<Vector3>(path) { new Vector3(nx, 0, ny) };
                    queue.Enqueue((nx, ny, newPath));
                }
            }
        }

        return null; // Return null if no path is found
    }

    // Coroutine to move the player along the path
    private IEnumerator MoveAlongPath(List<Vector3> path)
    {
        isMoving = true;

        foreach (Vector3 position in path)
        {
            while (Vector3.Distance(transform.position, position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        isMoving = false;
    }

}
    
