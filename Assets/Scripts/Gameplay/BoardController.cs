using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour
{

	[SerializeField] private int _itemsInRow = 4;
	[SerializeField] private float _gemsOffset = 1.15f;
	[SerializeField] private GemController _gemPrefab;

	private GemController[,] _gems;


	// Use this for initialization
	void Awake()
	{
		_gems = new GemController[_itemsInRow, _itemsInRow];

		float prefabSize = _gemPrefab.GetComponent<Renderer>().bounds.size.x;
		for (int i = 0; i < _itemsInRow; i++)
		{
			for (int j = 0; j < _itemsInRow; j++)
			{
				if (i == _itemsInRow - 1 && j == _itemsInRow-1) { continue; }
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
				gem.Init((i * _itemsInRow + (j) + 1));
				_gems[i, j] = gem;
			}
		}
	}
}
