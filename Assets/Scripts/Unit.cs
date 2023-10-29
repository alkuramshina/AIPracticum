using Configuration;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitSettings _unitSettings;

    private void Init(Material material,
        UnitSettings unitSettings)
    {
        GetComponent<MeshRenderer>().sharedMaterial = material;
        _unitSettings = unitSettings;
    }
    
    public static Unit Spawn(UnitSettings unitSettings, Material material,
        Transform spawnPoint)
    {
        var unit = Instantiate(unitSettings.unitPrefab).GetComponent<Unit>();
        
        unit.transform.position = spawnPoint.position;
        unit.transform.rotation = spawnPoint.rotation;
        
        unit.Init(material, unitSettings);
        
        return unit;
    }
}
