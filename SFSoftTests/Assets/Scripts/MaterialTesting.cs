using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTesting : MonoBehaviour {
	public string[] MaterialsToLoad = { "Materials/1", "Materials/2", "Materials/3", "Materials/4" };

	public UnityEngine.UI.Text textField;
	public MeshRenderer meshRenderer;

	private Material[] materials;

	private int currSelectedIndex = 0;
	private int currSelectedFolderIndex = 0;
	void Start () {
		this.LoadAllMaterials();
		this.PrintShaderToMaterialTable();

		this.LoadMaterialFolder();
		textField.text = this.GetCurrSelectedDesc();
		meshRenderer.material = this.GetSelectedMaterial();
	}

	private void PrintShaderToMaterialTable() {
		Dictionary<string, List<Material>> shaderToMaterials = new Dictionary<string, List<Material>>();
		foreach(Material mat in materials) {
			string shaderName = mat.shader.name;
			if(shaderToMaterials.ContainsKey(shaderName)) {
				shaderToMaterials[shaderName].Add(mat);
			} else {
				List<Material> newList = new List<Material>();
				newList.Add(mat);
				shaderToMaterials.Add(shaderName, newList);
			}
		}

		string output = "";
		foreach(KeyValuePair<string, List<Material>> pair in shaderToMaterials) {
			output += pair.Key + ": ";
			foreach(Material item in pair.Value) {
				output += item.name + ", ";
			}
			output += "\n";
		}

		Debug.Log(output);
	}

	private void LoadAllMaterials() {
		materials = Resources.LoadAll<Material>("Materials");
	}

	private void LoadMaterialFolder() {
		if(materials != null) {
			materials = null;
			System.GC.Collect();
			Resources.UnloadUnusedAssets();
		}
		materials = Resources.LoadAll<Material>(MaterialsToLoad[currSelectedFolderIndex]);
	}

	public void SelectNext() {
		if(currSelectedIndex+1 < materials.Length) {
			currSelectedIndex++;
		} else {
			currSelectedIndex = 0;
			this.IncrementFolderIndex();
			this.LoadMaterialFolder();
		}

		textField.text = this.GetCurrSelectedDesc();
		meshRenderer.material = this.GetSelectedMaterial();
	}

	public void SelectPrevious() {
		if(currSelectedIndex-1 < 0) {
			this.DecrementFolderIndex();
			this.LoadMaterialFolder();
			currSelectedIndex = materials.Length-1;
		} else {
			currSelectedIndex--;
		}

		textField.text = this.GetCurrSelectedDesc();
		meshRenderer.material = this.GetSelectedMaterial();
	}

	private void IncrementFolderIndex() {
		if(currSelectedFolderIndex + 1 < MaterialsToLoad.Length) {
			currSelectedFolderIndex++;
		} else {
			currSelectedFolderIndex = 0;
		}
	}

	private void DecrementFolderIndex() {
		if(currSelectedFolderIndex - 1 < 0) {
			currSelectedFolderIndex = MaterialsToLoad.Length-1;
		} else {
			currSelectedFolderIndex--;
		}
	}

	public Material GetSelectedMaterial() {
		return materials[currSelectedIndex];
	}

	public string GetCurrSelectedDesc() {
		return string.Format("Folder {0}/{1}\n{2}/{3}\nMaterial: {4}\nShader: {5}", (currSelectedFolderIndex+1), MaterialsToLoad.Length, (currSelectedIndex+1), materials.Length, materials[currSelectedIndex].name, materials[currSelectedIndex].shader.name);
	}
}
