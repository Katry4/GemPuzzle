using UnityEngine;
using System.Collections;
using System;

public class BoardController : MonoBehaviour
{

	[SerializeField] private int _itemsInRow = 4;
	[SerializeField] private float _gemsOffset = 1.15f;
	[SerializeField] private GemController _gemPrefab;

	private GemController[,] _gems;
	public IntVector2 _blankPos;


	// Use this for initialization
	void Awake()
	{
		_gems = new GemController[_itemsInRow, _itemsInRow];

		float prefabSize = _gemPrefab.GetComponent<Renderer>().bounds.size.x;
		for (int i = 0; i < _itemsInRow; i++)
		{
			for (int j = 0; j < _itemsInRow; j++)
			{
				if (i == _itemsInRow - 1 && j == _itemsInRow - 1)
				{
					_blankPos = new IntVector2(i, j);
					_gems[i, j] = GemController.BlankGem();
					continue;
				}
				GemController gem = Instantiate(_gemPrefab);
				gem.transform.parent = transform;
				gem.transform.localRotation = new Quaternion(0, 0, 0, 0);
				gem.transform.localScale = Vector3.one * prefabSize;

				//FIXME not elegant, but works as aspected. God forgive me
				Vector3 newPos = new Vector3(
					(j - _itemsInRow * 0.5f + 0.5f) * prefabSize * _gemsOffset,
					_gemPrefab.transform.localPosition.y,
					((_itemsInRow - i - 1) - _itemsInRow * 0.5f + 0.5f) * prefabSize) * _gemsOffset;

				gem.transform.localPosition = newPos;
				gem.Init(i * _itemsInRow + (j) + 1, prefabSize * _gemsOffset);
				_gems[i, j] = gem;
			}
		}

		LogArray();
	}

	internal void TryMove(IntVector2 dir)
	{
		TryToMove(dir);
		LogArray();
    }

	private bool TryToMove(IntVector2 direction)
	{
		Debug.Log("try to move ind dir " + direction);
		Debug.Log("prev blank " + _blankPos);
		if (direction.x == -1 && _blankPos.x == _itemsInRow - 1 || //left border
			direction.x == 1 && _blankPos.x == 0 || // right border
			direction.y == -1 && _blankPos.y == _itemsInRow - 1 || //top border
			direction.y == 1 && _blankPos.y == 0) // bottom border
		{
			return false;
		}
		else
		{

			IntVector2 targetPos = _blankPos - direction;

			var targetGem = _gems[targetPos.x, targetPos.y];
			Debug.Log("target gem " + targetGem.Number);
			targetGem.Move(direction);

			var blankGem = _gems[_blankPos.x, _blankPos.y];
			_gems[targetPos.x, targetPos.y] = blankGem;
			_gems[_blankPos.x, _blankPos.y] = targetGem;
			_blankPos = targetPos;

			Debug.Log("new blank " + _blankPos);
			return true;
		}
	}

	private void LogArray()
	{
		string label = "";
		for (int i = 0; i < _itemsInRow; i++)
		{
			for (int j = 0; j < _itemsInRow; j++)
			{
				label += _gems[i, j].Number+"\t";
			}
			label += "\n";
		}
		Debug.Log(" " + label);
	}
}
