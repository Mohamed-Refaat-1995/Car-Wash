using CarService.ViewModel;

namespace CarService.Repository
{
    public interface IRulesRepository
    {
        List<RulesInfoViewModel> GetAllRulesInfo();
    }
}