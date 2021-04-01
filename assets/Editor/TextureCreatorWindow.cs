using System.IO;
using UnityEditor;
using UnityEngine;

public class TextureCreatorWindow : EditorWindow {

    string filename = "myProceduralTexture";
    float perlinXScale;
    float perlinYScale;
    int perlinOctaves;
    float perlinPersistance;
    float perlinHeightScale;
    int perlinOffsetX;
    int perlinOffsetY;
    bool alphaToggle = false;
    bool seamlessToggle = false;
    bool mapToggle = false;

    float brightness = 0.5f;
    float contrast = 0.5f;

    Texture2D pTexture;

    [MenuItem("Window/TextureCreatorWindow")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(TextureCreatorWindow));

    }

    void OnEnable() {
        pTexture = new Texture2D(513, 513, TextureFormat.ARGB32, false);
    }

    void OnGUI() {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        filename = EditorGUILayout.TextField("Texture Name", filename);

        int wSize = (int)(EditorGUIUtility.currentViewWidth - 100);

        perlinXScale = EditorGUILayout.Slider("X Scale", perlinXScale, 0, 0.1f);
        perlinYScale = EditorGUILayout.Slider("Y Scale", perlinYScale, 0, 0.1f);
        perlinOctaves = EditorGUILayout.IntSlider("Octaves", perlinOctaves, 1, 10);
        perlinPersistance = EditorGUILayout.Slider("Persistance", perlinPersistance, 1, 10);
        perlinHeightScale = EditorGUILayout.Slider("Height Scale", perlinHeightScale, 0, 1);
        perlinOffsetX = EditorGUILayout.IntSlider("Offset X", perlinOffsetX, 0, 10000);
        perlinOffsetY = EditorGUILayout.IntSlider("Offset Y", perlinOffsetY, 0, 10000);
        brightness = EditorGUILayout.Slider("Brightness", brightness, 0, 2);
        contrast = EditorGUILayout.Slider("Contrast", contrast, 0, 2);
        alphaToggle = EditorGUILayout.Toggle("Alpha?", alphaToggle);
        mapToggle = EditorGUILayout.Toggle("Map?", mapToggle);
        seamlessToggle = EditorGUILayout.Toggle("Seamless", seamlessToggle);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        float minColour = 1;
        float maxColour = 0;

        if (GUILayout.Button("Generate", GUILayout.Width(wSize))) {
            int w = 513;
            int h = 513;
            float pValue;

            Color pixCol = Color.white;

            for (int y = 0; y < h; y++) {
                for (int x = 0; x < w; x++) {
                    /*if (seamlessToggle) {
                        float u = (float)x / (float)w;
                        float v = (float)y / (float)h;
                        float noise00 = Utils.fBM((x + perlinOffsetX) * perlinXScale,
                                                  (y + perlinOffsetY) * perlinYScale,
                                            perlinOctaves,
                                            perlinPersistance) * perlinHeightScale;
                        float noise01 = Utils.fBM((x + perlinOffsetX) * perlinXScale,
                                                  (y + perlinOffsetY + h) * perlinYScale,
                                            perlinOctaves,
                                            perlinPersistance) * perlinHeightScale;
                        float noise10 = Utils.fBM((x + perlinOffsetX + w) * perlinXScale,
                                                  (y + perlinOffsetY) * perlinYScale,
                                            perlinOctaves,
                                            perlinPersistance) * perlinHeightScale;
                        float noise11 = Utils.fBM((x + perlinOffsetX + w) * perlinXScale,
                                                  (y + perlinOffsetY + h) * perlinYScale,
                                            perlinOctaves,
                                            perlinPersistance) * perlinHeightScale;
                        float noiseTotal = u * v * noise00 +
                                        u * (1 - v) * noise01 +
                                        (1 - u) * v * noise10 +
                                        (1 - u) * (1 - v) * noise11;

                        float value = (int)(256 * noiseTotal) + 50;
                        float r = Mathf.Clamp((int)noise00, 0, 255);

                        float g = Mathf.Clamp(value, 0, 255);
                        float b = Mathf.Clamp(value + 50, 0, 255);
                        float a = Mathf.Clamp(value + 100, 0, 255);

                        pValue = (r + g + b) / (3 * 255.0f);
                    }*/

                    if (seamlessToggle) {

                        float u = (float)x / (float)w; // 0..1 0.4582
                        float v = (float)y / (float)h; // 0..1 0.9987

                        float noise00 = Utils.fBM( // unten links (u0, v0)

                                         (x + perlinOffsetX) * perlinXScale,
                                         (y + perlinOffsetY) * perlinYScale,
                                         perlinOctaves, perlinPersistance

                                      ) * perlinHeightScale;

                        float noise01 = Utils.fBM( // oben links (u0, v1)

                                         (x + perlinOffsetX) * perlinXScale,
                                         (y + perlinOffsetY + h) * perlinYScale,
                                         perlinOctaves, perlinPersistance

                                      ) * perlinHeightScale;

                        float noise10 = Utils.fBM( // unten rechts (u1, v0)

                                         (x + perlinOffsetX + w) * perlinXScale,
                                         (y + perlinOffsetY) * perlinYScale,
                                         perlinOctaves, perlinPersistance

                                      ) * perlinHeightScale;

                        float noise11 = Utils.fBM( // oben rechts (u1, v1)

                                         (x + perlinOffsetX + w) * perlinXScale,
                                         (y + perlinOffsetY + h) * perlinYScale,
                                         perlinOctaves, perlinPersistance

                                      ) * perlinHeightScale;

                        float noiseTotal = (u * v * noise00)
                                         + (u * (1 - v) * noise01)
                                         + ((1 - u) * v * noise10)
                                         + ((1 - u) * (1 - v) * noise11);

                        var value = (256 * noiseTotal) + 50;

                        var r = Mathf.Clamp(noise00, 0, 255);
                        var g = Mathf.Clamp(value, 0, 255);
                        var b = Mathf.Clamp(value + 50, 0, 255);
                        var a = Mathf.Clamp(value + 100, 0, 255);

                        pValue = (r + g + b) / (3 * 255.0f);

                    } else {
                        pValue = Utils.fBM((x + perlinOffsetX) * perlinXScale,
                                           (y + perlinOffsetY) * perlinYScale,
                                                perlinOctaves,
                                                perlinPersistance) * perlinHeightScale;
                    }

                    float colValue = contrast * (pValue - 0.5f) + 0.5f * brightness;
                    if (minColour > colValue) minColour = colValue;
                    if (maxColour < colValue) maxColour = colValue;
                    pixCol = new Color(colValue, colValue, colValue, alphaToggle ? colValue : 1);
                    pTexture.SetPixel(x, y, pixCol);
                }
            }
            if (mapToggle) {
                for (int y = 0; y < h; y++) {
                    for (int x = 0; x < w; x++) {
                        pixCol = pTexture.GetPixel(x, y);
                        float colValue = pixCol.r;
                        colValue = Utils.Map(colValue, minColour, maxColour, 0, 1);
                        pixCol.r = colValue;
                        pixCol.g = colValue;
                        pixCol.b = colValue;
                        pTexture.SetPixel(x, y, pixCol);
                    }
                }
            }
            pTexture.Apply(false, false);
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(pTexture, GUILayout.Width(wSize), GUILayout.Height(wSize));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Save", GUILayout.Width(wSize))) {
            byte[] bytes = pTexture.EncodeToPNG();
            System.IO.Directory.CreateDirectory(Application.dataPath + "/SavedTextures");
            File.WriteAllBytes(Application.dataPath + "/SavedTextures/" + filename + ".png", bytes);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
