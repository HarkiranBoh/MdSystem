using MedicalSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public interface IEquipmentRepository
    {
        IEnumerable<Equipment> GetAllEquipment();

        Equipment GetEquipmentById(int equipmentId);
    }
}
