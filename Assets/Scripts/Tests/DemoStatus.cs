using UnityEngine;
using System.Collections;
public class DemoStatus : MonoBehaviour 
{
    Unit cursedUnit;
    Equippable cursedItem;
    int step;
    void OnEnable ()
    {
        this.AddObserver(OnTurnCheck, TurnOrderController.TurnCheckNotification);
    }
    void OnDisable ()
    {
        this.RemoveObserver(OnTurnCheck, TurnOrderController.TurnCheckNotification);
    }
    void OnTurnCheck (object sender, object args)
    {
        BaseException exc = args as BaseException;
        if (exc.toggle == false)
            return;
        Unit target = sender as Unit;
        switch (step)
        {
        case 0:
            EquipCursedItem(target);
            break;
        case 1:
            Add<SlowStatusEffect>(target, 15);
            break;
        case 2:
            Add<StopStatusEffect>(target, 15);
            break;
        case 3:
            Add<HasteStatusEffect>(target, 15);
            break;
        default:
            UnEquipCursedItem(target);
            break;
        }
        step++;
    }
    void Add<T> (Unit target, int duration) where T : StatusEffect
    {
        DurationStatusCondition condition = target.GetComponent<Status>().Add<T, DurationStatusCondition>();
        condition.duration = duration;
    }
    void EquipCursedItem (Unit target)
    {
        cursedUnit = target;
        GameObject obj = new GameObject("Cursed Sword");
        obj.AddComponent<AddPoisonStatusFeature>();
        cursedItem = obj.AddComponent<Equippable>();
        cursedItem.defaultSlots = EquipSlots.Primary;
        Equipment equipment = target.GetComponent<Equipment>();
        equipment.Equip(cursedItem, EquipSlots.Primary);
    }
    void UnEquipCursedItem (Unit target)
    {
        if (target != cursedUnit || step < 10)
            return;
        Equipment equipment = target.GetComponent<Equipment>();
        equipment.UnEquip(cursedItem);
        Destroy(cursedItem.gameObject);
        Destroy(this);
    }
}