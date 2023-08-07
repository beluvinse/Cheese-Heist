using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyMouse: MonoBehaviour
{
    private float _duration;
    private GameObject _decoy;
    private Transform _decoySpawn;

    private MouseController _mouse;

    public DecoyMouse(float duration, GameObject decoy, MouseController mouse, Transform decoySpawn)
    {
        _duration = duration;
        _decoy = decoy;
        _mouse = mouse;
        _decoySpawn = decoySpawn;
    }



    public IEnumerator PlantDecoy()
    {
        var decoy = Instantiate(_decoy, _decoySpawn.position, Quaternion.identity);
        yield return new WaitForSeconds(_duration);
        Destroy(decoy);
    }
}
