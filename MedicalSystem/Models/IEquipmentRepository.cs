using System.Collections.Generic;


namespace MedicalSystem.Models
{
    public interface IEquipmentRepository
    {
        IEnumerable<Equipment> GetAllEquipment();
        Equipment GetEquipmentById(int equipmentId);

        void CreateEquipment(Equipment equipment);

        void UpdateEquipment(Equipment equipment);
    }
}
