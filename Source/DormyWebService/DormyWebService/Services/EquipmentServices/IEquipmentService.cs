using System.Collections.Generic;
using System.Threading.Tasks;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.EquipmentViewModels.UpdateEquipment;

namespace DormyWebService.Services.EquipmentServices
{
    public interface IEquipmentService
    {
        Task<Equipment> FindById(int id);
        Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequest requestModel);
        Task<bool> UpdateEquipment(UpdateEquipmentRequest requestModel);
        Task<List<GetEquipmentResponse>> GetEquipmentOfStudent(int studentId);

        Task<AdvancedGetEquipmentResponse> AdvancedGetEquipments(string sorts, string filters, int? page, int? pageSize);
    }
}