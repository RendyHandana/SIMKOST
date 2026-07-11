using SIMKOST.Resources.ViewModels; // <-- 1. Diubah agar mengarah ke folder ViewModel yang benar

namespace SIMKOST.Services
{
    public interface IKamarService
    {
        // 2. Mengubah semua method menjadi Asynchronous (Task)
        Task<IEnumerable<KamarViewModel>> GetDaftarKamarAsync();
        
        Task<KamarViewModel> GetKamarByIdAsync(int id);
        
        Task AddKamarAsync(KamarViewModel kamarVm);
        
        Task UpdateKamarAsync(KamarViewModel kamarVm);
        
        Task DeleteKamarAsync(int id);
    }
}