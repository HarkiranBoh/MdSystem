using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalSystem.Models;

namespace MedicalSystem.Models
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _appDbContext;

        public EquipmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Equipment> GetAllEquipment()
        {
            return _appDbContext.Equipment;
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            return _appDbContext.Equipment.FirstOrDefault(e => e.Id == equipmentId);
        }
    }
}
