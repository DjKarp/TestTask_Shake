using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalFinishZone : FinishZone
{
	private Material _startMaterial;
	private Color _startTextColor;

	private void Start()
    {
		_startMaterial = renderers[0].material;
		_startTextColor = text.color;

		Locked = !DataManager.IsMainGameFinished() && !DataManager.data._tempSurvivalModeUnlocked;

		if (Locked)
		{
			text.color = lockedTextColor;
			for (int i = 0; i < renderers.Count; i++)
			{
				renderers[i].material = lockMaterial;
			}
		}
	}

    private void Update()
    {
		if ((!Locked || DataManager.data._tempSurvivalModeUnlocked) && Vector3.Distance(GameManager.Instance.LevelManager.Player.transform.position, base.transform.position) < radius)
		{
			if (DataManager.data._tempSurvivalModeUnlocked)
            {
				DataManager.data._tempSurvivalModeUnlocked = false;
				DataManager.Save();
			}

			GameManager.Instance.LevelManager.gameMode = gameMode;
			GameManager.Instance.LevelManager.game3CType = game3CType;
			GameManager.Instance.LevelManager.TryLoadLevel(selectIndex);
		}
	}

	public void UpdateMaterialsOnOpenMode()
    {
		text.color = _startTextColor;
		for (int i = 0; i < renderers.Count; i++)
		{
			renderers[i].material = _startMaterial;
		}
	}
}
