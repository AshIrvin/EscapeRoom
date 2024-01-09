using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    [SerializeField] private bool _puzzleObjectPlaced;
    [SerializeField] private int _puzzleNo;
    [SerializeField] private int _itemNo;

    public void PlaceObjectInHolder(GameObject holder)
    {
        if (holder.CompareTag("PlaceObject"))
        {
            transform.position = holder.transform.position;
            _puzzleObjectPlaced = true;
            GameManager.Instance.UpdatePuzzle(_puzzleNo, _itemNo, _puzzleObjectPlaced);
        }
    }

    public void ItemRemovedFromHolder(GameObject holder)
    {
        if (holder.CompareTag("PlaceObject"))
        {
            _puzzleObjectPlaced = false;
            GameManager.Instance.UpdatePuzzle(_puzzleNo, _itemNo, _puzzleObjectPlaced);
        }
    }
}
