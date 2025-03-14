using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksCubeDataManager : MonoBehaviour
{
    public GameObject cubePrefab; // 큐브 오브젝트 프리팹
    private GameObject[,,] cubes = new GameObject[3, 3, 3]; // 큐브 오브젝트 배열
    private int[,,] cubeData = new int[3, 3, 3]; // 큐브의 상태를 저장하는 배열
    private bool isRotating = false;

    void Start()
    {
        InitializeCube();
        GenerateCube();
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

    void GenerateCube()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    GameObject cube = Instantiate(cubePrefab, new Vector3(x - 1, y - 1, z - 1), Quaternion.identity);
                    cubes[x, y, z] = cube;
                }
            }
        }
    }

    public void RotateFace(int layer, string axis)
    {
        if (isRotating) return;
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

        StartCoroutine(UpdateCubeRotation(layer, axis));
        PrintCubeState();
    }

    IEnumerator UpdateCubeRotation(int layer, string axis)
    {
        isRotating = true;
        Vector3 rotationAxis = (axis == "x") ? Vector3.right : (axis == "y") ? Vector3.up : Vector3.forward;
        float duration = 0.5f;
        float elapsed = 0f;
        float angle = 90f;

        List<GameObject> rotatingCubes = new List<GameObject>();
        foreach (GameObject cube in cubes)
        {
            if ((axis == "x" && Mathf.RoundToInt(cube.transform.position.x) == layer) ||
                (axis == "y" && Mathf.RoundToInt(cube.transform.position.y) == layer) ||
                (axis == "z" && Mathf.RoundToInt(cube.transform.position.z) == layer))
            {
                rotatingCubes.Add(cube);
            }
        }

        while (elapsed < duration)
        {
            float step = (angle / duration) * Time.deltaTime;
            foreach (GameObject cube in rotatingCubes)
            {
                cube.transform.RotateAround(Vector3.zero, rotationAxis, step);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject cube in rotatingCubes)
        {
            cube.transform.RotateAround(Vector3.zero, rotationAxis, angle - (elapsed * (angle / duration))); // 보정
        }

        isRotating = false;
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
        if (Input.GetMouseButtonDown(1) && !isRotating) // 마우스 우클릭 감지
        {
            int randomLayer = Random.Range(0, 3); // 0~2 중 랜덤한 레이어 선택
            string[] axes = { "x", "y" }; // X 또는 Y 축 중 랜덤 선택
            string randomAxis = axes[Random.Range(0, axes.Length)];

            Debug.Log($"\nMouse right-clicked. Rotating layer {randomLayer} on {randomAxis}-axis.");
            RotateFace(randomLayer, randomAxis); // 선택된 레이어를 랜덤한 축 기준 90도 회전
        }
    }

    void PrintCubeState()
    {
        Debug.Log("\nCurrent Cube State:");
        for (int y = 2; y >= 0; y--) // Y축 기준으로 위에서 아래로 출력
        {
            Debug.Log($"Layer Y={y}:");
            for (int z = 0; z < 3; z++)
            {
                string row = "";
                for (int x = 0; x < 3; x++)
                    row += cubeData[x, y, z] + " ";
                Debug.Log(row);
            }
        }
    }
}
