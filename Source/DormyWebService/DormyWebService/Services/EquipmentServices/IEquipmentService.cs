using System.Threading.Tasks;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment;
using DormyWebService.ViewModels.EquipmentViewModels.UpdateEquipment;

namespace DormyWebService.Services.EquipmentServices
{
    public interface IEquipmentService
    {
        Task<Equipment> FindById(int id);
        Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequest requestModel);
        Task<UpdateEquipmentResponse> UpdateEquipment(UpdateEquipmentRequest requestModel);
    }
}