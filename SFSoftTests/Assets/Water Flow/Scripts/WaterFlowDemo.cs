// Water Flow
// Version: 1.1.0
//
// Author:	Gold Experience Team (http://www.ge-team.com)
// Details:	https://www.assetstore.unity3d.com/en/#!/content/26430
// Support:	geteamdev@gmail.com
//
// Please direct any bugs/comments/suggestions to support e-mail.

#region Namespaces
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#endregion

// ######################################################################
// WaterFlowDemo class
//
// This class handles switching demo water, displays and updates GUI of water shader.
// ######################################################################

public class WaterFlowDemo : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Water Simple
	Color m_WaterSimple_Color;
	float m_WaterSimple_UMoveSpeed;
	float m_WaterSimple_VMoveSpeed;
	Color m_WaterSimpleOriginal_Color;
	float m_WaterSimpleOriginal_UMoveSpeed;
	float m_WaterSimpleOriginal_VMoveSpeed;

	// Water Diffuse
	Color m_WaterDiffuse_Color;
	float m_WaterDiffuse_Multiply;
	float m_WaterDiffuse_UMoveSpeed;
	float m_WaterDiffuse_VMoveSpeed;
	Color m_WaterDiffuseOriginal_Color;
	float m_WaterDiffuseOriginal_Multiply;
	float m_WaterDiffuseOriginal_UMoveSpeed;
	float m_WaterDiffuseOriginal_VMoveSpeed;

	// Water Heightmap
	int m_WaterHeightMap1Index;
	int m_WaterHeightMap2Index;
	float m_WaterHeightmap_WaterTile;
	float m_WaterHeightmap_HeightmapTile;
	float m_WaterHeightmap_Refraction;
	float m_WaterHeightmap_Strength;
	float m_WaterHeightmap_Multiply;
	float m_WaterHeightmapOriginal_WaterTile;
	float m_WaterHeightmapOriginal_HeightmapTile;
	float m_WaterHeightmapOriginal_Refraction;
	float m_WaterHeightmapOriginal_Strength;
	float m_WaterHeightmapOriginal_Multiply;

	// Water game objects
	public GameObject m_SimpleWater, m_DiffuseWater, m_HightmapWater;

	// Water active buttons
	public Button m_SimpleWaterButton, m_DiffuseWaterButton, m_HightmapWaterButton;

	// Water Setting panels
	public GameObject m_SimpleWaterSettingsPanel, m_DiffuseWaterSettingsPanel, m_HightmapWaterSettingsPanel;

	// Sliders
	public Slider m_SimpleR, m_SimpleG, m_SimpleB, m_SimpleA, m_SimpleUSpeed, m_SimpleVSpeed;
	public Slider m_DiffuseR, m_DiffuseG, m_DiffuseB, m_DiffuseA, m_DiffuseMultiply, m_DiffuseUSpeed, m_DiffuseVSpeed;
	public Slider m_HmTextures, m_HmHightmaps, m_HmReflaction, m_HmStrength, m_HmMultiply;

	#endregion

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{

		// Get information from Simple water's material.
		m_WaterSimpleOriginal_Color = m_SimpleWater.GetComponent<Renderer>().material.GetColor("_Color");
		m_WaterSimpleOriginal_UMoveSpeed = m_SimpleWater.GetComponent<Renderer>().material.GetFloat("_MoveSpeedU");
		m_WaterSimpleOriginal_VMoveSpeed = m_SimpleWater.GetComponent<Renderer>().material.GetFloat("_MoveSpeedV");
		m_WaterSimple_Color = m_WaterSimpleOriginal_Color;
		m_WaterSimple_UMoveSpeed = m_WaterSimpleOriginal_UMoveSpeed;
		m_WaterSimple_VMoveSpeed = m_WaterSimpleOriginal_VMoveSpeed;

		// Get information from Diffuse water's material.
		m_WaterDiffuseOriginal_Color = m_DiffuseWater.GetComponent<Renderer>().material.GetColor("_Color");
		m_WaterDiffuseOriginal_Multiply = m_DiffuseWater.GetComponent<Renderer>().material.GetFloat("_Multiply");
		m_WaterDiffuseOriginal_UMoveSpeed = m_DiffuseWater.GetComponent<Renderer>().material.GetFloat("_MoveSpeedU");
		m_WaterDiffuseOriginal_VMoveSpeed = m_DiffuseWater.GetComponent<Renderer>().material.GetFloat("_MoveSpeedV");
		m_WaterDiffuse_Color = m_WaterDiffuseOriginal_Color;
		m_WaterDiffuse_Multiply = m_WaterDiffuseOriginal_Multiply;
		m_WaterDiffuse_UMoveSpeed = m_WaterDiffuseOriginal_UMoveSpeed;
		m_WaterDiffuse_VMoveSpeed = m_WaterDiffuseOriginal_VMoveSpeed;

		// Get information from Heightmap water's material.
		m_WaterHeightmapOriginal_WaterTile = m_HightmapWater.GetComponent<Renderer>().material.GetFloat("_MainTexTile");
		m_WaterHeightmapOriginal_HeightmapTile = m_HightmapWater.GetComponent<Renderer>().material.GetFloat("_HeightMapTile");
		m_WaterHeightmapOriginal_Refraction = m_HightmapWater.GetComponent<Renderer>().material.GetFloat("_MainTexRefraction");
		m_WaterHeightmapOriginal_Strength = m_HightmapWater.GetComponent<Renderer>().material.GetFloat("_HeightMapStrength");
		m_WaterHeightmapOriginal_Multiply = m_HightmapWater.GetComponent<Renderer>().material.GetFloat("_HeightMapMultiply");
		m_WaterHeightmap_WaterTile = m_WaterHeightmapOriginal_WaterTile;
		m_WaterHeightmap_HeightmapTile = m_WaterHeightmapOriginal_HeightmapTile;
		m_WaterHeightmap_Refraction = m_WaterHeightmapOriginal_Refraction;
		m_WaterHeightmap_Strength = m_WaterHeightmapOriginal_Strength;
		m_WaterHeightmap_Multiply = m_WaterHeightmapOriginal_Multiply;

		// Setup Simple water sliders
		m_SimpleR.minValue = 0.0f; m_SimpleR.maxValue = 1.0f; m_SimpleR.value = m_WaterSimpleOriginal_Color.r; m_SimpleR.onValueChanged.AddListener(delegate { OnSlider_SimpleR(m_SimpleR.value); });
		m_SimpleG.minValue = 0.0f; m_SimpleG.maxValue = 1.0f; m_SimpleG.value = m_WaterSimpleOriginal_Color.g; m_SimpleG.onValueChanged.AddListener(delegate { OnSlider_SimpleG(m_SimpleG.value); });
		m_SimpleB.minValue = 0.0f; m_SimpleB.maxValue = 1.0f; m_SimpleB.value = m_WaterSimpleOriginal_Color.b; m_SimpleB.onValueChanged.AddListener(delegate { OnSlider_SimpleB(m_SimpleB.value); });
		m_SimpleA.minValue = 0.0f; m_SimpleA.maxValue = 1.0f; m_SimpleA.value = m_WaterSimpleOriginal_Color.a; m_SimpleA.onValueChanged.AddListener(delegate { OnSlider_SimpleA(m_SimpleA.value); });
		m_SimpleUSpeed.minValue = -6.0f; m_SimpleUSpeed.maxValue = 6.0f; m_SimpleUSpeed.value = m_WaterSimpleOriginal_UMoveSpeed; m_SimpleUSpeed.onValueChanged.AddListener(delegate { OnSlider_SimpleUSpeed(m_SimpleUSpeed.value); });
		m_SimpleVSpeed.minValue = -6.0f; m_SimpleVSpeed.maxValue = 6.0f; m_SimpleVSpeed.value = m_WaterSimpleOriginal_VMoveSpeed; m_SimpleVSpeed.onValueChanged.AddListener(delegate { OnSlider_SimpleVSpeed(m_SimpleVSpeed.value); });

		// Setup Diffuse water sliders
		m_DiffuseR.minValue = 0.0f; m_DiffuseR.maxValue = 1.0f; m_DiffuseR.value = m_WaterDiffuseOriginal_Color.r; m_DiffuseR.onValueChanged.AddListener(delegate { OnSlider_DiffuseR(m_DiffuseR.value); });
		m_DiffuseG.minValue = 0.0f; m_DiffuseG.maxValue = 1.0f; m_DiffuseG.value = m_WaterDiffuseOriginal_Color.g; m_DiffuseG.onValueChanged.AddListener(delegate { OnSlider_DiffuseG(m_DiffuseG.value); });
		m_DiffuseB.minValue = 0.0f; m_DiffuseB.maxValue = 1.0f; m_DiffuseB.value = m_WaterDiffuseOriginal_Color.b; m_DiffuseB.onValueChanged.AddListener(delegate { OnSlider_DiffuseB(m_DiffuseB.value); });
		m_DiffuseA.minValue = 0.0f; m_DiffuseA.maxValue = 1.0f; m_DiffuseA.value = m_WaterDiffuseOriginal_Color.a; m_DiffuseA.onValueChanged.AddListener(delegate { OnSlider_DiffuseA(m_DiffuseA.value); });
		m_DiffuseMultiply.minValue = 0.0f; m_DiffuseMultiply.maxValue = 5.0f; m_DiffuseMultiply.value = m_WaterDiffuseOriginal_Multiply; m_DiffuseMultiply.onValueChanged.AddListener(delegate { OnSlider_DiffuseMultiply(m_DiffuseMultiply.value); });
		m_DiffuseUSpeed.minValue = -6.0f; m_DiffuseUSpeed.maxValue = 6.0f; m_DiffuseUSpeed.value = m_WaterDiffuseOriginal_UMoveSpeed; m_DiffuseUSpeed.onValueChanged.AddListener(delegate { OnSlider_DiffuseUSpeed(m_DiffuseUSpeed.value); });
		m_DiffuseVSpeed.minValue = -6.0f; m_DiffuseVSpeed.maxValue = 6.0f; m_DiffuseVSpeed.value = m_WaterDiffuseOriginal_VMoveSpeed; m_DiffuseVSpeed.onValueChanged.AddListener(delegate { OnSlider_DiffuseVSpeed(m_DiffuseVSpeed.value); });

		// Setup Hightmap water sliders
		m_HmTextures.minValue = 0.25f; m_HmTextures.maxValue = 5.0f; m_HmTextures.value = m_WaterHeightmapOriginal_WaterTile; m_HmTextures.onValueChanged.AddListener(delegate { OnSlider_HmTextures(m_HmTextures.value); });
		m_HmHightmaps.minValue = 0.25f; m_HmHightmaps.maxValue = 5.0f; m_HmHightmaps.value = m_WaterHeightmapOriginal_HeightmapTile; m_HmHightmaps.onValueChanged.AddListener(delegate { OnSlider_HmHightmaps(m_HmHightmaps.value); });
		m_HmReflaction.minValue = 0.1f; m_HmReflaction.maxValue = 5.0f; m_HmReflaction.value = m_WaterHeightmapOriginal_Refraction; m_HmReflaction.onValueChanged.AddListener(delegate { OnSlider_HmReflaction(m_HmReflaction.value); });
		m_HmStrength.minValue = 0.2f; m_HmStrength.maxValue = 5.0f; m_HmStrength.value = m_WaterHeightmapOriginal_Strength; m_HmStrength.onValueChanged.AddListener(delegate { OnSlider_HmStrength(m_HmStrength.value); });
		m_HmMultiply.minValue = 0.05f; m_HmMultiply.maxValue = 0.5f; m_HmMultiply.value = m_WaterHeightmapOriginal_Multiply; m_HmMultiply.onValueChanged.AddListener(delegate { OnSlider_HmMultiply(m_HmMultiply.value); });

		// Initial Water buttons
		m_SimpleWaterButton.interactable = false;
		m_DiffuseWaterButton.interactable = true;
		m_HightmapWaterButton.interactable = true;

		// Update Water game objects and Setting panels
		UpdateWaterObjectsAndWaterSettingsPanels();

	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// Simple Water Slider functions
	// ########################################

	#region Simple Water Slider

	// Red color
	void OnSlider_SimpleR(float value)
	{
		m_WaterSimple_Color = new Color(value,
									m_WaterSimple_Color.g,
									m_WaterSimple_Color.b,
									m_WaterSimple_Color.a);
		m_SimpleWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterSimple_Color);
	}

	// Green color
	void OnSlider_SimpleG(float value)
	{
		m_WaterSimple_Color = new Color(m_WaterSimple_Color.r,
									value,
									m_WaterSimple_Color.b,
									m_WaterSimple_Color.a);
		m_SimpleWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterSimple_Color);
	}

	// Blue color
	void OnSlider_SimpleB(float value)
	{
		m_WaterSimple_Color = new Color(m_WaterSimple_Color.r,
									m_WaterSimple_Color.g,
									value,
									m_WaterSimple_Color.a);
		m_SimpleWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterSimple_Color);
	}

	// Alpha
	void OnSlider_SimpleA(float value)
	{
		m_WaterSimple_Color = new Color(m_WaterSimple_Color.r,
									m_WaterSimple_Color.g,
									m_WaterSimple_Color.b,
									value);
		m_SimpleWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterSimple_Color);
	}

	// U speed
	void OnSlider_SimpleUSpeed(float value)
	{
		m_WaterSimple_UMoveSpeed = value;
		m_SimpleWater.GetComponent<Renderer>().material.SetFloat("_MoveSpeedU", m_WaterSimple_UMoveSpeed);
	}

	// V speed
	void OnSlider_SimpleVSpeed(float value)
	{
		m_WaterSimple_VMoveSpeed = value;
		m_SimpleWater.GetComponent<Renderer>().material.SetFloat("_MoveSpeedV", m_WaterSimple_VMoveSpeed);
	}

	#endregion // Simple Water Slider

	// ########################################
	// Diffuse Water Slider functions
	// ########################################

	#region Diffuse Water Slider

	// Red color
	void OnSlider_DiffuseR(float value)
	{
		m_WaterDiffuse_Color = new Color(value,
									m_WaterDiffuse_Color.g,
									m_WaterDiffuse_Color.b,
									m_WaterDiffuse_Color.a);
		m_DiffuseWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterDiffuse_Color);
	}

	// Green color
	void OnSlider_DiffuseG(float value)
	{
		m_WaterDiffuse_Color = new Color(m_WaterDiffuse_Color.r,
									value,
									m_WaterDiffuse_Color.b,
									m_WaterDiffuse_Color.a);
		m_DiffuseWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterDiffuse_Color);
	}

	// Blue color
	void OnSlider_DiffuseB(float value)
	{
		m_WaterDiffuse_Color = new Color(m_WaterDiffuse_Color.r,
									m_WaterDiffuse_Color.g,
									value,
									m_WaterDiffuse_Color.a);
		m_DiffuseWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterDiffuse_Color);
	}

	// Alpha
	void OnSlider_DiffuseA(float value)
	{
		m_WaterDiffuse_Color = new Color(m_WaterDiffuse_Color.r,
									m_WaterDiffuse_Color.g,
									m_WaterDiffuse_Color.b,
									value);
		m_DiffuseWater.GetComponent<Renderer>().material.SetColor("_Color", m_WaterDiffuse_Color);
	}

	// Color multiply
	void OnSlider_DiffuseMultiply(float value)
	{
		m_WaterDiffuse_Multiply = value;
		m_DiffuseWater.GetComponent<Renderer>().material.SetFloat("_Multiply", m_WaterDiffuse_Multiply);
	}

	// U speed
	void OnSlider_DiffuseUSpeed(float value)
	{
		m_WaterDiffuse_UMoveSpeed = value;
		m_DiffuseWater.GetComponent<Renderer>().material.SetFloat("_MoveSpeedU", m_WaterDiffuse_UMoveSpeed);
	}

	// V speed
	void OnSlider_DiffuseVSpeed(float value)
	{
		m_WaterDiffuse_VMoveSpeed = value;
		m_DiffuseWater.GetComponent<Renderer>().material.SetFloat("_MoveSpeedV", m_WaterDiffuse_VMoveSpeed);
	}

	#endregion // Diffuse Water Slider

	// ########################################
	// Hightmap Water Slider functions
	// ########################################

	#region Hightmap Water Slider

	// Hightmap textures
	void OnSlider_HmTextures(float value)
	{
		m_WaterHeightmap_WaterTile = value;
		m_HightmapWater.GetComponent<Renderer>().material.SetFloat("_MainTexTile", m_WaterHeightmap_WaterTile);
	}

	// Hightmap tile
	void OnSlider_HmHightmaps(float value)
	{
		m_WaterHeightmap_HeightmapTile = value;
		m_HightmapWater.GetComponent<Renderer>().material.SetFloat("_HeightMapTile", m_WaterHeightmap_HeightmapTile);
	}

	// Reflaction
	void OnSlider_HmReflaction(float value)
	{
		m_WaterHeightmap_Refraction = value;
		m_HightmapWater.GetComponent<Renderer>().material.SetFloat("_MainTexRefraction", m_WaterHeightmap_Refraction);
	}

	// Strength
	void OnSlider_HmStrength(float value)
	{
		m_WaterHeightmap_Strength = value;
		m_HightmapWater.GetComponent<Renderer>().material.SetFloat("_HeightMapStrength", m_WaterHeightmap_Strength);
	}

	// Multiply
	void OnSlider_HmMultiply(float value)
	{
		m_WaterHeightmap_Multiply = value;
		m_HightmapWater.GetComponent<Renderer>().material.SetFloat("_HeightMapMultiply", m_WaterHeightmap_Multiply);
	}

	#endregion // Hightmap Water Slider

	// ########################################
	// GUI Window functions
	// ########################################

	#region GUI Window

	// Simple Water Panel
	public void OnSimpleWater()
	{
		m_SimpleWaterButton.interactable = false;
		m_DiffuseWaterButton.interactable = true;
		m_HightmapWaterButton.interactable = true;

		UpdateWaterObjectsAndWaterSettingsPanels();
	}

	// Diffuse Water Panel
	public void OnDiffuseWater()
	{
		m_SimpleWaterButton.interactable = true;
		m_DiffuseWaterButton.interactable = false;
		m_HightmapWaterButton.interactable = true;

		UpdateWaterObjectsAndWaterSettingsPanels();
	}

	// Hightmap Water Panel
	public void OnHightmapWater()
	{
		m_SimpleWaterButton.interactable = true;
		m_DiffuseWaterButton.interactable = true;
		m_HightmapWaterButton.interactable = false;

		UpdateWaterObjectsAndWaterSettingsPanels();
	}

	// Water and Setting Panel
	void UpdateWaterObjectsAndWaterSettingsPanels()
	{
		m_SimpleWater.SetActive(!m_SimpleWaterButton.interactable);
		m_SimpleWaterSettingsPanel.SetActive(!m_SimpleWaterButton.interactable);

		m_DiffuseWater.SetActive(!m_DiffuseWaterButton.interactable);
		m_DiffuseWaterSettingsPanel.SetActive(!m_DiffuseWaterButton.interactable);

		m_HightmapWater.SetActive(!m_HightmapWaterButton.interactable);
		m_HightmapWaterSettingsPanel.SetActive(!m_HightmapWaterButton.interactable);
	}

	#endregion // GUI Window
}
