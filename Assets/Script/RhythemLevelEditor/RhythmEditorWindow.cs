// RhythmEditorWindow.cs
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using RhythmNamespace;

public class RhythmEditorWindow : EditorWindow
{
    private List<RhythmData> rhythms = new List<RhythmData>();
    private Vector2 scrollPos;

    [MenuItem("Tools/Rhythm Editor")]
    public static void OpenWindow()
    {
        GetWindow<RhythmEditorWindow>("Rhythm Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Rhythm Editor", EditorStyles.boldLabel);

        if (GUILayout.Button("Add New Rhythm"))
        {
            rhythms.Add(new RhythmData() { name = "New Rhythm", bpm = 120, pattern = new int[16] });
        }

        scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(400));

        for (int i = 0; i < rhythms.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label($"Rhythm {i + 1}", EditorStyles.boldLabel);

            rhythms[i].name = EditorGUILayout.TextField("Name", rhythms[i].name);
            rhythms[i].bpm = EditorGUILayout.IntField("BPM", rhythms[i].bpm);

            GUILayout.Label("Pattern (16 Steps)");
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 16; j++)
            {
                rhythms[i].pattern[j] = EditorGUILayout.Toggle(rhythms[i].pattern[j] == 1) ? 1 : 0;
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Remove Rhythm"))
            {
                rhythms.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndVertical();
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Save to JSON"))
        {
            SaveToJson();
        }

        if (GUILayout.Button("Load from JSON"))
        {
            LoadFromJson();
        }
    }

    private void SaveToJson()
    {
        string path = EditorUtility.SaveFilePanel("Save Rhythm JSON", "", "Rhythms.json", "json");
        if (string.IsNullOrEmpty(path)) return;

        RhythmFile rhythmFile = new RhythmFile { rhythms = rhythms };
        string json = JsonUtility.ToJson(rhythmFile, true);
        File.WriteAllText(path, json);
        EditorUtility.DisplayDialog("Save Successful", "Rhythm data has been saved.", "OK");
    }

    private void LoadFromJson()
    {
        string path = EditorUtility.OpenFilePanel("Load Rhythm JSON", "", "json");
        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        RhythmFile rhythmFile = JsonUtility.FromJson<RhythmFile>(json);
        rhythms = rhythmFile.rhythms;
        EditorUtility.DisplayDialog("Load Successful", "Rhythm data has been loaded.", "OK");
    }
}
