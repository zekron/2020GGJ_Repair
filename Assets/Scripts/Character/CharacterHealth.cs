using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public static CharacterHealth instance = null;
    public List<Heart> _ListHeart = new List<Heart>(3);
    public Heart _Heart;
    public Sprite[] _HeartSprites = new Sprite[2];

    private int m_DefaultHeartNum = 3;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHeart(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Heart newHeart = new Heart();
            var obj = Instantiate(_Heart._Heart,
               _ListHeart[_ListHeart.Count - 1]._Heart.transform.localPosition + new Vector3(_Heart._Heart.sprite.bounds.size.x, 0, 0),
               Quaternion.identity,
               transform);
            obj.sprite = _HeartSprites[(int)eHeartState.eEmpty];
        }
    }
    public int GetHeartNum()
    {
        int num = 0;
        for (int i = 0; i < _ListHeart.Count; i++)
            num = _ListHeart[i]._State == eHeartState.eFull ? num + 1 : num;
        return num;
    }
}
