using UnityEngine;
using System.Collections;
using System;

public class BoardController : MonoBehaviour
{

	[SerializeField] private int _itemsInRow = 4;
	[SerializeField] private float _gemsOffset = 1.15f;

	const float _moveAnimationTime = 0.2f;
	[SerializeField] private GemController _gemPrefab;

	private GemController[,] _gems;
	public IntVector2 _blankPos;

	public Action OnWin;

	public void Init()
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


				float x = (j - _itemsInRow * 0.5f + 0.5f) * prefabSize;
				//FIXME not elegant, but works as aspected. God forgive me
				Vector3 newPos = new Vector3(
					(j - _itemsInRow * 0.5f + 0.5f) * prefabSize * _gemsOffset,
					_gemPrefab.transform.localPosition.y,
					((_itemsInRow - i - 1) - _itemsInRow * 0.5f + 0.5f) * prefabSize * _gemsOffset);

				gem.transform.localPosition = newPos;
				gem.Init(i * _itemsInRow + (j) + 1, prefabSize * _gemsOffset);
				_gems[j, i] = gem;
			}
		}
		Shuffle();

		//LogArray();
	}

	private void Shuffle()
	{
		int moves = 0;
		int difficulty;
		if (GameManager.Instance.GetCurrentGameType() == IDs.GameType.Time)
		{
			int sec = (int)Math.Round(GameManager.Instance.GetMaxGameplayTimeInMins() * 60);
            difficulty =  sec / 3;
		}
		else
		{
			difficulty = (int)Mathf.Round(GameManager.Instance.GetMaxAmountOfSteps() *0.75f);
		}
		
		while (moves <= difficulty)// || moves++ < difficulty)
		{
			if (TryToMove(IntVector2.RandomDirection(), 0))
			{
				moves++;
			}
		}
	}

	internal bool TryMove(IntVector2 dir)
	{
		if (TryToMove(dir))
		{
			CheckForWin();
			return true;
		}
		else return false;
	}

	private bool TryToMove(IntVector2 direction, float moveAnimationTime = _moveAnimationTime)
	{
		if (direction.x == -1 && _blankPos.x == _itemsInRow - 1 || //left border
			direction.x == 1 && _blankPos.x == 0 || // right border
			direction.y == -1 && _blankPos.y == _itemsInRow - 1 || //top border
			direction.y == 1 && _blankPos.y == 0) // bottom border
		{
			IntVector2 targetPos = _blankPos - direction;
			return false;
		}
		else
		{
			IntVector2 targetPos = _blankPos - direction;

			var targetGem = _gems[targetPos.x, targetPos.y];
			targetGem.MoveTo(direction, moveAnimationTime);

			var blankGem = _gems[_blankPos.x, _blankPos.y];
			_gems[targetPos.x, targetPos.y] = blankGem;
			_gems[_blankPos.x, _blankPos.y] = targetGem;
			_blankPos = targetPos;
			return true;
		}
	}

	private void CheckForWin()
	{
		if (_gems[_itemsInRow - 1, _itemsInRow - 1].IsEmpty())
		{
			int index = 1;
			for (int i = 0; i < _itemsInRow; i++)
			{
				for (int j = 0; j < _itemsInRow; j++)
				{
					if (i == _itemsInRow - 1 && j == _itemsInRow - 1)
					{
						OnWin();
					}
					else if (index++ != _gems[j, i].Number)
					{
						return;
					}
				}
			}

		}
	}

	private void LogArray()
	{
		string label = "";
		for (int i = 0; i < _itemsInRow; i++)
		{
			for (int j = 0; j < _itemsInRow; j++)
			{
				label += _gems[j, i].Number + "\t";
			}
			label += "\n";
		}
		Debug.Log(" " + label);
	}
}
