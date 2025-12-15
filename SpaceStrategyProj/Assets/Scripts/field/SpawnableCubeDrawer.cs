using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnableCubeDrawer : MonoBehaviour
{
    public float cubeSize = 10f;
    public float lineWidth = 0.1f;
    public float textSize = 2f;
    public Material lineMaterial; // Перетащите сюда ваш материал из инспектора

    // Вызываем при создании объекта
    void Start()
    {
        gameObject.isStatic = true;
        DrawCube();
        DisplayPositionText();
        gameObject.isStatic = true;
    }

    void DrawCube()
    {
        float halfSize = cubeSize / 2f;

        // Определяем 8 вершин куба в ЛОКАЛЬНЫХ координатах (относительно центра текущего GameObject)
        Vector3 p1 = new Vector3(-halfSize, -halfSize, -halfSize); // Back Bottom Left
        Vector3 p2 = new Vector3(-halfSize, -halfSize, halfSize);  // Front Bottom Left
        Vector3 p3 = new Vector3(halfSize, -halfSize, halfSize);   // Front Bottom Right
        Vector3 p4 = new Vector3(halfSize, -halfSize, -halfSize);  // Back Bottom Right
        Vector3 p5 = new Vector3(-halfSize, halfSize, -halfSize);  // Back Top Left
        Vector3 p6 = new Vector3(-halfSize, halfSize, halfSize);   // Front Top Left
        Vector3 p7 = new Vector3(halfSize, halfSize, halfSize);    // Front Top Right
        Vector3 p8 = new Vector3(halfSize, halfSize, -halfSize);   // Back Top Right

        // Создаем и настраиваем Line Renderers для 12 ребер куба
        // Используем ЛОКАЛЬНЫЕ координаты для правильного спавна и масштабирования

        // Нижняя грань
        DrawLine(p1, p2);
        DrawLine(p2, p3);
        DrawLine(p3, p4);
        DrawLine(p4, p1);

        // Верхняя грань
        DrawLine(p5, p6);
        DrawLine(p6, p7);
        DrawLine(p7, p8);
        DrawLine(p8, p5);

        // Вертикальные ребра
        DrawLine(p1, p5);
        DrawLine(p2, p6);
        DrawLine(p3, p7);
        DrawLine(p4, p8);
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        // Создаем новый дочерний GameObject для каждого ребра
        GameObject lineGO = new GameObject("LineSegment");
        lineGO.transform.parent = this.transform; // Делаем текущий объект родителем
        lineGO.transform.localPosition = Vector3.zero;

        LineRenderer lr = lineGO.AddComponent<LineRenderer>();

        // Настраиваем Line Renderer
        lr.material = lineMaterial;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = 2;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;

        // !!! Ключевое отличие: используем локальные координаты !!!
        // Это позволяет всему родительскому объекту перемещаться, вращаться и спавниться как единое целое.
        lr.useWorldSpace = false;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lineGO.isStatic = true;
    }
    void DisplayPositionText()
    {
        // 1. Создаем новый дочерний объект для текста
        GameObject textGO = new GameObject("PositionText");
        textGO.transform.parent = this.transform;

        // 2. Добавляем компонент TextMeshPro
        TextMeshPro tmPro = textGO.AddComponent<TextMeshPro>();

        // 3. Форматируем текст с текущей мировой позицией родительского объекта
        Vector3 pos = transform.position;
        tmPro.text = $"X:{pos.x/10}\nY:{pos.y/10}\nZ:{pos.z/10}";
        tmPro.fontSize = textSize;
        tmPro.alignment = TextAlignmentOptions.Center; // Выравнивание по центру

        // 4. Позиционируем объект с текстом точно в центре родительского объекта
        textGO.transform.localPosition = Vector3.zero;

        // 5. Настраиваем TextMeshProUGUI, чтобы текст всегда смотрел на камеру (billboard effect)
        tmPro.transform.rotation = Quaternion.identity;

        // Дополнительная настройка, чтобы текст был виден на фоне
        tmPro.color = Color.white;
        textGO.isStatic = true;
    }
}