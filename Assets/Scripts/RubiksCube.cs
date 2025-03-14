using UnityEngine;

public class RubiksCubeDataManager : MonoBehaviour
{
    private int[,,] cubeData = new int[3, 3, 3]; // 큐브의 상태를 저장하는 배열

    void Start()
    {
        InitializeCube();
    }

    void InitializeCube()
    {
        int id = 0;
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                    cubeData[x, y, z] = id++;

        Debug.Log("Rubik's Cube Initialized.");
        PrintCubeState();
    }

    public void RotateFace(int layer, string axis)
    {
        Debug.Log($"\nRotating {axis}-axis layer {layer} by 90 degrees:");
        int[,] face = new int[3, 3];

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                face[i, j] = (axis == "x") ? cubeData[layer, i, j] :
                             (axis == "y") ? cubeData[i, layer, j] :
                                             cubeData[i, j, layer];

        // 2D 배열 회전 (90도 시계 방향)
        face = RotateMatrix90(face);

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (axis == "x") cubeData[layer, i, j] = face[i, j];
                else if (axis == "y") cubeData[i, layer, j] = face[i, j];
                else cubeData[i, j, layer] = face[i, j];

        PrintCubeState();
    }

    int[,] RotateMatrix90(int[,] matrix)
    {
        int[,] rotated = new int[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                rotated[j, 2 - i] = matrix[i, j]; // 90도 회전

        Debug.Log("\nMatrix rotated by 90 degrees:");
        for (int i = 0; i < 3; i++)
        {
            string row = "";
            for (int j = 0; j < 3; j++)
                row += rotated[i, j] + " ";
            Debug.Log(row);
        }

        return rotated;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 우클릭 감지
        {
            int randomLayer = Random.Range(0, 3); // 0~2 중 랜덤한 Y 레이어 선택
            Debug.Log($"\nMouse right-clicked. Rotating layer {randomLayer} on Y-axis.");
            RotateFace(randomLayer, "y"); // 선택된 레이어를 Y축 기준 90도 회전
        }
    }

    void PrintCubeState()
    {
        Debug.Log("\nCurrent Cube State:");
        for (int y = 2; y >= 0; y--) // Y축 기준으로 위에서 아래로 출력
        {
            Debug.Log($"Layer Y={y}:");
            for (int x = 0; x < 3; x++)
            {
                string row = "";
                for (int z = 0; z < 3; z++)
                    row += cubeData[x, y, z] + " ";
                Debug.Log(row);
            }
        }
    }
}