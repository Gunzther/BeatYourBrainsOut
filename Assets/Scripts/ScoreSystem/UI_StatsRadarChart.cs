using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.ScoreSystem
{
    public class UI_StatsRadarChart : MonoBehaviour {

    [SerializeField] private Material radarMaterial;
    [SerializeField] private Texture2D radarTexture2D;

    private Scores scores;
    private CanvasRenderer radarMeshCanvasRenderer;

    private void Awake() {
        radarMeshCanvasRenderer = transform.Find("radarMesh").GetComponent<CanvasRenderer>();
    }

    public void SetStats(Scores scores) {
        this.scores = scores;
        scores.OnScoresChanged += Scores_OnScoresChanged;
        UpdateScoresVisual();
    }

    private void Scores_OnScoresChanged(object sender, System.EventArgs e) {
        UpdateScoresVisual();
    }

    private void UpdateScoresVisual() {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[3 * 5];

        float angleIncrement = 360f / 5;
        float radarChartSize = 145f;

        Vector3 damageDealScoreVertex = Quaternion.Euler(0, 0, -angleIncrement * 0) * Vector3.up * radarChartSize * scores.GetScoreAmountNormalized(Scores.Type.DamageDealScore);
        int damageDealVertexIndex = 1;
        Vector3 damageReceivedScoreVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * radarChartSize * scores.GetScoreAmountNormalized(Scores.Type.DamageReceivedScore);
        int damageReceivedScoreVertexIndex = 2;
        Vector3 craftingDoneScoreVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * Vector3.up * radarChartSize * scores.GetScoreAmountNormalized(Scores.Type.CraftingDoneScore);
        int craftingDoneScoreVertexIndex = 3;
        Vector3 jukingDoneScoreVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * Vector3.up * radarChartSize * scores.GetScoreAmountNormalized(Scores.Type.JukingDoneScore);
        int jukingDoneScoreVertexIndex = 4;
        Vector3 healingDoneScoreVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * Vector3.up * radarChartSize * scores.GetScoreAmountNormalized(Scores.Type.HealingDoneScore);
        int healingDoneScoreVertexIndex = 5;

        vertices[0] = Vector3.zero;
        vertices[damageDealVertexIndex]  = damageDealScoreVertex;
        vertices[damageReceivedScoreVertexIndex] = damageReceivedScoreVertex;
        vertices[craftingDoneScoreVertexIndex]   = craftingDoneScoreVertex;
        vertices[jukingDoneScoreVertexIndex]    = jukingDoneScoreVertex;
        vertices[healingDoneScoreVertexIndex]  = healingDoneScoreVertex;

        uv[0]                   = Vector2.zero;
        uv[damageDealVertexIndex]   = Vector2.one;
        uv[damageReceivedScoreVertexIndex]  = Vector2.one;
        uv[craftingDoneScoreVertexIndex]    = Vector2.one;
        uv[jukingDoneScoreVertexIndex]     = Vector2.one;
        uv[healingDoneScoreVertexIndex]   = Vector2.one;

        triangles[0] = 0;
        triangles[1] = damageDealVertexIndex;
        triangles[2] = damageReceivedScoreVertexIndex;

        triangles[3] = 0;
        triangles[4] = damageReceivedScoreVertexIndex;
        triangles[5] = craftingDoneScoreVertexIndex;

        triangles[6] = 0;
        triangles[7] = craftingDoneScoreVertexIndex;
        triangles[8] = jukingDoneScoreVertexIndex;

        triangles[9]  = 0;
        triangles[10] = jukingDoneScoreVertexIndex;
        triangles[11] = healingDoneScoreVertexIndex;

        triangles[12] = 0;
        triangles[13] = healingDoneScoreVertexIndex;
        triangles[14] = damageDealVertexIndex;
        triangles[14] = damageDealVertexIndex;


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        radarMeshCanvasRenderer.SetMesh(mesh);
        radarMeshCanvasRenderer.SetMaterial(radarMaterial, radarTexture2D);
    }

}
}

