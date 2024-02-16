using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Custom class that derives from AssetPostprocessor
public class FennelAssetProcessor : AssetPostprocessor {
    // Static method called after the import process for all assets
    static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths) {
        var changed = new List<TextAsset>();
        
        // Loop through all imported assets
        foreach (string path in importedAssets)
        {
            // Check if the asset is a Text Asset with the suffix "fnl"
            if (path.EndsWith(".fnl"))
            {
                // Perform your custom logic here
                Debug.Log($"Custom logic for reimported Text Asset: {path}");

                // For example, load the Text Asset and process its content
                TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                if (textAsset != null)
                {
                    // Process the Text Asset's content
                    ProcessTextAssetContent(textAsset);
                }
                
                changed.Add(textAsset);
                var lua = Component.FindObjectOfType<NLuaController>();

                foreach (var asset in changed) {
                    lua.RefreshFennelScript(asset.name);
                }
            }
        }
    }

    // Example method to process the content of a Text Asset
    static void ProcessTextAssetContent(TextAsset textAsset)
    {
        // Implement your processing logic here
        Debug.Log($"Processing content of Text Asset: {textAsset.name}");
        // For example, log the content to the console
        Debug.Log(textAsset.text);
    }
}